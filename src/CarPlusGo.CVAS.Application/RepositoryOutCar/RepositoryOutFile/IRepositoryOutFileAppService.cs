using Abp.Application.Services;
using CarPlusGo.CVAS.RepositoryOutCar.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.RepositoryOutCar
{
    public interface IRepositoryOutFileAppService: IAsyncCrudAppService<RepositoryOutFileDto,long, RepositoryOutFileResultRequestDto, RepositoryOutFileDto, RepositoryOutFileDto>
    {
    }
}
