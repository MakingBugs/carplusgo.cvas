using Abp.Application.Services;
using CarPlusGo.CVAS.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Common
{
    public interface IPRInvLinkAppService
        : IAsyncCrudAppService<PRInvLinkDto, long, PRInvLinkResultRequestDto, PRInvLinkDto, PRInvLinkDto>
    {
    }
}
