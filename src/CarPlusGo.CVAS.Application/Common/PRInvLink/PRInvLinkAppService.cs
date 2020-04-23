using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Linq.Extensions;
using System.Text;

namespace CarPlusGo.CVAS.Common
{
    public class PRInvLinkAppService 
        : AsyncCrudAppService<PRInvLink, PRInvLinkDto, long, PRInvLinkResultRequestDto, PRInvLinkDto, PRInvLinkDto>, IPRInvLinkAppService
    {
        public PRInvLinkAppService(IRepository<PRInvLink, long> repository)
           : base(repository)
        {
        }
        protected override IQueryable<PRInvLink> CreateFilteredQuery(PRInvLinkResultRequestDto input)
        {
            return Repository.GetAllIncluding()
                .Where(x => x.IsDeleted == false)
                .WhereIf(input.SourceAuto.HasValue, x => x.SourceAuto == input.SourceAuto);

        }
    }
}
