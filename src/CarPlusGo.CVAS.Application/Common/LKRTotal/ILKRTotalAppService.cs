using Abp.Application.Services;
using CarPlusGo.CVAS.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Common
{
    public interface ILKRTotalAppService
        : IAsyncCrudAppService<LKRTotalDto, long, LKRTotalResultRequestDto, LKRTotalDto, LKRTotalDto>
    {
    }
}
