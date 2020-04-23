using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.CXLPFile.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Linq.Extensions;

namespace CarPlusGo.CVAS.CXLPFile
{
    public class RRLKRAppService
    : AsyncCrudAppService<RRLKR, RRLKRDto, long, RRLKRResultRequestDto, RRLKRDto, RRLKRDto>, IRRLKRAppService
    {
        public RRLKRAppService(IRepository<RRLKR, long> repository)
           : base(repository)
        {
        }
        protected override IQueryable<RRLKR> CreateFilteredQuery(RRLKRResultRequestDto input)
        {
            return Repository.GetAllIncluding()
                .Where(x => x.IsDeleted == false)
                .WhereIf(input.FormType.HasValue, x => x.FormType == input.FormType)
                .WhereIf(input.RRLKRAuto.HasValue, x => x.Id == input.RRLKRAuto);

        }
    }
}
