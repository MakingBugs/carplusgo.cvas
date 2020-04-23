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
    public class CXLPRecordAppService
    : AsyncCrudAppService<CXLPRecord, CXLPRecordDto, long, CXLPRecordResultRequestDto, CXLPRecordDto, CXLPRecordDto>, ICXLPRecordAppService
    {
        public CXLPRecordAppService(IRepository<CXLPRecord, long> repository)
           : base(repository)
        {
        }
        protected override IQueryable<CXLPRecord> CreateFilteredQuery(CXLPRecordResultRequestDto input)
        {
            return Repository.GetAllIncluding(x=>x.CaseDealWayItemCode,x=>x.ContractorsItemCode,x=>x.CVEmp,x=>x.MVEmp)
                .Where(x => x.IsDeleted == false)
                .WhereIf(input.CxlpAuto.HasValue,x=>x.CxlpAuto==input.CxlpAuto);

        }
    }
}
