using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using CarPlusGo.CVAS.Car;
using CarPlusGo.CVAS.Common;
using CarPlusGo.CVAS.OrdersFile.Dto;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CarPlusGo.CVAS.OrdersFile
{
    public class OrdersListAppService 
        : AsyncCrudAppService<Orders, OrdersListDto, long, PagedOrdersListResultRequestDto>
    {
        readonly IRepository<FactoryBrand, long> _factorybrand;
        readonly IRepository<Brand, long> _brand;
        readonly IRepository<Clasen, long> _clasen;
        readonly IRepository<ItemCode, long> _itemcode;

        public OrdersListAppService(IRepository<Orders, long> repository
            , IRepository<FactoryBrand, long> factorybrand
            , IRepository<Brand, long> brand
            , IRepository<Clasen, long> clasen
            , IRepository<ItemCode, long> itemcode
            )
            : base(repository)
        {
            _factorybrand = factorybrand;
            _brand = brand;
            _clasen = clasen;
            _itemcode = itemcode;
        }

        [RemoteService(false)]
        public override Task<OrdersListDto> Get(EntityDto<long> input)
        {
            return null;
        }


        [RemoteService(false)]
        public override Task<PagedResultDto<OrdersListDto>> GetAll(PagedOrdersListResultRequestDto input)
        {
            return null;
        }

        [RemoteService(false)]
        public override Task<OrdersListDto> Create(OrdersListDto input)
        {
            return null;
        }

        [RemoteService(false)]
        public override Task<OrdersListDto> Update(OrdersListDto input)
        {
            return null;
        }

        [RemoteService(false)]
        public override Task Delete(EntityDto<long> input)
        {
            return null;
        }

        public async Task<PagedResultDto<OrdersListDto>> GetOrdersList(PagedOrdersListResultRequestDto input)
        {
            CheckGetAllPermission();

            var query = CreateFilteredQuery(input);

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await AsyncQueryableExecuter.ToListAsync(query);
             
            var retval = new PagedResultDto<OrdersListDto>(
                totalCount,
                entities.Select(MapToEntityDto).ToList() 
            );

            var item327 = _itemcode.GetAll().Where(x => x.ItemType == 327);

            foreach (OrdersListDto o in retval.Items)
            { 
                o.OrderStatusName = item327
                    .Where(x => x.Num == o.OrderStatus)
                    .FirstOrDefault().ItemName;
            }

            return retval;
        }

        protected override IQueryable<Orders> CreateFilteredQuery(PagedOrdersListResultRequestDto input)
        { 
            var query = Repository.GetAllIncluding(x => x.TradeItem,
                x => x.FactoryBrand,
                x => x.Brand,
                x => x.Clasen)
                 .WhereIf(input != null && input.Orders_Auto > 0, x => x.Id == input.Orders_Auto)
                .WhereIf(input != null && input.From != null, x => DateTime.Compare((DateTime)input.From, x.Cdt) <= 0)
                .WhereIf(input != null && input.To != null, x => DateTime.Compare(x.Cdt, (DateTime)input.To) <= 0)
                .WhereIf(input != null && input.Status != null, x => x.OrderStatus == input.Status);
 
            //foreach (Orders o in query)
            //{
            //    var itemcode = _itemcode.GetAll()
            //        .Where(x => x.Num == o.OrderStatus && x.ItemType == 327).FirstOrDefault();
            //    o.OrderStatusName = itemcode.ItemName;
            //}

            return query;

            //return Repository.GetAll()
            //    .WhereIf(input != null && input.Orders_Auto > 0, x => x.Id == input.Orders_Auto)
            //    .WhereIf(input != null && input.From != null, x => x.Cdt > input.From)
            //    .WhereIf(input != null && input.To != null, x => x.Cdt < input.To)
            //    .WhereIf(input != null && input.Status != null, x => x.OrderStatus == input.Status);
        }
    }
}
