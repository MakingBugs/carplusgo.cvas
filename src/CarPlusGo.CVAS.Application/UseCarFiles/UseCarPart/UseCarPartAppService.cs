using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using CarPlusGo.CVAS.UseCarFiles.Dto;
using Abp.Domain.Repositories;
using System.Linq;
using CarPlusGo.CVAS.UseCarFiles;

namespace CarPlusGo.CVAS.UseCarFile
{
    public class UseCarPartAppService:AsyncCrudAppService<UseCarPart, UseCarPartDto, long, UseCarPartResultRequestDto, UseCarPartDto, UseCarPartDto>,IUseCarPartAppService
    {
        public UseCarPartAppService(IRepository<UseCarPart,long> repository) : base(repository)
        {

        }
        protected override IQueryable<UseCarPart> CreateFilteredQuery(UseCarPartResultRequestDto input)
        {
            return Repository.GetAllIncluding()
                .Where(x => x.IsDeleted == false);
        }
    }
}
