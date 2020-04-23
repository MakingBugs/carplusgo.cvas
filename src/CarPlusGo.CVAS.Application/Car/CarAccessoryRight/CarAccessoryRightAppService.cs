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
    public class CarAccessoryRightAppService: AsyncCrudAppService<CarAccessoryRight, CarAccessoryRightDto, long, CarAccessoryRightResultRequestDto, CarAccessoryRightDto, CarAccessoryRightDto>, ICarAccessoryRightAppService
    {
        public CarAccessoryRightAppService(IRepository<CarAccessoryRight, long> repository) : base(repository)
        {
        }
        protected override IQueryable<CarAccessoryRight> CreateFilteredQuery(CarAccessoryRightResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.ItemCode,x =>x.CarAccessory)
                .Where(x => x.IsDeleted == false)
                .WhereIf(input.CarAccessoryID.HasValue, x => x.CarAccessoryID == input.CarAccessoryID)
                .WhereIf(input.Selected.HasValue, x => x.Selected == input.Selected);

        }
    }
}
