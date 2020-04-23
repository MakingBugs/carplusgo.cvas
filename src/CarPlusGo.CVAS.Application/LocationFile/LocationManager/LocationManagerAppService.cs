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
    public class LocationManagerAppService:
        AsyncCrudAppService<LocationManager, LocationManagerDto, long, LocationManagerResultRequestDto, LocationManagerDto, LocationManagerDto>, ILocationManagerAppService
    {
        public LocationManagerAppService(IRepository<LocationManager, long> repository)
            : base(repository)
        {
        }
        protected override IQueryable<LocationManager> CreateFilteredQuery(LocationManagerResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Location)
                .Where(x => x.IsDeleted == false)
                .WhereIf(input.AreaID.HasValue, x => x.AreaID == input.AreaID)
                .WhereIf(input.IsStop.HasValue, x => x.IsStop == input.IsStop);

        }
    }
}
