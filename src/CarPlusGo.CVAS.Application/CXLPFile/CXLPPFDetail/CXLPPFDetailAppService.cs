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
    public class CXLPPFDetailAppService
    : AsyncCrudAppService<CXLPPFDetail, CXLPPFDetailDto, long, CXLPPFDetailResultRequestDto, CXLPPFDetailDto, CXLPPFDetailDto>, ICXLPPFDetailAppService
    {
        public CXLPPFDetailAppService(IRepository<CXLPPFDetail, long> repository)
           : base(repository)
        {
        }
        protected override IQueryable<CXLPPFDetail> CreateFilteredQuery(CXLPPFDetailResultRequestDto input)
        {
            return Repository.GetAllIncluding(x=>x.BankTypeData)
                .Where(x => x.IsDeleted == false)
                .WhereIf(input.CXLPAuto.HasValue,x=>x.CxlpAuto==input.CXLPAuto);

        }
    }
}
