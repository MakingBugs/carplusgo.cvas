using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Authorization.Users;
using CarPlusGo.CVAS.Common;
using CarPlusGo.CVAS.LocationFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPlusGo.CVAS.Public
{
    public class Fun
    {
        public class AdminData
        {
            public List<long> areaIdlist { get; set; }
            public List<long> repositoryIdlist { get; set; }
        }
        public static async Task<AdminData> GetPermission(UserManager _userManager, IRepository<VEmp, long> _VEmp, IRepository<LocationManager, long> _LoctionManager, IRepository<RepositoryManager, long> _RepositoryManager,long UserId)
        {
            var user = await _userManager.GetUserByIdAsync(UserId);
            var vemp = await _VEmp.FirstOrDefaultAsync(x => x.UserName == user.Name);
            var loctionmanager = _LoctionManager.GetAll().Where(x => x.EmpID == vemp.UserAuto.ToString());
            var repositorymanager = _RepositoryManager.GetAll().Where(x => x.ManagerID == vemp.UserAuto);
            //区域管理员ID数组
            List<long> areaIdlist = new List<long>();
            //仓库管理员ID数组
            List<long> repositoryIdlist = new List<long>();
            if (loctionmanager.Count() > 0)
            {
                var arealist = loctionmanager.Where(x => x.EmpID == vemp.UserAuto.ToString());
                foreach (var item in arealist)
                {
                    areaIdlist.Add(item.AreaID);
                }
            }
            else if (repositorymanager.Count() > 0)
            {
                var repositorylist = repositorymanager.Where(x => x.ManagerID == vemp.UserAuto);
                foreach (var item in repositorylist)
                {
                    repositoryIdlist.Add(item.RepositoryID);
                }
            }
            AdminData adminData = new AdminData();
            adminData.areaIdlist = areaIdlist;
            adminData.repositoryIdlist = repositoryIdlist;
            //var AdminData = new { areaIdlist = areaIdlist, repositoryIdlist = repositoryIdlist };
            return adminData;
        }
    }
}
