using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Car.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Linq.Extensions;
using System.Threading.Tasks;

namespace CarPlusGo.CVAS.Car
{
    public class CarPartAppService : AsyncCrudAppService<CarPart, CarPartDto, long, CarPartResultRequestDto, CreateCarPartDto, CreateCarPartDto>, ICarPartAppService
    {
        private readonly IRepository<CarPartRight, long> _CarPartRight;
        public CarPartAppService(IRepository<CarPart, long> repository, IRepository<CarPartRight, long> CarPartRight) : base(repository)
        {
            _CarPartRight = CarPartRight;
        }
        protected override IQueryable<CarPart> CreateFilteredQuery(CarPartResultRequestDto input)
        {
            return Repository.GetAll()
                .Where(x => x.IsDeleted == false)
                .WhereIf(input.CarPartName != null && input.CarPartName != "", x => x.CarPartName.Contains(input.CarPartName))
                .WhereIf(input.IsStop.HasValue, x => x.IsStop == input.IsStop);

        }
        public override async Task<CarPartDto> Create(CreateCarPartDto input)
        {
            CheckCreatePermission();

            var entity = MapToEntity(input);
            entity.Id = Repository.InsertAndGetId(entity);
            foreach (var item in input.CarPartRightList)
            {
                var carPartRight = ObjectMapper.Map<CarPartRight>(item);
                carPartRight.CarPartID = entity.Id;
                await _CarPartRight.InsertAsync(carPartRight);
            }
            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }
        public override async Task<CarPartDto> Update(CreateCarPartDto input)
        {
            CheckCreatePermission();

            var entity = await GetEntityByIdAsync(input.Id);

            MapToEntity(input, entity);
            foreach (var item in input.CarPartRightList)
            {
                if (item.CarPartID == entity.Id)
                {
                    var car = _CarPartRight.FirstOrDefault(item.Id);
                    if (car == null)
                    {
                        var carPartRight = ObjectMapper.Map<CarPartRight>(item);
                        carPartRight.CarPartID = entity.Id;
                        await _CarPartRight.InsertAsync(carPartRight);
                    }
                    else
                    {
                        car.Selected = item.Selected;
                    }
                    
                }
            }
            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }
    }
}
