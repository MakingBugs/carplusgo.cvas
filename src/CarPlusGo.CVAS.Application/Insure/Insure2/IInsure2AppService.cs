using Abp.Application.Services;
using CarPlusGo.CVAS.Insure.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Insure
{
    public interface IInsure2AppService 
    : IAsyncCrudAppService<Insure2Dto, long, Insure2ResultRequestDto, Insure2Dto, Insure2Dto>
    {

    }
}
