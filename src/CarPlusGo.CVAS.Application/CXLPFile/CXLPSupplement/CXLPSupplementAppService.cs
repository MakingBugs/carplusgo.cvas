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
    public class CXLPSupplementAppService
    : AsyncCrudAppService<CXLPSupplement, CXLPSupplementDto, long, CXLPSupplementResultRequestDto, CXLPSupplementDto, CXLPSupplementDto>, ICXLPSupplementAppService
    {
        public CXLPSupplementAppService(IRepository<CXLPSupplement, long> repository)
           : base(repository)
        {
        }
        protected override IQueryable<CXLPSupplement> CreateFilteredQuery(CXLPSupplementResultRequestDto input)
        {
            return Repository.GetAllIncluding(x=>x.ContractorsItemCode)
                .Where(x => x.IsDeleted == false)
                .WhereIf(input.CXLPAuto.HasValue,x=>x.CXLPAuto==input.CXLPAuto);

        }
    }
}
