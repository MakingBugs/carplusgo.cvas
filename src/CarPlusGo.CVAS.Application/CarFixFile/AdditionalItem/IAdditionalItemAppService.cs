using Abp.Application.Services;
using CarPlusGo.CVAS.CarFixFile.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.CarFixFile
{
    public interface IAdditionalItemAppService
        : IAsyncCrudAppService<AdditionalItemDto, long, AdditionalItemResultRequestDto, AdditionalItemDto, AdditionalItemDto>
    {
    }
}
