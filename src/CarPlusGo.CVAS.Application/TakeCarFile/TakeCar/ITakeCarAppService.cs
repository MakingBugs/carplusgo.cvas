using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using CarPlusGo.CVAS.TakeCarFile.Dto;

namespace CarPlusGo.CVAS.TakeCarFile
{
    public interface ITakeCarAppService
        :IAsyncCrudAppService<TakeCarDto, long, TakeCarResultRequestDto , CreateTakeCarDto, CreateTakeCarDto>
    {
    }
}
