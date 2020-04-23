using Abp.Application.Services;
using CarPlusGo.CVAS.Mobile.MemberAnalysis.MembersGeneral.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Mobile.MemberAnalysis.MembersGeneral
{
    public interface IMembersGeneralAppService : IApplicationService
    {
        MembersGeneralDto MembersGeneral(MembersGeneralResultRequestDto input);
    }
}
