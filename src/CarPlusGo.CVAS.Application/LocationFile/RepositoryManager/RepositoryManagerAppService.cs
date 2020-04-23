using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.LocationFile.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Linq.Extensions;

namespace CarPlusGo.CVAS.LocationFile
{
    public class RepositoryManagerAppService
        : AsyncCrudAppService<RepositoryManager, RepositoryManagerDto, long, RepositoryManagerResultRequestDto, RepositoryManagerDto, RepositoryManagerDto>, IRepositoryManagerAppService
    {
        public RepositoryManagerAppService(IRepository<RepositoryManager, long> repository)
           : base(repository)
        {
        }
        protected override IQueryable<RepositoryManager> CreateFilteredQuery(RepositoryManagerResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Repository, x => x.Repository.Location)
                .Where(x => x.IsDeleted == false)
                .WhereIf(input.IsStop.HasValue, x => x.IsStop == input.IsStop)
                .WhereIf(input.RepositoryID.HasValue, x => x.RepositoryID == input.RepositoryID)
                .WhereIf(input.ManagerID.HasValue, x => x.ManagerID == input.ManagerID);

        }
    }
}
