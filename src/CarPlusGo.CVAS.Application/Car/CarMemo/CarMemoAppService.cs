using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Car.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Linq.Extensions;

namespace CarPlusGo.CVAS.Car
{
    public class CarMemoAppService
    : AsyncCrudAppService<CarMemo, CarMemoDto, long, PagedCarMemoResultRequestDto,CarMemoDto, CarMemoDto>, ICarMemoAppService
    {
        public CarMemoAppService(IRepository<CarMemo, long> repository)
            : base(repository)
        {
        }
        protected override IQueryable<CarMemo> CreateFilteredQuery(PagedCarMemoResultRequestDto input)
        {
            return Repository.GetAllIncluding()
                .WhereIf(!string.IsNullOrEmpty(input.CarMakNo), x => x.CarMakNo==input.CarMakNo);
        }
    }
}
