using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using CarPlusGo.CVAS.Car.Dto;
using Abp.Domain.Repositories;
using System.Linq;
using Abp.Linq.Extensions;
using System.Threading.Tasks;
using AutoMapper;

namespace CarPlusGo.CVAS.Car
{
    public class CarCertificateAppService:AsyncCrudAppService<CarCertificate,CarCertificateDto,long,CarCertificateResultRequestDto, CreateCarCertificateDto, CreateCarCertificateDto>, ICarCertificateAppService
    {
        private readonly IRepository<CarCertificateRight, long> _carCertificateRight;
        public CarCertificateAppService(IRepository<CarCertificate,long> repository, IRepository<CarCertificateRight, long> carCertificateRight) : base(repository)
        {
            _carCertificateRight = carCertificateRight;
        }
        protected override IQueryable<CarCertificate> CreateFilteredQuery(CarCertificateResultRequestDto input)
        {
            return Repository.GetAll()
                .Where(x => x.IsDeleted == false)
                .WhereIf(input.CarCertificateName != null && input.CarCertificateName != "", x => x.CarCertificateName.Contains(input.CarCertificateName))
                .WhereIf(input.IsStop.HasValue, x => x.IsStop == input.IsStop);

        }

        public override async Task<CarCertificateDto> Create(CreateCarCertificateDto input)
        {
            CheckCreatePermission();

            var entity = MapToEntity(input);
            entity.Id = Repository.InsertAndGetId(entity);
            foreach (var item in input.CarCertificateRightList)
            {
                var carCertificateRight = ObjectMapper.Map<CarCertificateRight>(item);
                carCertificateRight.CarCertificateID = entity.Id;
                await _carCertificateRight.InsertAsync(carCertificateRight);
            }
            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }
        public override async Task<CarCertificateDto> Update(CreateCarCertificateDto input)
        {
            CheckUpdatePermission();

            var entity = await GetEntityByIdAsync(input.Id);

            MapToEntity(input, entity);

            foreach (var item in input.CarCertificateRightList)
            {
                if (item.CarCertificateID == entity.Id)
                {
                    var car = _carCertificateRight.FirstOrDefault(item.Id);
                    if (car == null)
                    {
                        var carCertificateRight = ObjectMapper.Map<CarCertificateRight>(item);
                        carCertificateRight.CarCertificateID = entity.Id;
                        await _carCertificateRight.InsertAsync(carCertificateRight);
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
