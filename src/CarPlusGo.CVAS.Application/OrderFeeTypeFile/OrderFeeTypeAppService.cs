using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Common;
using CarPlusGo.CVAS.OrdersFeeTypeFile.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using Abp.Extensions;
using System.Linq;

namespace CarPlusGo.CVAS.OrdersFeeTypeFile
{
    public class OrderFeeTypeAppService
        : AsyncCrudAppService<OrdersFeeType, OrdersFeeTypeDto, long, PagedOrdersFeeTypeResultRequestDto, OrdersFeeTypeDto, OrdersFeeTypeDto>
        ,IOrderFeeTypeAppService

    {
        readonly IRepository<ItemCode, long> _itemcode;
        readonly IRepository<Inc, long> _inc;

        public OrderFeeTypeAppService(IRepository<OrdersFeeType, long> repository
            , IRepository<ItemCode, long> itemcode
            , IRepository<Inc, long> inc) 
            : base(repository)
        {
            _itemcode = itemcode;
            _inc = inc;
        }

        [RemoteService(false)]
        public override Task<OrdersFeeTypeDto> Get(EntityDto<long> input)
        {
            //CheckGetPermission();

            //var entity = await GetEntityByIdAsync(input.Id);
            //return MapToEntityDto(entity);
            return null;
        }

        [RemoteService(false)]
        public virtual Task<PagedResultDto<OrdersFeeTypeDto>> GetAll(PagedResultRequestDto input)
        {
            return null;
        }

        public async Task<OrdersFeeTypeDto> GetOrdersFeeTypeByID(EntityDto<long> input)
        {
            CheckGetPermission();

            var entity = await GetEntityByIdAsync(input.Id); 
            int count = 0;

            count = _inc.GetAll().
                WhereIf(true, x => x.Id == entity.IncAuto).Count();
            if (count > 0)
            {
                var inc = await _inc.GetAsync(entity.IncAuto);
                entity.Inc = inc;
            }
            var dto = MapToEntityDto(entity);

            count = _itemcode.GetAll()
                .WhereIf(true, x => x.ItemType == 1301 && x.Num == entity.FeeTypeAuto)
                .Count();
            if (count > 0)
            {
                var feetype = _itemcode.GetAll()
                .WhereIf(true, x => x.ItemType == 1301 && x.Num == entity.FeeTypeAuto).FirstOrDefault();
                dto.FeeType = feetype;
            } 
            return dto;
        }

        public async Task<PagedResultDto<OrdersFeeTypeDto>> GetOrdersFeeTypeByInc(PagedOrdersFeeTypeResultRequestDto input)
        {
            CheckGetAllPermission();

            var query = Repository.GetAll()
                .WhereIf(input != null && input.Inc_Auto != 0, x => x.IncAuto == input.Inc_Auto); 

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await AsyncQueryableExecuter.ToListAsync(query);

            var retval = new PagedResultDto<OrdersFeeTypeDto>(
                totalCount,
                entities.Select(MapToEntityDto).ToList());

            var incs = _inc.GetAll();
            var itemcode = _itemcode.GetAll()
                .WhereIf(true, x => x.ItemType == 1301);

            foreach (OrdersFeeTypeDto i in retval.Items)
            {
                if (i.IncAuto != 0)
                {
                    if ((from r in incs
                         where r.Id == input.Inc_Auto
                         select r).Count() > 0)
                    {
                        var inc = CommonFunc.ObjectClone<Inc>((from r in incs
                                                               where r.Id == input.Inc_Auto
                                                               select r).FirstOrDefault());
                        i.Inc = inc;
                    }
                }

                if (i.FeeTypeAuto != 0)
                {
                    if ((from r in itemcode
                         where r.Num == i.FeeTypeAuto
                         select r).Count() > 0)
                    {
                        var feetype = CommonFunc.ObjectClone<ItemCode>((from r in itemcode
                                                                        where r.Num == i.FeeTypeAuto
                                                                        select r).FirstOrDefault());

                        i.FeeType = feetype;
                    }
                } 
            } 

            return retval;
        } 
    }
}
