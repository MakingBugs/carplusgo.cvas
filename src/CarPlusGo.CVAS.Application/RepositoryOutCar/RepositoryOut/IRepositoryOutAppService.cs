using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using CarPlusGo.CVAS.RepositoryOutCar.Dto;

namespace CarPlusGo.CVAS.RepositoryOutCar
{
    public interface IRepositoryOutAppService:IAsyncCrudAppService<RepositoryOutDto,long, RepositoryOutResultRequestDto, CreateRepositoryOutDto, CreateRepositoryOutDto>
    {
    }
}
