using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using CarPlusGo.CVAS.RepositoryOutCar.Dto;
using Abp.Domain.Repositories;
using System.Linq;
using Abp.Linq.Extensions;

namespace CarPlusGo.CVAS.RepositoryOutCar
{
    public class RepositoryOutCarPartAppService
        :AsyncCrudAppService<RepositoryOutCarPart, RepositoryOutCarPartDto,long, RepositoryOutCarPartResultRequestDto, RepositoryOutCarPartDto, RepositoryOutCarPartDto>,IRepositoryOutCarPartAppService
    {
        public RepositoryOutCarPartAppService(IRepository<RepositoryOutCarPart,long> repository) : base(repository)
        {

        }
        protected override IQueryable<RepositoryOutCarPart> CreateFilteredQuery(RepositoryOutCarPartResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.RepositoryOut, x => x.CarPart, x => x.ItemCode)
                .Where(x => x.IsDeleted == false)
                .WhereIf(input.Ids.Count() > 0, x => input.Ids.Any(s => x.RepositoryOutID == s));
        }
    }
}
