using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Mobile.Analysis.Dto;
using System.Threading.Tasks;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.ObjectMapping;
using System;
using CarPlusGo.CVAS.Mobile.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using CarPlusGo.CVAS.Mobile.TShareBank;
using System.Linq;
using CarPlusGo.CVAS.Mobile.TShareBank.Enum;
using System.Collections.Generic;
using CarPlusGo.CVAS.Mobile.BiStat;
using CarPlusGo.CVAS.Mobile.Dto;

namespace CarPlusGo.CVAS.Mobile.Analysis
{
    public class AnalysisAppService
        : IAnalysisAppService
    {
        private readonly IMongoDBRepository _mongoDBRepository;
        private readonly IRepository<TShareBank.TargetConfig, long> _targetConfigRepository;
        private readonly IRepository<UmengApiData, long> _umengApiDataRepository;
        private readonly IRepository<DayDriverTime, long> _dayDriverTimeRepository;
        public IObjectMapper ObjectMapper { get; set; }
        public AnalysisAppService(IMongoDBRepository orderRepository, IRepository<TShareBank.TargetConfig, long> targetConfigRepository, IRepository<UmengApiData, long> umengApiDataRepository, IRepository<DayDriverTime, long> dayDriverTimeRepository)
        {
            _mongoDBRepository = orderRepository;
            _targetConfigRepository = targetConfigRepository;
            _umengApiDataRepository = umengApiDataRepository;
            _dayDriverTimeRepository = dayDriverTimeRepository;
            ObjectMapper = NullObjectMapper.Instance;
        }

        #region 主页
        public async Task<HomeDataDto> HomeData(HomeDataResultRequestDto input)
        {
            ConvertTime(input);
            var builderFilter = Builders<BsonDocument>.Filter;
            //订单
            var orderGroup = new BsonDocument
            {
                { "_id", 1 },
                { "OrderAmount", new BsonDocument("$sum", "$orderAmount") },
                { "OrderCount", new BsonDocument("$sum", 1) }
            };
            var orderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status20", input.From.Value, input.To.Value);
            var orderBson = await orderAggregateFluent.Group(orderGroup).FirstOrDefaultAsync();
            //注册用户
            string[] rolesArr = { "mobile" };
            var userGroup = new BsonDocument
            {
                { "_id", 1 },
                { "RegisterUserNum", new BsonDocument("$sum", 1) }
            };
            var userAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_user").Aggregate()
                .Match(builderFilter.And(builderFilter.Gte("createTime", input.From), builderFilter.Lte("createTime", input.To), builderFilter.All("roles", rolesArr)));
            var userBson = await userAggregateFluent.Group(userGroup).FirstOrDefaultAsync();
            //司机上线
            var driverQuery = _dayDriverTimeRepository.GetAll().WhereIf(input.From.HasValue && input.To.HasValue, x => Convert.ToDateTime(x.Date) >= input.From.Value && Convert.ToDateTime(x.Date) <= input.To.Value && x.OnlineTimes > 0);
            var onlineDriverNum = driverQuery.Count();
            //司机日均
            var daliyOnlineDriverNum = onlineDriverNum > 0 ? driverQuery.GroupBy(x => x.Date).Select(x => x.Count()).Average() : 0;
            //活跃用户
            var umengApiDataQuery = _umengApiDataRepository.GetAll().WhereIf(input.From.HasValue && input.To.HasValue, x => x.Date >= input.From && x.Date <= input.To);
            var activeUser = umengApiDataQuery.Count() > 0 ? umengApiDataQuery.Average(x => x.UniqActiveUsers) : 0;
            var actual = new TargetConfigDto
            {
                OrderAmount = orderBson != null ? orderBson.GetValue("OrderAmount").ToDecimal() : 0.00m,
                OrderCount = orderBson != null ? orderBson.GetValue("OrderCount").ToInt64() : 0,
                OnlineDriverNum = Convert.ToInt64(daliyOnlineDriverNum),
                DriverDailyOrderNum = orderBson != null && onlineDriverNum > 0 ? Decimal.Round(orderBson.GetValue("OrderCount").ToInt64() * 1.00m / onlineDriverNum, 2) : 0.00m,
                RegisterUserNum = userBson != null ? userBson.GetValue("RegisterUserNum").ToInt64() : 0,
                DailyActivityNum = Convert.ToInt64(activeUser),
            };
            var targetConfig = await _targetConfigRepository.FirstOrDefaultAsync(x => input.From.Value >= x.From && input.To.Value <= x.To && x.Unit == input.Unit);
            return new HomeDataDto
            {
                Actual = actual,
                Target = ObjectMapper.Map<TargetConfigDto>(targetConfig)
            };
        }

        protected void ConvertTime(HomeDataResultRequestDto input)
        {
            if (!input.Time.IsNullOrEmpty())
            {
                switch (input.Unit)
                {
                    case Unit.Month:
                        string[] timeString = input.Time.Split('-');
                        input.From = new DateTime(Convert.ToInt32(timeString[0]), Convert.ToInt32(timeString[1]), 1, 0, 0, 0, DateTimeKind.Local);
                        input.To = new DateTime(input.From.Value.Year, input.From.Value.Month, input.From.Value.TotalDaysInMonth(), 0, 0, 0, DateTimeKind.Local).ToDayEnd();
                        break;
                    case Unit.Day:
                        input.From = Convert.ToDateTime(input.Time).ToLocalTime().Date;
                        input.To = input.From.Value.ToLocalTime().ToDayEnd();
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region 业绩概况
        public async Task<PerformanceOverviewDto> PerformanceOverview(PerformanceOverviewResultRequestDto input)
        {
            var diffDays = (input.From - input.To).Days - 1;
            return new PerformanceOverviewDto
            {
                BookingOrder = await GetOrderData(input.From, input.To, OrderType.Booking),
                ImmediateOrder = await GetOrderData(input.From, input.To, OrderType.Immediate),
                LastPeriodBookingOrder = await GetOrderData(input.From.AddDays(diffDays), input.To.AddDays(diffDays), OrderType.Booking),
                LastPeriodImmediateOrder = await GetOrderData(input.From.AddDays(diffDays), input.To.AddDays(diffDays), OrderType.Immediate),
                LastMonthSamePeriodBookingOrder = await GetOrderData(input.From.AddMonths(-1), input.To.AddMonths(-1), OrderType.Booking),
                LastMonthSamePeriodImmediateOrder = await GetOrderData(input.From.AddMonths(-1), input.To.AddMonths(-1), OrderType.Immediate),
                PickupOrder = await GetOrderData(input.From, input.To, null, BookingOrderType.Pickup),
                DropoffOrder = await GetOrderData(input.From, input.To, null, BookingOrderType.Dropoff),
                LastPeriodPickupOrder = await GetOrderData(input.From.AddDays(diffDays), input.To.AddDays(diffDays), null, BookingOrderType.Pickup),
                LastPeriodDropoffOrder = await GetOrderData(input.From.AddDays(diffDays), input.To.AddDays(diffDays), null, BookingOrderType.Dropoff),
                LastMonthSamePeriodPickupOrder = await GetOrderData(input.From.AddMonths(-1), input.To.AddMonths(-1), null, BookingOrderType.Pickup),
                LastMonthSamePeriodDropoffOrder = await GetOrderData(input.From.AddMonths(-1), input.To.AddMonths(-1), null, BookingOrderType.Dropoff),
            };
        }

        protected async Task<PerformanceDetailDto> GetOrderData(DateTime from, DateTime to, OrderType? orderType = null, BookingOrderType? bookingOrderType = null)
        {
            var orderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status20", from.ToLocalTime().Date, to.ToLocalTime().ToDayEnd(), orderType, bookingOrderType);
            var orderGroup = new BsonDocument
            {
                { "_id", 1 },
                { "OrderAmount", new BsonDocument("$sum", "$orderAmount") },
                { "PayAmount", new BsonDocument("$sum", "$amountPayment") },
                { "OrderCount", new BsonDocument("$sum", 1) }
            };
            var orderBson = await orderAggregateFluent.Group(orderGroup).FirstOrDefaultAsync();
            return new PerformanceDetailDto
            {
                OrderAmount = orderBson != null ? orderBson.GetValue("OrderAmount").ToDecimal() : 0.00m,
                OrderCount = orderBson != null ? orderBson.GetValue("OrderCount").ToInt32() : 0,
                PayAmount = orderBson != null ? orderBson.GetValue("PayAmount").ToDecimal() : 0.00m
            };
        }

        protected IAggregateFluent<BsonDocument> GetOrderAggregateFluent(string timeKey, DateTime beginTime, DateTime endTime, OrderType? orderType = null, BookingOrderType? bookingOrderType = null, params FilterDefinition<BsonDocument>[] otherFilterDefinitions)
        {
            var builderFilter = Builders<BsonDocument>.Filter;
            List<FilterDefinition<BsonDocument>> filterDefinitions = new List<FilterDefinition<BsonDocument>>();
            filterDefinitions.Add(builderFilter.Gte(timeKey, beginTime));
            filterDefinitions.Add(builderFilter.Lte(timeKey, endTime));
            if (orderType != null)
            {
                filterDefinitions.Add(builderFilter.Eq("orderType", orderType));
                if (orderType == OrderType.Booking)
                {
                    filterDefinitions.Add(builderFilter.Eq("orderType2", BookingOrderType.Normal));
                }
            }
            if (bookingOrderType != null)
            {
                filterDefinitions.Add(builderFilter.Eq("orderType2", bookingOrderType));
            }
            if (otherFilterDefinitions.Length > 0)
            {
                filterDefinitions.AddRange(otherFilterDefinitions);
            }
            return _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_order").Aggregate()
                .Match(builderFilter.And(filterDefinitions));
        }
        #endregion

        #region 业绩趋势
        public async Task<PerformanceTrendDto> PerformanceTrend(PerformanceTrendResultRequestDto input)
        {
            DateTime beginTime = DateTime.Now.AddDays(-7).Date;
            DateTime endTime = DateTime.Now.AddDays(-1).ToDayEnd();
            switch (input.Unit)
            {
                case Unit.Month:
                    beginTime = new DateTime(DateTime.Now.AddMonths(-6).Year, DateTime.Now.AddMonths(-6).Month, 1, 0, 0, 0, DateTimeKind.Local).Date;
                    endTime = new DateTime(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, DateTime.Now.AddMonths(-1).TotalDaysInMonth(), 0, 0, 0, DateTimeKind.Local).ToDayEnd();
                    return new PerformanceTrendDto
                    {
                        TotalOrder = await GetPerformanceTrends("%Y-%m", beginTime, endTime),
                        BookingOrder = await GetPerformanceTrends("%Y-%m", beginTime, endTime, OrderType.Booking),
                        ImmediateOrder = await GetPerformanceTrends("%Y-%m", beginTime, endTime, OrderType.Immediate),
                        PickupOrder = await GetPerformanceTrends("%Y-%m", beginTime, endTime, null, BookingOrderType.Pickup),
                        DropoffOrder = await GetPerformanceTrends("%Y-%m", beginTime, endTime, null, BookingOrderType.Dropoff),
                    };
                case Unit.Week:
                    beginTime = DateTime.Now.StartOfWeek(DayOfWeek.Friday).AddDays(-49);
                    endTime = DateTime.Now.StartOfWeek(DayOfWeek.Friday).AddDays(-1).ToDayEnd();
                    return new PerformanceTrendDto
                    {
                        TotalOrder = await GetWeekPerformanceTrends(beginTime, endTime),
                        BookingOrder = await GetWeekPerformanceTrends(beginTime, endTime, OrderType.Booking),
                        ImmediateOrder = await GetWeekPerformanceTrends(beginTime, endTime, OrderType.Immediate),
                        PickupOrder = await GetWeekPerformanceTrends(beginTime, endTime, null, BookingOrderType.Pickup),
                        DropoffOrder = await GetWeekPerformanceTrends(beginTime, endTime, null, BookingOrderType.Dropoff),
                    };
                case Unit.Day:
                default:
                    return new PerformanceTrendDto
                    {
                        TotalOrder = await GetPerformanceTrends("%Y-%m-%d", beginTime, endTime),
                        BookingOrder = await GetPerformanceTrends("%Y-%m-%d", beginTime, endTime, OrderType.Booking),
                        ImmediateOrder = await GetPerformanceTrends("%Y-%m-%d", beginTime, endTime, OrderType.Immediate),
                        PickupOrder = await GetPerformanceTrends("%Y-%m-%d", beginTime, endTime, null, BookingOrderType.Pickup),
                        DropoffOrder = await GetPerformanceTrends("%Y-%m-%d", beginTime, endTime, null, BookingOrderType.Dropoff),
                    };
            }
        }

        protected async Task<IOrderedEnumerable<PerformanceDetailDto>> GetWeekPerformanceTrends(DateTime beginTime, DateTime endTime, OrderType? orderType = null, BookingOrderType? bookingOrderType = null)
        {
            var returnPerformanceTrends = new List<PerformanceDetailDto>();
            var performanceTrends = await GetPerformanceTrends("%Y-%m-%d", beginTime, endTime, orderType, bookingOrderType);
            for (int i = 0; i < performanceTrends.Count();)
            {
                var weekBegin = Convert.ToDateTime(performanceTrends.ElementAtOrDefault(i).Key).StartOfWeek(DayOfWeek.Friday);
                var weekList = performanceTrends.Where(x => Convert.ToDateTime(x.Key) >= weekBegin && Convert.ToDateTime(x.Key) < weekBegin.AddDays(7));
                returnPerformanceTrends.Add(new PerformanceDetailDto
                {
                    Year = weekBegin.AddDays(6).Year,
                    Key = string.Format(@"{0:D2}W{1}", weekBegin.AddDays(6).Month, weekBegin.AddDays(6).WeekDayInstanceOfMonth()),
                    OrderAmount = weekList.Sum(x => x.OrderAmount),
                    OrderCount = weekList.Sum(x => x.OrderCount),
                    PayAmount = weekList.Sum(x => x.PayAmount)
                }); ;
                i += weekList.Count();
            }
            return returnPerformanceTrends.OrderBy(x=>x.Year).ThenBy(x=>x.Key);
        }

        protected async Task<IOrderedEnumerable<PerformanceDetailDto>> GetPerformanceTrends(string dateFormat, DateTime beginTime, DateTime endTime, OrderType? orderType = null, BookingOrderType? bookingOrderType = null)
        {
            var performanceTrends = new List<PerformanceDetailDto>();
            var orderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status20", beginTime, endTime, orderType, bookingOrderType);
            var orderGroup = new BsonDocument
            {
                { "_id", new BsonDocument("$dateToString",new BsonDocument{
                    { "format",dateFormat },{ "date", "$updateTimeList.status20" },{ "timezone", "+08:00" }
                })},
                { "OrderAmount", new BsonDocument("$sum", "$orderAmount") },
                { "PayAmount", new BsonDocument("$sum", "$amountPayment") },
                { "OrderCount", new BsonDocument("$sum", 1) }
            };
            var orderBson = await orderAggregateFluent.Group(orderGroup).ToListAsync();
            orderBson.ForEach(x =>
            {
                var performanceTrend = new PerformanceDetailDto();
                performanceTrend.Key = x.GetValue("_id").ToString();
                performanceTrend.OrderAmount = x.GetValue("OrderAmount").ToDecimal();
                performanceTrend.PayAmount = x.GetValue("PayAmount").ToDecimal();
                performanceTrend.OrderCount = x.GetValue("OrderCount").ToInt32();
                performanceTrends.Add(performanceTrend);
            });
            return performanceTrends.OrderBy(x => x.Key);
        }
        #endregion

        #region 订单完成概况
        public async Task<OrderOverviewDto> OrderOverview(OrderOverviewResultRequestDto input)
        {

            return new OrderOverviewDto
            {
                TotalOrder = await GetOrderOverviewDetail(input),
                BookingOrder = await GetOrderOverviewDetail(input, OrderType.Booking),
                ImmediateOrder = await GetOrderOverviewDetail(input, OrderType.Immediate),
                PickupOrder = await GetOrderOverviewDetail(input, null, BookingOrderType.Pickup),
                DropoffOrder = await GetOrderOverviewDetail(input, null, BookingOrderType.Dropoff),
            };
        }

        protected async Task<OrderDetailDto> GetOrderOverviewDetail(OrderOverviewResultRequestDto input, OrderType? orderType = null, BookingOrderType? bookingOrderType = null)
        {
            var builderFilter = Builders<BsonDocument>.Filter;
            var systemList = new List<int>() { 40, 45 };
            var placeOrderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status1", input.From.ToLocalTime().Date, input.To.ToLocalTime().ToDayEnd(), orderType, bookingOrderType);
            var completedOrderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status1", input.From.ToLocalTime().Date, input.To.ToLocalTime().ToDayEnd(), orderType, bookingOrderType, builderFilter.Exists("updateTimeList.status20", true));
            var acceptOrderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status1", input.From.ToLocalTime().Date, input.To.ToLocalTime().ToDayEnd(), orderType, bookingOrderType, builderFilter.Exists("updateTimeList.status5", true));
            var systemCancelOrderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status1", input.From.ToLocalTime().Date, input.To.ToLocalTime().ToDayEnd(), orderType, bookingOrderType, builderFilter.In("orderStatus", systemList));
            var unacceptedPassengerCancelOrderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status1", input.From.ToLocalTime().Date, input.To.ToLocalTime().ToDayEnd(), orderType, bookingOrderType, builderFilter.And(builderFilter.Eq("orderStatus", 30), builderFilter.Exists("updateTimeList.status5", false)));
            var acceptedPassengerCancelOrderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status1", input.From.ToLocalTime().Date, input.To.ToLocalTime().ToDayEnd(), orderType, bookingOrderType, builderFilter.And(builderFilter.Eq("orderStatus", 30), builderFilter.Exists("updateTimeList.status5", true)));

            CheckUnique(input.IsUnique, ref placeOrderAggregateFluent, ref completedOrderAggregateFluent, ref acceptOrderAggregateFluent, ref systemCancelOrderAggregateFluent, ref unacceptedPassengerCancelOrderAggregateFluent, ref acceptedPassengerCancelOrderAggregateFluent);

            var orderGroup = new BsonDocument
            {
                { "_id", 1 },
                { "OrderCount", new BsonDocument("$sum", 1) }
            };
            var placeOrderBson = await placeOrderAggregateFluent.Group(orderGroup).FirstOrDefaultAsync();
            var completedOrderBson = await completedOrderAggregateFluent.Group(orderGroup).FirstOrDefaultAsync();
            var systemCancelOrderBson = await systemCancelOrderAggregateFluent.Group(orderGroup).FirstOrDefaultAsync();
            var unacceptedPassengerCancelOrderBson = await unacceptedPassengerCancelOrderAggregateFluent.Group(orderGroup).FirstOrDefaultAsync();
            var acceptedPassengerCancelOrderBson = await acceptedPassengerCancelOrderAggregateFluent.Group(orderGroup).FirstOrDefaultAsync();

            return new OrderDetailDto
            {
                OrderCount = placeOrderBson != null ? placeOrderBson.GetValue("OrderCount").ToInt32() : 0,
                CompletedOrderCount = completedOrderBson != null ? completedOrderBson.GetValue("OrderCount").ToInt32() : 0,
                SystemCancelOrderCount = systemCancelOrderBson != null ? systemCancelOrderBson.GetValue("OrderCount").ToInt32() : 0,
                UnacceptedPassengerCancelOrderCount = unacceptedPassengerCancelOrderBson != null ? unacceptedPassengerCancelOrderBson.GetValue("OrderCount").ToInt32() : 0,
                AcceptedPassengerCancelOrderCount = acceptedPassengerCancelOrderBson != null ? acceptedPassengerCancelOrderBson.GetValue("OrderCount").ToInt32() : 0,
            };
        }

        private static void CheckUnique(bool isUnique, ref IAggregateFluent<BsonDocument> placeOrderAggregateFluent, ref IAggregateFluent<BsonDocument> completedOrderAggregateFluent, ref IAggregateFluent<BsonDocument> acceptOrderAggregateFluent, ref IAggregateFluent<BsonDocument> systemCancelOrderAggregateFluent, ref IAggregateFluent<BsonDocument> unacceptedPassengerCancelOrderAggregateFluent, ref IAggregateFluent<BsonDocument> acceptedPassengerCancelOrderAggregateFluent)
        {
            if (isUnique)
            {
                var bsonList = new List<BsonValue>();
                bsonList.Add(BsonValue.Create(new BsonDocument
                {
                    { "$dateToString",new BsonDocument{
                        { "format", "%Y-%m-%dT%H:%M" },{ "date", "$updateTimeList.status1" },{"timezone","+08:00"}
                    }}
                }));
                bsonList.Add(0);
                bsonList.Add(15);
                var orderUniqueGroup = new BsonDocument
                {
                    { "_id", new BsonDocument{
                        { "passengerId","$passengerId"},
                        { "time", new BsonDocument {{"$substr", new BsonArray(bsonList) }}}
                    }},
                    {"updateTimeList",new BsonDocument("$last","$updateTimeList") }
                };
                placeOrderAggregateFluent = placeOrderAggregateFluent.Group(orderUniqueGroup);
                completedOrderAggregateFluent = completedOrderAggregateFluent.Group(orderUniqueGroup);
                acceptOrderAggregateFluent = acceptOrderAggregateFluent.Group(orderUniqueGroup);
                systemCancelOrderAggregateFluent = systemCancelOrderAggregateFluent.Group(orderUniqueGroup);
                unacceptedPassengerCancelOrderAggregateFluent = unacceptedPassengerCancelOrderAggregateFluent.Group(orderUniqueGroup);
                acceptedPassengerCancelOrderAggregateFluent = acceptedPassengerCancelOrderAggregateFluent.Group(orderUniqueGroup);
            }
        }
        #endregion

        #region 订单完成趋势
        public async Task<OrderTrendDto> OrderTrend(OrderTrendResultRequestDto input)
        {
            DateTime beginTime = DateTime.Now.AddDays(-7).Date;
            DateTime endTime = DateTime.Now.AddDays(-1).ToDayEnd();
            switch (input.Unit)
            {
                case Unit.Month:
                    beginTime = new DateTime(DateTime.Now.AddMonths(-6).Year, DateTime.Now.AddMonths(-6).Month, 1, 0, 0, 0, DateTimeKind.Local).Date;
                    endTime = new DateTime(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, DateTime.Now.AddMonths(-1).TotalDaysInMonth(), 0, 0, 0, DateTimeKind.Local).ToDayEnd();
                    return new OrderTrendDto
                    {
                        TotalOrder = await GetOrderTrends("%Y-%m", beginTime, endTime, input.IsUnique),
                        BookingOrder = await GetOrderTrends("%Y-%m", beginTime, endTime, input.IsUnique, OrderType.Booking),
                        ImmediateOrder = await GetOrderTrends("%Y-%m", beginTime, endTime, input.IsUnique, OrderType.Immediate),
                        PickupOrder = await GetOrderTrends("%Y-%m", beginTime, endTime, input.IsUnique, null, BookingOrderType.Pickup),
                        DropoffOrder = await GetOrderTrends("%Y-%m", beginTime, endTime, input.IsUnique, null, BookingOrderType.Dropoff),
                    };
                case Unit.Week:
                    beginTime = DateTime.Now.StartOfWeek(DayOfWeek.Friday).AddDays(-49);
                    endTime = DateTime.Now.StartOfWeek(DayOfWeek.Friday).AddDays(-1);
                    return new OrderTrendDto
                    {
                        TotalOrder = await GetWeekOrderTrends(beginTime, endTime, input.IsUnique),
                        BookingOrder = await GetWeekOrderTrends(beginTime, endTime, input.IsUnique, OrderType.Booking),
                        ImmediateOrder = await GetWeekOrderTrends(beginTime, endTime, input.IsUnique, OrderType.Immediate),
                        PickupOrder = await GetWeekOrderTrends(beginTime, endTime, input.IsUnique, null, BookingOrderType.Pickup),
                        DropoffOrder = await GetWeekOrderTrends(beginTime, endTime, input.IsUnique, null, BookingOrderType.Dropoff),
                    };
                case Unit.Day:
                default:
                    return new OrderTrendDto
                    {
                        TotalOrder = await GetOrderTrends("%Y-%m-%d", beginTime, endTime, input.IsUnique),
                        BookingOrder = await GetOrderTrends("%Y-%m-%d", beginTime, endTime, input.IsUnique, OrderType.Booking),
                        ImmediateOrder = await GetOrderTrends("%Y-%m-%d", beginTime, endTime, input.IsUnique, OrderType.Immediate),
                        PickupOrder = await GetOrderTrends("%Y-%m-%d", beginTime, endTime, input.IsUnique, null, BookingOrderType.Pickup),
                        DropoffOrder = await GetOrderTrends("%Y-%m-%d", beginTime, endTime, input.IsUnique, null, BookingOrderType.Dropoff),
                    };
            }
        }

        protected async Task<IOrderedEnumerable<OrderDetailDto>> GetWeekOrderTrends(DateTime beginTime, DateTime endTime, bool isUnique, OrderType? orderType = null, BookingOrderType? bookingOrderType = null)
        {
            var returnOrderTrendDetails = new List<OrderDetailDto>();
            var orderTrends = await GetOrderTrends("%Y-%m-%d", beginTime, endTime, isUnique, orderType, bookingOrderType);
            for (int i = 0; i < orderTrends.Count();)
            {
                var weekBegin = Convert.ToDateTime(orderTrends.ElementAtOrDefault(i).Key).StartOfWeek(DayOfWeek.Friday);
                var weekList = orderTrends.Where(x => Convert.ToDateTime(x.Key) >= weekBegin && Convert.ToDateTime(x.Key) < weekBegin.AddDays(7));
                returnOrderTrendDetails.Add(new OrderDetailDto
                {
                    Year = weekBegin.AddDays(6).Year,
                    Key = string.Format(@"{0:D2}W{1}", weekBegin.AddDays(6).Month, weekBegin.AddDays(6).WeekDayInstanceOfMonth()),
                    OrderCount = weekList.Sum(x => x.OrderCount),
                    AcceptedOrderCount = weekList.Sum(x => x.AcceptedOrderCount),
                    CompletedOrderCount = weekList.Sum(x => x.CompletedOrderCount),
                });
                i += weekList.Count();
            }
            return returnOrderTrendDetails.OrderBy(x => x.Year).ThenBy(x => x.Key);
        }

        protected async Task<IOrderedEnumerable<OrderDetailDto>> GetOrderTrends(string dateFormat, DateTime beginTime, DateTime endTime, bool isUnique, OrderType? orderType = null, BookingOrderType? bookingOrderType = null)
        {
            var orderTrendDetails = new List<OrderDetailDto>();
            var builderFilter = Builders<BsonDocument>.Filter;

            var systemList = new List<int>() { 40, 45 };
            var placeOrderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status1", beginTime.ToLocalTime().Date, endTime.ToLocalTime().ToDayEnd(), orderType, bookingOrderType);
            var completedOrderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status1", beginTime.ToLocalTime().Date, endTime.ToLocalTime().ToDayEnd(), orderType, bookingOrderType, builderFilter.Exists("updateTimeList.status20", true));
            var acceptOrderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status1", beginTime.ToLocalTime().Date, endTime.ToLocalTime().ToDayEnd(), orderType, bookingOrderType, builderFilter.Exists("updateTimeList.status5", true));
            var systemCancelOrderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status1", beginTime.ToLocalTime().Date, endTime.ToLocalTime().ToDayEnd(), orderType, bookingOrderType, builderFilter.In("orderStatus", systemList));
            var unacceptedPassengerCancelOrderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status1", beginTime.ToLocalTime().Date, endTime.ToLocalTime().ToDayEnd(), orderType, bookingOrderType, builderFilter.And(builderFilter.Eq("orderStatus", 30), builderFilter.Exists("updateTimeList.status5", false)));
            var acceptedPassengerCancelOrderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status1", beginTime.ToLocalTime().Date, endTime.ToLocalTime().ToDayEnd(), orderType, bookingOrderType, builderFilter.And(builderFilter.Eq("orderStatus", 30), builderFilter.Exists("updateTimeList.status5", true)));

            CheckUnique(isUnique, ref placeOrderAggregateFluent, ref completedOrderAggregateFluent, ref acceptOrderAggregateFluent, ref systemCancelOrderAggregateFluent, ref unacceptedPassengerCancelOrderAggregateFluent, ref acceptedPassengerCancelOrderAggregateFluent);

            return await GetOrderTrend(dateFormat, orderTrendDetails, placeOrderAggregateFluent, completedOrderAggregateFluent, acceptOrderAggregateFluent);
        }

        private static async Task<IOrderedEnumerable<OrderDetailDto>> GetOrderTrend(string dateFormat, List<OrderDetailDto> orderTrendDetails, IAggregateFluent<BsonDocument> placeOrderAggregateFluent, IAggregateFluent<BsonDocument> completedOrderAggregateFluent, IAggregateFluent<BsonDocument> acceptOrderAggregateFluent)
        {
            var placeOrderGroup = new BsonDocument
            {
                { "_id", new BsonDocument("$dateToString",new BsonDocument{
                    { "format",dateFormat },{ "date", "$updateTimeList.status1" },{ "timezone", "+08:00" }
                })},
                { "OrderCount", new BsonDocument("$sum", 1) }
            };
            var placeOrderBson = await placeOrderAggregateFluent.Group(placeOrderGroup).ToListAsync();
            var completedOrderBson = await completedOrderAggregateFluent.Group(placeOrderGroup).ToListAsync();
            var acceptOrderBson = await acceptOrderAggregateFluent.Group(placeOrderGroup).ToListAsync();
            placeOrderBson.ForEach(x =>
            {
                var orderTrendDetail = new OrderDetailDto();
                orderTrendDetail.Key = x.GetValue("_id").ToString();
                orderTrendDetail.OrderCount = x.GetValue("OrderCount").ToInt32();
                var acceptOrder = acceptOrderBson.FirstOrDefault(p => p.GetValue("_id").ToString() == x.GetValue("_id").ToString());
                orderTrendDetail.AcceptedOrderCount = acceptOrder != null ? acceptOrder.GetValue("OrderCount").ToInt32() : 0;
                var completedOrder = completedOrderBson.FirstOrDefault(p => p.GetValue("_id").ToString() == x.GetValue("_id").ToString());
                orderTrendDetail.CompletedOrderCount = completedOrder != null ? completedOrder.GetValue("OrderCount").ToInt32() : 0;
                orderTrendDetails.Add(orderTrendDetail);
            });

            return orderTrendDetails.OrderBy(x => x.Key);
        }
        #endregion

        #region 分时段订单量
        public async Task<IEnumerable<DaypartingOrderTrendDto>> DaypartingOrderTrend(DaypartingOrderTrendResultRequestDto input)
        {
            var daypartingOrderTrends = new List<DaypartingOrderTrendDto>();
            foreach (var item in input.Times)
            {
                daypartingOrderTrends.Add(new DaypartingOrderTrendDto
                {
                    Date = item.ToLocalTime().ToString("yyyy-MM-dd"),
                    OrderTrend = new OrderTrendDto
                    {
                        TotalOrder = await GetOrderTrends("%H", item.ToLocalTime().Date, item.ToLocalTime().ToDayEnd(), input.IsUnique),
                        ImmediateOrder = await GetOrderTrends("%H", item.ToLocalTime().Date, item.ToLocalTime().ToDayEnd(), input.IsUnique, OrderType.Immediate),
                        BookingOrder = await GetOrderTrends("%H", item.ToLocalTime().Date, item.ToLocalTime().ToDayEnd(), input.IsUnique, OrderType.Booking),
                        PickupOrder = await GetOrderTrends("%H", item.ToLocalTime().Date, item.ToLocalTime().ToDayEnd(), input.IsUnique, null, BookingOrderType.Pickup),
                        DropoffOrder = await GetOrderTrends("%H", item.ToLocalTime().Date, item.ToLocalTime().ToDayEnd(), input.IsUnique, null, BookingOrderType.Dropoff),
                    }
                });
            }
            return daypartingOrderTrends.OrderBy(x => x.Date);
        }
        #endregion

        //#region 取消下单
        //public async Task<CancelOrderDto> CancelOrder(CancelOrderResultRequestDto input)
        //{
        //    return new CancelOrderDto
        //    {
        //        TotalOrder = await GetCancelOrderDetail(input),
        //        ImmediateOrder = await GetCancelOrderDetail(input, OrderType.Immediate),
        //        BookingOrder = await GetCancelOrderDetail(input, OrderType.Booking),
        //        PickupOrder = await GetCancelOrderDetail(input, null, BookingOrderType.Pickup),
        //        DropoffOrder = await GetCancelOrderDetail(input, null, BookingOrderType.Dropoff),
        //    };
        //}

        //protected async Task<CancelOrderDetailDto> GetCancelOrderDetail(CancelOrderResultRequestDto input, OrderType? orderType = null, BookingOrderType? bookingOrderType = null)
        //{
        //    var builderFilter = Builders<BsonDocument>.Filter;
        //    var passengerList = new List<int>() { 40, 45 };
        //    var placeOrderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status1", input.From.ToLocalTime().Date, input.To.ToLocalTime().ToDayEnd(), orderType, bookingOrderType);
        //    var systemCancelOrderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status1", input.From.ToLocalTime().Date, input.To.ToLocalTime().ToDayEnd(), orderType, bookingOrderType, builderFilter.In("orderStatus", passengerList));
        //    var passengerCancelOrderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status1", input.From.ToLocalTime().Date, input.To.ToLocalTime().ToDayEnd(), orderType, bookingOrderType, builderFilter.Eq("orderStatus", 30));
        //    CancelOrderCheckIsUnique(input.IsUnique, ref placeOrderAggregateFluent, ref systemCancelOrderAggregateFluent, ref passengerCancelOrderAggregateFluent);
        //    var orderGroup = new BsonDocument
        //    {
        //        { "_id", 1 },
        //        { "OrderCount", new BsonDocument("$sum", 1) }
        //    };
        //    var placeOrderBson = await placeOrderAggregateFluent.Group(orderGroup).FirstOrDefaultAsync();
        //    var systemCancelOrderBson = await systemCancelOrderAggregateFluent.Group(orderGroup).FirstOrDefaultAsync();
        //    var passengerCancelOrderBson = await passengerCancelOrderAggregateFluent.Group(orderGroup).FirstOrDefaultAsync();
        //    return new CancelOrderDetailDto
        //    {
        //        OrderCount = placeOrderBson != null ? placeOrderBson.GetValue("OrderCount").ToInt32() : 0,
        //        SystemCancelOrderCount = systemCancelOrderBson != null ? systemCancelOrderBson.GetValue("OrderCount").ToInt32() : 0,
        //        PassengerCancelOrderCount = passengerCancelOrderBson != null ? passengerCancelOrderBson.GetValue("OrderCount").ToInt32() : 0,
        //    };
        //}

        //private static void CancelOrderCheckIsUnique(bool isUnique, ref IAggregateFluent<BsonDocument> placeOrderAggregateFluent, ref IAggregateFluent<BsonDocument> systemCancelOrderAggregateFluent, ref IAggregateFluent<BsonDocument> passengerCancelOrderAggregateFluent)
        //{
        //    if (isUnique)
        //    {
        //        var bsonList = new List<BsonValue>();
        //        bsonList.Add(BsonValue.Create(new BsonDocument
        //        {
        //            { "$dateToString",new BsonDocument{
        //                { "format", "%Y-%m-%dT%H:%M" },{ "date", "$updateTimeList.status1" },{"timezone","+08:00"}
        //            }}
        //        }));
        //        bsonList.Add(0);
        //        bsonList.Add(15);
        //        var orderUniqueGroup = new BsonDocument
        //        {
        //            { "_id", new BsonDocument{
        //                { "passengerId","$passengerId"},
        //                { "time", new BsonDocument {{"$substr", new BsonArray(bsonList) }}}
        //            }},
        //            {"updateTimeList",new BsonDocument("$last","$updateTimeList") }
        //        };
        //        placeOrderAggregateFluent = placeOrderAggregateFluent.Group(orderUniqueGroup);
        //        systemCancelOrderAggregateFluent = systemCancelOrderAggregateFluent.Group(orderUniqueGroup);
        //        passengerCancelOrderAggregateFluent = passengerCancelOrderAggregateFluent.Group(orderUniqueGroup);
        //    }
        //}
        //#endregion

        #region 下单失败日趋势
        public async Task<OrderTrendDto> CancelOrderTrend(CancelOrderTrendResultRequestDto input)
        {
            DateTime beginTime = DateTime.Now.AddDays(-7).Date;
            DateTime endTime = DateTime.Now.AddDays(-1).ToDayEnd();
            switch (input.Unit)
            {
                case Unit.Month:
                    beginTime = new DateTime(DateTime.Now.AddMonths(-6).Year, DateTime.Now.AddMonths(-6).Month, 1, 0, 0, 0, DateTimeKind.Local).Date;
                    endTime = new DateTime(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, DateTime.Now.AddMonths(-1).TotalDaysInMonth(), 0, 0, 0, DateTimeKind.Local).ToDayEnd();
                    return new OrderTrendDto
                    {
                        TotalOrder = await GetCancelOrderDetailTrend("%Y-%m", beginTime, endTime, input.IsUnique),
                        BookingOrder = await GetCancelOrderDetailTrend("%Y-%m", beginTime, endTime, input.IsUnique, OrderType.Booking),
                        ImmediateOrder = await GetCancelOrderDetailTrend("%Y-%m", beginTime, endTime, input.IsUnique, OrderType.Immediate),
                        PickupOrder = await GetCancelOrderDetailTrend("%Y-%m", beginTime, endTime, input.IsUnique, null, BookingOrderType.Pickup),
                        DropoffOrder = await GetCancelOrderDetailTrend("%Y-%m", beginTime, endTime, input.IsUnique, null, BookingOrderType.Dropoff),
                    };
                case Unit.Week:
                    beginTime = DateTime.Now.StartOfWeek(DayOfWeek.Friday).AddDays(-49);
                    endTime = DateTime.Now.StartOfWeek(DayOfWeek.Friday).AddDays(-1).ToDayEnd();
                    return new OrderTrendDto
                    {
                        TotalOrder = await GetWeekCancelOrderDetailTrend(beginTime, endTime, input.IsUnique),
                        BookingOrder = await GetWeekCancelOrderDetailTrend(beginTime, endTime, input.IsUnique, OrderType.Booking),
                        ImmediateOrder = await GetWeekCancelOrderDetailTrend(beginTime, endTime, input.IsUnique, OrderType.Immediate),
                        PickupOrder = await GetWeekCancelOrderDetailTrend(beginTime, endTime, input.IsUnique, null, BookingOrderType.Pickup),
                        DropoffOrder = await GetWeekCancelOrderDetailTrend(beginTime, endTime, input.IsUnique, null, BookingOrderType.Dropoff),
                    };
                case Unit.Day:
                default:
                    return new OrderTrendDto
                    {
                        TotalOrder = await GetCancelOrderDetailTrend("%Y-%m-%d", beginTime, endTime, input.IsUnique),
                        BookingOrder = await GetCancelOrderDetailTrend("%Y-%m-%d", beginTime, endTime, input.IsUnique, OrderType.Booking),
                        ImmediateOrder = await GetCancelOrderDetailTrend("%Y-%m-%d", beginTime, endTime, input.IsUnique, OrderType.Immediate),
                        PickupOrder = await GetCancelOrderDetailTrend("%Y-%m-%d", beginTime, endTime, input.IsUnique, null, BookingOrderType.Pickup),
                        DropoffOrder = await GetCancelOrderDetailTrend("%Y-%m-%d", beginTime, endTime, input.IsUnique, null, BookingOrderType.Dropoff),
                    };
            }
        }

        protected async Task<IOrderedEnumerable<OrderDetailDto>> GetWeekCancelOrderDetailTrend(DateTime beginTime, DateTime endTime, bool isUnique, OrderType? orderType = null, BookingOrderType? bookingOrderType = null)
        {
            var returnCancelOrderDetails = new List<OrderDetailDto>();
            var orderTrends = await GetCancelOrderDetailTrend("%Y-%m-%d", beginTime, endTime, isUnique, orderType, bookingOrderType);
            for (int i = 0; i < orderTrends.Count();)
            {
                var weekBegin = Convert.ToDateTime(orderTrends.ElementAtOrDefault(i).Key).StartOfWeek(DayOfWeek.Friday);
                var weekList = orderTrends.Where(x => Convert.ToDateTime(x.Key) >= weekBegin && Convert.ToDateTime(x.Key) < weekBegin.AddDays(7));
                returnCancelOrderDetails.Add(new OrderDetailDto
                {
                    Year = weekBegin.AddDays(6).Year,
                    Key = string.Format(@"{0:D2}W{1}", weekBegin.AddDays(6).Month, weekBegin.AddDays(6).WeekDayInstanceOfMonth()),
                    OrderCount = weekList.Sum(x => x.OrderCount),
                    UnacceptedPassengerCancelOrderCount = weekList.Sum(x => x.UnacceptedPassengerCancelOrderCount),
                    AcceptedPassengerCancelOrderCount = weekList.Sum(x => x.AcceptedPassengerCancelOrderCount),
                    SystemCancelOrderCount = weekList.Sum(x => x.SystemCancelOrderCount),
                });
                i += weekList.Count();
            }

            return returnCancelOrderDetails.OrderBy(x => x.Year).ThenBy(x => x.Key);
        }

        protected async Task<IOrderedEnumerable<OrderDetailDto>> GetCancelOrderDetailTrend(string dateFormat, DateTime beginTime, DateTime endTime, bool isUnique, OrderType? orderType = null, BookingOrderType? bookingOrderType = null)
        {
            var cancelOrderDetails = new List<OrderDetailDto>();
            var builderFilter = Builders<BsonDocument>.Filter;

            var systemList = new List<int>() { 40, 45 };
            var placeOrderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status1", beginTime.ToLocalTime().Date, endTime.ToLocalTime().ToDayEnd(), orderType, bookingOrderType);
            var completedOrderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status1", beginTime.ToLocalTime().Date, endTime.ToLocalTime().ToDayEnd(), orderType, bookingOrderType, builderFilter.Exists("updateTimeList.status20", true));
            var acceptOrderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status1", beginTime.ToLocalTime().Date, endTime.ToLocalTime().ToDayEnd(), orderType, bookingOrderType, builderFilter.Exists("updateTimeList.status5", true));
            var systemCancelOrderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status1", beginTime.ToLocalTime().Date, endTime.ToLocalTime().ToDayEnd(), orderType, bookingOrderType, builderFilter.In("orderStatus", systemList));
            var unacceptedPassengerCancelOrderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status1", beginTime.ToLocalTime().Date, endTime.ToLocalTime().ToDayEnd(), orderType, bookingOrderType, builderFilter.And(builderFilter.Eq("orderStatus", 30), builderFilter.Exists("updateTimeList.status5", false)));
            var acceptedPassengerCancelOrderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status1", beginTime.ToLocalTime().Date, endTime.ToLocalTime().ToDayEnd(), orderType, bookingOrderType, builderFilter.And(builderFilter.Eq("orderStatus", 30), builderFilter.Exists("updateTimeList.status5", true)));

            CheckUnique(isUnique, ref placeOrderAggregateFluent, ref completedOrderAggregateFluent, ref acceptOrderAggregateFluent, ref systemCancelOrderAggregateFluent, ref unacceptedPassengerCancelOrderAggregateFluent, ref acceptedPassengerCancelOrderAggregateFluent);

            return await GetOrderedCancelOrderDetail(dateFormat, cancelOrderDetails, placeOrderAggregateFluent, systemCancelOrderAggregateFluent, unacceptedPassengerCancelOrderAggregateFluent, acceptedPassengerCancelOrderAggregateFluent);
        }

        private static async Task<IOrderedEnumerable<OrderDetailDto>> GetOrderedCancelOrderDetail(string dateFormat, List<OrderDetailDto> cancelOrderDetails, IAggregateFluent<BsonDocument> placeOrderAggregateFluent, IAggregateFluent<BsonDocument> systemCancelOrderAggregateFluent, IAggregateFluent<BsonDocument> unacceptedPassengerCancelOrderAggregateFluent, IAggregateFluent<BsonDocument> acceptedPassengerCancelOrderAggregateFluent)
        {
            var placeOrderGroup = new BsonDocument
            {
                { "_id", new BsonDocument("$dateToString",new BsonDocument{
                    { "format",dateFormat },{ "date", "$updateTimeList.status1" },{ "timezone", "+08:00" }
                })},
                { "OrderCount", new BsonDocument("$sum", 1) }
            };
            var placeOrderBson = await placeOrderAggregateFluent.Group(placeOrderGroup).ToListAsync();
            var systemCancelOrderBson = await systemCancelOrderAggregateFluent.Group(placeOrderGroup).ToListAsync();
            var unacceptedPassengerCancelOrderBson = await unacceptedPassengerCancelOrderAggregateFluent.Group(placeOrderGroup).ToListAsync();
            var acceptedPassengerCancelOrderBson = await acceptedPassengerCancelOrderAggregateFluent.Group(placeOrderGroup).ToListAsync();

            placeOrderBson.ForEach(x =>
            {
                var cancelOrderDetail = new OrderDetailDto();
                cancelOrderDetail.Key = x.GetValue("_id").ToString();
                cancelOrderDetail.OrderCount = x.GetValue("OrderCount").ToInt32();
                var systemCancelOrder = systemCancelOrderBson.FirstOrDefault(p => p.GetValue("_id").ToString() == x.GetValue("_id").ToString());
                cancelOrderDetail.SystemCancelOrderCount = systemCancelOrder != null ? systemCancelOrder.GetValue("OrderCount").ToInt32() : 0;
                var unacceptedPassengerCancelOrder = unacceptedPassengerCancelOrderBson.FirstOrDefault(p => p.GetValue("_id").ToString() == x.GetValue("_id").ToString());
                cancelOrderDetail.UnacceptedPassengerCancelOrderCount = unacceptedPassengerCancelOrder != null ? unacceptedPassengerCancelOrder.GetValue("OrderCount").ToInt32() : 0;
                var acceptedPassengerCancelOrder = acceptedPassengerCancelOrderBson.FirstOrDefault(p => p.GetValue("_id").ToString() == x.GetValue("_id").ToString());
                cancelOrderDetail.AcceptedPassengerCancelOrderCount = acceptedPassengerCancelOrder != null ? acceptedPassengerCancelOrder.GetValue("OrderCount").ToInt32() : 0;
                cancelOrderDetails.Add(cancelOrderDetail);
            });
            return cancelOrderDetails.OrderBy(x => x.Key);
        }
        #endregion

        #region 下单失败分时段
        public async Task<IEnumerable<DaypartingOrderTrendDto>> DaypartingCancelOrderTrend(DaypartingCancelOrderTrendResultRequestDto input)
        {
            var daypartingOrderTrends = new List<DaypartingOrderTrendDto>();
            foreach (var item in input.Times)
            {
                daypartingOrderTrends.Add(new DaypartingOrderTrendDto
                {
                    Date = item.ToLocalTime().ToString("yyyy-MM-dd"),
                    OrderTrend = new OrderTrendDto
                    {
                        TotalOrder = await GetCancelOrderDetailTrend("%H", item.ToLocalTime().Date, item.ToLocalTime().ToDayEnd(), input.IsUnique),
                        ImmediateOrder = await GetCancelOrderDetailTrend("%H", item.ToLocalTime().Date, item.ToLocalTime().ToDayEnd(), input.IsUnique, OrderType.Immediate),
                        BookingOrder = await GetCancelOrderDetailTrend("%H", item.ToLocalTime().Date, item.ToLocalTime().ToDayEnd(), input.IsUnique, OrderType.Booking),
                        PickupOrder = await GetCancelOrderDetailTrend("%H", item.ToLocalTime().Date, item.ToLocalTime().ToDayEnd(), input.IsUnique, null, BookingOrderType.Pickup),
                        DropoffOrder = await GetCancelOrderDetailTrend("%H", item.ToLocalTime().Date, item.ToLocalTime().ToDayEnd(), input.IsUnique, null, BookingOrderType.Dropoff),
                    }
                });
            }
            return daypartingOrderTrends.OrderBy(x => x.Date);
        }
        #endregion

        #region 接单取消原因
        public async Task<CancelOrderReasonDto> CancelOrderReason(CancelOrderReasonResultRequestDto input)
        {
            return new CancelOrderReasonDto
            {
                TotalOrder = await GetCancelOrderReasonDetails(input),
                ImmediateOrder = await GetCancelOrderReasonDetails(input, OrderType.Immediate),
                BookingOrder = await GetCancelOrderReasonDetails(input, OrderType.Booking),
                PickupOrder = await GetCancelOrderReasonDetails(input, null, BookingOrderType.Pickup),
                DropoffOrder = await GetCancelOrderReasonDetails(input, null, BookingOrderType.Dropoff),
            };
        }

        protected async Task<IOrderedEnumerable<CancelOrderReasonDetailDto>> GetCancelOrderReasonDetails(CancelOrderReasonResultRequestDto input, OrderType? orderType = null, BookingOrderType? bookingOrderType = null)
        {
            var cancelOrderReasonDetails = new List<CancelOrderReasonDetailDto>();

            var builderFilter = Builders<BsonDocument>.Filter;
            List<FilterDefinition<BsonDocument>> filterDefinitions = new List<FilterDefinition<BsonDocument>>();
            filterDefinitions.Add(builderFilter.Eq("orderStatus", 30));
            if (input.IsAccepted != null)
            {
                filterDefinitions.Add(builderFilter.Exists("updateTimeList.status5", input.IsAccepted.Value));
            }
            var passengerCancelOrderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status1", input.From.ToLocalTime().Date, input.To.ToLocalTime().ToDayEnd(), orderType, bookingOrderType, builderFilter.And(filterDefinitions));

            if (input.IsUnique)
            {
                var bsonList = new List<BsonValue>();
                bsonList.Add(BsonValue.Create(new BsonDocument
                {
                    { "$dateToString",new BsonDocument{
                        { "format", "%Y-%m-%dT%H:%M" },{ "date", "$updateTimeList.status1" },{"timezone","+08:00"}
                    }}
                }));
                bsonList.Add(0);
                bsonList.Add(15);
                var orderUniqueGroup = new BsonDocument
                {
                    { "_id", new BsonDocument{
                        { "passengerId","$passengerId"},
                        { "time", new BsonDocument {{"$substr", new BsonArray(bsonList) }}}
                    }},
                    {"updateTimeList",new BsonDocument("$last","$updateTimeList") },
                    {"calReasonNote",new BsonDocument("$last","$calReasonNote") }
                };
                passengerCancelOrderAggregateFluent = passengerCancelOrderAggregateFluent.Group(orderUniqueGroup);
            }

            var orderGroup = new BsonDocument
            {
                { "_id", "$calReasonNote" },
                { "OrderCount", new BsonDocument("$sum", 1) }
            };
            var passengerCancelOrderBson = await passengerCancelOrderAggregateFluent.Group(orderGroup).ToListAsync();
            passengerCancelOrderBson.ForEach(x =>
            {
                var cancelOrderReasonDetail = new CancelOrderReasonDetailDto();
                cancelOrderReasonDetail.Name = x.GetValue("_id").ToString();
                cancelOrderReasonDetail.Value = x.GetValue("OrderCount").ToInt32();
                cancelOrderReasonDetails.Add(cancelOrderReasonDetail);
            });

            return cancelOrderReasonDetails.OrderBy(x => x.Value);
        }
        #endregion

        #region 订单信息
        public async Task<OrderInfoDto> OrderInfo(OrderInfoResultRequestDto input)
        {
            return new OrderInfoDto
            {
                TotalOrder = await GetOrderInfoDetail(input),
                ImmediateOrder = await GetOrderInfoDetail(input, OrderType.Immediate),
                BookingOrder = await GetOrderInfoDetail(input, OrderType.Booking),
                PickupOrder = await GetOrderInfoDetail(input, null, BookingOrderType.Pickup),
                DropoffOrder = await GetOrderInfoDetail(input, null, BookingOrderType.Dropoff),
            };
        }

        protected async Task<OrderInfoDetailDto> GetOrderInfoDetail(OrderInfoResultRequestDto input, OrderType? orderType = null, BookingOrderType? bookingOrderType = null)
        {
            var builderFilter = Builders<BsonDocument>.Filter;
            var orderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status20", input.From.ToLocalTime().Date, input.To.ToLocalTime().ToDayEnd(), orderType, bookingOrderType);
            var payOrderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status20", input.From.ToLocalTime().Date, input.To.ToLocalTime().ToDayEnd(), orderType, bookingOrderType, builderFilter.Exists("updateTimeList.status35"));

            var durationList = new List<string>() { "$updateTimeList.status20", "$updateTimeList.status15" };
            var orderGroup = new BsonDocument
            {
                { "_id", 1 },
                { "OrderAmount", new BsonDocument("$avg", "$orderAmount") },
                { "Distance", new BsonDocument("$avg", "$totalDistance") },
                { "Duration", new BsonDocument("$avg", new BsonDocument
                    {
                        { "$subtract", new BsonArray(durationList) }
                    })
                }
            };
            var payOrderGroup = new BsonDocument
            {
                { "_id", 1 },
                { "PayAmount", new BsonDocument("$avg", "$amountPayment") },
                { "DriverEvaluation", new BsonDocument("$avg", "$driverEvaluation") },
                { "CarEvaluation", new BsonDocument("$avg", "$carEvaluation") },
            };
            var orderBson = await orderAggregateFluent.Group(orderGroup).FirstOrDefaultAsync();
            var payOrderBson = await payOrderAggregateFluent.Group(payOrderGroup).FirstOrDefaultAsync();

            return new OrderInfoDetailDto
            {
                OrderAmount = orderBson != null ? orderBson.GetValue("OrderAmount").ToDecimal() : 0.00m,
                Distance = orderBson != null ? orderBson.GetValue("Distance").ToDecimal() / 1000 : 0.00m,
                Duration = orderBson != null ? orderBson.GetValue("Duration").ToInt32() * 1.00m / 60000 : 0.00m,
                PayAmount = payOrderBson != null ? payOrderBson.GetValue("PayAmount").ToDecimal() : 0.00m,
                DriverEvaluation = payOrderBson != null ? payOrderBson.GetValue("DriverEvaluation").ToDecimal() : 0.00m,
                CarEvaluation = payOrderBson != null ? payOrderBson.GetValue("CarEvaluation").ToDecimal() : 0.00m,
            };
        }
        #endregion

        #region 订单趋势
        public async Task<OrderInfoTrendDto> OrderInfoTrend(OrderInfoTrendResultRequestDto input)
        {
            DateTime beginTime = DateTime.Now.AddDays(-7).Date;
            DateTime endTime = DateTime.Now.AddDays(-1).ToDayEnd();
            switch (input.Unit)
            {
                case Unit.Month:
                    beginTime = new DateTime(DateTime.Now.AddMonths(-6).Year, DateTime.Now.AddMonths(-6).Month, 1, 0, 0, 0, DateTimeKind.Local).Date;
                    endTime = new DateTime(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, DateTime.Now.AddMonths(-1).TotalDaysInMonth(), 0, 0, 0, DateTimeKind.Local).ToDayEnd();
                    return new OrderInfoTrendDto
                    {
                        TotalOrder = await GetOrderInfoDetails("%Y-%m", beginTime, endTime),
                        BookingOrder = await GetOrderInfoDetails("%Y-%m", beginTime, endTime, OrderType.Booking),
                        ImmediateOrder = await GetOrderInfoDetails("%Y-%m", beginTime, endTime, OrderType.Immediate),
                        PickupOrder = await GetOrderInfoDetails("%Y-%m", beginTime, endTime, null, BookingOrderType.Pickup),
                        DropoffOrder = await GetOrderInfoDetails("%Y-%m", beginTime, endTime, null, BookingOrderType.Dropoff),
                    };
                case Unit.Week:
                    beginTime = DateTime.Now.StartOfWeek(DayOfWeek.Friday).AddDays(-49);
                    endTime = DateTime.Now.StartOfWeek(DayOfWeek.Friday).AddDays(-1).ToDayEnd();
                    return new OrderInfoTrendDto
                    {
                        TotalOrder = await GetWeekOrderInfoDetails(beginTime, endTime),
                        BookingOrder = await GetWeekOrderInfoDetails(beginTime, endTime, OrderType.Booking),
                        ImmediateOrder = await GetWeekOrderInfoDetails(beginTime, endTime, OrderType.Immediate),
                        PickupOrder = await GetWeekOrderInfoDetails(beginTime, endTime, null, BookingOrderType.Pickup),
                        DropoffOrder = await GetWeekOrderInfoDetails(beginTime, endTime, null, BookingOrderType.Dropoff),
                    };
                case Unit.Day:
                default:
                    return new OrderInfoTrendDto
                    {
                        TotalOrder = await GetOrderInfoDetails("%Y-%m-%d", beginTime, endTime),
                        BookingOrder = await GetOrderInfoDetails("%Y-%m-%d", beginTime, endTime, OrderType.Booking),
                        ImmediateOrder = await GetOrderInfoDetails("%Y-%m-%d", beginTime, endTime, OrderType.Immediate),
                        PickupOrder = await GetOrderInfoDetails("%Y-%m-%d", beginTime, endTime, null, BookingOrderType.Pickup),
                        DropoffOrder = await GetOrderInfoDetails("%Y-%m-%d", beginTime, endTime, null, BookingOrderType.Dropoff),
                    };
            }
        }

        protected async Task<IOrderedEnumerable<OrderInfoDetailDto>> GetWeekOrderInfoDetails(DateTime beginTime, DateTime endTime, OrderType? orderType = null, BookingOrderType? bookingOrderType = null)
        {
            var returnOrderInfoDetails = new List<OrderInfoDetailDto>();
            var orderTrends = await GetOrderInfoDetails("%Y-%m-%d", beginTime, endTime, orderType, bookingOrderType);
            for (int i = 0; i < orderTrends.Count();)
            {
                var weekBegin = Convert.ToDateTime(orderTrends.ElementAtOrDefault(i).Key).StartOfWeek(DayOfWeek.Friday);
                var weekList = orderTrends.Where(x => Convert.ToDateTime(x.Key) >= weekBegin && Convert.ToDateTime(x.Key) < weekBegin.AddDays(7));
                returnOrderInfoDetails.Add(new OrderInfoDetailDto
                {
                    Year = weekBegin.AddDays(6).Year,
                    Key = string.Format(@"{0:D2}W{1}", weekBegin.AddDays(6).Month, weekBegin.AddDays(6).WeekDayInstanceOfMonth()),
                    OrderAmount = weekList.Average(x => x.OrderAmount),
                    PayAmount = weekList.Average(x => x.PayAmount),
                    Distance = weekList.Average(x => x.Distance),
                    Duration = weekList.Average(x => x.Duration),
                    DriverEvaluation = weekList.Average(x => x.DriverEvaluation),
                    CarEvaluation = weekList.Average(x => x.CarEvaluation),
                });
                i += weekList.Count();
            }

            return returnOrderInfoDetails.OrderBy(x => x.Year).ThenBy(x => x.Key);
        }

        protected async Task<IOrderedEnumerable<OrderInfoDetailDto>> GetOrderInfoDetails(string dateFormat, DateTime beginTime, DateTime endTime, OrderType? orderType = null, BookingOrderType? bookingOrderType = null)
        {
            var orderInfoDetails = new List<OrderInfoDetailDto>();
            var builderFilter = Builders<BsonDocument>.Filter;
            var orderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status20", beginTime.ToLocalTime().Date, endTime.ToLocalTime().ToDayEnd(), orderType, bookingOrderType);
            var payOrderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status20", beginTime.ToLocalTime().Date, endTime.ToLocalTime().ToDayEnd(), orderType, bookingOrderType, builderFilter.Exists("updateTimeList.status35"));

            var durationList = new List<string>() { "$updateTimeList.status20", "$updateTimeList.status15" };
            var orderGroup = new BsonDocument
            {
                { "_id", new BsonDocument("$dateToString",new BsonDocument{
                    { "format",dateFormat },{ "date", "$updateTimeList.status20" },{ "timezone", "+08:00" }
                })},
                { "OrderAmount", new BsonDocument("$avg", "$orderAmount") },
                { "Distance", new BsonDocument("$avg", "$totalDistance") },
                { "Duration", new BsonDocument("$avg", new BsonDocument
                    {
                        { "$subtract", new BsonArray(durationList) }
                    })
                }
            };
            var payOrderGroup = new BsonDocument
            {
                { "_id", new BsonDocument("$dateToString",new BsonDocument{
                    { "format",dateFormat },{ "date", "$updateTimeList.status20" },{ "timezone", "+08:00" }
                })},
                { "PayAmount", new BsonDocument("$avg", "$amountPayment") },
                { "DriverEvaluation", new BsonDocument("$avg", "$driverEvaluation") },
                { "CarEvaluation", new BsonDocument("$avg", "$carEvaluation") },
            };

            var orderBson = await orderAggregateFluent.Group(orderGroup).ToListAsync();
            var payOrderBson = await payOrderAggregateFluent.Group(payOrderGroup).ToListAsync();

            orderBson.ForEach(x =>
            {
                var orderInfoDetail = new OrderInfoDetailDto();
                orderInfoDetail.Key = x.GetValue("_id").ToString();
                orderInfoDetail.OrderAmount = x.GetValue("OrderAmount").ToDecimal();
                orderInfoDetail.Distance = x.GetValue("Distance").ToDecimal() / 1000;
                orderInfoDetail.Duration = x.GetValue("Duration").ToInt32() * 1.00m / 60000;
                var payOrder = payOrderBson.FirstOrDefault(p => p.GetValue("_id").ToString() == x.GetValue("_id").ToString());
                if (payOrder != null)
                {
                    orderInfoDetail.PayAmount = payOrder.GetValue("PayAmount").ToDecimal();
                    orderInfoDetail.DriverEvaluation = payOrder.GetValue("DriverEvaluation").ToDecimal();
                    orderInfoDetail.CarEvaluation = payOrder.GetValue("CarEvaluation").ToDecimal();
                }
                orderInfoDetails.Add(orderInfoDetail);
            });

            return orderInfoDetails.OrderBy(x => x.Key);
        }
        #endregion
    }
}
