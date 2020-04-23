using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using CarPlusGo.CVAS.TakeCarFile.Dto;

namespace CarPlusGo.CVAS.TakeCarFile
{
    public interface ITakeCarApplyAppService:IAsyncCrudAppService<TakeCarApplyDto,long,TakeCarApplyResultRequestDto,TakeCarApplyDto,TakeCarApplyDto>
    {
    }
}
