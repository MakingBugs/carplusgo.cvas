using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.RepositoryOutCar.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Linq.Extensions;

namespace CarPlusGo.CVAS.RepositoryOutCar
{
    public class RepositoryOutAccessoryAppService
        :AsyncCrudAppService<RepositoryOutAccessory,RepositoryOutAccessoryDto, long, RepositoryOutAccessoryResultRequestDto, RepositoryOutAccessoryDto, RepositoryOutAccessoryDto>,IRepositoryOutAccessoryAppService
    {
        public RepositoryOutAccessoryAppService(IRepository<RepositoryOutAccessory,long> repository) : base(repository)
        {

        }
        protected override IQueryable<RepositoryOutAccessory> CreateFilteredQuery(RepositoryOutAccessoryResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.RepositoryOut, x => x.CarAccessory)
                .Where(x => x.IsDeleted == false)
                .WhereIf(input.Ids.Count() > 0, x => input.Ids.Any(s => x.RepositoryOutID == s));
        }
    }
}
