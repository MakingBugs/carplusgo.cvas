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
    public class RepositoryOutFileAppService
        : AsyncCrudAppService<RepositoryOutFile, RepositoryOutFileDto, long, RepositoryOutFileResultRequestDto, RepositoryOutFileDto, RepositoryOutFileDto>, IRepositoryOutFileAppService
    {
        public RepositoryOutFileAppService(IRepository<RepositoryOutFile,long> repository) 
            : base(repository)
        {

        }
        protected override IQueryable<RepositoryOutFile> CreateFilteredQuery(RepositoryOutFileResultRequestDto input)
        {
            return Repository.GetAllIncluding()
            .Where(x => x.IsDeleted == false)
            .WhereIf(input.RepositoryOutID.HasValue, x => x.RepositoryOutID == input.RepositoryOutID)
            .WhereIf(input.Type.HasValue, x => x.Type == input.Type)
            .WhereIf(input.Ids.Count() > 0, x => input.Ids.Any(s => x.RepositoryOutID == s));
        }
    }
}
