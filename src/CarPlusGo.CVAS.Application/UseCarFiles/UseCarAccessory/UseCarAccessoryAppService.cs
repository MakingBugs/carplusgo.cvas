using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.UseCarFiles.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarPlusGo.CVAS.UseCarFiles
{
    public class UseCarAccessoryAppService
        : AsyncCrudAppService<UseCarAccessory, UseCarAccessoryDto, long, UseCarAccessoryResultRequestDto, UseCarAccessoryDto, UseCarAccessoryDto>, IUseCarAccessoryAppService
    {
        public UseCarAccessoryAppService(IRepository<UseCarAccessory, long> repository) : base(repository)
        {

        }
        protected override IQueryable<UseCarAccessory> CreateFilteredQuery(UseCarAccessoryResultRequestDto input)
        {
            return Repository.GetAllIncluding()
                .Where(x => x.IsDeleted == false);
        }
    }
}
