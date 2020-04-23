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
    public class CarAccessoryAppService:
        AsyncCrudAppService<CarAccessory, CarAccessoryDto, long, CarAccessoryResultRequestDto, CreateCarAccessoryDto, CreateCarAccessoryDto>, ICarAccessoryAppService
    {
        private readonly IRepository<CarAccessoryRight, long> _CarAccessoryRight;
        public CarAccessoryAppService(IRepository<CarAccessory, long> repository, IRepository<CarAccessoryRight, long> CarAccessoryRight)
            : base(repository)
        {
            _CarAccessoryRight = CarAccessoryRight;
        }
        protected override IQueryable<CarAccessory> CreateFilteredQuery(CarAccessoryResultRequestDto input)
        {
            return Repository.GetAll()
                .Where(x => x.IsDeleted == false)
                .WhereIf(input.CarAccessoryName != null && input.CarAccessoryName != "", x => x.CarAccessoryName.Contains(input.CarAccessoryName))
                .WhereIf(input.IsStop.HasValue, x => x.IsStop == input.IsStop);

        }
        public override async Task<CarAccessoryDto> Create(CreateCarAccessoryDto input)
        {
            CheckCreatePermission();

            var entity = MapToEntity(input);
            entity.Id = Repository.InsertAndGetId(entity);
            foreach (var item in input.CarAccessoryRightList)
            {

                var carAccessoryRight = ObjectMapper.Map<CarAccessoryRight>(item);
                carAccessoryRight.CarAccessoryID = entity.Id;
                await _CarAccessoryRight.InsertAsync(carAccessoryRight);
            }
            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }
        public override async Task<CarAccessoryDto> Update(CreateCarAccessoryDto input)
        {
            CheckCreatePermission();
            var entity = await GetEntityByIdAsync(input.Id);
            MapToEntity(input, entity);

            foreach (var item in input.CarAccessoryRightList)
            {
                if (item.CarAccessoryID == entity.Id)
                {
                    var car = _CarAccessoryRight.FirstOrDefault(item.Id);
                    if (car == null)
                    {
                        var carAccessoryRight = ObjectMapper.Map<CarAccessoryRight>(item);
                        carAccessoryRight.CarAccessoryID = entity.Id;
                        await _CarAccessoryRight.InsertAsync(carAccessoryRight);
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
