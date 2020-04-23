using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Car;
using CarPlusGo.CVAS.Common;
using CarPlusGo.CVAS.Insure.Dto;
using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CarPlusGo.CVAS.Insure.Enum;
using Abp.UI;
using Abp.Linq.Extensions;
using Abp.Extensions;

namespace CarPlusGo.CVAS.Insure
{
    public class InsuranceDetailAppService
        : AsyncCrudAppService<InsuranceDetail, InsuranceDetailDto, long, PagedInsuranceDetailResultRequestDto, CreateInsuranceDetailDto, UpdateInsuranceDetailDto>, IInsuranceDetailAppService
    {
        private readonly IRepository<CarBase, long> _carBaseRepository;
        private readonly IRepository<Supplier, long> _supplierRepository;
        private readonly IRepository<InsurancePreset, long> _insurancePresetRepository;
        private readonly IRepository<InsuranceLog, long> _insuranceLogRepository;
        private readonly IRepository<InsurancePolicy, long> _insurancePolicyRepository;
        public InsuranceDetailAppService(
            IRepository<InsuranceDetail, long> repository,
            IRepository<CarBase, long> carBaseRepository,
            IRepository<InsurancePreset, long> insurancePresetRepository,
            IRepository<Supplier, long> supplierRepository,
            IRepository<InsuranceLog, long> insuranceLogRepository,
            IRepository<InsurancePolicy, long> insurancePolicyRepository)
            : base(repository)
        {
            _carBaseRepository = carBaseRepository;
            _insurancePresetRepository = insurancePresetRepository;
            _supplierRepository = supplierRepository;
            _insuranceLogRepository = insuranceLogRepository;
            _insurancePolicyRepository = insurancePolicyRepository;
        }

        protected override IQueryable<InsuranceDetail> CreateFilteredQuery(PagedInsuranceDetailResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.InsuranceType, x => x.InsurancePolicy)
                .WhereIf(input.InsurancePolicyIds != null && input.InsurancePolicyIds.Count > 0, x => x.InsurancePolicyId.IsIn(input.InsurancePolicyIds.ToArray()));
        }

        public override async Task<InsuranceDetailDto> Create(CreateInsuranceDetailDto input)
        {
            CheckCreatePermission();

            var entity = MapToEntity(input);

            await Repository.InsertAsync(entity);

            _insuranceLogRepository.Insert(new InsuranceLog
            {
                InsuranceDetailId = entity.Id,
                InsuranceOperationType = InsuranceOperationType.Normal,
                SerialNumber = "",
                OldInsuranceJson = "",
                NewInsuranceJson = JsonConvert.SerializeObject(entity)
            });
            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }

        public override async Task<InsuranceDetailDto> Update(UpdateInsuranceDetailDto input)
        {
            CheckUpdatePermission();

            var entity = await GetEntityByIdAsync(input.Id);

            var OldInsuranceJson = JsonConvert.SerializeObject(entity);

            MapToEntity(input, entity);

            _insuranceLogRepository.Insert(new InsuranceLog
            {
                InsuranceDetailId = entity.Id,
                InsuranceOperationType = InsuranceOperationType.Normal,
                SerialNumber = "",
                OldInsuranceJson = OldInsuranceJson,
                NewInsuranceJson = JsonConvert.SerializeObject(entity)
            });
            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }

        public async Task<int> ImportAll(ImportInsuranceDetailDto[] inputs)
        {
            CheckCreatePermission();

            int insertNum = 0;

            inputs.ToList().ForEach(x =>
            {
                var insuranceLog = new InsuranceLog();
                InsurancePolicy insurancePolicy;
                InsuranceDetail entity;
                switch (x.InsuranceOperationType)
                {
                    case InsuranceOperationType.BatchAdd:
                        insurancePolicy = _insurancePolicyRepository.FirstOrDefault(i => i.InsuranceNum == x.InsuranceNum && i.CarBaseId == x.CarBaseId);
                        x.InsurancePolicyId = insurancePolicy.Id;
                        entity = Repository.FirstOrDefault(o => o.InsurancePolicyId == insurancePolicy.Id && o.InsuranceType.Id == x.InsuranceTypeId);
                        if (entity == null)
                        {
                            entity = MapToEntity(x);
                            entity.Id = Repository.InsertAndGetId(entity);
                        }
                        else
                        {
                            insuranceLog.OldInsuranceJson = JsonConvert.SerializeObject(entity);
                            x.Id = entity.Id;
                            MapToEntity(x, entity);
                        }
                        insuranceLog.NewInsuranceJson = JsonConvert.SerializeObject(entity);
                        insuranceLog.SerialNumber = x.SerialNumber;
                        insuranceLog.InsuranceDetailId = entity.Id;
                        break;
                    case InsuranceOperationType.BatchReduce:
                        insurancePolicy = _insurancePolicyRepository.FirstOrDefault(i => i.InsuranceNum == x.InsuranceNum && i.CarBaseId == x.CarBaseId);
                        x.InsurancePolicyId = insurancePolicy.Id;
                        entity = Repository.FirstOrDefault(o => o.InsurancePolicyId == insurancePolicy.Id && o.InsuranceType.Id == x.InsuranceTypeId);
                        insuranceLog.OldInsuranceJson = JsonConvert.SerializeObject(entity);
                        x.Id = entity.Id;
                        MapToEntity(x, entity);
                        insuranceLog.NewInsuranceJson = JsonConvert.SerializeObject(entity);
                        insuranceLog.SerialNumber = x.SerialNumber;
                        insuranceLog.InsuranceDetailId = entity.Id;
                        break;
                    case InsuranceOperationType.Insure:
                        insurancePolicy = _insurancePolicyRepository.FirstOrDefault(i => i.InsuranceNum == x.InsuranceNum && i.CarBaseId == x.CarBaseId);
                        if (insurancePolicy == null)
                        {
                            var carBase = _carBaseRepository.FirstOrDefault(c => c.Id==x.CarBaseId);
                            var insurancePreset = _insurancePresetRepository.FirstOrDefault(i => i.Id == x.InsurancePresetId);
                            var supplier = _supplierRepository.FirstOrDefault(s => s.Id == x.SupplierId);
                            insurancePolicy = new InsurancePolicy
                            {
                                CarBaseId = carBase.Id,
                                SupplierId = supplier.Id,
                                InsuranceContractType = x.InsuranceContractType,
                                InsurancePolicyType = x.InsurancePolicyType,
                                InsuranceNum = x.InsuranceNum,
                                StartTime = x.StartTime,
                                EndTime = x.EndTime,
                                CompulsoryInsuranceRebateRate = insurancePreset.CompulsoryInsuranceRebateRate,
                                CommercialInsuranceRebateRate = insurancePreset.CommercialInsuranceRebateRate,
                                ExtraRebateRate = insurancePreset.ExtraRebateRate,
                                CarrierLiabilityInsuranceRebateRate = insurancePreset.CarrierLiabilityInsuranceRebateRate,
                            };
                            insurancePolicy.Id = _insurancePolicyRepository.InsertAndGetId(insurancePolicy);
                        }
                        
                        x.InsurancePolicyId = insurancePolicy.Id;

                        entity = MapToEntity(x);

                        if (Repository.FirstOrDefault(d=>d.InsuranceTypeId == entity.InsuranceTypeId && d.InsurancePolicyId == entity.InsurancePolicyId) != null)
                        {
                            throw new UserFriendlyException($"保单编号为{insurancePolicy.InsuranceNum}的保单存在与导入数据相同险种明细，请确认后重新导入");
                        }
                        entity.Id = Repository.InsertAndGetId(entity);

                        insuranceLog.NewInsuranceJson = JsonConvert.SerializeObject(entity);
                        insuranceLog.InsuranceDetailId = entity.Id;

                        break;
                }
                insuranceLog.InsuranceOperationType = x.InsuranceOperationType;
                _insuranceLogRepository.Insert(insuranceLog);
                insertNum++;
            });
            await CurrentUnitOfWork.SaveChangesAsync();

            return insertNum;
        }

        protected override void MapToEntity(UpdateInsuranceDetailDto updateInput, InsuranceDetail entity)
        {
            ObjectMapper.Map(updateInput, entity);
            UpdateInsuranceCalculation(entity);
        }

        protected override InsuranceDetail MapToEntity(CreateInsuranceDetailDto createInput)
        {
            var entity = ObjectMapper.Map<InsuranceDetail>(createInput);
            return UpdateInsuranceCalculation(entity);
        }

        protected InsuranceDetail MapToEntity(ImportInsuranceDetailDto importInput)
        {
            var entity = ObjectMapper.Map<InsuranceDetail>(importInput);
            return UpdateInsuranceCalculation(entity);
        }

        protected void MapToEntity(ImportInsuranceDetailDto importInput, InsuranceDetail entity)
        {
            ObjectMapper.Map(importInput, entity);
            UpdateInsuranceCalculation(entity);
        }

        private InsuranceDetail UpdateInsuranceCalculation(InsuranceDetail entity)
        {
            var insurancePolicy = _insurancePolicyRepository.FirstOrDefault(i => i.Id == entity.InsurancePolicyId);

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
