using Abp.Application.Services;
using CarPlusGo.CVAS.UseCarApplyFile.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.UseCarApplyFile
{
    public interface IUseCarApplyAppService
    : IAsyncCrudAppService<UseCarApplyDto, long, UseCarApplyResultRequestDto, UseCarApplyDto, UseCarApplyDto>
    {

    }
}
