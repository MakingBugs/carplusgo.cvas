using AutoMapper;
using Abp.Authorization;
using Abp.Authorization.Roles;
using CarPlusGo.CVAS.Authorization.Roles;

namespace CarPlusGo.CVAS.Roles.Dto
{
    public class RoleMapProfile : Profile
    {
        public RoleMapProfile()
        {
            // Role and permission
            CreateMap<Permission, string>().ConvertUsing(r => r.Name);
            CreateMap<RolePermissionSetting, string>().ConvertUsing(r => r.Name);


            CreateMap<Permission, PermissionDto>().ForMember(x => x.Title, opt => { opt.MapFrom(s => s.DisplayName); });

            CreateMap<CreateRoleDto, Role>().ForMember(x => x.Permissions, opt => opt.Ignore());
            CreateMap<RoleDto, Role>().ForMember(x => x.Permissions, opt => opt.Ignore());
        }
    }
}
