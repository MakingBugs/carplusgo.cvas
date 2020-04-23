using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System.Linq;
using Abp.Linq.Extensions;
using Abp.Application.Services.Dto;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using CarPlusGo.CVAS.Common.Dto; 

namespace CarPlusGo.CVAS.Common
{
    public class ItemCodeAppService :
        AsyncCrudAppService<ItemCode, ItemCodeDto, long, PagedItemCodeResultRequestDto, ItemCodeDto, ItemCodeDto>,
        IItemCodeAppService
    {
        public ItemCodeAppService(IRepository<ItemCode, long> repository) 
            : base(repository)
        {

        }

        protected override IQueryable<ItemCode> CreateFilteredQuery(PagedItemCodeResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(input.ItemTypes != null && input.ItemTypes.Length > 0, x => x.ItemType.IsIn(input.ItemTypes))
                .WhereIf(input.ItemName != null && input.ItemName != "", x => x.ItemName.Contains(input.ItemName));
        }

        protected override IQueryable<ItemCode> ApplySorting(IQueryable<ItemCode> query, PagedItemCodeResultRequestDto input)
        {
            return query.OrderBy(x => x.Id);
        }

        [RemoteService(false)]
        public override Task Delete(EntityDto<long> input)
        {
            return null;
        }

        [RemoteService(false)]
        public override Task<ItemCodeDto> Get(EntityDto<long> input)
        {
            return null;
        }

        [RemoteService(false)]
        public override Task<PagedResultDto<ItemCodeDto>> GetAll(PagedItemCodeResultRequestDto input)
        {
            return null;
        }

        public async Task<PagedResultDto<ItemCodeDto>> GetItemCodeByItemTypes(PagedItemCodeResultRequestDto input)
        {
            CheckGetAllPermission();

            var query = CreateFilteredQuery(input);

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await AsyncQueryableExecuter.ToListAsync(query);

            return new PagedResultDto<ItemCodeDto>(
                totalCount,
                entities.Select(MapToEntityDto).ToList()
            );
        }


        public async Task<ItemCodeDto> GetItemCodeByID(EntityDto<long> input)
        {
           
            CheckGetPermission();

            var entity = await GetEntityByIdAsync(input.Id);
            return MapToEntityDto(entity);
        }
    }
}

