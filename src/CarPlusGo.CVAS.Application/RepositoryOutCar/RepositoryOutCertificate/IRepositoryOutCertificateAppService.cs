using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using CarPlusGo.CVAS.RepositoryOutCar.Dto;

namespace CarPlusGo.CVAS.RepositoryOutCar
{
    public interface IRepositoryOutCertificateAppService
         : IAsyncCrudAppService<RepositoryOutCertificateDto, long, RepositoryOutCertificateResultRequestDto, RepositoryOutCertificateDto, RepositoryOutCertificateDto>
    {
    }
}
