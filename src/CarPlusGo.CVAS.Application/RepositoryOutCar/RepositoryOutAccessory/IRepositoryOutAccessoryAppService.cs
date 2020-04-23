using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using CarPlusGo.CVAS.RepositoryOutCar.Dto;

namespace CarPlusGo.CVAS.RepositoryOutCar
{
    public interface IRepositoryOutAccessoryAppService
        :IAsyncCrudAppService<RepositoryOutAccessoryDto,long, RepositoryOutAccessoryResultRequestDto, RepositoryOutAccessoryDto, RepositoryOutAccessoryDto>
    {
    }
}
