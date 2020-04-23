using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Insure.Dto;
using System.Linq;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CarPlusGo.CVAS.Insure
{
    public class InsurancePresetAppService
        : AsyncCrudAppService<InsurancePreset, InsurancePresetDto, long, PagedInsurancePresetResultRequestDto, CreateOrUpdateInsurancePresetDto, CreateOrUpdateInsurancePresetDto>, IInsurancePresetAppService
    {
        public InsurancePresetAppService(IRepository<InsurancePreset, long> repository)
        : base(repository)
        {
        }

        protected override IQueryable<InsurancePreset> CreateFilteredQuery(PagedInsurancePresetResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Supplier)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Keyword))
                .WhereIf(input.SupplierId.HasValue, x => x.SupplierId == input.SupplierId)
                .WhereIf(input.InsuranceContractType.HasValue, x => x.InsuranceContractType == input.InsuranceContractType)
                .WhereIf(input.InsurancePolicyType.HasValue, x => x.InsurancePolicyType == input.InsurancePolicyType)
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive);
        }

        protected override InsurancePresetDto MapToEntityDto(InsurancePreset entity)
        {
            var entityDto = ObjectMapper.Map<InsurancePresetDto>(entity);
            entityDto.PresetInsuranceType = JsonConvert.DeserializeObject<List<long>>(entity.PresetInsuranceType);
            return entityDto;
        }

        protected override InsurancePreset MapToEntity(CreateOrUpdateInsurancePresetDto createInput)
        {
            var entity = ObjectMapper.Map<InsurancePreset>(createInput);
            entity.PresetInsuranceType = JsonConvert.SerializeObject(createInput.PresetInsuranceType);
            return entity;
        }

        protected override void MapToEntity(CreateOrUpdateInsurancePresetDto updateInput, InsurancePreset entity)
        {
            ObjectMapper.Map(updateInput, entity);
            entity.PresetInsuranceType = JsonConvert.SerializeObject(updateInput.PresetInsuranceType);
        }
    }
}
