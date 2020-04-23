using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Common.Dto
{
    [AutoMap(typeof(PRInvLink))]
    public class PRInvLinkDto : FullAuditedEntityDto<long>
    {
        public long PrinvAuto { get; set; }
        public int Prtype { get; set; }
        public int InvSource { get; set; }
        public long SourceAuto { get; set; }
        public decimal LinkAmt { get; set; }
    }
}
