using Abp.Application.Services;
using CarPlusGo.CVAS.UseCarFiles.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.UseCarFiles
{
    interface IUseCarAccessoryAppService
        : IAsyncCrudAppService<UseCarAccessoryDto, long, UseCarAccessoryResultRequestDto, UseCarAccessoryDto, UseCarAccessoryDto>
    {
    }
}
