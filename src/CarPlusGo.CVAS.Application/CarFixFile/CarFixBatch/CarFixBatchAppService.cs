using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.CarFixFile.Dto;
using Abp.Linq.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarPlusGo.CVAS.CarFixFile
{
    public class CarFixBatchAppService
        : AsyncCrudAppService<CarFixBatch, CarFixBatchDto, long, CarFixBatchResultRequestDto, CarFixBatchDto, CarFixBatchDto>, ICarFixBatchAppService
    {
        public CarFixBatchAppService(IRepository<CarFixBatch, long> repository)
           : base(repository)
        {
        }
        protected override IQueryable<CarFixBatch> CreateFilteredQuery(CarFixBatchResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.AccBank,x=>x.Supplier,x=>x.Inc)
                .Where(x => x.IsDeleted == false)
                .WhereIf(input.PAyDT.HasValue, x => x.PAyDT.Value.Date == input.PAyDT)
                .WhereIf(input.SupplierAuto.HasValue, x => x.SupplierAuto == input.SupplierAuto)
                .WhereIf(input.CarFixBatchTNO.HasValue, x => x.CarFixBatchTNO == input.CarFixBatchTNO);

        }
    }
}
