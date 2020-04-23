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
    public class CXLPDZFAppService
    : AsyncCrudAppService<CXLPDZF, CXLPDZFDto, long, CXLPDZFResultRequestDto, CXLPDZFDto, CXLPDZFDto>, ICXLPDZFAppService
    {
        public CXLPDZFAppService(IRepository<CXLPDZF, long> repository)
           : base(repository)
        {
        }
        protected override IQueryable<CXLPDZF> CreateFilteredQuery(CXLPDZFResultRequestDto input)
        {
            return Repository.GetAllIncluding(x=>x.DZFTypeItemCode)
                .Where(x => x.IsDeleted == false)
                .WhereIf(input.CxlpAuto.HasValue,x=>x.CxlpAuto==input.CxlpAuto);

        }
    }
}
