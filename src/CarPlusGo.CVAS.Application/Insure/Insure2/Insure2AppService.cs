using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Insure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Linq.Extensions;

namespace CarPlusGo.CVAS.Insure
{
    public class Insure2AppService
    : AsyncCrudAppService<Insure2, Insure2Dto, long, Insure2ResultRequestDto, Insure2Dto, Insure2Dto>, IInsure2AppService
    {
        public Insure2AppService(IRepository<Insure2, long> repository)
           : base(repository)
        {
        }
        protected override IQueryable<Insure2> CreateFilteredQuery(Insure2ResultRequestDto input)
        {
            return Repository.GetAllIncluding(x=>x.SupplierData)
                .Where(x => x.IsDeleted == false)
                .WhereIf(input.CarBaseAuto.HasValue,x=>x.CarBaseAuto==input.CarBaseAuto)
                .WhereIf(input.Year.HasValue,x=>x.Year==input.Year)
                .WhereIf(input.InsureType.HasValue,x=>x.InsureType==input.InsureType);

        }
    }
}
