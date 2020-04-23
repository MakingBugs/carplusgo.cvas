using Abp.Application.Services;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Mobile.Dto;
using CarPlusGo.CVAS.Mobile.TShareBank;
using System.Linq;
using Abp.Extensions;
using Abp.Linq.Extensions;
using System.Threading.Tasks;
using System;
using CarPlusGo.CVAS.Mobile.TShareBank.Enum;

namespace CarPlusGo.CVAS.Mobile
{
    public class TargetConfigAppService
        : AsyncCrudAppService<TargetConfig, TargetConfigDto, long, PagedTargetConfigResultRequestDto, TargetConfigDto, TargetConfigDto>, ITargetConfigAppService
    {
        public TargetConfigAppService(IRepository<TargetConfig, long> repository)
            : base(repository)
        {
        }

        protected override IQueryable<TargetConfig> CreateFilteredQuery(PagedTargetConfigResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(input.From.HasValue && input.To.HasValue, x => x.From >= input.From && x.From <= input.To);
        }

        public override async Task<TargetConfigDto> Create(TargetConfigDto input)
        {
            CheckCreatePermission();

            CheckTime(input);

            var entity = MapToEntity(input);

            await Repository.InsertAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }

        public override async Task<TargetConfigDto> Update(TargetConfigDto input)
        {
            CheckUpdatePermission();

            CheckTime(input);

            var entity = await GetEntityByIdAsync(input.Id);

            MapToEntity(input, entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }

        private void CheckTime(TargetConfigDto input)
        {
            switch (input.Unit)
            {
                case Unit.Month:
                    input.From = new DateTime(input.From.ToLocalTime().Year, input.From.ToLocalTime().Month, 1, 0, 0, 0, DateTimeKind.Local);
                    input.To = new DateTime(input.From.Year, input.From.Month, input.From.TotalDaysInMonth(), 0, 0, 0, DateTimeKind.Local).ToDayEnd();
                    break;
                case Unit.Day:
                    input.From = input.From.ToLocalTime().Date;
                    input.To = input.To.ToLocalTime().ToDayEnd();
                    break;
                default:
                    break;
            }
        }
    }
}
