using Abp.Application.Services;
using CarPlusGo.CVAS.UseCarFiles.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.UseCarFiles
{
    public interface IUseCarCertificateAppService
        : IAsyncCrudAppService<UseCarCertificateDto, long, UseCarCertificateResultRequestDto, UseCarCertificateDto, UseCarCertificateDto>
    {
    }
}
