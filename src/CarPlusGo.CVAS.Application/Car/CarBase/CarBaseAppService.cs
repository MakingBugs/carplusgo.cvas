using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Car.Dto;
using System.Linq;
using Abp.Linq.Extensions;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CarPlusGo.CVAS.Authorization.Users;
using CarPlusGo.CVAS.Common;
using CarPlusGo.CVAS.LocationFile;
using CarPlusGo.CVAS.Public;
using CarPlusGo.CVAS.RepositoryOutCar;
using System.Collections.Generic;
using CarPlusGo.CVAS.UseCarApplyFile;

namespace CarPlusGo.CVAS.Car
{
    public class CarBaseAppService
        : AsyncCrudAppService<CarBase, CarBaseDto, long, PagedCarBaseResultRequestDto, UpdateCarBaseDto, UpdateCarBaseDto>, ICarBaseAppService
    {
        private readonly UserManager _userManager;
        private readonly IRepository<VEmp, long> _VEmp;
        private readonly IRepository<LocationManager, long> _LoctionManager;
        private readonly IRepository<RepositoryManager, long> _RepositoryManager;
        private readonly IRepository<Repository, long> _Repository;
        private readonly IRepository<RepositoryOut, long> _RepositoryOut;
        private readonly IRepository<UseCarApply, long> _UseCarApply;
        public CarBaseAppService(IRepository<CarBase, long> repository,
            UserManager userManager,
            IRepository<VEmp, long> VEmp,
            IRepository<LocationManager, long> LoctionManager,
            IRepository<RepositoryManager, long> RepositoryManager,
            IRepository<Repository, long> Repository,
            IRepository<RepositoryOut, long> RepositoryOut,
            IRepository<UseCarApply, long> UseCarApply)
            : base(repository)
        {
            _userManager = userManager;
            _VEmp = VEmp;
            _LoctionManager = LoctionManager;
            _RepositoryManager = RepositoryManager;
            _Repository = Repository;
            _RepositoryOut = RepositoryOut;
            _UseCarApply = UseCarApply;
        }

        protected override IQueryable<CarBase> CreateFilteredQuery(PagedCarBaseResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.CreditProvince, x => x.CreditCity, x => x.Brand, x => x.Clasen, x => x.FactoryBrand, x => x.Inc, x => x.ItemCode,x=>x.Repository.Location)
                .WhereIf(input.UseType.HasValue, x => x.UseType.Equals(input.UseType) && x.ItemType == 225)
                .WhereIf(input.ClasenAuto.HasValue, x => x.ClasenAuto.Equals(input.ClasenAuto))
                .WhereIf(input.CarBaseAuto.HasValue,x=>x.Id==input.CarBaseAuto)
                .WhereIf(input.CarDtFrom != null && input.CarDtTo != null, x => input.CarDtFrom <= x.CarDt && x.CarDt <= input.CarDtTo)
                .WhereIf(!string.IsNullOrEmpty(input.Keyword), x => x.CarNo.Contains(input.Keyword) || x.MakNo.Contains(input.Keyword) || x.EngNo.Contains(input.Keyword))
                .WhereIf(input.IsBusiness.HasValue, x => x.IsBusiness == input.IsBusiness)
                .WhereIf(input.Oil.HasValue, x => x.Oil == input.Oil)
                .WhereIf(input.RepositoryID.HasValue, x => x.RepositoryID == input.RepositoryID);
        }

        public async Task<PagedResultDto<CarBaseDto>> SelectCarBase(PagedCarBaseSelectResultRequestDto input)
        {
            CheckGetAllPermission();

            var query = CreateFilteredQuery(input)
                .WhereIf(input.MakNo != null && input.MakNo != "", x => x.MakNo == input.MakNo)
                .WhereIf(input.CarNo != null && input.CarNo != "", x => x.CarNo == input.CarNo)
                .WhereIf(input.EngNo != null && input.EngNo != "", x => x.EngNo == input.EngNo)
                .WhereIf(input.MakDtFrom != null && input.MakDtTo != null, x => input.MakDtFrom <= x.MakDt && x.MakDt <= input.MakDtTo)
                .WhereIf(input.CarDtFrom != null && input.CarDtTo != null, x => input.CarDtFrom <= x.CarDt && x.CarDt <= input.CarDtTo)
                .WhereIf(!string.IsNullOrEmpty(input.Keyword), x => x.CarNo.Contains(input.Keyword) || x.MakNo.Contains(input.Keyword) || x.EngNo.Contains(input.Keyword))
                .WhereIf(input.IsBusiness.HasValue, x => x.IsBusiness == input.IsBusiness)
                .WhereIf(input.Oil.HasValue, x => x.Oil == input.Oil)
                .WhereIf(input.RepositoryID.HasValue, x => x.RepositoryID == input.RepositoryID);

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await AsyncQueryableExecuter.ToListAsync(query);

            return new PagedResultDto<CarBaseDto>(
                totalCount,
                entities.Select(MapToEntityDto).ToList()
            );
        }
        public long? SelectMaxCarBaseId(PagedCarBaseSelectResultRequestDto input)
        {
            return CreateFilteredQuery(input).Max(x => x.Id);
        }

        public async Task<PagedResultDto<CarBaseDto>> SelectCarByJurisdiction(PagedSelectUsableCarByJurisdictionDto input)
        {
            CheckGetAllPermission();
            IQueryable<CarBase> query;

            var AdminData = await Fun.GetPermission(_userManager, _VEmp, _LoctionManager, _RepositoryManager, AbpSession.UserId.Value);

            List<long> RepositoryOutStatusList = new List<long> { 15, 35, 50 };//出入库记录表中，[可使用]车辆状态
            List<long> UseCarApplyStatulist = new List<long> { 20, 30 };//出还车记录表中，[不可使用]的车辆状态
            if (AdminData.areaIdlist.Count > 0)
            {
                var RepositoryIdList = _Repository.GetAll().Where(x => AdminData.areaIdlist.Contains(x.AreaID)).Select(x => x.Id);//拿到有权限的仓库ID
                var caridlist = CreateFilteredQuery(input).Where(x => RepositoryIdList.Contains(x.RepositoryID.Value)).Select(x => x.Id);//拿到用户有权管理的所有车俩ID
                var cantusecarid = _UseCarApply.GetAll().Where(x => UseCarApplyStatulist.Contains(x.Status)).Select(x=>x.CarBase);//从出还车记录表中拿出不可用的车辆ID
                var CarBaseIdList1 = _RepositoryOut.GetAll().Where(x => caridlist.Contains(x.CarBaseAuto)).Where(x=> RepositoryOutStatusList.Contains(x.Status)).Select(x=>x.CarBaseAuto);//根据出入库记录表拿到可使用车辆ID
               
                //取出车辆详细信息
                query = CreateFilteredQuery(input)
                    .Where(x=> CarBaseIdList1.Contains(x.Id))//第一次过滤
                    .Where(x=> cantusecarid.Contains(x.Id))//第二次过滤
                    .WhereIf(input.RepositoryId.HasValue,x=>x.RepositoryID==input.RepositoryId)
                    .WhereIf(!string.IsNullOrEmpty(input.MakNo),x=>x.MakNo==input.MakNo);
            }
            else
            {
                var caridlist = CreateFilteredQuery(input).Where(x => AdminData.repositoryIdlist.Contains(x.RepositoryID.Value)).Select(x => x.Id);//拿到用户有权管理的所有车俩ID
                var cantusecarid = _UseCarApply.GetAll().Where(x => UseCarApplyStatulist.Contains(x.Status)).Select(x => x.CarBase);//从出还车记录表中拿出不可用的车辆ID
                var CarBaseIdList1 = _RepositoryOut.GetAll().Where(x => caridlist.Contains(x.CarBaseAuto)).Where(x => RepositoryOutStatusList.Contains(x.Status)).Select(x => x.CarBaseAuto);//根据出入库记录表拿到可使用车辆ID

                //取出车辆详细信息
                query = CreateFilteredQuery(input)
                    .Where(x => CarBaseIdList1.Contains(x.Id))//第一次过滤
                    .Where(x => cantusecarid.Contains(x.Id))//第二次过滤
                    .WhereIf(input.RepositoryId.HasValue, x => x.RepositoryID == input.RepositoryId)
                    .WhereIf(!string.IsNullOrEmpty(input.MakNo), x => x.MakNo == input.MakNo);
            }

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await AsyncQueryableExecuter.ToListAsync(query);

            return new PagedResultDto<CarBaseDto>(
                totalCount,
                entities.Select(MapToEntityDto).ToList()
            );
        }
    

        public async Task<int> CreateAll(UpdateCarBaseDto[] inputList)
        {
            CheckCreatePermission();
            int insertNum = 0;

            foreach (var input in inputList)
            {
                var data = Repository.GetAll().Where(x => x.MakNo == input.MakNo).FirstOrDefault();

                
                var entity = MapToEntity(input);
                if (data!=null)
                {
                    input.Id = data.Id;
                    MapToEntity(input, data);
                }
                else
                {
                    await Repository.InsertAsync(entity);
                }
                insertNum++;
            }

            await CurrentUnitOfWork.SaveChangesAsync();
            return insertNum;
        }
    }
}