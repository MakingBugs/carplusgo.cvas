using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.CXLPFile.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Linq.Extensions;

namespace CarPlusGo.CVAS.CXLPFile
{
    public class CXLPAppService
    : AsyncCrudAppService<CXLP, CXLPDto, long, CXLPResultRequestDto, CXLPDto, CXLPDto>, ICXLPAppService
    {
        public CXLPAppService(IRepository<CXLP, long> repository)
           : base(repository)
        {
        }
        protected override IQueryable<CXLP> CreateFilteredQuery(CXLPResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.CarBase, x => x.InsurancePolicy, x => x.Order, x => x.CaseStatusItemCode, x => x.CaseTypeItemCode, x => x.VEmp)
                .Where(x => x.IsDeleted == false)
                .WhereIf(input.InsureNo!=null&&input.InsureNo!="",x=>x.InsureNo==input.InsureNo)
                .WhereIf(input.CaseType.HasValue, x => x.CaseType == input.CaseType)
                .WhereIf(input.CaseStatus.HasValue, x => x.CaseStatus == input.CaseStatus)
                .WhereIf(input.MakNo != null && input.MakNo != "", x => x.MakNo == input.MakNo)
                .WhereIf(input.CXLPNO != null && input.CXLPNO != "", x => x.CXLPNO == input.CXLPNO)
                .WhereIf(input.CaseTime.HasValue,x=>x.CaseTime==input.CaseTime)
                .WhereIf(input.CaseTimeFrom.HasValue&&input.CaseTimeTo.HasValue, x => input.CaseTimeFrom<= x.CaseTime && x.CaseTime <= input.CaseTimeTo)
                .WhereIf(input.ZhuanFuDTFrom.HasValue&&input.ZhuanFuDTTo.HasValue, x => input.ZhuanFuDTFrom<=x.ZhuanFuDT && x.ZhuanFuDT <= input.ZhuanFuDTTo);

        }
        public string SelectMaxCXLPNO(CXLPResultRequestDto input)
        {
            return CreateFilteredQuery(input).Max(x => x.CXLPNO);
        }
    }
}
