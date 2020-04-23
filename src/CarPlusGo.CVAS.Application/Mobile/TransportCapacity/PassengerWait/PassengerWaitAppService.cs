using CarPlusGo.CVAS.Mobile.MongoDB;
using CarPlusGo.CVAS.Mobile.TransportCapacity.PassengerWait.Dto;
using MongoDB.Bson;
using MongoDB.Driver;
using Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CarPlusGo.CVAS.Mobile.TransportCapacity.PassengerWait
{
    public class PassengerWaitAppService: IPassengerWaitAppService
    {
        private readonly IMongoDBRepository _mongoDBRepository;
        public PassengerWaitAppService(IMongoDBRepository orderRepository)
        {
            _mongoDBRepository = orderRepository;
        }

        #region 乘客等待-页面1接口
        public Blanket3Dto Blanket(Blanket3ResultRequestDto input)
        {
            var builderFilter = Builders<BsonDocument>.Filter;

            #region 即时单平均订单完成等待用时
            Dictionary<string, object> OrderType1SuccessSpendTimeDictionary = new Dictionary<string, object>();
            string[] OrderType1SuccessSpendTimeList = { "$updateTimeList.status10", "$updateTimeList.status5" };
            OrderType1SuccessSpendTimeDictionary.Add("$subtract", OrderType1SuccessSpendTimeList);
            var OrderType1SuccessSpendTimeGroup = new BsonDocument
            {
                { "_id", 1 },
                { "AvgSpendTime", new BsonDocument("$avg",new BsonDocument(OrderType1SuccessSpendTimeDictionary)) },
                { "OrderCount", new BsonDocument("$sum", 1) }
            };
            var OrderType1SuccessSpendTimeAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_order").Aggregate()
                .Match(builderFilter.And(builderFilter.Exists("updateTimeList.status20",true),builderFilter.Eq("orderType",1),builderFilter.Gte("updateTimeList.status1", input.From.ToLocalTime().Date), builderFilter.Lte("updateTimeList.status1", input.To.ToLocalTime().ToDayEnd())));
            var OrderType1SuccessSpendTimeBsonDate = OrderType1SuccessSpendTimeAggregateFluent.Group(OrderType1SuccessSpendTimeGroup).FirstOrDefault();
            var OrderType1SuccessSpendTimeBson = OrderType1SuccessSpendTimeBsonDate == null ? 0 : OrderType1SuccessSpendTimeBsonDate.GetValue("AvgSpendTime");
            #endregion

            #region 预约单平均订单完成等待用时
            Dictionary<string, object> OrderType20SuccessSpendTimeDictionary = new Dictionary<string, object>();
            string[] OrderType20SuccessSpendTimeList = { "$updateTimeList.status10", "$appointmentTime" };
            OrderType20SuccessSpendTimeDictionary.Add("$subtract", OrderType20SuccessSpendTimeList);

            Dictionary<string, object> OrderType20SuccessSpendTimeDictionary2 = new Dictionary<string, object>();
            object[] OrderType20SuccessSpendTimeList2 = { new BsonDocument(OrderType20SuccessSpendTimeDictionary), 0 };

            OrderType20SuccessSpendTimeDictionary2.Add("$max", OrderType20SuccessSpendTimeList2);
            var OrderType20SuccessSpendTimeGroup = new BsonDocument
            {
                { "_id", 1 },
                { "AvgSpendTime", new BsonDocument("$avg",new BsonDocument(OrderType20SuccessSpendTimeDictionary2)) },
                { "OrderCount", new BsonDocument("$sum", 1) }
            };
            var OrderType20SuccessSpendTimeAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_order").Aggregate()
                .Match(builderFilter.And(builderFilter.Exists("updateTimeList.status20", true), builderFilter.Eq("orderType", 2), builderFilter.Eq("orderType2", 0), builderFilter.Gte("updateTimeList.status1", input.From.ToLocalTime().Date), builderFilter.Lte("updateTimeList.status1", input.To.ToLocalTime().ToDayEnd())));
            var OrderType20SuccessSpendTimeBsonDate = OrderType20SuccessSpendTimeAggregateFluent.Group(OrderType20SuccessSpendTimeGroup).FirstOrDefault();
            var OrderType20SuccessSpendTimeBson = OrderType20SuccessSpendTimeBsonDate == null ? 0 : OrderType20SuccessSpendTimeBsonDate.GetValue("AvgSpendTime");
            #endregion

            #region 接机单平均订单完成等待用时
            Dictionary<string, object> OrderType21SuccessSpendTimeDictionary = new Dictionary<string, object>();
            string[] OrderType21SuccessSpendTimeList = { "$updateTimeList.status10", "$appointmentTime" };
            OrderType21SuccessSpendTimeDictionary.Add("$subtract", OrderType21SuccessSpendTimeList);

            Dictionary<string, object> OrderType21SuccessSpendTimeDictionary2 = new Dictionary<string, object>();
            object[] OrderType21SuccessSpendTimeList2 = { new BsonDocument(OrderType21SuccessSpendTimeDictionary), 0 };

            OrderType21SuccessSpendTimeDictionary2.Add("$max", OrderType21SuccessSpendTimeList2);
            var OrderType21SuccessSpendTimeGroup = new BsonDocument
            {
                { "_id", 1 },
                { "AvgSpendTime", new BsonDocument("$avg",new BsonDocument(OrderType21SuccessSpendTimeDictionary2)) },
                { "OrderCount", new BsonDocument("$sum", 1) }
            };
            var OrderType21SuccessSpendTimeAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_order").Aggregate()
                .Match(builderFilter.And(builderFilter.Exists("updateTimeList.status20", true), builderFilter.Eq("orderType", 2), builderFilter.Eq("orderType2", 1), builderFilter.Gte("updateTimeList.status1", input.From.ToLocalTime().Date), builderFilter.Lte("updateTimeList.status1", input.To.ToLocalTime().ToDayEnd())));
            var OrderType21SuccessSpendTimeBsonDate = OrderType21SuccessSpendTimeAggregateFluent.Group(OrderType21SuccessSpendTimeGroup).FirstOrDefault();
            var OrderType21SuccessSpendTimeBson = OrderType21SuccessSpendTimeBsonDate == null ? 0 : OrderType21SuccessSpendTimeBsonDate.GetValue("AvgSpendTime");
            #endregion

            #region 送机单平均订单完成等待用时
            Dictionary<string, object> OrderType22SuccessSpendTimeDictionary = new Dictionary<string, object>();
            string[] OrderType22SuccessSpendTimeList = { "$updateTimeList.status10", "$appointmentTime" };
            OrderType22SuccessSpendTimeDictionary.Add("$subtract", OrderType22SuccessSpendTimeList);

            Dictionary<string, object> OrderType22SuccessSpendTimeDictionary2 = new Dictionary<string, object>();
            object[] OrderType22SuccessSpendTimeList2 = { new BsonDocument(OrderType22SuccessSpendTimeDictionary), 0 };

            OrderType22SuccessSpendTimeDictionary2.Add("$max", OrderType22SuccessSpendTimeList2);
            var OrderType22SuccessSpendTimeGroup = new BsonDocument
            {
                { "_id", 1 },
                { "AvgSpendTime", new BsonDocument("$avg",new BsonDocument(OrderType22SuccessSpendTimeDictionary2)) },
                { "OrderCount", new BsonDocument("$sum", 1) }
            };
            var OrderType22SuccessSpendTimeAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_order").Aggregate()
                .Match(builderFilter.And(builderFilter.Exists("updateTimeList.status20", true), builderFilter.Eq("orderType", 2), builderFilter.Eq("orderType2", 2), builderFilter.Gte("updateTimeList.status1", input.From.ToLocalTime().Date), builderFilter.Lte("updateTimeList.status1", input.To.ToLocalTime().ToDayEnd())));
            var OrderType22SuccessSpendTimeBsonDate = OrderType22SuccessSpendTimeAggregateFluent.Group(OrderType22SuccessSpendTimeGroup).FirstOrDefault();
            var OrderType22SuccessSpendTimeBson = OrderType22SuccessSpendTimeBsonDate == null ? 0 : OrderType22SuccessSpendTimeBsonDate.GetValue("AvgSpendTime");
            #endregion

            #region 即时单平均订单取消等待用时
            Dictionary<string, object> OrderType1CancelSpendTimeDictionary = new Dictionary<string, object>();
            string[] OrderType1CancelSpendTimeList = { "$calTime", "$updateTimeList.status5" };
            OrderType1CancelSpendTimeDictionary.Add("$subtract", OrderType1CancelSpendTimeList);
            var OrderType1CancelSpendTimeGroup = new BsonDocument
            {
                { "_id", 1 },
                { "AvgSpendTime", new BsonDocument("$avg",new BsonDocument(OrderType1CancelSpendTimeDictionary)) },
                { "OrderCount", new BsonDocument("$sum", 1) }
            };
            var OrderType1CancelSpendTimeAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_order").Aggregate()
                .Match(builderFilter.And(builderFilter.Exists("updateTimeList.status5", true), builderFilter.Exists("updateTimeList.status10", false), builderFilter.Eq("orderStatus", 30), builderFilter.Eq("orderType", 1), builderFilter.Gte("updateTimeList.status1", input.From.ToLocalTime().Date), builderFilter.Lte("updateTimeList.status1", input.To.ToLocalTime().ToDayEnd())));
            var OrderType1CancelSpendTimeBsonDate = OrderType1CancelSpendTimeAggregateFluent.Group(OrderType1CancelSpendTimeGroup).FirstOrDefault();
            var OrderType1CancelSpendTimeBson = OrderType1CancelSpendTimeBsonDate == null ? 0 : OrderType1CancelSpendTimeBsonDate.GetValue("AvgSpendTime");
            #endregion

            #region 预约单平均订单取消等待用时
            Dictionary<string, object> OrderType20CancelSpendTimeDictionary = new Dictionary<string, object>();
            string[] OrderType20CancelSpendTimeList = { "$calTime", "$appointmentTime" };
            OrderType20CancelSpendTimeDictionary.Add("$subtract", OrderType20CancelSpendTimeList);

            Dictionary<string, object> OrderType20CancelSpendTimeDictionary2 = new Dictionary<string, object>();
            object[] OrderType20CancelSpendTimeList2 = { new BsonDocument(OrderType20CancelSpendTimeDictionary), 0 };

            OrderType20CancelSpendTimeDictionary2.Add("$max", OrderType20CancelSpendTimeList2);
            var OrderType20CancelSpendTimeGroup = new BsonDocument
            {
                { "_id", 1 },
                { "AvgSpendTime", new BsonDocument("$avg",new BsonDocument(OrderType20CancelSpendTimeDictionary2)) },
                { "OrderCount", new BsonDocument("$sum", 1) }
            };
            var OrderType20CancelSpendTimeAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_order").Aggregate()
                .Match(builderFilter.And(builderFilter.Exists("updateTimeList.status5", true), builderFilter.Exists("updateTimeList.status10", false), builderFilter.Eq("orderStatus", 30), builderFilter.Eq("orderType", 2), builderFilter.Eq("orderType2", 0), builderFilter.Gte("updateTimeList.status1", input.From.ToLocalTime().Date), builderFilter.Lte("updateTimeList.status1", input.To.ToLocalTime().ToDayEnd())));
            var OrderType20CancelSpendTimeBsonDate = OrderType20CancelSpendTimeAggregateFluent.Group(OrderType20CancelSpendTimeGroup).FirstOrDefault();
            var OrderType20CancelSpendTimeBson = OrderType20CancelSpendTimeBsonDate == null ? 0 : OrderType20CancelSpendTimeBsonDate.GetValue("AvgSpendTime");
            #endregion

            #region 接机单平均订单取消等待用时
            Dictionary<string, object> OrderType21CancelSpendTimeDictionary = new Dictionary<string, object>();
            string[] OrderType21CancelSpendTimeList = { "$calTime", "$appointmentTime" };
            OrderType21CancelSpendTimeDictionary.Add("$subtract", OrderType21CancelSpendTimeList);

            Dictionary<string, object> OrderType21CancelSpendTimeDictionary2 = new Dictionary<string, object>();
            object[] OrderType21CancelSpendTimeList2 = { new BsonDocument(OrderType21CancelSpendTimeDictionary), 0 };

            OrderType21CancelSpendTimeDictionary2.Add("$max", OrderType21CancelSpendTimeList2);
            var OrderType21CancelSpendTimeGroup = new BsonDocument
            {
                { "_id", 1 },
                { "AvgSpendTime", new BsonDocument("$avg",new BsonDocument(OrderType21CancelSpendTimeDictionary2)) },
                { "OrderCount", new BsonDocument("$sum", 1) }
            };
            var OrderType21CancelSpendTimeAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_order").Aggregate()
                .Match(builderFilter.And(builderFilter.Exists("updateTimeList.status5", true), builderFilter.Exists("updateTimeList.status10", false), builderFilter.Eq("orderStatus", 30), builderFilter.Eq("orderType", 2), builderFilter.Eq("orderType2", 1), builderFilter.Gte("updateTimeList.status1", input.From.ToLocalTime().Date), builderFilter.Lte("updateTimeList.status1", input.To.ToLocalTime().ToDayEnd())));
            var OrderType21CancelSpendTimeBsonDate = OrderType21CancelSpendTimeAggregateFluent.Group(OrderType21CancelSpendTimeGroup).FirstOrDefault();
            var OrderType21CancelSpendTimeBson = OrderType21CancelSpendTimeBsonDate == null ? 0 : OrderType21CancelSpendTimeBsonDate.GetValue("AvgSpendTime");
            #endregion

            #region 送机单平均订单取消等待用时
            Dictionary<string, object> OrderType22CancelSpendTimeDictionary = new Dictionary<string, object>();
            string[] OrderType22CancelSpendTimeList = { "$calTime", "$appointmentTime" };
            OrderType22CancelSpendTimeDictionary.Add("$subtract", OrderType22CancelSpendTimeList);

            Dictionary<string, object> OrderType22CancelSpendTimeDictionary2 = new Dictionary<string, object>();
            object[] OrderType22CancelSpendTimeList2 = { new BsonDocument(OrderType22CancelSpendTimeDictionary), 0 };

            OrderType22CancelSpendTimeDictionary2.Add("$max", OrderType22CancelSpendTimeList2);
            var OrderType22CancelSpendTimeGroup = new BsonDocument
            {
                { "_id", 1 },
                { "AvgSpendTime", new BsonDocument("$avg",new BsonDocument(OrderType22CancelSpendTimeDictionary2)) },
                { "OrderCount", new BsonDocument("$sum", 1) }
            };
            var OrderType22CancelSpendTimeAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_order").Aggregate()
                .Match(builderFilter.And(builderFilter.Exists("updateTimeList.status5", true), builderFilter.Exists("updateTimeList.status10", false), builderFilter.Eq("orderStatus", 30), builderFilter.Eq("orderType", 2), builderFilter.Eq("orderType2", 2), builderFilter.Gte("updateTimeList.status1", input.From.ToLocalTime().Date), builderFilter.Lte("updateTimeList.status1", input.To.ToLocalTime().ToDayEnd())));
            var OrderType22CancelSpendTimeBsonDate = OrderType22CancelSpendTimeAggregateFluent.Group(OrderType22CancelSpendTimeGroup).FirstOrDefault();
            var OrderType22CancelSpendTimeBson = OrderType22CancelSpendTimeBsonDate == null ? 0 : OrderType22CancelSpendTimeBsonDate.GetValue("AvgSpendTime");
            #endregion

            return new Blanket3Dto
            {
                OrderType1SuccessSpendTime = OrderType1SuccessSpendTimeBson != BsonNull.Value ? OrderType1SuccessSpendTimeBson.ToDouble() / 1000 : 0,
                OrderType1CancelSpendTime = OrderType1CancelSpendTimeBson != BsonNull.Value ? OrderType1CancelSpendTimeBson.ToDouble() / 1000 : 0,
                OrderType20SuccessSpendTime = OrderType20SuccessSpendTimeBson != BsonNull.Value ? OrderType20SuccessSpendTimeBson.ToDouble() / 1000 : 0,
                OrderType20CancelSpendTime = OrderType20CancelSpendTimeBson != BsonNull.Value ? OrderType20CancelSpendTimeBson.ToDouble() / 1000 : 0,
                OrderType21SuccessSpendTime = OrderType21SuccessSpendTimeBson != BsonNull.Value ? OrderType21SuccessSpendTimeBson.ToDouble() / 1000 : 0,
                OrderType21CancelSpendTime = OrderType21CancelSpendTimeBson != BsonNull.Value ? OrderType21CancelSpendTimeBson.ToDouble() / 1000 : 0,
                OrderType22SuccessSpendTime = OrderType22SuccessSpendTimeBson != BsonNull.Value ? OrderType22SuccessSpendTimeBson.ToDouble() / 1000 : 0,
                OrderType22CancelSpendTime = OrderType22CancelSpendTimeBson != BsonNull.Value ? OrderType22CancelSpendTimeBson.ToDouble() / 1000 : 0
            };
        }
        #endregion

        #region 乘客等待-页面2接口
        public Period3Dto Period(Period3ResultRequestDto input)
        {
            var data = new Period3Dto();
            foreach (var i in input.OrderType)
            {
                int? OrderType = null;
                int? OrderType2 = null;
                switch (i)
                {
                    //case 0:
                    //    data = GetPeriodDto(i, data, input.Period, input.OrderStatus, OrderType, OrderType2, null);
                    //    break;
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

        #region 乘客等待-页面3接口
        public Dictionary<string,Period3Dto> ByTheHour(ByTheHour3ResultRequestDto input)
        {
            var datalist = new Dictionary<string, Period3Dto>();
            foreach (var Day in input.DayList)
            {
                var data = new Period3Dto();
                foreach (var i in input.OrderType)
                {
                    int? OrderType = null;
                    int? OrderType2 = null;
                    switch (i)
                    {
                        //case 0:
                        //    data = GetPeriodDto(i, data, 0, input.OrderStatus, OrderType, OrderType2, input.DayList);
                        //    break;
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
        protected Period3Dto GetPeriodDto(int type, Period3Dto data, int Period, int[] OrdersStatus, int? OrderType, int? OrderType2, DateTime? Day)
        {
            foreach (var i in OrdersStatus)
            {
                int? OrderStatus = null;
                if (i == 2)
                {
                    OrderStatus = 30;
                    switch (type)
                    {
                        //case 0:
                        //    data.AllCancel = GetOrderResult(Period, OrderType, OrderType2, OrderStatus, DayList);
                        //    break;
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
                        //case 0:
                        //    data.AllSucces = GetOrderResult(Period, OrderType, OrderType2, OrderStatus, DayList);
                        //    break;
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
        protected IOrderedEnumerable<Period3DetailDto> GetOrderResult(int Period, int? OrderType, int? OrderType2, int? OrderStatus, DateTime? Day)
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
                    endTime = DateTime.Now.StartOfWeek(DayOfWeek.Friday).AddDays(-1).ToDayEnd();
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
        protected IOrderedEnumerable<Period3DetailDto> GetWeekPerformanceTrends(string dateFormat, DateTime beginTime, DateTime endTime, int? OrderType, int? OrderType2, int? OrderStatus)
        {
            var list = new List<Period3DetailDto>();
            var result = OrderGroup(dateFormat, beginTime, endTime, OrderType, OrderType2, OrderStatus);
            for (int i = 0; i < result.Count();)
            {
                var weekBegin = Convert.ToDateTime(result.ElementAtOrDefault(i).Key).StartOfWeek(DayOfWeek.Friday);
                var weekList = result.Where(x => Convert.ToDateTime(x.Key) >= weekBegin && Convert.ToDateTime(x.Key) < weekBegin.AddDays(7));
                list.Add(new Period3DetailDto
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
        protected IOrderedEnumerable<Period3DetailDto> OrderGroup(string dateFormat, DateTime beginTime, DateTime endTime, int? OrderType, int? OrderType2, int? OrderStatus)
        {
            string SubtractBegin;
            string SubtractEnd;
            if(OrderStatus == null)
            {
                if (OrderType==1)
                {
                    SubtractBegin = "$updateTimeList.status10";
                    SubtractEnd= "$updateTimeList.status5";
                }
                else
                {
                    SubtractBegin = "$updateTimeList.status10";
                    SubtractEnd = "$appointmentTime";
                }
            }
            else
            {
                if (OrderType == 1)
                {
                    SubtractBegin = "$calTime";
                    SubtractEnd = "$updateTimeList.status5";
                }
                else
                {
                    SubtractBegin = "$calTime";
                    SubtractEnd = "$appointmentTime";
                }
            }

            var list = new List<Period3DetailDto>();

            var orderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status1", beginTime, endTime, OrderType, OrderType2, OrderStatus);
            Dictionary<string, object> dateToStringKeyValuePairs = new Dictionary<string, object>();
            dateToStringKeyValuePairs.Add("format", dateFormat);
            dateToStringKeyValuePairs.Add("date", "$updateTimeList.status1");
            dateToStringKeyValuePairs.Add("timezone", "+08:00");
            Dictionary<string, object> SubtractDictionary = new Dictionary<string, object>();
            string[] SubtractList = { SubtractBegin, SubtractEnd };
            SubtractDictionary.Add("$subtract", SubtractList);

            Dictionary<string, object> SubtractDictionary2 = new Dictionary<string, object>();
            object[] SubtractList2 = { new BsonDocument(SubtractDictionary), 0 };
            SubtractDictionary2.Add("$max", SubtractList2);
            var orderGroup = new BsonDocument
            {
                { "_id", new BsonDocument("$dateToString",new BsonDocument(dateToStringKeyValuePairs)) },
                { "AvgSpendTime", new BsonDocument("$avg",new BsonDocument(SubtractDictionary2)) },
                { "OrderCount", new BsonDocument("$sum", 1) }
            };
            var orderBson = orderAggregateFluent.Group(orderGroup).ToList();
            orderBson.ForEach(x =>
            {
                var date = new Period3DetailDto();
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
                filterDefinitions.Add(builderFilter.Exists("updateTimeList.status5", true));
                filterDefinitions.Add(builderFilter.Exists("updateTimeList.status10", false));
            }
            else
            {
                filterDefinitions.Add(builderFilter.Exists("updateTimeList.status20", true));
            }
            return _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_order").Aggregate()
                .Match(builderFilter.And(filterDefinitions));
        }
        #endregion

    }
}
