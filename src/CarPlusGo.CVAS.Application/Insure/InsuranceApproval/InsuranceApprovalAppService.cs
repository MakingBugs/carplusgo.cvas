using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using CarPlusGo.CVAS.Insure.Dto;
using CarPlusGo.CVAS.Insure.Enum;
using System;
using System.Threading.Tasks;
using System.Linq;
using CarPlusGo.CVAS.BPM;
using CarPlusGo.CVAS.Authorization.Users;
using Abp.Extensions;
using System.Collections.Generic;

namespace CarPlusGo.CVAS.Insure
{
    public class InsuranceApprovalAppService
        : AsyncCrudAppService<InsuranceApproval, InsuranceApprovalDto, long, PagedInsuranceApprovalResultRequestDto, CreateInsuranceApprovalDto, InsuranceApprovalDto>, IInsuranceApprovalAppService
    {
        private readonly IRepository<InsurancePolicy, long> _insurancePolicyRepository;
        private readonly IRepository<InsuranceApprovalDetail, long> _insuranceApprovalDetailRepository;
        private readonly IRepository<InsuranceDetail, long> _insuranceDetailRepository;
        private readonly IRepository<ReadyBPM, string> _bpmRepository;
        private readonly IRepository<UserInfo, long> _userInfoRepository;
        private readonly UserManager _userManager;

        public InsuranceApprovalAppService(
            IRepository<InsuranceApproval, long> repository,
            IRepository<InsuranceApprovalDetail, long> insuranceApprovalDetailRepository,
            IRepository<InsurancePolicy, long> insurancePolicyRepository,
            IRepository<InsuranceDetail, long> insuranceDetailRepository,
            IRepository<ReadyBPM, string> bpmrepository,
            IRepository<UserInfo, long> userInfoRepository,
            UserManager userManager)
            : base(repository)
        {
            _insurancePolicyRepository = insurancePolicyRepository;
            _insuranceDetailRepository = insuranceDetailRepository;
            _insuranceApprovalDetailRepository = insuranceApprovalDetailRepository;
            _bpmRepository = bpmrepository;
            _userInfoRepository = userInfoRepository;
            _userManager = userManager;
        }

        public override async Task<InsuranceApprovalDto> Create(CreateInsuranceApprovalDto input)
        {
            CheckCreatePermission();

            var entity = MapToEntity(input);

            entity.InsuranceApprovalStatus = InsuranceApprovalStatus.Approving;
            entity.Id = await Repository.InsertAndGetIdAsync(entity);

            var allInsurancePolicys = _insurancePolicyRepository.GetAllIncluding(x => x.CarBase, x => x.CarBase.Inc).Where(x => x.Id.IsIn(input.InsurancePolicyIds.ToArray())).ToList();
            var allInsuranceDetails = _insuranceDetailRepository.GetAll().Where(x => x.InsurancePolicyId.IsIn(input.InsurancePolicyIds.ToArray())).ToList();
            var allInsuranceApprovalDetails = _insuranceApprovalDetailRepository.GetAllIncluding(x => x.InsuranceApproval).Where(x => x.InsurancePolicyId.IsIn(input.InsurancePolicyIds.ToArray()) && x.InsuranceApproval.InsuranceApprovalStatus == InsuranceApprovalStatus.Approved);

            UpdateInsuranceApprovalDetails(input, entity, allInsurancePolicys, allInsuranceDetails, allInsuranceApprovalDetails);

            var user = await _userManager.GetUserByIdAsync(AbpSession.UserId.Value);
            var bpmUser = _userInfoRepository.FirstOrDefault(x => x.BPMUserID.ToLower() == user.UserName.ToLower());

            await InsertBPMForm(entity, bpmUser);

            UpdateInsuranceApproval(entity, allInsuranceDetails, allInsuranceApprovalDetails, bpmUser);

            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }

        public override async Task<InsuranceApprovalDto> Update(InsuranceApprovalDto input)
        {
            CheckUpdatePermission();

            var entity = await GetEntityByIdAsync(input.Id);

            CheckInsuranceApprovalStatus(input, entity);

            MapToEntity(input, entity);

            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }

        public override Task Delete(EntityDto<long> input)
        {
            CheckDeletePermission();

            var entity = Repository.FirstOrDefault(x => x.Id == input.Id);

            var entityDto = MapToEntityDto(entity);

            _insuranceApprovalDetailRepository
                .GetAll().Where(r => r.InsuranceApprovalId == input.Id).Select(x => x.InsurancePolicyId).ToList()
                .ForEach(x =>
                {
                    var insurancePolicy = _insurancePolicyRepository.FirstOrDefault(y => y.Id == x);
                    insurancePolicy.InsurancePolicyStatus = InsurancePolicyStatus.NewBuild;
                });

            _insuranceApprovalDetailRepository.DeleteAsync(r => r.InsuranceApprovalId == input.Id);

            return Repository.DeleteAsync(input.Id);
        }

        private void UpdateInsuranceApproval(InsuranceApproval entity, List<InsuranceDetail> allInsuranceDetails, IQueryable<InsuranceApprovalDetail> allInsuranceApprovalDetails, UserInfo bpmUser)
        {
            switch (entity.InsuranceApprovalType)
            {
                case InsuranceApprovalType.TemporaryLoan:
                case InsuranceApprovalType.PaymentRequest:
                case InsuranceApprovalType.Change:
                    entity.Amount = allInsuranceDetails.Sum(x => x.TransactionAmount + x.NoDeductibleTransactionAmount)
                        - allInsuranceApprovalDetails.Where(a => a.InsuranceApproval.InsuranceApprovalType != InsuranceApprovalType.WriteOff && a.InsuranceApproval.InsuranceApprovalType != InsuranceApprovalType.RebateInvoice).Sum(x => x.TransactionAmount + x.NoDeductibleTransactionAmount);
                    entity.ExtraAmount = 0;
                    break;
                case InsuranceApprovalType.WriteOff:
                    entity.Amount = allInsuranceApprovalDetails.Where(a => a.InsuranceApproval.InsuranceApprovalType == InsuranceApprovalType.TemporaryLoan).Sum(x => x.TransactionAmount + x.NoDeductibleTransactionAmount)
                        - allInsuranceApprovalDetails.Where(a => a.InsuranceApproval.InsuranceApprovalType == InsuranceApprovalType.WriteOff).Sum(x => x.TransactionAmount + x.NoDeductibleTransactionAmount);
                    entity.ExtraAmount = 0;
                    break;
                case InsuranceApprovalType.RebateInvoice:
                    entity.Amount = allInsuranceDetails.Sum(d => d.RebateAmount)
                        - allInsuranceApprovalDetails.Where(a => a.InsuranceApproval.InsuranceApprovalType == InsuranceApprovalType.RebateInvoice).Sum(d => d.RebateAmount);
                    entity.ExtraAmount = allInsuranceDetails.Sum(d => d.ExtraRebateAmount)
                        - allInsuranceApprovalDetails.Where(a => a.InsuranceApproval.InsuranceApprovalType == InsuranceApprovalType.RebateInvoice).Sum(d => d.ExtraRebateAmount);
                    break;
                default:
                    break;
            }

            entity.UserName = bpmUser.BPMUserName;
        }

        private async Task InsertBPMForm(InsuranceApproval entity, UserInfo bpmUser)
        {
            await _bpmRepository.InsertAsync(new ReadyBPM()
            {
                DiagramId = "InsuranceApproval_Proc_01",
                ApplicantDept = bpmUser.BPMDeptID,
                ApplicantDeptName = bpmUser.BPMDeptName,
                ApplicantId = bpmUser.BPMUserID,
                ApplicantName = bpmUser.BPMUserName,
                FillerId = bpmUser.BPMUserID,
                FillerName = bpmUser.BPMUserName,
                ApplicantDateTime = DateTime.Now,
                Priority = 2,
                DraftFlag = 0,
                FlowActivated = 0,
                TagId = entity.Id.ToString(),
            });
        }

        private void UpdateInsuranceApprovalDetails(CreateInsuranceApprovalDto input, InsuranceApproval entity, List<InsurancePolicy> allInsurancePolicys, List<InsuranceDetail> allInsuranceDetails, IQueryable<InsuranceApprovalDetail> allInsuranceApprovalDetails)
        {
            input.InsurancePolicyIds.ForEach(x =>
            {
                var insurancePolicy = allInsurancePolicys.FirstOrDefault(y => y.Id == x);
                var insurancePolicyDetails = allInsuranceDetails.Where(z => z.InsurancePolicyId == x);
                var insuranceApprovalDetails = allInsuranceApprovalDetails.Where(b => b.InsurancePolicyId == x);

                CheckInsurancePolicy(insurancePolicy);

                insurancePolicy.InsurancePolicyStatus = InsurancePolicyStatus.Approving;

                InsertInsuranceApprovalDetail(input, entity, insurancePolicy, insurancePolicyDetails, insuranceApprovalDetails);
            });
        }

        private void InsertInsuranceApprovalDetail(CreateInsuranceApprovalDto input, InsuranceApproval entity, InsurancePolicy insurancePolicy, IEnumerable<InsuranceDetail> insurancePolicyDetails, IQueryable<InsuranceApprovalDetail> insuranceApprovalDetails)
        {
            decimal originalAmount = 0, transactionAmount = 0, noDeductibleOriginalAmount = 0, noDeductibleTransactionAmount = 0, rebateAmount = 0, extraRebateAmount = 0;
            switch (input.InsuranceApprovalType)
            {
                case InsuranceApprovalType.TemporaryLoan:
                case InsuranceApprovalType.Change:
                case InsuranceApprovalType.PaymentRequest:
                    insuranceApprovalDetails = insuranceApprovalDetails.Where(a => a.InsuranceApproval.InsuranceApprovalType != InsuranceApprovalType.WriteOff && a.InsuranceApproval.InsuranceApprovalType != InsuranceApprovalType.RebateInvoice);
                    originalAmount = insurancePolicyDetails.Sum(d => d.OriginalAmount) - insuranceApprovalDetails.Sum(d => d.OriginalAmount);
                    transactionAmount = insurancePolicyDetails.Sum(d => d.TransactionAmount) - insuranceApprovalDetails.Sum(d => d.TransactionAmount);
                    noDeductibleOriginalAmount = insurancePolicyDetails.Sum(d => d.NoDeductibleOriginalAmount) - insuranceApprovalDetails.Sum(d => d.NoDeductibleOriginalAmount);
                    noDeductibleTransactionAmount = insurancePolicyDetails.Sum(d => d.NoDeductibleTransactionAmount) - insuranceApprovalDetails.Sum(d => d.NoDeductibleTransactionAmount);
                    rebateAmount = insurancePolicyDetails.Sum(d => d.RebateAmount) - insuranceApprovalDetails.Sum(d => d.RebateAmount);
                    extraRebateAmount = insurancePolicyDetails.Sum(d => d.ExtraRebateAmount) - insuranceApprovalDetails.Sum(d => d.ExtraRebateAmount);
                    break;
                case InsuranceApprovalType.WriteOff:
                    var temporaryLoanDetails = insuranceApprovalDetails.Where(a => a.InsuranceApproval.InsuranceApprovalType == InsuranceApprovalType.TemporaryLoan);
                    var writeOffDetails = insuranceApprovalDetails.Where(a => a.InsuranceApproval.InsuranceApprovalType == InsuranceApprovalType.WriteOff);
                    originalAmount = temporaryLoanDetails.Sum(d => d.OriginalAmount) - writeOffDetails.Sum(d => d.OriginalAmount);
                    transactionAmount = temporaryLoanDetails.Sum(d => d.TransactionAmount) - writeOffDetails.Sum(d => d.TransactionAmount);
                    noDeductibleOriginalAmount = temporaryLoanDetails.Sum(d => d.NoDeductibleOriginalAmount) - writeOffDetails.Sum(d => d.NoDeductibleOriginalAmount);
                    noDeductibleTransactionAmount = temporaryLoanDetails.Sum(d => d.NoDeductibleTransactionAmount) - writeOffDetails.Sum(d => d.NoDeductibleTransactionAmount);
                    rebateAmount = temporaryLoanDetails.Sum(d => d.RebateAmount) - writeOffDetails.Sum(d => d.RebateAmount);
                    extraRebateAmount = temporaryLoanDetails.Sum(d => d.ExtraRebateAmount) - writeOffDetails.Sum(d => d.ExtraRebateAmount);
                    break;
                case InsuranceApprovalType.RebateInvoice:
                    insuranceApprovalDetails = insuranceApprovalDetails.Where(a => a.InsuranceApproval.InsuranceApprovalType == InsuranceApprovalType.RebateInvoice);
                    originalAmount = insurancePolicyDetails.Sum(d => d.OriginalAmount) - insuranceApprovalDetails.Sum(d => d.OriginalAmount);
                    transactionAmount = insurancePolicyDetails.Sum(d => d.TransactionAmount) - insuranceApprovalDetails.Sum(d => d.TransactionAmount);
                    noDeductibleOriginalAmount = insurancePolicyDetails.Sum(d => d.NoDeductibleOriginalAmount) - insuranceApprovalDetails.Sum(d => d.NoDeductibleOriginalAmount);
                    noDeductibleTransactionAmount = insurancePolicyDetails.Sum(d => d.NoDeductibleTransactionAmount) - insuranceApprovalDetails.Sum(d => d.NoDeductibleTransactionAmount);
                    rebateAmount = insurancePolicyDetails.Sum(d => d.RebateAmount) - insuranceApprovalDetails.Sum(d => d.RebateAmount);
                    extraRebateAmount = insurancePolicyDetails.Sum(d => d.ExtraRebateAmount) - insuranceApprovalDetails.Sum(d => d.ExtraRebateAmount);
                    break;
                default:
                    break;
            }

            _insuranceApprovalDetailRepository.Insert(new InsuranceApprovalDetail
            {
                CreatorUserId = input.CreatorUserId,
                InsuranceApprovalId = entity.Id,
                InsurancePolicyId = insurancePolicy.Id,
                InsuranceNum = insurancePolicy.InsuranceNum,
                MakNo = insurancePolicy.CarBase.MakNo,
                InsurancePolicyType = insurancePolicy.InsurancePolicyType,
                OriginalAmount = originalAmount,
                TransactionAmount = transactionAmount,
                NoDeductibleOriginalAmount = noDeductibleOriginalAmount,
                NoDeductibleTransactionAmount = noDeductibleTransactionAmount,
                RebateAmount = rebateAmount,
                ExtraRebateAmount = extraRebateAmount,
                CusName = insurancePolicy.CarBase.Inc.Sname
            });
        }
        
        private void CheckInsuranceApprovalStatus(InsuranceApprovalDto input, InsuranceApproval entity)
        {
            if (input.InsuranceApprovalStatus != entity.InsuranceApprovalStatus)
            {
                switch (input.InsuranceApprovalStatus)
                {
                    case InsuranceApprovalStatus.Approving:
                        _insuranceApprovalDetailRepository
                            .GetAll().Where(r => r.InsuranceApprovalId == input.Id).Select(x => x.InsurancePolicyId).ToList()
                            .ForEach(x =>
                            {
                                var insurancePolicy = _insurancePolicyRepository.FirstOrDefault(y => y.Id == x);

                                CheckInsurancePolicy(insurancePolicy);

                                insurancePolicy.InsurancePolicyStatus = InsurancePolicyStatus.Approving;
                            });
                        break;
                    case InsuranceApprovalStatus.Approved:
                        _insuranceApprovalDetailRepository
                            .GetAll().Where(r => r.InsuranceApprovalId == input.Id).Select(x => x.InsurancePolicyId).ToList()
                            .ForEach(x =>
                            {
                                var insurancePolicy = _insurancePolicyRepository.FirstOrDefault(y => y.Id == x);

                                if (insurancePolicy.InsurancePolicyStatus != InsurancePolicyStatus.Approving)
                                {
                                    throw new UserFriendlyException($"保单编号为{insurancePolicy.InsuranceNum}的保单不是签核中保单，无法核准");
                                }

                                insurancePolicy.InsurancePolicyStatus = InsurancePolicyStatus.Approved;
                            });
                        break;
                    case InsuranceApprovalStatus.Reject:
                        _insuranceApprovalDetailRepository
                            .GetAll().Where(r => r.InsuranceApprovalId == input.Id).Select(x => x.InsurancePolicyId).ToList()
                            .ForEach(x =>
                            {
                                var insurancePolicy = _insurancePolicyRepository.FirstOrDefault(y => y.Id == x);

                                if (insurancePolicy.InsurancePolicyStatus != InsurancePolicyStatus.Approving)
                                {
                                    throw new UserFriendlyException($"保单编号为{insurancePolicy.InsuranceNum}的保单不是签核中保单，无法驳回");
                                }

                                insurancePolicy.InsurancePolicyStatus = InsurancePolicyStatus.Reject;
                            });
                        break;
                    default:
                        break;
                }
            }
        }

        private void CheckInsurancePolicy(InsurancePolicy insurancePolicy)
        {
            if (insurancePolicy.InsurancePolicyStatus == InsurancePolicyStatus.Approving)
            {
                throw new UserFriendlyException($"保单编号为{insurancePolicy.InsuranceNum}的保单为签核中保单，无法送签");
            }
        }


    }
}
