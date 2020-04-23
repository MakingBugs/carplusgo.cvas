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
    public class CarPartRightAppService : AsyncCrudAppService<CarPartRight, CarPartRightDto, long, CarPartRightResultRequestDto, CarPartRightDto, CarPartRightDto>, ICarPartRightAppService
    {
        public CarPartRightAppService(IRepository<CarPartRight, long> repository) : base(repository)
        {
        }
        protected override IQueryable<CarPartRight> CreateFilteredQuery(CarPartRightResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.ItemCode, x => x.CarPart)
                .Where(x => x.IsDeleted == false)
                .WhereIf(input.CarPartID.HasValue, x => x.CarPartID == input.CarPartID)
                .WhereIf(input.Selected.HasValue, x => x.Selected == input.Selected)
                .WhereIf(input.OilIds.Count() > 0, x => input.OilIds.Any(s => x.OilID == s) && x.OilType == 231);

        }
    }
}
