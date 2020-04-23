using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.CarFixFile.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarPlusGo.CVAS.CarFixFile
{
    public class AdditionalItemAppService
         : AsyncCrudAppService<AdditionalItem, AdditionalItemDto, long, AdditionalItemResultRequestDto, AdditionalItemDto, AdditionalItemDto>, IAdditionalItemAppService
    {
        public AdditionalItemAppService(IRepository<AdditionalItem, long> repository)
           : base(repository)
        {
        }
        protected override IQueryable<AdditionalItem> CreateFilteredQuery(AdditionalItemResultRequestDto input)
        {
            return Repository.GetAll()
                .Where(x => x.IsDeleted == false);

        }
    }
}
