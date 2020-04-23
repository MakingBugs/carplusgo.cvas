using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using CarPlusGo.CVAS.RepositoryOutCar.Dto;

namespace CarPlusGo.CVAS.RepositoryOutCar
{
    public interface IRepositoryOutCarPartAppService
        :IAsyncCrudAppService<RepositoryOutCarPartDto,long, RepositoryOutCarPartResultRequestDto, RepositoryOutCarPartDto, RepositoryOutCarPartDto>
    {
    }
}
