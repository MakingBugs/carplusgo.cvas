using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.CarFixFile.Dto;
using System.Linq;
using Abp.Linq.Extensions;

namespace CarPlusGo.CVAS.CarFixFile
{
    public class CarRepairAppService
        : AsyncCrudAppService<CarRepair, CarRepairDto, long, CarRepairResultRequestDto, CarRepairDto, CarRepairDto>, ICarRepairAppService
    {
        public CarRepairAppService(IRepository<CarRepair, long> repository)
           : base(repository)
        {
        }
        protected override IQueryable<CarRepair> CreateFilteredQuery(CarRepairResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.PayModeItemCode, x => x.RepairTypeItemCode, x => x.CarBase, x => x.Supplier)
                .Where(x => x.IsDeleted == false)
                .WhereIf(input.CarBaseAuto.HasValue, x => x.CarBaseAuto == input.CarBaseAuto)
                .WhereIf(input.Status.HasValue, x => x.Status == input.Status);

        }
    }
}
