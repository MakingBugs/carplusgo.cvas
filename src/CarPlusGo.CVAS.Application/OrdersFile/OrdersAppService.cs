using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Car;
using CarPlusGo.CVAS.Common;
using CarPlusGo.CVAS.OrdersFile.Dto;
using Abp.Linq.Extensions;
using Abp.Extensions;

namespace CarPlusGo.CVAS.OrdersFile
{
    public class OrdersAppService : 
        AsyncCrudAppService<Orders, OrdersDto, long, PagedOrdersResultRequestDto, OrdersDto, OrdersDto>
        , IOrdersAppService
    {    
        readonly IRepository<TradeItem, long> _tradeitem = null;
        readonly IRepository<FactoryBrand, long> _factorybrand = null;
        readonly IRepository<Brand, long> _brand = null;
        readonly IRepository<Clasen, long> _clasen = null;
        readonly IRepository<ItemCode, long> _itemcode = null;

        public OrdersAppService(IRepository<Orders, long> repository,
            IRepository<TradeItem, long> tradeitem,
            IRepository<FactoryBrand, long> factorybrand,
            IRepository<Brand, long> brand,
            IRepository<Clasen, long> clasen,
            IRepository<ItemCode, long> itemcode)
            : base(repository)
        {
            _clasen = clasen;
            _tradeitem = tradeitem;
            _factorybrand = factorybrand;
            _brand = brand;
            _itemcode = itemcode;
        }

        [RemoteService(false)]
        public override Task<OrdersDto> Get(EntityDto<long> input)
        {
            return null;
        }

        [RemoteService(false)]
        public override Task<PagedResultDto<OrdersDto>> GetAll(PagedOrdersResultRequestDto input)
        {
            return null;
        }
        public async Task<OrdersDto> GetOrdersByID(EntityDto<long> input)
        {
            CheckGetPermission(); 

            var entity = await GetEntityByIdAsync(input.Id);
            var tradeitem = await _tradeitem.GetAsync(entity.TradeItemAuto);
            var factorybrand = await _factorybrand.GetAsync(entity.FactoryBrandAuto);
            var brand = await _brand.GetAsync(entity.BrandAuto);
            var clasen = await _clasen.GetAsync(entity.ClasenAuto);

            entity.TradeItem = tradeitem;
            entity.FactoryBrand = factorybrand;
            entity.Brand = brand;
            entity.Clasen = clasen;

            return MapToEntityDto(entity);
        } 

        //public async Task<PagedResultDto<OrdersDto>> GetOrdersList(PagedOrdersResultRequestDto input)
        //{
        //    CheckGetAllPermission();

        //    var query = CreateFilteredQuery(input); 
           
            
        //    var totalCount = await AsyncQueryableExecuter.CountAsync(query);

        //    query = ApplySorting(query, input);
        //    query = ApplyPaging(query, input);

        //    var entities = await AsyncQueryableExecuter.ToListAsync(query);

        //    foreach (Orders o in entities)
        //    {
        //        var factorybrand = await _factorybrand.GetAsync(o.FactoryBrandAuto);
        //        var brand = await _brand.GetAsync(o.BrandAuto);
        //        var clasen = await _clasen.GetAsync(o.ClasenAuto);
        //        var itemcode = _itemcode.GetAll()
        //            .WhereIf(true, x => x.Num == o.OrderStatus && x.ItemType == 327)
        //            .FirstOrDefault();

        //        o.FactoryBrand = factorybrand;
        //        o.Brand = brand;
        //        o.Clasen = clasen;

        //    }

        //    return new PagedResultDto<OrdersDto>(
        //        totalCount,
        //        entities.Select(MapToEntityDto).ToList()
        //    );
        //}
 
        protected override IQueryable<Orders> CreateFilteredQuery(PagedOrdersResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(input.Orders_Auto != 0, x => x.Id == input.Orders_Auto)
                .WhereIf(input.From != null, x => x.Cdt >= input.From)
                .WhereIf(input.To != null, x => x.Cdt <= input.To)
                .WhereIf(input.Status != null, x => x.OrderStatus == input.Status);
        }

        public async Task<object> GetOverAmtByClasen(PagedOverAmtResultReuestDto input)
        {
            decimal dRetval = 0, dOverAmt = 0;
            decimal dDefaultOverAmt = await GetDefaultOverAmtAsync(input.Clasen_Auto, input.MM);


            if (input.RentType == 2 && (input.OrderType == 1 || input.OrderType == 2 || input.OrderType == 5 || input.OrderType == 7))
            {
                if (dDefaultOverAmt > 0)
                {
                    if (input.MakNoType == 4)
                        dOverAmt = Math.Round(input.ListPrice * (dDefaultOverAmt - 6) / 100, 0);
                    else
                        dOverAmt = Math.Round(input.ListPrice * dDefaultOverAmt / 100, 0);

                    dRetval = UOverAmt(dOverAmt, input.ListPrice, input.MM, input.BsType, input.KM);
                }
                else
                    throw new Exception("此車型無殘值標準!");
            }

            var retval = new
            {
                overAmt = dRetval 
            };


            return retval;
        } 
        public async Task<object> GetRateKMAmtByClasen(PagedRateKmAmtResultRequest input)
        {
            decimal dDefaultRateKMAmt = await GetDefaultRentKMAmtAsync(input.Clasen_Auto, input.MM, input.KM);
            decimal dYear = Math.Ceiling(input.MM / (decimal)12);
            decimal dRetval = 0;
            decimal dRateKMAmt = Math.Round(dDefaultRateKMAmt / input.MM, 0);
            switch (dYear)
            {
                case 1:
                case 2:
                    dRetval = Math.Ceiling(dRateKMAmt * (decimal)0.9);
                    break;
                case 3:
                    dRetval = dRateKMAmt;
                    break;
                case 4:
                    dRetval = Math.Ceiling(dRateKMAmt * (decimal)1.1);
                    break;
                case 5:
                    dRetval = Math.Ceiling(dRateKMAmt * (decimal)1.2);
                    break;
                default:
                    dRetval = 0;
                    break;
            }

            var retval = new
            {

                RateKMAmt = dRetval
            };

            return retval;

        }
         
        async Task<decimal> GetDefaultOverAmtAsync(long lClasen_Auto, int iMM)
        {
            decimal dRetval = 0;

            var c = await _clasen.GetAsync(lClasen_Auto);

            switch (iMM)
            {
                case 6:
                    dRetval = c.OverP_0_5;
                    break;
                case 12:
                    dRetval = c.OverP_1;
                    break;
                case 24:
                    dRetval = c.OverP_2;
                    break;
                case 36:
                    dRetval = c.OverP_3;
                    break;
                case 48:
                    dRetval = c.OverP_4;
                    break;
                case 60:
                    dRetval = c.OverP_5;
                    break;
                case 72:
                    dRetval = c.OverP_6;
                    break;
                case 84:
                    dRetval = c.OverP_7;
                    break;
                case 90:
                    dRetval = c.OverP_8;
                    break;
                case int d when iMM / 12 == 0:
                    if (c.OverP_0_5 > 0 && c.OverP_1 > 0)
                    {
                        if (iMM > 6)
                        {
                            dRetval = (c.OverP_0_5 - (c.OverP_0_5 - c.OverP_1) / 6 * iMM % 6);
                        }
                        else
                        {
                            dRetval = c.OverP_0_5;
                        }
                    }
                    else
                        dRetval = 0;

                    break;
                case int d when iMM / 12 == 1:
                    dRetval = (c.OverP_1 > 0 && c.OverP_2 > 0) ? c.OverP_1 - ((c.OverP_1 - c.OverP_2) / 12 * iMM % 12) : 0;
                    break;
                case int d when iMM / 12 == 2:
                    dRetval = (c.OverP_2 > 0 && c.OverP_3 > 0) ? c.OverP_2 - ((c.OverP_2 - c.OverP_3) / 12 * iMM % 12) : 0;
                    break;
                case int d when iMM / 12 == 3:
                    dRetval = (c.OverP_3 > 0 && c.OverP_4 > 0) ? c.OverP_3 - ((c.OverP_3 - c.OverP_4) / 12 * iMM % 12) : 0;
                    break;
                case int d when iMM / 12 == 4:
                    dRetval = (c.OverP_4 > 0 && c.OverP_5 > 0) ? c.OverP_4 - ((c.OverP_4 - c.OverP_5) / 12 * iMM % 12) : 0;
                    break;
                case int d when iMM / 12 == 5:
                    dRetval = (c.OverP_5 > 0 && c.OverP_6 > 0) ? c.OverP_5 - ((c.OverP_5 - c.OverP_6) / 12 * iMM % 12) : 0;
                    break;
                case int d when iMM / 12 == 6:
                    dRetval = (c.OverP_6 > 0 && c.OverP_7 > 0) ? c.OverP_6 - ((c.OverP_6 - c.OverP_7) / 12 * iMM % 12) : 0;
                    break;
                case int d when iMM / 12 == 7:
                    dRetval = (c.OverP_7 > 0 && c.OverP_8 > 0) ? c.OverP_7 - ((c.OverP_7 - c.OverP_8) / 12 * iMM % 12) : 0;
                    break;
                default:

                    break;
            }
            return dRetval;
        }

        async Task<decimal> GetDefaultRentKMAmtAsync(long lClasen_Auto, int iMM, int iKM)
        {
            var c = await _clasen.GetAsync(lClasen_Auto);

            decimal dDefaultRentKmAmt;
            switch (iKM)
            {
                case 5000: dDefaultRentKmAmt = c.RateKM_0_5W; break;
                case 10000: dDefaultRentKmAmt = c.RateKM_1W; break;
                case 15000: dDefaultRentKmAmt = c.RateKM_1_5W; break;
                case 20000: dDefaultRentKmAmt = c.RateKM_2W; break;
                case 25000: dDefaultRentKmAmt = c.RateKM_2_5W; break;
                case 30000: dDefaultRentKmAmt = c.RateKM_3W; break;
                case 35000: dDefaultRentKmAmt = c.RateKM_3_5W; break;
                case 40000: dDefaultRentKmAmt = c.RateKM_4W; break;
                case 45000: dDefaultRentKmAmt = c.RateKM_4_5W; break;
                case 50000: dDefaultRentKmAmt = c.RateKM_5W; break;
                case 55000: dDefaultRentKmAmt = c.RateKM_5_5W; break;
                case 60000: dDefaultRentKmAmt = c.RateKM_6W; break;
                case 65000: dDefaultRentKmAmt = c.RateKM_6_5W; break;
                case 70000: dDefaultRentKmAmt = c.RateKM_7W; break;
                case 75000: dDefaultRentKmAmt = c.RateKM_7_5W; break;
                case 80000: dDefaultRentKmAmt = c.RateKM_8W; break;
                case 85000: dDefaultRentKmAmt = c.RateKM_8_5W; break;
                case 90000: dDefaultRentKmAmt = c.RateKM_9W; break;
                case 95000: dDefaultRentKmAmt = c.RateKM_9_5W; break;
                case 100000: dDefaultRentKmAmt = c.RateKM_10W; break;
                case 105000: dDefaultRentKmAmt = c.RateKM_10_5W; break;
                case 110000: dDefaultRentKmAmt = c.RateKM_11W; break;
                case 115000: dDefaultRentKmAmt = c.RateKM_11_5W; break;
                case 120000: dDefaultRentKmAmt = c.RateKM_12W; break;
                case 125000: dDefaultRentKmAmt = c.RateKM_12_5W; break;
                case 130000: dDefaultRentKmAmt = c.RateKM_13W; break;
                case 135000: dDefaultRentKmAmt = c.RateKM_13_5W; break;
                case 140000: dDefaultRentKmAmt = c.RateKM_14W; break;
                case 145000: dDefaultRentKmAmt = c.RateKM_14_5W; break;
                case 150000: dDefaultRentKmAmt = c.RateKM_15W; break;
                case 155000: dDefaultRentKmAmt = c.RateKM_15_5W; break;
                case 160000: dDefaultRentKmAmt = c.RateKM_16W; break;
                case 165000: dDefaultRentKmAmt = c.RateKM_16_5W; break;
                case 170000: dDefaultRentKmAmt = c.RateKM_17W; break;
                case 175000: dDefaultRentKmAmt = c.RateKM_17_5W; break;
                case 180000: dDefaultRentKmAmt = c.RateKM_18W; break;
                case 185000: dDefaultRentKmAmt = c.RateKM_18_5W; break;
                case 190000: dDefaultRentKmAmt = c.RateKM_19W; break;
                case 195000: dDefaultRentKmAmt = c.RateKM_19_5W; break;
                case 200000: dDefaultRentKmAmt = c.RateKM_20W; break;
                case 205000: dDefaultRentKmAmt = c.RateKM_20_5W; break;
                case 210000: dDefaultRentKmAmt = c.RateKM_21W; break;
                case 215000: dDefaultRentKmAmt = c.RateKM_21_5W; break;
                case 220000: dDefaultRentKmAmt = c.RateKM_22W; break;
                case 225000: dDefaultRentKmAmt = c.RateKM_22_5W; break;
                case 230000: dDefaultRentKmAmt = c.RateKM_23W; break;
                case 235000: dDefaultRentKmAmt = c.RateKM_23_5W; break;
                case 240000: dDefaultRentKmAmt = c.RateKM_24W; break;
                case 245000: dDefaultRentKmAmt = c.RateKM_24_5W; break;
                case 250000: dDefaultRentKmAmt = c.RateKM_25W; break;
                case 255000: dDefaultRentKmAmt = c.RateKM_25_5W; break;
                case 260000: dDefaultRentKmAmt = c.RateKM_26W; break;
                case 265000: dDefaultRentKmAmt = c.RateKM_26_5W; break;
                case 270000: dDefaultRentKmAmt = c.RateKM_27W; break;
                case 275000: dDefaultRentKmAmt = c.RateKM_27_5W; break;
                case 280000: dDefaultRentKmAmt = c.RateKM_28W; break;
                case 285000: dDefaultRentKmAmt = c.RateKM_28_5W; break;
                case 290000: dDefaultRentKmAmt = c.RateKM_29W; break;
                case 295000: dDefaultRentKmAmt = c.RateKM_29_5W; break;
                case 300000: dDefaultRentKmAmt = c.RateKM_30W; break;
                case 305000: dDefaultRentKmAmt = c.RateKM_30W; break;  
                default: dDefaultRentKmAmt = 0; break;
            }

            return dDefaultRentKmAmt;
        }

        decimal UOverAmt(decimal dOvetAmt, int iListPrice, int iMM, int iBsType, int iKM)
        {
            int pa = 0;

            decimal dYear = Math.Ceiling((decimal)iMM / 12);

            if (iBsType == 2)
                pa = 10;

            if (iKM / dYear >= 50000 && iKM < 200000)
                pa += 3;
            else if (iKM >= 200000)
                pa += 5;

            decimal dListPrice = Math.Round((decimal)iListPrice * pa / 100, 0);
            decimal dOverAmtTmp = dOvetAmt - dListPrice;

            return dOverAmtTmp;
        }

    }
}
