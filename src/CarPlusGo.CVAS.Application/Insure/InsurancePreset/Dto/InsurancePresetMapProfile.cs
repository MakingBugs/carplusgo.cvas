using AutoMapper;

namespace CarPlusGo.CVAS.Insure.Dto
{
    public class InsurancePresetMapProfile : Profile
    {
        public InsurancePresetMapProfile()
        {
            CreateMap<InsurancePresetDto, InsurancePreset>();
            CreateMap<InsurancePresetDto, InsurancePreset>()
                .ForMember(x => x.PresetInsuranceType, opt => opt.Ignore());

            CreateMap<CreateOrUpdateInsurancePresetDto, InsurancePreset>();
            CreateMap<CreateOrUpdateInsurancePresetDto, InsurancePreset>().ForMember(x => x.PresetInsuranceType, opt => opt.Ignore());
        }
    }
}
