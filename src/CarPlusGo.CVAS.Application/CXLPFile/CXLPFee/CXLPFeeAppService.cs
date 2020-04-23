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
    public class CXLPFeeAppService
   : AsyncCrudAppService<CXLPFee, CXLPFeeDto, long, CXLPFeeResultRequestDto, CXLPFeeDto, CXLPFeeDto>, ICXLPFeeAppService
    {
        public CXLPFeeAppService(IRepository<CXLPFee, long> repository)
           : base(repository)
        {
        }
        protected override IQueryable<CXLPFee> CreateFilteredQuery(CXLPFeeResultRequestDto input)
        {
            return Repository.GetAllIncluding(x=>x.DZFTypeItemCode,CXLP=>CXLP.FeeTypeItemCode)
                .Where(x => x.IsDeleted == false)
                .WhereIf(input.CxlpAuto.HasValue,x=>x.CxlpAuto==input.CxlpAuto);

        }
    }
}
