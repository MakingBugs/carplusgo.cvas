using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Authorization.Users;
using CarPlusGo.CVAS.Common;
using CarPlusGo.CVAS.LocationFile;
using CarPlusGo.CVAS.UseCarApplyFile.Dto;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using CarPlusGo.CVAS.Public;

namespace CarPlusGo.CVAS.UseCarApplyFile
{
    public class UseCarApplyAppService
    : AsyncCrudAppService<UseCarApply, UseCarApplyDto, long, UseCarApplyResultRequestDto, UseCarApplyDto, UseCarApplyDto>, IUseCarApplyAppService
    {
        private readonly UserManager _userManager;
        private readonly IRepository<VEmp, long> _VEmp;
        private readonly IRepository<LocationManager, long> _LoctionManager;
        private readonly IRepository<RepositoryManager, long> _RepositoryManager;
        public UseCarApplyAppService(
            IRepository<UseCarApply, long> repository,
            UserManager userManager,
            IRepository<VEmp, long> VEmp,
            IRepository<LocationManager, long> LoctionManager,
            IRepository<RepositoryManager, long> RepositoryManager)
           : base(repository)
        {
            _userManager = userManager;
            _VEmp = VEmp;
            _LoctionManager = LoctionManager;
            _RepositoryManager = RepositoryManager;
        }
        protected override IQueryable<UseCarApply> CreateFilteredQuery(UseCarApplyResultRequestDto input)
        {
            var statuslist = new List<int> { 10, 20, 30 };
            return Repository.GetAllIncluding(x=>x.UserReasonData,x=>x.Location,x=>x.Repository,x=>x.Clasen.Brand.FactoryBrand,x=>x.CarBaseData.Clasen.Brand.FactoryBrand)
            .Where(x => x.IsDeleted == false)
            .Where(x=> statuslist.Contains(x.Status));

        }
        public override async Task<PagedResultDto<UseCarApplyDto>> GetAll(UseCarApplyResultRequestDto input)
        {
            CheckGetAllPermission();
            IQueryable<UseCarApply> query;

            var AdminData = await Fun.GetPermission(_userManager, _VEmp, _LoctionManager, _RepositoryManager, AbpSession.UserId.Value);
            

            if (AdminData.areaIdlist.Count > 0)
            {
                query = CreateFilteredQuery(input)
                    .Where(x => AdminData.areaIdlist.Contains(x.AreaID));
            }
            else
            {
                query = CreateFilteredQuery(input)
                    .Where(x => AdminData.repositoryIdlist.Contains(x.RepositoryID));
            }

            foreach (var item in query)
            {
                var AbpUserData = await _userManager.GetUserByIdAsync(item.CreatorUserId.Value);
                var ProposerDate = await _VEmp.FirstOrDefaultAsync(x => x.UserName == AbpUserData.Name);//申请人信息
                var UserData = await _VEmp.FirstOrDefaultAsync(x => x.UserAuto == item.EmpID);//用车人信息


                item.ProposerData = ProposerDate;
                item.UserData = UserData;
            }

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await AsyncQueryableExecuter.ToListAsync(query);

            return new PagedResultDto<UseCarApplyDto>(
                totalCount,
                entities.Select(MapToEntityDto).ToList()
            );
        }
    }
}
