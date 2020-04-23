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
    public class CXLPMaterialAppService
    : AsyncCrudAppService<CXLPMaterial, CXLPMaterialDto, long, CXLPMaterialResultRequestDto, CXLPMaterialDto, CXLPMaterialDto>, ICXLPMaterialAppService
    {
        public CXLPMaterialAppService(IRepository<CXLPMaterial, long> repository)
           : base(repository)
        {
        }
        protected override IQueryable<CXLPMaterial> CreateFilteredQuery(CXLPMaterialResultRequestDto input)
        {
            return Repository.GetAllIncluding()
                .Where(x => x.IsDeleted == false)
                .WhereIf(input.CXLPAuto.HasValue,x=>x.CXLPAuto==input.CXLPAuto)
                .WhereIf(input.CXLPMaterialType.HasValue,x=>x.CXLPMaterialType==input.CXLPMaterialType);

        }
    }
}
