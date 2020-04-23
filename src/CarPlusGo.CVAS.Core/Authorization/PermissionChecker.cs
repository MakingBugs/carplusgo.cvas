using Abp.Authorization;
using CarPlusGo.CVAS.Authorization.Roles;
using CarPlusGo.CVAS.Authorization.Users;

namespace CarPlusGo.CVAS.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
