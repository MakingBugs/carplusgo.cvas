using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using CarPlusGo.CVAS.Car.Dto;

namespace CarPlusGo.CVAS.Car
{
    public interface ICarCertificateAppService:IAsyncCrudAppService<CarCertificateDto,long,CarCertificateResultRequestDto, CreateCarCertificateDto, CreateCarCertificateDto>
    {
    }
}
