using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.UseCarFiles.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarPlusGo.CVAS.UseCarFiles
{
    public class UseCarFileAppService
        : AsyncCrudAppService<UseCarFile, UseCarFileDto, long, UseCarFileResultRequestDto, UseCarFileDto, UseCarFileDto>, IUseCarFileAppService
    {
        public UseCarFileAppService(IRepository<UseCarFile, long> repository) : base(repository)
        {

        }
        protected override IQueryable<UseCarFile> CreateFilteredQuery(UseCarFileResultRequestDto input)
        {
            return Repository.GetAllIncluding()
                .Where(x => x.IsDeleted == false);
        }
    }
}
