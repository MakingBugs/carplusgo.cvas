using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.UI;
using CarPlusGo.CVAS.Insure.Dto;
using CarPlusGo.CVAS.Insure.Enum;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Extensions;
using Abp.Linq.Extensions;

namespace CarPlusGo.CVAS.Insure
{
    public class InsurancePolicyAppService
        : AsyncCrudAppService<InsurancePolicy, InsurancePolicyDto, long, PagedInsurancePolicyResultRequestDto, InsurancePolicyDto, InsurancePolicyDto>, IInsurancePolicyAppService
    {
        private readonly IRepository<InsuranceDetail, long> _insuranceDetailRepository;
        private readonly IRepository<InsuranceLog, long> _insuranceLogRepository;
        public InsurancePolicyAppService(IRepository<InsurancePolicy, long> repository, IRepository<InsuranceDetail, long> insuranceDetailRepository, IRepository<InsuranceLog, long> insuranceLogRepository)
            : base(repository)
        {
            _insuranceDetailRepository = insuranceDetailRepository;
            _insuranceLogRepository = insuranceLogRepository;
        }

        protected override IQueryable<InsurancePolicy> CreateFilteredQuery(PagedInsurancePolicyResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Supplier, x => x.CarBase, x => x.CarBase.Brand, x => x.CarBase.FactoryBrand, x => x.CarBase.ItemCode, x => x.CarBase.Inc, x => x.CarBase.Clasen)
                .WhereIf(input.CarBaseIds != null && input.CarBaseIds.Length > 0, x => x.CarBaseId.IsIn(input.CarBaseIds))
                .WhereIf(!input.Keyword.IsNullOrEmpty(), x => x.CarBase.CarNo.Contains(input.Keyword) || x.CarBase.MakNo.Contains(input.Keyword) || x.InsuranceNum.Contains(input.Keyword))
                .WhereIf(input.SupplierId.HasValue, x => x.SupplierId == input.SupplierId)
                .WhereIf(input.InsuranceContractType.HasValue, x => x.InsuranceContractType == input.InsuranceContractType)
                .WhereIf(input.InsurancePolicyType.HasValue, x => x.InsurancePolicyType == input.InsurancePolicyType)
                .WhereIf(input.InsurancePolicyStatus.HasValue, x => x.InsurancePolicyStatus == input.InsurancePolicyStatus)
                .WhereIf(input.StartTimeFrom.HasValue && input.StartTimeTo.HasValue, x => x.StartTime >= input.StartTimeFrom.Value.ToLocalTime() && x.StartTime <= input.StartTimeTo.Value.ToLocalTime().ToDayEnd())
                .WhereIf(input.EndTimeFrom.HasValue && input.EndTimeTo.HasValue, x => x.EndTime >= input.EndTimeFrom.Value.ToLocalTime() && x.EndTime <= input.EndTimeTo.Value.ToLocalTime().ToDayEnd());
        }

        protected override void MapToEntity(InsurancePolicyDto updateInput, InsurancePolicy entity)
        {
            CheckInsurancePolicy(entity);

            updateInput.CarBase = null;
            updateInput.Supplier = null;
            updateInput.StartTime = updateInput.StartTime.ToLocalTime();
            updateInput.EndTime = updateInput.EndTime.ToLocalTime();

            if (updateInput.CarrierLiabilityInsuranceRebateRate != entity.CarrierLiabilityInsuranceRebateRate
                || updateInput.CommercialInsuranceRebateRate != entity.CommercialInsuranceRebateRate
                || updateInput.CompulsoryInsuranceRebateRate != entity.CompulsoryInsuranceRebateRate
                || updateInput.ExtraRebateRate != entity.ExtraRebateRate)
            {
                ObjectMapper.Map(updateInput, entity);
                CurrentUnitOfWork.SaveChanges();
                var insuranceDetails = _insuranceDetailRepository.GetAll().Where(d => d.InsurancePolicyId == entity.Id).ToList();
                for (int i = 0; i < insuranceDetails.Count; i++)
                {
                    var OldInsuranceJson = JsonConvert.SerializeObject(insuranceDetails[i]);
                    insuranceDetails[i] = UpdateInsuranceCalculation(insuranceDetails[i]);
                    _insuranceLogRepository.Insert(new InsuranceLog
                    {
                        InsuranceOperationType = InsuranceOperationType.Normal,
                        InsuranceDetailId = insuranceDetails[i].Id,
                        SerialNumber = "",
                        OldInsuranceJson = OldInsuranceJson,
                        NewInsuranceJson = JsonConvert.SerializeObject(insuranceDetails[i])
                    });
                }
            }
            else
            {
                ObjectMapper.Map(updateInput, entity);
            }
        }

        public async Task<int> Surrender(SurrenderDto input)
        {
            CheckUpdatePermission();

            int surrenderNum = 0;
            input.InsurancePolicyIds.ForEach(x =>
            {
                var insurancePolicy = Repository.FirstOrDefault(p => p.Id == x);

                CheckInsurancePolicy(insurancePolicy);

                var insuranceDetails = _insuranceDetailRepository.GetAll().Where(d => d.InsurancePolicyId == x).ToList();
                if (insurancePolicy == null || insuranceDetails == null || insuranceDetails.Count <= 0)
                {
                    throw new UserFriendlyException("保单资料异常");
                }
                var percent = Convert.ToDecimal(Math.Round((input.EndTime.ToLocalTime().Date - insurancePolicy.StartTime.ToLocalTime().Date).TotalDays) / Math.Round((insurancePolicy.EndTime.ToLocalTime().Date - insurancePolicy.StartTime.ToLocalTime().Date).TotalDays));
                for (int i = 0; i < insuranceDetails.Count; i++)
                {
                    var OldInsuranceJson = JsonConvert.SerializeObject(insuranceDetails[i]);
                    insuranceDetails[i].OriginalAmount = insuranceDetails[i].OriginalAmount * percent;
                    insuranceDetails[i].TransactionAmount = insuranceDetails[i].TransactionAmount * percent;
                    insuranceDetails[i].NoDeductibleOriginalAmount = insuranceDetails[i].NoDeductibleOriginalAmount * percent;
                    insuranceDetails[i].NoDeductibleTransactionAmount = insuranceDetails[i].NoDeductibleTransactionAmount * percent;
                    insuranceDetails[i] = UpdateInsuranceCalculation(insuranceDetails[i]);
                    _insuranceLogRepository.Insert(new InsuranceLog
                    {
                        InsuranceOperationType = InsuranceOperationType.Surrender,
                        InsuranceDetailId = insuranceDetails[i].Id,
                        SerialNumber = input.SerialNumber,
                        OldInsuranceJson = OldInsuranceJson,
                        NewInsuranceJson = JsonConvert.SerializeObject(insuranceDetails[i])
                    });
                }
                insurancePolicy.EndTime = input.EndTime.ToLocalTime();
                surrenderNum++;
            });

            await CurrentUnitOfWork.SaveChangesAsync();
            return surrenderNum;
        }

        private InsuranceDetail UpdateInsuranceCalculation(InsuranceDetail entity)
        {
            var insurancePolicy = Repository.FirstOrDefault(i => i.Id == entity.InsurancePolicyId);

            CheckInsurancePolicy(insurancePolicy);

            switch (insurancePolicy.InsurancePolicyType)
            {
                case InsurancePolicyType.CompulsoryInsurance:
                    entity.RebateAmount = Convert.ToDecimal(insurancePolicy.CompulsoryInsuranceRebateRate / 100) * (entity.NoDeductibleTransactionAmount + entity.TransactionAmount);
                    entity.ExtraRebateAmount = 0;
                    break;
                case InsurancePolicyType.CommercialInsurance:
                    entity.RebateAmount = Convert.ToDecimal(insurancePolicy.CommercialInsuranceRebateRate / 100) * (entity.NoDeductibleTransactionAmount + entity.TransactionAmount);
                    entity.ExtraRebateAmount = Convert.ToDecimal(insurancePolicy.ExtraRebateRate / 100) * (entity.NoDeductibleTransactionAmount + entity.TransactionAmount);
                    break;
                case InsurancePolicyType.CarrierLiabilityInsurance:
                    entity.RebateAmount = 0;
                    entity.ExtraRebateAmount = Convert.ToDecimal(insurancePolicy.CarrierLiabilityInsuranceRebateRate / 100) * (entity.NoDeductibleTransactionAmount + entity.TransactionAmount);
                    break;
            }
            return entity;
        }

        private void CheckInsurancePolicy(InsurancePolicy insurancePolicy)
        {
            if (insurancePolicy.InsurancePolicyStatus == InsurancePolicyStatus.Approving)
            {
                throw new UserFriendlyException($"保单编号为{insurancePolicy.InsuranceNum}的保单为签核中保单，无法修改");
            }
        }
    }
}
