using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.CarFixFile.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarPlusGo.CVAS.CarFixFile
{
    public class CarFixItemAppService
    : AsyncCrudAppService<CarFixItem, CarFixItemDto, long, CarFixItemResultRequestDto, CarFixItemDto, CarFixItemDto>, ICarFixItemAppService
    {
        public CarFixItemAppService(IRepository<CarFixItem, long> repository)
           : base(repository)
        {
        }
        protected override IQueryable<CarFixItem> CreateFilteredQuery(CarFixItemResultRequestDto input)
        {
            return Repository.GetAllIncluding()
                .Where(x => x.IsDeleted == false);

        }
    }
}
