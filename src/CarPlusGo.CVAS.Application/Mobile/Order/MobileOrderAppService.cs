using Abp.Application.Services.Dto;
using CarPlusGo.CVAS.Mobile.MongoDB;
using CarPlusGo.CVAS.Mobile.Dto;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using Abp.Extensions;
using System.Linq;
using System.Collections.Generic;
using CarPlusGo.CVAS.Mobile.TShareBank.Enum;
using System;
using Abp.Authorization;
using CarPlusGo.CVAS.Authorization;

namespace CarPlusGo.CVAS.Mobile.Order
{
    [AbpAuthorize(PermissionNames.Pages_MobileOrder)]
    public class MobileOrderAppService : IMobileOrderAppService
    {
        private readonly IMongoDBRepository _mongoDBRepository;
        public MobileOrderAppService(IMongoDBRepository orderRepository)
        {
            _mongoDBRepository = orderRepository;
        }

        [AbpAuthorize(PermissionNames.Pages_MobileOrder_Find)]
        public async Task<PagedResultDto<MobileOrderDto>> GetAll(PagedMobileOrderResultRequestDto input)
        {

            var query = CreateFilteredQuery(input);

            query = ApplySorting(query, input);

            var entities = await query.ToListAsync();

            entities = await FilterPhoneNumber(entities, input.PhoneNum);

            entities = await FilterCarNumber(entities, input.CarNum);

            var totalCount = entities.Count;

            entities = ApplyPaging(entities, input).ToList();

            entities = await AddOtherData(entities);

            return new PagedResultDto<MobileOrderDto>(
                totalCount,
                entities.Select(MapToEntityDto).ToList()
            );
        }

        [AbpAuthorize(PermissionNames.Pages_MobileOrder_Export)]
        public async Task<PagedResultDto<MobileOrderDto>> ExportAll(PagedMobileOrderResultRequestDto input)
        {

            var query = CreateFilteredQuery(input);

            query = ApplySorting(query, input);

            var entities = await query.ToListAsync();

            entities = await FilterPhoneNumber(entities, input.PhoneNum);

            entities = await FilterCarNumber(entities, input.CarNum);

            var totalCount = entities.Count;

            entities = ApplyPaging(entities, input).ToList();

            entities = await AddOtherData(entities);

            return new PagedResultDto<MobileOrderDto>(
                totalCount,
                entities.Select(ExportMapToEntityDto).ToList()
            );
        }

        protected async Task<List<BsonDocument>> AddOtherData(List<BsonDocument> entities)
        {
            var builderFilter = Builders<BsonDocument>.Filter;
            var orderIds = new List<string>();
            var carIds = new List<ObjectId>();
            var userIds = new List<ObjectId>();
            var couponIds = new List<ObjectId>();
            var returnEntities = new List<BsonDocument>();
            entities.ForEach(x =>
            {
                BsonValue bson;
                orderIds.Add(x.GetValue("_id").AsObjectId.ToString());
                if (x.TryGetValue("carAuthid", out bson) && !bson.IsBsonNull && !bson.ToString().IsNullOrEmpty())
                {
                    carIds.Add(new ObjectId(bson.ToString()));
                }
                if (x.TryGetValue("passengerId", out bson) && !bson.IsBsonNull && !bson.ToString().IsNullOrEmpty())
                {
                    userIds.Add(new ObjectId(bson.ToString()));
                }
                if (x.TryGetValue("driverId", out bson) && !bson.IsBsonNull && !bson.ToString().IsNullOrEmpty())
                {
                    userIds.Add(new ObjectId(bson.ToString()));
                }
            });
            var invoices = await _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_invoice").Find(builderFilter.In("orderid", orderIds)).ToListAsync();
            var cars = await _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_car").Find(builderFilter.In("_id", carIds)).ToListAsync();
            var users = await _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_user").Find(builderFilter.In("_id", userIds)).ToListAsync();
            var pays = await _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_pay").Find(builderFilter.In("orderid", orderIds)).ToListAsync();
            pays.ForEach(p =>
            {
                BsonValue bson;
                if (p.TryGetValue("couponId", out bson) && !bson.IsBsonNull && !bson.ToString().IsNullOrEmpty())
                {
                    couponIds.Add(new ObjectId(bson.ToString()));
                }
            });
            var couponAccounts = await _mongoDBRepository.Database.GetCollection<BsonDocument>("coupon_account").Find(builderFilter.In("_id", couponIds)).ToListAsync();
            couponIds.Clear();
            couponAccounts.ForEach(c =>
            {
                BsonValue bson;
                if (c.TryGetValue("couponId", out bson) && !bson.IsBsonNull && !bson.ToString().IsNullOrEmpty())
                {
                    couponIds.Add(new ObjectId(bson.ToString()));
                }
            });
            var couponTemplates = await _mongoDBRepository.Database.GetCollection<BsonDocument>("coupon_templates").Find(builderFilter.In("_id", couponIds)).ToListAsync();
            entities.ForEach(x =>
            {
                BsonValue bson;
                var invoice = invoices.FirstOrDefault(i => i.TryGetValue("orderid", out bson) && bson.ToString() == x.GetValue("_id").AsObjectId.ToString());
                if (invoice != null)
                {
                    if (invoice.TryGetValue("state", out bson) && !bson.IsBsonNull)
                    {
                        x.Add(new BsonElement("invoiceStatus", bson.ToInt32()));
                    }
                }
                var car = cars.FirstOrDefault(c => x.TryGetValue("carAuthid", out bson) && c.GetValue("_id").AsObjectId.ToString() == bson.ToString());
                if (car != null)
                {
                    if (car.TryGetValue("carNumber", out bson) && !bson.IsBsonNull && !bson.ToString().IsNullOrEmpty())
                    {
                        x.Add(new BsonElement("carNum", bson.ToString()));
                    }
                }
                var passenger = users.FirstOrDefault(u => x.TryGetValue("passengerId", out bson) && u.GetValue("_id").AsObjectId.ToString() == bson.ToString());
                if (passenger != null)
                {
                    if (passenger.TryGetValue("userName", out bson) && !bson.IsBsonNull && !bson.ToString().IsNullOrEmpty())
                    {
                        x.Add(new BsonElement("passengerUserName", bson.ToString()));
                    }
                }
                var driver = users.FirstOrDefault(u => x.TryGetValue("driverId", out bson) && u.GetValue("_id").AsObjectId.ToString() == bson.ToString());
                if (driver != null)
                {
                    if (driver.TryGetValue("userName", out bson) && !bson.IsBsonNull && !bson.ToString().IsNullOrEmpty())
                    {
                        x.Add(new BsonElement("driverUserName", bson.ToString()));
                    }
                }
                var pay = pays.FirstOrDefault(p => p.TryGetValue("orderid", out bson) && bson.ToString() == x.GetValue("_id").AsObjectId.ToString());
                if (pay != null)
                {
                    var couponAccount = couponAccounts.FirstOrDefault(ca => pay.TryGetValue("couponId", out bson) && ca.GetValue("_id").AsObjectId.ToString() == bson.ToString());
                    if (couponAccount != null)
                    {
                        var couponTemplate = couponTemplates.FirstOrDefault(ct => couponAccount.TryGetValue("couponId", out bson) && ct.GetValue("_id").AsObjectId.ToString() == bson.ToString());
                        if (couponTemplate != null)
                        {
                            if (couponTemplate.TryGetValue("content", out bson) && !bson.IsBsonNull && !bson.ToString().IsNullOrEmpty())
                            {
                                x.Add(new BsonElement("giftType", bson.ToString()));
                            }
                        }
                    }

                    if (pay.TryGetValue("dynamicDiscount", out bson) && !bson.IsBsonNull)
                    {
                        x.Add(new BsonElement("dynamicDiscount", bson));
                    }
                }
                returnEntities.Add(x);
            });
            return returnEntities;
        }

        protected async Task<List<BsonDocument>> FilterPhoneNumber(List<BsonDocument> entities, string phoneNum)
        {
            if (!phoneNum.IsNullOrEmpty())
            {
                var builderFilter = Builders<BsonDocument>.Filter;
                var userBson = await _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_user").Find(builderFilter.Eq("userName", phoneNum.Trim())).FirstOrDefaultAsync();
                if (userBson != null)
                {
                    BsonValue bson;
                    var userId = userBson.GetValue("_id").AsObjectId.ToString();
                    var returnEntities = new List<BsonDocument>();
                    entities.ToList().ForEach(x =>
                    {
                        if (x.TryGetValue("passengerId", out bson) && x.GetValue("passengerId").ToString() == userId)
                        {
                            returnEntities.Add(x);
                        }
                    });
                    return returnEntities;
                }
            }
            return entities;
        }

        protected async Task<List<BsonDocument>> FilterCarNumber(List<BsonDocument> entities, string carNum)
        {
            if (!carNum.IsNullOrEmpty())
            {
                var builderFilter = Builders<BsonDocument>.Filter;
                var carBson = await _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_car").Find(builderFilter.And(builderFilter.Eq("carNumber", carNum.Trim()), builderFilter.Eq("delFlag", "0"))).FirstOrDefaultAsync();
                if (carBson != null)
                {
                    BsonValue bson;
                    var carId = carBson.GetValue("_id").AsObjectId.ToString();
                    var returnEntities = new List<BsonDocument>();
                    entities.ToList().ForEach(x =>
                    {
                        if (x.TryGetValue("carAuthid", out bson) && x.GetValue("carAuthid").ToString() == carId)
                        {
                            returnEntities.Add(x);
                        }
                    });
                    return returnEntities;
                }
            }
            return entities;
        }

        protected IAggregateFluent<BsonDocument> CreateFilteredQuery(PagedMobileOrderResultRequestDto input)
        {
            var builderFilter = Builders<BsonDocument>.Filter;
            var filterDefinitions = new List<FilterDefinition<BsonDocument>>();
            if (input.StartTimeFrom.HasValue)
            {
                filterDefinitions.Add(builderFilter.Gte("updateTimeList.status1", input.StartTimeFrom.Value.ToLocalTime().Date));
            }
            if (input.StartTimeTo.HasValue)
            {
                filterDefinitions.Add(builderFilter.Lte("updateTimeList.status1", input.StartTimeTo.Value.ToLocalTime().ToDayEnd()));
            }
            if (input.EndTimeFrom.HasValue)
            {
                filterDefinitions.Add(builderFilter.Gte("updateTimeList.status20", input.EndTimeFrom.Value.ToLocalTime().Date));
            }
            if (input.EndTimeTo.HasValue)
            {
                filterDefinitions.Add(builderFilter.Lte("updateTimeList.status20", input.EndTimeTo.Value.ToLocalTime().ToDayEnd()));
            }
            AddFilterDefinition(filterDefinitions, !input.OrderNum.IsNullOrEmpty(), builderFilter.Eq("orderNum", input.OrderNum));
            AddFilterDefinition(filterDefinitions, input.OrderStatus.HasValue && input.OrderStatus != OrderStatus.All, builderFilter.Eq("orderStatus", input.OrderStatus));

            var query = _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_order").Aggregate();
            if (filterDefinitions.Count > 0)
            {
                query = query.Match(builderFilter.And(filterDefinitions));
            }
            return query;
        }

        protected void AddFilterDefinition(List<FilterDefinition<BsonDocument>> filterDefinitions, bool condition, FilterDefinition<BsonDocument> filterDefinition)
        {
            if (condition)
            {
                filterDefinitions.Add(filterDefinition);
            }
        }

        protected IAggregateFluent<BsonDocument> ApplySorting(IAggregateFluent<BsonDocument> query, PagedMobileOrderResultRequestDto input)
        {
            //Try to sort query if available
            var sortInput = input as ISortedResultRequest;
            if (sortInput != null)
            {
                if (!sortInput.Sorting.IsNullOrWhiteSpace())
                {
                    return query.Sort(sortInput.Sorting);
                }
            }

            //IQueryable.Task requires sorting, so we should sort if Take will be used.
            if (input is ILimitedResultRequest)
            {
                return query.Sort(new BsonDocument { { "_id", -1 } });
            }

            //No sorting
            return query;
        }

        protected IEnumerable<BsonDocument> ApplyPaging(IEnumerable<BsonDocument> entities, PagedMobileOrderResultRequestDto input)
        {
            //Try to use paging if available
            var pagedInput = input as IPagedResultRequest;
            if (pagedInput != null)
            {
                return entities.Skip(pagedInput.SkipCount).Take(pagedInput.MaxResultCount);
            }

            //Try to limit query result if available
            var limitedInput = input as ILimitedResultRequest;
            if (limitedInput != null)
            {
                return entities.Take(limitedInput.MaxResultCount);
            }

            //No paging
            return entities;
        }

        protected MobileOrderDto MapToEntityDto(BsonDocument entity)
        {
            var mobileOrder = ExportMapToEntityDto(entity);
            mobileOrder.DriverUserName = PhoneNumHandle(mobileOrder.DriverUserName);
            mobileOrder.PassengerUserName = PhoneNumHandle(mobileOrder.PassengerUserName);
            return mobileOrder;
        }

        protected MobileOrderDto ExportMapToEntityDto(BsonDocument entity)
        {
            BsonValue bson;
            BsonValue bson2;
            var updateTime = entity.TryGetValue("updateTimeList", out bson) ? bson.AsBsonDocument : new BsonDocument();
            var orderType = entity.TryGetValue("orderType", out bson) ? (OrderType)bson.ToInt32() : OrderType.Unknow;
            return new MobileOrderDto
            {
                OrderNum = entity.TryGetValue("orderNum", out bson) ? bson.ToString() : "",
                OrderType = entity.TryGetValue("orderType", out bson) && entity.TryGetValue("orderType2", out bson2) ? SwitchOrderType((OrderType)bson.ToInt32(), (BookingOrderType)bson2.ToInt32()) : "",
                PlaceOrderTime = updateTime.TryGetValue("status1", out bson) ? bson.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss") : "",
                StartTime = updateTime.TryGetValue("status1", out bson) ? bson.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss") : "",
                EndTime = updateTime.TryGetValue("status20", out bson) ? bson.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss") : "",
                StartPoint = entity.TryGetValue("startPoinit", out bson) ? bson.ToString() : "",
                StartPoinitLocation = entity.TryGetValue("startPoinitLocation", out bson) ? bson.AsBsonArray.ToString() : "",
                EndPoint = entity.TryGetValue("endPoint", out bson) ? bson.ToString() : "",
                EndPointLocation = entity.TryGetValue("endPointLocation", out bson) ? bson.AsBsonArray.ToString() : "",
                StartAddressReal = entity.TryGetValue("startAddressReal", out bson) ? bson.ToString() : "",
                StartPoinitLocationReal = entity.TryGetValue("startPoinitLocationReal", out bson) ? bson.AsBsonArray.ToString() : "",
                TravelEndAddress = entity.TryGetValue("travelEndAddress", out bson) ? bson.ToString() : "",
                EndPointLocationReal = entity.TryGetValue("endPointLocationReal", out bson) ? bson.AsBsonArray.ToString() : "",
                OrderStatus = entity.TryGetValue("orderStatus", out bson) ? SwitchOrderStatus((OrderStatus)bson.ToInt32()) : "",
                InvoiceStatus = entity.TryGetValue("invoiceStatus", out bson) ? SwitchInvoiceStatus((InvoiceStatus)bson.ToInt32()) : "",
                CarNum = entity.TryGetValue("carNum", out bson) ? bson.ToString() : "",
                DriverUserName = entity.TryGetValue("driverUserName", out bson) ? bson.ToString() : "",
                PassengerUserName = entity.TryGetValue("passengerUserName", out bson) ? bson.ToString() : "",
                GiftType = entity.TryGetValue("giftType", out bson) ? bson.ToString() : "",
                DriverEvaluation = entity.TryGetValue("driverEvaluation", out bson) ? bson.ToInt32() : 0,
                CarEvaluation = entity.TryGetValue("carEvaluation", out bson) ? bson.ToInt32() : 0,
                CalTime = entity.TryGetValue("calTime", out bson) ? bson.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss") : "",
                CalReasonNote = entity.TryGetValue("calReasonNote", out bson) ? (!bson.IsBsonNull ? bson.ToString() : "") : "",
                OrderAmount = entity.TryGetValue("orderAmount", out bson) ? bson.ToDecimal() : 0.00m,
                DynamicDiscountAmount = entity.TryGetValue("dynamicDiscount", out bson) ? bson.ToDecimal() : 0.00m,
                DiscountedAmount = entity.TryGetValue("orderAmount", out bson) ? (entity.TryGetValue("dynamicDiscount", out bson2) ? bson.ToDecimal() - bson2.ToDecimal() : bson.ToDecimal()) : 0.00m,
                PayAmount = entity.TryGetValue("payAmount", out bson) ? bson.ToDecimal() : 0.00m,
                PayTime = updateTime.TryGetValue("status35", out bson) ? bson.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss") : "",
                AmountPayment = entity.TryGetValue("amountPayment", out bson) ? bson.ToDecimal() : 0.00m,
                AppointmentTime = entity.TryGetValue("appointmentTime", out bson) ? bson.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss") : "",
                DriverAcceptOrderTime = updateTime.TryGetValue("status5", out bson) ? bson.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss") : "",
                DriverStartOffTime = updateTime.TryGetValue("status6", out bson) ? bson.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss") : "",
                DriverArrivedTime = updateTime.TryGetValue("status10", out bson) ? bson.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss") : "",
                PassengerGetOnTime = updateTime.TryGetValue("status15", out bson) ? bson.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss") : "",
                ExpMileage = entity.TryGetValue("expMileage", out bson) ? Math.Round(bson.ToInt64() * 1.0 / 1000, 2) : 0.00,
                ExpTime = entity.TryGetValue("expTime", out bson) ? Math.Round(bson.ToInt64() * 1.0 / 60, 2) : 0.00,
                ExpAmount = entity.TryGetValue("expAmount", out bson) ? bson.ToDecimal() : 0.00m,
                TotalDistance = entity.TryGetValue("totalDistance", out bson) ? Math.Round(bson.ToInt64() * 1.0 / 1000, 2) : 0.00,
                TotalTime = updateTime.TryGetValue("status15", out bson) && updateTime.TryGetValue("status20", out bson2) ? Math.Round((bson2.ToLocalTime() - bson.ToLocalTime()).TotalMinutes, 2) : 0,
                RealAcceptedTime = updateTime.TryGetValue("status5", out bson) && updateTime.TryGetValue("status15", out bson2) && orderType == OrderType.Immediate ? Math.Round((bson2.ToLocalTime() - bson.ToLocalTime()).TotalMinutes, 2) : updateTime.TryGetValue("status6", out bson) && updateTime.TryGetValue("status15", out bson2) && orderType == OrderType.Booking ? Math.Round((bson2.ToLocalTime() - bson.ToLocalTime()).TotalMinutes, 2) : 0,
                EndPointDistance = entity.TryGetValue("endPointLocation", out bson) && entity.TryGetValue("endPointLocationReal", out bson2) ? Math.Round(GetDistance(bson.AsBsonArray[1].ToDouble(), bson.AsBsonArray[0].ToDouble(), bson2.AsBsonArray[1].ToDouble(), bson2.AsBsonArray[0].ToDouble()) / 1000, 2) : 0.00,
            };
        }

        private string PhoneNumHandle(string phoneNum)
        {
            return phoneNum.Length > 7 ? phoneNum.Substring(0, 3) + "****" + phoneNum.Substring(7) : "";
        }

        private string SwitchInvoiceStatus(InvoiceStatus invoiceStatus)
        {
            switch (invoiceStatus)
            {
                case InvoiceStatus.Never:
                    return "未开票";
                case InvoiceStatus.Auditing:
                    return "审核中";
                case InvoiceStatus.Invoicing:
                    return "开票中";
                case InvoiceStatus.Invoiced:
                    return "已开票";
                case InvoiceStatus.Failure:
                    return "开票失败";
                default:
                    return "";
            }
        }

        private string SwitchOrderStatus(OrderStatus orderStatus)
        {
            switch (orderStatus)
            {
                case OrderStatus.PlaceOrder:
                    return "乘客下单";
                case OrderStatus.DriverAcceptOrder:
                    return "司机接到订单";
                case OrderStatus.DriverStartOff:
                    return "司机去接乘客";
                case OrderStatus.DriverArrived:
                    return "司机已到达";
                case OrderStatus.StartStroke:
                    return "开始行程";
                case OrderStatus.EndStroke:
                    return "行程结束";
                case OrderStatus.NotEvaluated:
                    return "行程结束未评价";
                case OrderStatus.CancelOrder:
                    return "取消订单";
                case OrderStatus.CancelOrderNeverPay:
                    return "取消订单未支付";
                case OrderStatus.Paid:
                    return "乘客已支付";
                case OrderStatus.AppointmentFailure:
                    return "预约失败";
                case OrderStatus.DriverNeverAccepted:
                    return "立即单派单失败";
                default:
                case OrderStatus.All:
                    return "";
            }
        }

        private string SwitchOrderType(OrderType orderType, BookingOrderType bookingOrderType)
        {
            switch (orderType)
            {
                case OrderType.Immediate:
                    return "即时单";
                case OrderType.Booking:
                    switch (bookingOrderType)
                    {
                        case BookingOrderType.Normal:
                            return "预约单";
                        case BookingOrderType.Pickup:
                            return "接机单";
                        case BookingOrderType.Dropoff:
                            return "送机单";
                        default:
                            return "";
                    }
                default:
                    return "";
            }
        }

        //地球半径，单位米
        private const double EARTH_RADIUS = 6378137;
        /// <summary>
        /// 计算两点位置的距离，返回两点的距离，单位 米
        /// 该公式为GOOGLE提供，误差小于0.2米
        /// </summary>
        public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = Rad(lat1);
            double radLng1 = Rad(lng1);
            double radLat2 = Rad(lat2);
            double radLng2 = Rad(lng2);
            double a = radLat1 - radLat2;
            double b = radLng1 - radLng2;
            double result = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2))) * EARTH_RADIUS;
            return result;
        }

        /// <summary>
        /// 经纬度转化成弧度
        /// </summary>
        private static double Rad(double d)
        {
            return (double)d * Math.PI / 180d;
        }
    }
}
