using CarPlusGo.CVAS.Mobile.MongoDB;
using CarPlusGo.CVAS.Mobile.TransportCapacity.OrderReceivingSpendTime.Dto;
using MongoDB.Bson;
using MongoDB.Driver;
using Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using CarPlusGo.CVAS.Mobile.OrderReceivingSpendTime;

namespace CarPlusGo.CVAS.Mobile.TransportCapacity.OrderReceivingSpendTime
{
    public class OrderReceivingSpendTimeAppService : IOrderReceivingSpendTimeAppService
    {
        private readonly IMongoDBRepository _mongoDBRepository;
        public OrderReceivingSpendTimeAppService(IMongoDBRepository orderRepository)
        {
            _mongoDBRepository = orderRepository;
        }

        #region 接单用时-页面1接口
        public BlanketDto Blanket(BlanketResultRequestDto input)
        {
            var builderFilter = Builders<BsonDocument>.Filter;

            #region 平均接单时间
            Dictionary<string, object> OrderReceivingSpendTimeDictionary = new Dictionary<string, object>();
            string[] OrderReceivingSpendTimeList = { "$updateTimeList.status5", "$updateTimeList.status1" };
            OrderReceivingSpendTimeDictionary.Add("$subtract", OrderReceivingSpendTimeList);
            var OrderReceivingSpendTimeGroup = new BsonDocument
            {
                { "_id", 1 },
                { "AvgSpendTime", new BsonDocument("$avg",new BsonDocument(OrderReceivingSpendTimeDictionary)) },
                { "OrderCount", new BsonDocument("$sum", 1) }
            };
            var OrderReceivingSpendTimeAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_order").Aggregate()
                .Match(builderFilter.And(builderFilter.Exists("updateTimeList.status5", true), builderFilter.Gte("updateTimeList.status1", input.From.ToLocalTime().Date), builderFilter.Lte("updateTimeList.status1", input.To.ToLocalTime().ToDayEnd())));
            var OrderReceivingSpendTimeBsonDate = OrderReceivingSpendTimeAggregateFluent.Group(OrderReceivingSpendTimeGroup).FirstOrDefault();
            var OrderReceivingSpendTimeBson = OrderReceivingSpendTimeBsonDate == null ? 0 : OrderReceivingSpendTimeBsonDate.GetValue("AvgSpendTime");
            #endregion

            #region 平均取消订单时间
            Dictionary<string, object> CancelOrderSpendTimeDictionary = new Dictionary<string, object>();
            string[] CancelOrderSpendTimeList = { "$calTime", "$updateTimeList.status1" };
            CancelOrderSpendTimeDictionary.Add("$subtract", CancelOrderSpendTimeList);
            var CancelOrderSpendTimeGroup = new BsonDocument
                {
                    { "_id", 1 },
                    { "AvgSpendTime", new BsonDocument("$avg",new BsonDocument(CancelOrderSpendTimeDictionary)) },
                    { "OrderCount", new BsonDocument("$sum", 1) }
                };
            var CancelOrderSpendTimeAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_order").Aggregate()
                .Match(builderFilter.And(builderFilter.Exists("updateTimeList.status5", false), builderFilter.Gte("updateTimeList.status1", input.From.ToLocalTime().Date), builderFilter.Lte("updateTimeList.status1", input.To.ToLocalTime().ToDayEnd()), builderFilter.Eq("orderStatus", 30)));
            var CancelOrderSpendTimeBsonDate = CancelOrderSpendTimeAggregateFluent.Group(CancelOrderSpendTimeGroup).FirstOrDefault();
            var CancelOrderSpendTimeBson = CancelOrderSpendTimeBsonDate == null ? 0 : CancelOrderSpendTimeBsonDate.GetValue("AvgSpendTime");
            #endregion

            #region 即时单下单成功
            Dictionary<string, object> OrderType1SuccessSpendTimeDictionary = new Dictionary<string, object>();
            string[] OrderType1SuccessSpendTimeList = { "$updateTimeList.status5", "$updateTimeList.status1" };
            OrderType1SuccessSpendTimeDictionary.Add("$subtract", OrderType1SuccessSpendTimeList);
            var OrderType1SuccessSpendTimeGroup = new BsonDocument
            {
                { "_id", 1 },
                { "AvgSpendTime", new BsonDocument("$avg",new BsonDocument(OrderType1SuccessSpendTimeDictionary)) },
                { "OrderCount", new BsonDocument("$sum", 1) }
            };
            var OrderType1SuccessSpendTimeAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_order").Aggregate()
                .Match(builderFilter.And(builderFilter.Exists("updateTimeList.status5", true), builderFilter.Gte("updateTimeList.status1", input.From.ToLocalTime().Date), builderFilter.Lte("updateTimeList.status1", input.To.ToLocalTime().ToDayEnd()), builderFilter.Eq("orderType", 1)));
            var OrderType1SuccessSpendTimeBsonDate = OrderType1SuccessSpendTimeAggregateFluent.Group(OrderType1SuccessSpendTimeGroup).FirstOrDefault();
            var OrderType1SuccessSpendTimeBson = OrderType1SuccessSpendTimeBsonDate == null ? 0 : OrderType1SuccessSpendTimeBsonDate.GetValue("AvgSpendTime");
            #endregion

            #region 即时单下单失败
            Dictionary<string, object> OrderType1FailSpendTimeDictionary = new Dictionary<string, object>();
            string[] OrderType1FailSpendTimeList = { "$calTime", "$updateTimeList.status1" };
            OrderType1FailSpendTimeDictionary.Add("$subtract", OrderType1FailSpendTimeList);
            var OrderType1FailSpendTimeGroup = new BsonDocument
            {
                { "_id", 1 },
                { "AvgSpendTime", new BsonDocument("$avg",new BsonDocument(OrderType1FailSpendTimeDictionary)) },
                { "OrderCount", new BsonDocument("$sum", 1) }
            };
            var OrderType1FailSpendTimeAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_order").Aggregate()
                .Match(builderFilter.And(builderFilter.Exists("updateTimeList.status5", false), builderFilter.Gte("updateTimeList.status1", input.From.ToLocalTime().Date), builderFilter.Lte("updateTimeList.status1", input.To.ToLocalTime().ToDayEnd()), builderFilter.Eq("orderType", 1), builderFilter.Eq("orderStatus", 30)));
            var OrderType1FailSpendTimeBsonDate = OrderType1FailSpendTimeAggregateFluent.Group(OrderType1FailSpendTimeGroup).FirstOrDefault();
            var OrderType1FailSpendTimeBson = OrderType1FailSpendTimeBsonDate == null ? 0 : OrderType1FailSpendTimeBsonDate.GetValue("AvgSpendTime");
            #endregion

            #region 预约单下单成功
            Dictionary<string, object> OrderType20SuccessSpendTimeDictionary = new Dictionary<string, object>();
            string[] OrderType20SuccessSpendTimeList = { "$updateTimeList.status5", "$updateTimeList.status1" };
            OrderType20SuccessSpendTimeDictionary.Add("$subtract", OrderType20SuccessSpendTimeList);
            var OrderType20SuccessSpendTimeGroup = new BsonDocument
                {
                    { "_id", 1 },
                    { "AvgSpendTime", new BsonDocument("$avg",new BsonDocument(OrderType20SuccessSpendTimeDictionary)) },
                    { "OrderCount", new BsonDocument("$sum", 1) }
                };
            var OrderType20SuccessSpendTimeAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_order").Aggregate()
                .Match(builderFilter.And(builderFilter.Exists("updateTimeList.status5", true), builderFilter.Gte("updateTimeList.status1", input.From.ToLocalTime().Date), builderFilter.Lte("updateTimeList.status1", input.To.ToLocalTime().ToDayEnd()), builderFilter.Eq("orderType", 2), builderFilter.Eq("orderType2", 0)));
            var OrderType20SuccessSpendTimeBsonDate = OrderType20SuccessSpendTimeAggregateFluent.Group(OrderType20SuccessSpendTimeGroup).FirstOrDefault();
            var OrderType20SuccessSpendTimeBson = OrderType20SuccessSpendTimeBsonDate == null ? 0 : OrderType20SuccessSpendTimeBsonDate.GetValue("AvgSpendTime");
            #endregion

            #region 预约单下单失败
            Dictionary<string, object> OrderType20FailSpendTimeDictionary = new Dictionary<string, object>();
            string[] OrderType20FailSpendTimeList = { "$calTime", "$updateTimeList.status1" };
            OrderType20FailSpendTimeDictionary.Add("$subtract", OrderType20FailSpendTimeList);
            var OrderType20FailSpendTimeGroup = new BsonDocument
                    {
                        { "_id", 1 },
                        { "AvgSpendTime", new BsonDocument("$avg",new BsonDocument(OrderType20FailSpendTimeDictionary)) },
                        { "OrderCount", new BsonDocument("$sum", 1) }
                    };
            var OrderType20FailSpendTimeAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_order").Aggregate()
                .Match(builderFilter.And(builderFilter.Exists("updateTimeList.status5", false), builderFilter.Gte("updateTimeList.status1", input.From.ToLocalTime().Date), builderFilter.Lte("updateTimeList.status1", input.To.ToLocalTime().ToDayEnd()), builderFilter.Eq("orderType", 2), builderFilter.Eq("orderType2", 0), builderFilter.Eq("orderStatus", 30)));
            var OrderType20FailSpendTimeBsonBsonDate = OrderType20FailSpendTimeAggregateFluent.Group(OrderType20FailSpendTimeGroup).FirstOrDefault();
            var OrderType20FailSpendTimeBson = OrderType20FailSpendTimeBsonBsonDate == null ? 0 : OrderType20FailSpendTimeBsonBsonDate.GetValue("AvgSpendTime");
            #endregion

            #region 接机单下单成功
            Dictionary<string, object> OrderType21SuccessSpendTimeDictionary = new Dictionary<string, object>();
            string[] OrderType21SuccessSpendTimeList = { "$updateTimeList.status5", "$updateTimeList.status1" };
            OrderType21SuccessSpendTimeDictionary.Add("$subtract", OrderType21SuccessSpendTimeList);
            var OrderType21SuccessSpendTimeGroup = new BsonDocument
                    {
                        { "_id", 1 },
                        { "AvgSpendTime", new BsonDocument("$avg",new BsonDocument(OrderType21SuccessSpendTimeDictionary)) },
                        { "OrderCount", new BsonDocument("$sum", 1) }
                    };
            var OrderType21SuccessSpendTimeAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_order").Aggregate()
                .Match(builderFilter.And(builderFilter.Exists("updateTimeList.status5", true), builderFilter.Gte("updateTimeList.status1", input.From.ToLocalTime().Date), builderFilter.Lte("updateTimeList.status1", input.To.ToLocalTime().ToDayEnd()), builderFilter.Eq("orderType", 2), builderFilter.Eq("orderType2", 1)));
            var OrderType21SuccessSpendTimeBsonDate = OrderType21SuccessSpendTimeAggregateFluent.Group(OrderType21SuccessSpendTimeGroup).FirstOrDefault();
            var OrderType21SuccessSpendTimeBson = OrderType21SuccessSpendTimeBsonDate == null ? 0 : OrderType21SuccessSpendTimeBsonDate.GetValue("AvgSpendTime");
            #endregion

            #region 接机单下单失败
            Dictionary<string, object> OrderType21FailSpendTimeDictionary = new Dictionary<string, object>();
            string[] OrderType21FailSpendTimeList = { "$calTime", "$updateTimeList.status1" };
            OrderType21FailSpendTimeDictionary.Add("$subtract", OrderType21FailSpendTimeList);
            var OrderType21FailSpendTimeGroup = new BsonDocument
                    {
                        { "_id", 1 },
                        { "AvgSpendTime", new BsonDocument("$avg",new BsonDocument(OrderType21FailSpendTimeDictionary)) },
                        { "OrderCount", new BsonDocument("$sum", 1) }
                    };
            var OrderType21FailSpendTimeAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_order").Aggregate()
                .Match(builderFilter.And(builderFilter.Exists("updateTimeList.status5", false), builderFilter.Gte("updateTimeList.status1", input.From.ToLocalTime().Date), builderFilter.Lte("updateTimeList.status1", input.To.ToLocalTime().ToDayEnd()), builderFilter.Eq("orderType", 2), builderFilter.Eq("orderType2", 1), builderFilter.Eq("orderStatus", 30)));
            var OrderType21FailSpendTimeBsonDate = OrderType21FailSpendTimeAggregateFluent.Group(OrderType21FailSpendTimeGroup).FirstOrDefault();
            var OrderType21FailSpendTimeBson = OrderType21FailSpendTimeBsonDate == null ? 0 : OrderType21FailSpendTimeBsonDate.GetValue("AvgSpendTime");
            #endregion

            #region 送机单下单成功
            Dictionary<string, object> OrderType22SuccessSpendTimeDictionary = new Dictionary<string, object>();
            string[] OrderType22SuccessSpendTimeList = { "$updateTimeList.status5", "$updateTimeList.status1" };
            OrderType22SuccessSpendTimeDictionary.Add("$subtract", OrderType22SuccessSpendTimeList);
            var OrderType22SuccessSpendTimeGroup = new BsonDocument
                    {
                        { "_id", 1 },
                        { "AvgSpendTime", new BsonDocument("$avg",new BsonDocument(OrderType22SuccessSpendTimeDictionary)) },
                        { "OrderCount", new BsonDocument("$sum", 1) }
                    };
            var OrderType22SuccessSpendTimeAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_order").Aggregate()
                .Match(builderFilter.And(builderFilter.Exists("updateTimeList.status5", true), builderFilter.Gte("updateTimeList.status1", input.From.ToLocalTime().Date), builderFilter.Lte("updateTimeList.status1", input.To.ToLocalTime().ToDayEnd()), builderFilter.Eq("orderType", 2), builderFilter.Eq("orderType2", 2)));
            var OrderType22SuccessSpendTimeBsonDate = OrderType22SuccessSpendTimeAggregateFluent.Group(OrderType22SuccessSpendTimeGroup).FirstOrDefault();
            var OrderType22SuccessSpendTimeBson = OrderType22SuccessSpendTimeBsonDate == null ? 0 : OrderType22SuccessSpendTimeBsonDate.GetValue("AvgSpendTime");
            #endregion

            #region 送机单下单失败
            Dictionary<string, object> OrderType22FailSpendTimeDictionary = new Dictionary<string, object>();
            string[] OrderType22FailSpendTimeList = { "$calTime", "$updateTimeList.status1" };
            OrderType22FailSpendTimeDictionary.Add("$subtract", OrderType22FailSpendTimeList);
            var OrderType22FailSpendTimeGroup = new BsonDocument
                {
                    { "_id", 1 },
                    { "AvgSpendTime", new BsonDocument("$avg",new BsonDocument(OrderType22FailSpendTimeDictionary)) },
                    { "OrderCount", new BsonDocument("$sum", 1) }
                };
            var OrderType22FailSpendTimeAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_order").Aggregate()
                .Match(builderFilter.And(builderFilter.Exists("updateTimeList.status5", false), builderFilter.Gte("updateTimeList.status1", input.From.ToLocalTime().Date), builderFilter.Lte("updateTimeList.status1", input.To.ToLocalTime().ToDayEnd()), builderFilter.Eq("orderType", 2), builderFilter.Eq("orderType2", 2), builderFilter.Eq("orderStatus", 30)));
            var OrderType22FailSpendTimeBsonDate = OrderType22FailSpendTimeAggregateFluent.Group(OrderType22FailSpendTimeGroup).FirstOrDefault();
            var OrderType22FailSpendTimeBson = OrderType22FailSpendTimeBsonDate == null ? 0 : OrderType22FailSpendTimeBsonDate.GetValue("AvgSpendTime");
            #endregion

            return new BlanketDto
            {
                OrderReceivingSpendTime = OrderReceivingSpendTimeBson != BsonNull.Value ? OrderReceivingSpendTimeBson.ToDouble() / 1000 : 0,
                CancelOrderSpendTime = CancelOrderSpendTimeBson != BsonNull.Value ? CancelOrderSpendTimeBson.ToDouble() / 1000 : 0,
                OrderType1SuccessSpendTime = OrderType1SuccessSpendTimeBson != BsonNull.Value ? OrderType1SuccessSpendTimeBson.ToDouble() / 1000 : 0,
                OrderType1FailSpendTime = OrderType1FailSpendTimeBson != BsonNull.Value ? OrderType1FailSpendTimeBson.ToDouble() / 1000 : 0,
                OrderType20SuccessSpendTime = OrderType20SuccessSpendTimeBson != BsonNull.Value ? OrderType20SuccessSpendTimeBson.ToDouble() / 1000 : 0,
                OrderType20FailSpendTime = OrderType20FailSpendTimeBson != BsonNull.Value ? OrderType20FailSpendTimeBson.ToDouble() / 1000 : 0,
                OrderType21SuccessSpendTime = OrderType21SuccessSpendTimeBson != BsonNull.Value ? OrderType21SuccessSpendTimeBson.ToDouble() / 1000 : 0,
                OrderType21FailSpendTime = OrderType21FailSpendTimeBson != BsonNull.Value ? OrderType21FailSpendTimeBson.ToDouble() / 1000 : 0,
                OrderType22SuccessSpendTime = OrderType22SuccessSpendTimeBson != BsonNull.Value ? OrderType22SuccessSpendTimeBson.ToDouble() / 1000 : 0,
                OrderType22FailSpendTime = OrderType22FailSpendTimeBson != BsonNull.Value ? OrderType22FailSpendTimeBson.ToDouble() / 1000 : 0
            };
        }
        #endregion

        #region 接单用时-页面2接口
        public PeriodDto Period(PeriodResultRequestDto input)
        {
            var data = new PeriodDto();
            foreach (var i in input.OrderType)
            {
                int? OrderType = null;
                int? OrderType2 = null;
                switch (i)
                {
                    case 0:
                        data = GetPeriodDto(i,data, input.Period, input.OrderStatus, OrderType, OrderType2, null);
                        break;
                    case 1:
                        OrderType = 1;
                        data = GetPeriodDto(i, data, input.Period, input.OrderStatus, OrderType, OrderType2, null);
                        break;
                    case 2:
                        OrderType = 2;
                        OrderType2 = 0;
                        data = GetPeriodDto(i, data, input.Period, input.OrderStatus, OrderType, OrderType2, null);
                        break;
                    case 3:
                        OrderType = 2;
                        OrderType2 = 1;
                        data = GetPeriodDto(i, data, input.Period, input.OrderStatus, OrderType, OrderType2, null);
                        break;
                    case 4:
                        OrderType = 2;
                        OrderType2 = 2;
                        data = GetPeriodDto(i, data, input.Period, input.OrderStatus, OrderType, OrderType2, null);
                        break;
                }
            }
            return data;

        }
        #endregion

        #region 接单用时-页面3接口
        public Dictionary<string, PeriodDto> ByTheHour(ByTheHourResultRequestDto input)
        {
            var datalist = new Dictionary<string, PeriodDto>();
            foreach (var Day in input.DayList)
            {
                var data = new PeriodDto();
                foreach (var i in input.OrderType)
                {
                    int? OrderType = null;
                    int? OrderType2 = null;
                    switch (i)
                    {
                        case 0:
                            data = GetPeriodDto(i, data, 0, input.OrderStatus, OrderType, OrderType2, Day);
                            break;
                        case 1:
                            OrderType = 1;
                            data = GetPeriodDto(i, data, 0, input.OrderStatus, OrderType, OrderType2, Day);
                            break;
                        case 2:
                            OrderType = 2;
                            OrderType2 = 0;
                            foreach (var j in input.OrderStatus)
                            {
                                data = GetPeriodDto(i, data, 0, input.OrderStatus, OrderType, OrderType2, Day);
                            }
                            break;
                        case 3:
                            OrderType = 2;
                            OrderType2 = 1;
                            data = GetPeriodDto(i, data, 0, input.OrderStatus, OrderType, OrderType2, Day);
                            break;
                        case 4:
                            OrderType = 2;
                            OrderType2 = 2;
                            data = GetPeriodDto(i, data, 0, input.OrderStatus, OrderType, OrderType2, Day);
                            break;
                    }
                }
                datalist.Add(Day.ToLocalTime().ToString("yyyy-MM-dd"), data);
            }

            return datalist;
        }
        #endregion

        #region 抓取不同状态的数据
        protected PeriodDto GetPeriodDto(int type,PeriodDto data, int Period, int[] OrdersStatus, int? OrderType, int? OrderType2,DateTime? Day)
        {
            foreach (var i in OrdersStatus)
            {
                int? OrderStatus = null;
                if (i == 2)
                {
                    OrderStatus = 30;
                    switch (type)
                    {
                        case 0:
                            data.AllCancel= GetOrderResult(Period, OrderType, OrderType2, OrderStatus, Day);
                            break;
                        case 1:
                            data.ForthwithCancel = GetOrderResult(Period, OrderType, OrderType2, OrderStatus, Day);
                            break;
                        case 2:
                            data.OrderCancel = GetOrderResult(Period, OrderType, OrderType2, OrderStatus, Day);
                            break;
                        case 3:
                            data.AirportPickupCancel = GetOrderResult(Period, OrderType, OrderType2, OrderStatus, Day);
                            break;
                        case 4:
                            data.AirportDropOffCancel = GetOrderResult(Period, OrderType, OrderType2, OrderStatus, Day);
                            break;
                    }
                }
                else
                {
                    switch (type)
                    {
                        case 0:
                            data.AllSucces = GetOrderResult(Period, OrderType, OrderType2, OrderStatus, Day);
                            break;
                        case 1:
                            data.ForthwithSucces = GetOrderResult(Period, OrderType, OrderType2, OrderStatus, Day);
                            break;
                        case 2:
                            data.OrderSucces = GetOrderResult(Period, OrderType, OrderType2, OrderStatus, Day);
                            break;
                        case 3:
                            data.AirportPickupSuccess = GetOrderResult(Period, OrderType, OrderType2, OrderStatus, Day);
                            break;
                        case 4:
                            data.AirportDropOffSucces = GetOrderResult(Period, OrderType, OrderType2, OrderStatus, Day);
                            break;
                    }
                }
            }
            return data;
        }
        #endregion

        #region 动态周期
        protected IOrderedEnumerable<PeriodDetailDto> GetOrderResult(int Period, int? OrderType, int? OrderType2, int? OrderStatus,DateTime? Day)
        {
            DateTime beginTime = DateTime.Now.AddDays(-7).Date;
            DateTime endTime = DateTime.Now.AddDays(-1).ToDayEnd();
            switch (Period)
            {
                case 3:
                    beginTime = (new DateTime(DateTime.Now.AddMonths(-6).Year, DateTime.Now.AddMonths(-6).Month, 1)).Date;
                    endTime = (new DateTime(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, DateTime.Now.AddMonths(-1).TotalDaysInMonth())).ToDayEnd();
                    return OrderGroup("%Y-%m", beginTime, endTime, OrderType, OrderType2, OrderStatus);
                case 2:
                    beginTime = DateTime.Now.StartOfWeek(DayOfWeek.Friday).AddDays(-49).Date;
                    endTime = DateTime.Now.StartOfWeek(DayOfWeek.Friday).AddDays(-1).Date;
                    return GetWeekPerformanceTrends("%Y-%m-%d", beginTime, endTime, OrderType, OrderType2, OrderStatus);
                case 1:
                default:
                    return OrderGroup("%Y-%m-%d", beginTime, endTime, OrderType, OrderType2, OrderStatus);
                case 0:
                    beginTime = Day.Value.ToLocalTime().Date;
                    endTime = Day.Value.ToLocalTime().ToDayEnd();
                    return OrderGroup("%H", beginTime, endTime, OrderType, OrderType2, OrderStatus);
            }
        }
        #endregion

        #region 以周为单位
        protected IOrderedEnumerable<PeriodDetailDto> GetWeekPerformanceTrends(string dateFormat, DateTime beginTime, DateTime endTime, int? OrderType, int? OrderType2, int? OrderStatus)
        {
            var list = new List<PeriodDetailDto>();
            var result = OrderGroup(dateFormat, beginTime, endTime, OrderType, OrderType2, OrderStatus);
            for (int i = 0; i < result.Count();)
            {
                var weekBegin = Convert.ToDateTime(result.ElementAtOrDefault(i).Key).StartOfWeek(DayOfWeek.Friday);
                var weekList = result.Where(x => Convert.ToDateTime(x.Key) >= weekBegin && Convert.ToDateTime(x.Key) < weekBegin.AddDays(7));
                list.Add(new PeriodDetailDto
                {
                    Year= weekBegin.AddDays(6).Year,
                    Key = string.Format(@"{0:D2}W{1}", weekBegin.AddDays(6).Month, weekBegin.AddDays(6).WeekDayInstanceOfMonth()),
                    SpendTime = weekList.Average(x => x.SpendTime)
                });
                i += weekList.Count();
            }
            return list.OrderBy(x => x.Year).ThenBy(x=>x.Key);
        }
        #endregion

        #region 动态创建Group对象
        protected IOrderedEnumerable<PeriodDetailDto> OrderGroup(string dateFormat, DateTime beginTime, DateTime endTime, int? OrderType, int? OrderType2, int? OrderStatus)
        {
            string SubtractBegin;
            if (OrderStatus == null) SubtractBegin = "$updateTimeList.status5";
            else SubtractBegin = "$calTime";

            var list = new List<PeriodDetailDto>();

            var orderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status1", beginTime, endTime, OrderType, OrderType2, OrderStatus);
            Dictionary<string, object> dateToStringKeyValuePairs = new Dictionary<string, object>();
            dateToStringKeyValuePairs.Add("format", dateFormat);
            dateToStringKeyValuePairs.Add("date", "$updateTimeList.status1");
            dateToStringKeyValuePairs.Add("timezone", "+08:00");
            Dictionary<string, object> SubtractDictionary = new Dictionary<string, object>();
            string[] SubtractList = { SubtractBegin, "$updateTimeList.status1" };
            SubtractDictionary.Add("$subtract", SubtractList);
            var orderGroup = new BsonDocument
            {
                { "_id", new BsonDocument("$dateToString",new BsonDocument(dateToStringKeyValuePairs)) },
                { "AvgSpendTime", new BsonDocument("$avg",new BsonDocument(SubtractDictionary)) },
                { "OrderCount", new BsonDocument("$sum", 1) }
            };
            var orderBson = orderAggregateFluent.Group(orderGroup).ToList();
            orderBson.ForEach(x =>
            {
                var date = new PeriodDetailDto();
                date.Key = x.GetValue("_id").ToString();
                date.SpendTime = x.GetValue("AvgSpendTime") == BsonNull.Value ? 0 : x.GetValue("AvgSpendTime").ToDouble() / 1000;
                list.Add(date);
            });

            return list.OrderBy(x => x.Key);
        }
        #endregion

        #region 动态添加筛选条件
        protected IAggregateFluent<BsonDocument> GetOrderAggregateFluent(string TimeKey, DateTime beginTime, DateTime endTime, int? OrderType, int? OrderType2, int? OrderStatus)
        {
            var builderFilter = Builders<BsonDocument>.Filter;
            
            List<FilterDefinition<BsonDocument>> filterDefinitions = new List<FilterDefinition<BsonDocument>>();

            filterDefinitions.Add(builderFilter.Gte(TimeKey, beginTime));
            filterDefinitions.Add(builderFilter.Lte(TimeKey, endTime));
            if (OrderType != null) filterDefinitions.Add(builderFilter.Eq("orderType", OrderType));
            if (OrderType2 != null) filterDefinitions.Add(builderFilter.Eq("orderType2", OrderType2));
            if (OrderStatus != null)
            {
                filterDefinitions.Add(builderFilter.Eq("orderStatus", OrderStatus));
                filterDefinitions.Add(builderFilter.Exists("updateTimeList.status5", false));
            }

            return _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_order").Aggregate()
                .Match(builderFilter.And(filterDefinitions));
        }
        #endregion
        

    }
}
