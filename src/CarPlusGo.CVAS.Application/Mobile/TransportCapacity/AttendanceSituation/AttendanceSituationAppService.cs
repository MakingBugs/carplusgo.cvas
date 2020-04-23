using CarPlusGo.CVAS.Mobile.MongoDB;
using CarPlusGo.CVAS.Mobile.TransportCapacity.AttendanceSituation.Dto;
using MongoDB.Bson;
using MongoDB.Driver;
using Abp.Extensions;
using Abp.Linq.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CarPlusGo.CVAS.Mobile.BiStat;
using Abp.Domain.Repositories;

namespace CarPlusGo.CVAS.Mobile.TransportCapacity.AttendanceSituation
{
    public class AttendanceSituationAppService : IAttendanceSituationAppService
    {
        private readonly IMongoDBRepository _mongoDBRepository;
        private readonly IRepository<DayDriverTime, long> _dayDriverTimeRepository;
        public AttendanceSituationAppService(IMongoDBRepository orderRepository, IRepository<DayDriverTime, long> dayDriverTimeRepository)
        {
            _mongoDBRepository = orderRepository;
            _dayDriverTimeRepository = dayDriverTimeRepository;
        }

        #region 司机出勤-页面1接口
        public Blanket5Dto Blanket(Blanket5ResultRequestDto input)
        {
            var builderFilter = Builders<BsonDocument>.Filter;

            #region 在岗司机数
            var Drivers1CountGroup = new BsonDocument
            {
                { "_id", 1 },
                { "Count", new BsonDocument("$sum", 1) }
            };

            var DriversCountAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_user").Aggregate()
                .Match(builderFilter.And(builderFilter.Eq("roles", "driver"), builderFilter.Eq("delFlag", "0"), builderFilter.Eq("driverStatus", 0), builderFilter.Eq("driverCertStatus", 1)));
            var DriversCountBsonDate = DriversCountAggregateFluent.Group(Drivers1CountGroup).FirstOrDefault();
            var DriversCountBson = DriversCountBsonDate == null ? 0 : DriversCountBsonDate.GetValue("Count");
            #endregion

            #region 日均 上线/未上线/满勤/未满勤 司机数

            Dictionary<string, object> DriversCountDictionary = new Dictionary<string, object>();
            DriversCountDictionary.Add("$toDate", "$date");
            Dictionary<string, object> dateToStringKeyValuePairs = new Dictionary<string, object>();
            dateToStringKeyValuePairs.Add("format", "%Y-%m-%d");
            dateToStringKeyValuePairs.Add("date", new BsonDocument(DriversCountDictionary));
            dateToStringKeyValuePairs.Add("timezone", "+08:00");

            var DriversCountGroup = new BsonDocument
            {
                { "_id", new BsonDocument("$dateToString",new BsonDocument(dateToStringKeyValuePairs))  },
                { "Count1", new BsonDocument("$sum", 1) }
            };
            var DriversCountGroup2 = new BsonDocument
            {
                { "_id", 1},
                { "Count2", new BsonDocument("$avg", "$Count1") }
            };
            var ProjectGroup = new BsonDocument
            {
                { "_id",1},
                { "onlineTimes",1},
                { "attendTimes",1},
                { "date", new BsonDocument(DriversCountDictionary) },

            };
            //日均上线司机数
            //var OnlineDriversCountAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_driver_time_statistics").Aggregate()
            //    .Project(ProjectGroup)
            //    .Match(builderFilter.And(builderFilter.Gt("attendTimes", 0), builderFilter.Gte("date", input.From.ToLocalTime().Date), builderFilter.Lte("date", input.To.ToLocalTime().ToDayEnd())));
            //var OnlineDriversCountBsonDate = OnlineDriversCountAggregateFluent.Group(DriversCountGroup).Group(DriversCountGroup2).FirstOrDefault();
            //var OnlineDriversCountBson = OnlineDriversCountBsonDate == null ? 0 : OnlineDriversCountBsonDate.GetValue("Count2");

            var OnlineDriversCountBsonDate = _dayDriverTimeRepository.GetAll().Where(x => Convert.ToDateTime(x.Date) >= input.From.ToLocalTime().Date && Convert.ToDateTime(x.Date) <= input.To.ToLocalTime().ToDayEnd() && x.OnlineTimes > 0);
            var OnlineDriversCountBson = OnlineDriversCountBsonDate.Count() > 0 ? OnlineDriversCountBsonDate.GroupBy(x => x.Date).Select(x => x.Count()).Average():0; 


            //日均未上线司机数
            //var OfflineDriversCountAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_driver_time_statistics").Aggregate()
            //    .Project(ProjectGroup)
            //    .Match(builderFilter.And(builderFilter.Eq("attendTimes", 0), builderFilter.Gte("date", input.From.ToLocalTime().Date), builderFilter.Lte("date", input.To.ToLocalTime().ToDayEnd())));
            //var OfflineDriversCountBsonDate = OfflineDriversCountAggregateFluent.Group(DriversCountGroup).Group(DriversCountGroup2).FirstOrDefault();
            //var OfflineDriversCountBson = OfflineDriversCountBsonDate == null ? 0 : OfflineDriversCountBsonDate.GetValue("Count2");

            var OfflineDriversCountBsonDate = _dayDriverTimeRepository.GetAll().Where(x => Convert.ToDateTime(x.Date) >= input.From.ToLocalTime().Date && Convert.ToDateTime(x.Date) <= input.To.ToLocalTime().ToDayEnd() && x.OnlineTimes == 0);
            var OfflineDriversCountBson = OfflineDriversCountBsonDate.Count() > 0 ? OfflineDriversCountBsonDate.GroupBy(x => x.Date).Select(x => x.Count()).Average() : 0;

            //日均满勤司机数
            //var WorkFullHoursDriversCountAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_driver_time_statistics").Aggregate()
            //    .Project(ProjectGroup)
            //    .Match(builderFilter.And(builderFilter.Gte("attendTimes", 8), builderFilter.Gte("date", input.From.ToLocalTime().Date), builderFilter.Lte("date", input.To.ToLocalTime().ToDayEnd())));
            //var a = WorkFullHoursDriversCountAggregateFluent.Group(DriversCountGroup).ToList();
            //var WorkFullHoursDriversCountBsonDate = WorkFullHoursDriversCountAggregateFluent.Group(DriversCountGroup).Group(DriversCountGroup2).FirstOrDefault();
            //var WorkFullHoursDriversCountBson = WorkFullHoursDriversCountBsonDate == null ? 0 : WorkFullHoursDriversCountBsonDate.GetValue("Count2");

            var WorkFullHoursDriversCountBsonDate = _dayDriverTimeRepository.GetAll().Where(x => Convert.ToDateTime(x.Date) >= input.From.ToLocalTime().Date && Convert.ToDateTime(x.Date) <= input.To.ToLocalTime().ToDayEnd() && x.OnlineTimes >= 8);
            var WorkFullHoursDriversCountBson = WorkFullHoursDriversCountBsonDate.Count() > 0 ? WorkFullHoursDriversCountBsonDate.GroupBy(x => x.Date).Select(x => x.Count()).Average() : 0;

            //日均未满勤司机数
            //var UnWorkFullHoursDriversCountAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_driver_time_statistics").Aggregate()
            //    .Project(ProjectGroup)
            //    .Match(builderFilter.And(builderFilter.Gt("attendTimes", 0), builderFilter.Lt("attendTimes", 8), builderFilter.Gte("date", input.From.ToLocalTime().Date), builderFilter.Lte("date", input.To.ToLocalTime().ToDayEnd())));
            //var UnWorkFullHoursDriversCountBsonDate = UnWorkFullHoursDriversCountAggregateFluent.Group(DriversCountGroup).Group(DriversCountGroup2).FirstOrDefault();
            //var UnWorkFullHoursDriversCountBson = UnWorkFullHoursDriversCountBsonDate == null ? 0 : UnWorkFullHoursDriversCountBsonDate.GetValue("Count2");

            var UnWorkFullHoursDriversCountBsonDate = _dayDriverTimeRepository.GetAll().Where(x => Convert.ToDateTime(x.Date) >= input.From.ToLocalTime().Date && Convert.ToDateTime(x.Date) <= input.To.ToLocalTime().ToDayEnd() && x.OnlineTimes > 0&& x.OnlineTimes < 8);
            var UnWorkFullHoursDriversCountBson = UnWorkFullHoursDriversCountBsonDate.Count() > 0 ? UnWorkFullHoursDriversCountBsonDate.GroupBy(x => x.Date).Select(x => x.Count()).Average() : 0;
            #endregion

            #region 司机人均日 单量/流水
            var subDate = input.To.Subtract(input.From).Days + 1;//所选时间段的天数
            Double SunOrders = 0;//总人均接单数
            Double SumAmount = 0;//总人均流水
            for (int i = 0; i < subDate; i++)
            {
                var DayBegin = input.From.AddDays(+i).ToLocalTime().Date;
                var DayEnd = input.From.AddDays(+i).ToLocalTime().ToDayEnd();
                //当天在线司机数
                //var AggregateFluent1 = _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_driver_time_statistics").Aggregate()
                //.Project(ProjectGroup)
                //.Match(builderFilter.And(builderFilter.Gt("attendTimes", 0), builderFilter.Gte("date", DayBegin), builderFilter.Lte("date", DayEnd)));
                //var BsonDate1 = AggregateFluent1.Group(DriversCountGroup).Group(DriversCountGroup2).FirstOrDefault();
                //var OnlineDriversCount = BsonDate1 == null ? 0 : BsonDate1.GetValue("Count2");
                var OnlineDriversCount = _dayDriverTimeRepository.GetAll().Where(x => Convert.ToDateTime(x.Date) >= DayBegin && Convert.ToDateTime(x.Date) <= DayEnd && x.OnlineTimes > 0).Count();
                //当天接单数
                Dictionary<string, object> KeyValuePairs = new Dictionary<string, object>();
                KeyValuePairs.Add("format", "%Y-%m-%d");
                KeyValuePairs.Add("date", "$updateTimeList.status20");
                KeyValuePairs.Add("timezone", "+08:00");
                var OrdersCountGroup = new BsonDocument
                {
                    { "_id", new BsonDocument("$dateToString",new BsonDocument(KeyValuePairs))  },
                    { "Count", new BsonDocument("$sum", 1) },
                    {"SunAmount", new BsonDocument("$sum", "$orderAmount")}
                };
                var OrdersProjectGroup = new BsonDocument
                {
                    { "_id",1},
                    { "driverId",1},
                    { "updateTimeList",1},
                    { "orderAmount",1 }
                };
                var AggregateFluent2 = _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_order").Aggregate()
                .Project(OrdersProjectGroup)
                .Match(builderFilter.And(builderFilter.Exists("updateTimeList.status20", true), builderFilter.Gte("updateTimeList.status20", DayBegin), builderFilter.Lte("updateTimeList.status20", DayEnd)));
                var BsonDate2 = AggregateFluent2.Group(OrdersCountGroup).FirstOrDefault();
                var OrdersCount = BsonDate2 == null ? 0 : BsonDate2.GetValue("Count");

                var SunAmount = BsonDate2 == null ? 0 : BsonDate2.GetValue("SunAmount");

                if (OnlineDriversCount > 0)
                {
                    //人均接单数
                    var AvgOrders = OrdersCount.ToDouble() / OnlineDriversCount;
                    SunOrders += AvgOrders;
                    //人均流水
                    var AvgAmount = SunAmount.ToDouble() / OnlineDriversCount;
                    SumAmount += AvgAmount;
                }
                else
                {
                    subDate = subDate - 1;
                }
            }
            //人均日单量
            Double AvgDayOrdersCount = SunOrders / subDate;
            //人均日流水
            double AvgSunAmount = SumAmount / subDate;
            #endregion

            return new Blanket5Dto
            {
                DriversCount = DriversCountBson.ToInt32(),
                OnlineDriversCount = OnlineDriversCountBson,
                OfflineDriversCount = OfflineDriversCountBson,
                WorkFullHoursDriversCount = WorkFullHoursDriversCountBson,
                UnWorkFullHoursDriversCount = UnWorkFullHoursDriversCountBson,
                OrdersCount = AvgDayOrdersCount,
                JournalAccount = AvgSunAmount
            };
        }
        #endregion

        #region 司机出勤-页面2接口
        public Period5Dto Period(Period5ResultRequestDto input)
        {
            var data = new Period5Dto();
            return GetPeriodDto(data, input.Period, input.DriverStatus);
        }
        #endregion

        #region 司机出勤-页面3接口
        public IEnumerable<AttendanceDetailDto> AttendanceDetail(AttendanceDetailResultRequestDto input)
        {
            var list = new List<AttendanceDetailDto>();
            var builderFilter = Builders<BsonDocument>.Filter;
            Dictionary<string, object> DriversCountDictionary = new Dictionary<string, object>();
            DriversCountDictionary.Add("$toDate", "$date");
            Dictionary<string, object> dateToStringKeyValuePairs = new Dictionary<string, object>();
            dateToStringKeyValuePairs.Add("format", "%Y-%m-%d");
            dateToStringKeyValuePairs.Add("date", new BsonDocument(DriversCountDictionary));
            dateToStringKeyValuePairs.Add("timezone", "+08:00");

            var DriversCountGroup = new BsonDocument
            {
                { "_id","$team.teamName"},
                { "Count", new BsonDocument("$sum", 1) },
                {"SunAmount", new BsonDocument("$sum", "$orderAmount")}
            };
            var ProjectGroup = new BsonDocument
            {
                { "driverId",new BsonDocument("$toObjectId","$driverId")},
                { "onlineTimes",1},
                { "attendTimes",1},
                { "date", new BsonDocument(DriversCountDictionary) },

            };
            var ProjectGroup2 = new BsonDocument
            {
                { "driverId",new BsonDocument("$toObjectId","$driverId")},
                { "updateTimeList",1},
                { "orderAmount",1}
            };

            var TeamGroup = new BsonDocument
            {
                { "_id","$teamName"},
                { "Count", new BsonDocument("$sum", 1) }
            };

            //日均上线司机数
            //var OnlineDriversCountAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_driver_time_statistics").Aggregate()
            //    .Project(ProjectGroup)
            //    .Match(builderFilter.And(builderFilter.Gt("attendTimes", 0), builderFilter.Gte("date", input.Day.ToLocalTime().Date), builderFilter.Lte("date", input.Day.ToLocalTime().ToDayEnd())));
            //var OnlineDriversCountBsonDate = OnlineDriversCountAggregateFluent.Lookup("tshare_user", "driverId", "_id", "team").Group(DriversCountGroup).ToList();

            //var DriverList = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_user").Aggregate()
            //    .Match(builderFilter.And(builderFilter.Eq("roles", "driver"))).ToList();

            var DriverList = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_user").Aggregate()
                .Match(builderFilter.And(builderFilter.Eq("roles", "driver"), builderFilter.Eq("driverStatus", 0))).ToList();





            var NewList = _dayDriverTimeRepository.GetAll()
                .Join(DriverList, x => x.DriverId, y => y.GetValue("_id").AsObjectId.ToString(), (x, y) => new DriveTeamDto
                {
                    TeamId = y.GetValue("teamId").ToString(),
                    TeamName = y.GetValue("teamName").ToString(),
                    Date = x.Date,
                    OinlineTime = x.OnlineTimes
                })
                .ToList().Where(x => Convert.ToDateTime(x.Date) >= input.Day.ToLocalTime().Date && Convert.ToDateTime(x.Date) <= input.Day.ToLocalTime().ToDayEnd());
            var OnlineDriversCountBsonDate = NewList.Where(x=>x.OinlineTime > 0)
                .GroupBy(x => x.TeamName).Select(x => new
                {
                    TeamName = x.Key,
                    Count = x.Count()
                }).ToList();

            //日均满勤司机数
            //var WorkFullHoursDriversCountAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_driver_time_statistics").Aggregate()
            //    .Project(ProjectGroup)
            //    .Match(builderFilter.And(builderFilter.Gte("attendTimes", 8), builderFilter.Gte("date", input.Day.ToLocalTime().Date), builderFilter.Lte("date", input.Day.ToLocalTime().ToDayEnd())));
            //var WorkFullHoursDriversCountBsonDate = WorkFullHoursDriversCountAggregateFluent.Lookup("tshare_user", "driverId", "_id", "team").Group(DriversCountGroup).ToList();

            var WorkFullHoursDriversCountBsonDate = NewList.Where(x => x.OinlineTime >= 8)
                .GroupBy(x => x.TeamName).Select(x => new
                {
                    TeamName = x.Key,
                    Count = x.Count()
                }).ToList();

            //日均单量/流水
            var OrdersCountAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_order").Aggregate()
                .Match(builderFilter.And(builderFilter.Gte("updateTimeList.status20", input.Day.ToLocalTime().Date), builderFilter.Lte("updateTimeList.status20", input.Day.ToLocalTime().ToDayEnd())))
                .Project(ProjectGroup2);
            var a = OrdersCountAggregateFluent.Lookup("tshare_user", "driverId", "_id", "team").ToList();
            var OrdersCountBsonDate = OrdersCountAggregateFluent.Lookup("tshare_user", "driverId", "_id", "team").Group(DriversCountGroup).ToList();

            //车队司机总数
            //var TeamDriversCountAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_user").Aggregate()
            //   .Match(builderFilter.And(builderFilter.Eq("roles", "driver")));
            //var TeamDriversCountBson = TeamDriversCountAggregateFluent.Group(TeamGroup).ToList();

            var TeamDriversCountBson = DriverList.GroupBy(x => x.GetValue("teamName").ToString().Replace("{","\"").Replace("}", "\"")).Select(x => new
            {
                TeamName = x.Key,
                Count = x.Count()
            }).ToList();

            TeamDriversCountBson.ForEach(x =>
            {
                var data = new AttendanceDetailDto();
                data.TeamName = x.TeamName;
                OnlineDriversCountBsonDate.ForEach(i =>
                {
                    if (i.TeamName == x.TeamName)
                    {
                        data.OnlineDriversCount = i.Count;
                        OrdersCountBsonDate.ForEach(z =>
                        {
                            var str = z.GetValue("_id").ToString().Substring(1);
                            var str2 = str.Substring(0,str.Length - 1);
                            if (x.TeamName == str2)
                            {
                                data.OrdersCount = z.GetValue("Count").ToDouble() / i.Count;
                                data.JournalAccount = z.GetValue("SunAmount").ToDouble() / i.Count;
                            };
                        });
                    }
                });
                WorkFullHoursDriversCountBsonDate.ForEach(y =>
                {
                    if (y.TeamName == x.TeamName)
                    {
                        data.WorkFullHoursDriversCount = y.Count;
                    }
                });
                data.TeamDrivers = x.Count;
                list.Add(data);
            });

            //OnlineDriversCountBsonDate.ForEach(x =>
            //{
            //    var data = new AttendanceDetailDto();
            //    data.TeamName = x.GetValue("_id").ToString();
            //    data.OnlineDriversCount = Convert.ToInt32(x.GetValue("Count"));
            //    WorkFullHoursDriversCountBsonDate.ForEach(y =>
            //    {
            //        if (y.GetValue("_id") == x.GetValue("_id"))
            //        {
            //            data.WorkFullHoursDriversCount = y.GetValue("Count").ToInt32();
            //        }
            //    });
            //    OrdersCountBsonDate.ForEach(z =>
            //    {
            //        if (z.GetValue("_id") == x.GetValue("_id"))
            //        {
            //            data.OrdersCount = z.GetValue("Count").ToDouble() / x.GetValue("Count").ToDouble();
            //            data.JournalAccount= z.GetValue("SunAmount").ToDouble() / x.GetValue("Count").ToDouble();
            //        };
            //    });
            //    list.Add(data);
            //});
            return list;
        }
        #endregion

        #region 抓取不同状态的数据
        protected Period5Dto GetPeriodDto(Period5Dto data, int Period, int[] DriverStatus)
        {
            foreach (var i in DriverStatus)
            {
                if (i==1)
                {
                    data.OnlineDriversCount = GetOrderResult(1,Period);
                }
                if (i==2)
                {
                    data.WorkFullHoursDriversCount = GetOrderResult(2,Period);
                }
            }
            data.OrdersCount = GetOrderResult(3, Period);
            data.JournalAccount = GetOrderResult(4, Period);
            return data;
        }
        #endregion

        #region 动态周期
        protected IOrderedEnumerable<Period5DetailDto> GetOrderResult(int? Type, int Period)
        {
            DateTime beginTime = DateTime.Now.AddDays(-7).Date;
            DateTime endTime = DateTime.Now.AddDays(-1).ToDayEnd();
            switch (Period)
            {
                case 3:
                    beginTime = (new DateTime(DateTime.Now.AddMonths(-6).Year, DateTime.Now.AddMonths(-6).Month, 1)).Date;
                    endTime = (new DateTime(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, DateTime.Now.AddMonths(-1).TotalDaysInMonth())).ToDayEnd();
                    return OrderGroup(Type, "%Y-%m", beginTime, endTime, Period);
                case 2:
                    beginTime = DateTime.Now.StartOfWeek(DayOfWeek.Friday).AddDays(-49).Date;
                    endTime = DateTime.Now.StartOfWeek(DayOfWeek.Friday).AddDays(-1).ToDayEnd();
                    //return GetWeekPerformanceTrends(Type, "%Y-%m-%d", beginTime, endTime, DriverStatus);
                    return OrderGroup(Type, "%Y-%m-%d", beginTime, endTime, Period);
                case 1:
                default:
                    return OrderGroup(Type, "%Y-%m-%d", beginTime, endTime, Period);
            }
        }
        #endregion

        #region 以周为单位
        protected IOrderedEnumerable<Period5DetailDto> GetWeekPerformanceTrends(int? Type,string dateFormat, DateTime beginTime, DateTime endTime, int DriverStatus)
        {
            var list = new List<Period5DetailDto>();
            var result = OrderGroup(Type,dateFormat, beginTime, endTime, DriverStatus);
            for (int i = 0; i < result.Count();)
            {
                var weekBegin = Convert.ToDateTime(result.ElementAtOrDefault(i).Key).StartOfWeek(DayOfWeek.Friday);
                var weekList = result.Where(x => Convert.ToDateTime(x.Key) >= weekBegin && Convert.ToDateTime(x.Key) < weekBegin.AddDays(7));
                list.Add(new Period5DetailDto
                {
                    Key = string.Format(@"{0:D2}W{1}", weekBegin.AddDays(6).Month, weekBegin.AddDays(6).WeekDayInstanceOfMonth()),
                    Count = weekList.Average(x => x.Count)
                });
                i += weekList.Count();
            }
            return list.OrderBy(x => x.Key);
        }
        #endregion

        #region 动态创建Group对象
        protected IOrderedEnumerable<Period5DetailDto> OrderGroup(int? Type,string dateFormat, DateTime beginTime, DateTime endTime, int Period)
        {
            var list = new List<Period5DetailDto>();

            IAggregateFluent<BsonDocument> orderAggregateFluent2 = null;

           
            Dictionary<string, object> DriversCountDictionary = new Dictionary<string, object>();
            DriversCountDictionary.Add("$toDate", "$date");
            Dictionary<string, object> dateToStringKeyValuePairs = new Dictionary<string, object>();
            Dictionary<string, object> dateToStringKeyValuePairs2 = new Dictionary<string, object>();
            if (Type == 1)
            {
                dateToStringKeyValuePairs.Add("format", dateFormat);
                dateToStringKeyValuePairs.Add("date", new BsonDocument(DriversCountDictionary));
                dateToStringKeyValuePairs.Add("timezone", "+08:00");
            }
            else
            {
                dateToStringKeyValuePairs.Add("format", dateFormat);
                dateToStringKeyValuePairs.Add("date", new BsonDocument(DriversCountDictionary));
                dateToStringKeyValuePairs.Add("timezone", "+08:00");

                dateToStringKeyValuePairs2.Add("format", dateFormat);
                dateToStringKeyValuePairs2.Add("date", "$updateTimeList.status20");
                dateToStringKeyValuePairs2.Add("timezone", "+08:00");
            }

            var DriversCountGroup = new BsonDocument
            {
                { "_id", new BsonDocument("$dateToString",new BsonDocument(dateToStringKeyValuePairs))  },
                { "Count1", new BsonDocument("$sum", 1) },
            };
            var DriversCountGroup2 = new BsonDocument
            {
                { "_id", new BsonDocument("$dateToString",new BsonDocument(dateToStringKeyValuePairs2))  },
                { "OrderCount", new BsonDocument("$sum", 1) },
                {"SumAmount", new BsonDocument("$sum", "$orderAmount")}
            };

            //if (Type == 1)
            //{
            //    orderAggregateFluent = GetDriverAggregateFluent(1, "date", beginTime, endTime, DriverStatus);
            //}
            //else
            //{
            //    orderAggregateFluent = GetDriverAggregateFluent(1, "date", beginTime, endTime, 1);
            //    orderAggregateFluent2 = GetDriverAggregateFluent(2, "updateTimeList.status20", beginTime, endTime, DriverStatus);
            //}
            

            //if (Type==1)
            //{
            //    var Bson = orderAggregateFluent.Group(DriversCountGroup).ToList();
            //    Bson.ForEach(x =>
            //    {
            //        var date = new Period5DetailDto();
            //        date.Key = x.GetValue("_id").ToString();
            //        date.Count = x.GetValue("Count1").ToDouble();
            //        list.Add(date);
            //    });
            //    return list.OrderBy(x => x.Key);
            //}
            //else
            //{
            //    if (Type==2)
            //    {
            //        var Bson = orderAggregateFluent.Group(DriversCountGroup).ToList();
            //        var Bson2 = orderAggregateFluent2.Group(DriversCountGroup2).ToList();
            //        Bson2.ForEach(x =>
            //        {
            //            var date = new Period5DetailDto();
            //            date.Key = x.GetValue("_id").ToString();
            //            Bson.ForEach(y =>
            //            {
            //                if (x.GetValue("_id")== y.GetValue("_id"))
            //                {
            //                    date.Count = x.GetValue("OrderCount").ToDouble()/y.GetValue("Count1").ToDouble() ;
            //                }
            //            });
            //            list.Add(date);
            //        });
            //        return list.OrderBy(x => x.Key);
            //    }
            //    else
            //    {
            //        var Bson = orderAggregateFluent.Group(DriversCountGroup).ToList();
            //        var Bson2 = orderAggregateFluent2.Group(DriversCountGroup2).ToList();
            //        Bson2.ForEach(x =>
            //        {
            //            var date = new Period5DetailDto();
            //            date.Key = x.GetValue("_id").ToString();
            //            Bson.ForEach(y =>
            //            {
            //                if (x.GetValue("_id") == y.GetValue("_id"))
            //                {
            //                    date.Count = x.GetValue("SumAmount").ToDouble()/y.GetValue("Count1").ToDouble();
            //                }
            //            });
            //            list.Add(date);
            //        });
            //        return list.OrderBy(x => x.Key);
            //    }

            //}
            switch (Period)
            {
                case 1://日
                    var OnlineDriversDay = _dayDriverTimeRepository.GetAll().Where(x => Convert.ToDateTime(x.Date) >= beginTime && Convert.ToDateTime(x.Date) <= endTime && x.OnlineTimes > 0)
                            .GroupBy(x => x.Date).Select(x => new {
                                Date = x.Key,
                                Count = x.Count()
                            }).ToList();
                    if (Type == 1)
                    {
                        foreach (var item in OnlineDriversDay)
                        {
                            var data = new Period5DetailDto();
                            data.Key = item.Date;
                            data.Count = item.Count;
                            list.Add(data);
                        }
                        return list.OrderBy(x => x.Key);
                    }
                    else if (Type == 2)
                    {
                        
                        var WorkFullHoursDriversCount = _dayDriverTimeRepository.GetAll().Where(x => Convert.ToDateTime(x.Date) >= beginTime && Convert.ToDateTime(x.Date) <= endTime && x.OnlineTimes >= 8)
                            .GroupBy(x => x.Date).Select(x => new {
                                Date = x.Key,
                                Count = x.Count()
                            }).ToList();
                        foreach (var item in WorkFullHoursDriversCount)
                        {
                            var data = new Period5DetailDto();
                            data.Key = item.Date;
                            data.Count = item.Count;
                            list.Add(data);
                        }
                        return list.OrderBy(x => x.Key);
                    }
                    else if (Type == 3)
                    {
                        orderAggregateFluent2 = GetDriverAggregateFluent(2, "updateTimeList.status20", beginTime, endTime, null);
                        var Bson2 = orderAggregateFluent2.Group(DriversCountGroup2).ToList();
                        Bson2.ForEach(x =>
                        {
                            var data = new Period5DetailDto();
                            data.Key = x.GetValue("_id").ToString();
                            foreach (var item in OnlineDriversDay)
                            {
                                if (x.GetValue("_id") == item.Date)
                                {
                                    data.Count = x.GetValue("OrderCount").ToDouble() / item.Count;
                                }
                            }
                            list.Add(data);
                        });
                        return list.OrderBy(x => x.Key);
                    }
                    else
                    {
                        orderAggregateFluent2 = GetDriverAggregateFluent(2, "updateTimeList.status20", beginTime, endTime, null);
                        var Bson2 = orderAggregateFluent2.Group(DriversCountGroup2).ToList();
                        Bson2.ForEach(x =>
                        {
                            var data = new Period5DetailDto();
                            data.Key = x.GetValue("_id").ToString();
                            foreach (var item in OnlineDriversDay)
                            {
                                if (x.GetValue("_id").ToString() == item.Date)
                                {
                                    data.Count = x.GetValue("SumAmount").ToDouble() / item.Count;
                                }
                            }
                            list.Add(data);
                        });
                        break;
                    }
                case 2://周
                    for (int i = 0; i < 7; i++)
                    {
                        var from = beginTime.AddDays(i * 7);
                        var to = beginTime.AddDays(6).AddDays(i * 7).ToDayEnd();
                        TimeSpan timeSpan = to - from;
                        var SubDay = timeSpan.Days+1;

                        var OnlineDriversWeek2 = _dayDriverTimeRepository.GetAll().Where(x => Convert.ToDateTime(x.Date) >= from && Convert.ToDateTime(x.Date) <= to && x.OnlineTimes > 0)
                            .GroupBy(x => x.Date).Select(x => new
                            {
                                Date = x.Key,
                                Count = x.Count()
                            }).ToList();

                        var OnlineDriversWeek = OnlineDriversWeek2.Count() == 0 ? 0 : OnlineDriversWeek2.Average(x => x.Count);

                        var data = new Period5DetailDto();
                        data.Key = string.Format(@"{0:D2}W{1}", from.AddDays(6).Month, from.AddDays(6).WeekDayInstanceOfMonth());
                        if (Type == 1)
                        {
                            data.Count = OnlineDriversWeek;
                            data.Year = to.Year;
                            list.Add(data);
                        }
                        else if (Type == 2)
                        {
                            var WorkFullHoursDriversCount2 = _dayDriverTimeRepository.GetAll().Where(x => Convert.ToDateTime(x.Date) >= from && Convert.ToDateTime(x.Date) <= to && x.OnlineTimes >= 8)
                            .GroupBy(x => x.Date).Select(x => new {
                                Date = x.Key,
                                Count = x.Count()
                            }).ToList();
                            var WorkFullHoursDriversCount = WorkFullHoursDriversCount2.Count() == 0 ? 0 : WorkFullHoursDriversCount2.Average(x => x.Count);
                            data.Count = WorkFullHoursDriversCount;
                            data.Year = to.Year;
                            list.Add(data);
                        }
                        else if (Type == 3)
                        {
                            orderAggregateFluent2 = GetDriverAggregateFluent(2, "updateTimeList.status20", from, to, null);
                            var Bson2 = orderAggregateFluent2.Group(DriversCountGroup2).ToList();
                            double Sum = 0;
                            Bson2.ForEach(x =>
                            {
                                Sum = Sum + x.GetValue("OrderCount").ToDouble();
                            });
                            data.Count = OnlineDriversWeek==0? 0 : (Sum/ SubDay) / OnlineDriversWeek;
                            data.Year = to.Year;
                            list.Add(data);
                        }
                        else
                        {
                            orderAggregateFluent2 = GetDriverAggregateFluent(2, "updateTimeList.status20", from, to, null);
                            var Bson2 = orderAggregateFluent2.Group(DriversCountGroup2).ToList();
                            double Sum = 0;
                            Bson2.ForEach(x =>
                            {
                                Sum = Sum + x.GetValue("SumAmount").ToDouble();
                            });
                            data.Count = OnlineDriversWeek == 0 ? 0 : (Sum / SubDay) / OnlineDriversWeek;
                            data.Year = to.Year;
                            list.Add(data);
                        }
                    }
                    break;
                case 3://月
                    for (int i = 0; i < 6; i++)
                    {
                        var from = beginTime.AddMonths(i);
                        var to = (new DateTime(beginTime.Year, beginTime.Month, beginTime.TotalDaysInMonth())).ToDayEnd().AddMonths(i);

                        TimeSpan timeSpan = to - from;
                        var SubDay = timeSpan.Days+1;

                        var OnlineDriversM2 = _dayDriverTimeRepository.GetAll().Where(x => Convert.ToDateTime(x.Date) >= from && Convert.ToDateTime(x.Date) <= to && x.OnlineTimes > 0)
                           .GroupBy(x => x.Date).Select(x => new
                           {
                               Date = x.Key,
                               Count = x.Count()
                           }).ToList();

                        var OnlineDriversM = OnlineDriversM2.Count() == 0 ? 0 : OnlineDriversM2.Average(x => x.Count);

                        var data = new Period5DetailDto();
                        data.Key = from.ToString("yyyy-MM");
                        if (Type == 1)
                        {
                            data.Count = OnlineDriversM;
                            list.Add(data);
                        }
                        else if (Type == 2)
                        {
                            var WorkFullHoursDriversCountM2 = _dayDriverTimeRepository.GetAll().Where(x => Convert.ToDateTime(x.Date) >= from && Convert.ToDateTime(x.Date) <= to && x.OnlineTimes >= 8)
                           .GroupBy(x => x.Date).Select(x => new {
                               Date = x.Key,
                               Count = x.Count()
                           }).ToList();
                            var WorkFullHoursDriversCountM = WorkFullHoursDriversCountM2.Count() == 0 ? 0 : WorkFullHoursDriversCountM2.Average(x => x.Count);
                            data.Count = WorkFullHoursDriversCountM;
                            list.Add(data);
                        }
                        else if (Type == 3)
                        {
                            orderAggregateFluent2 = GetDriverAggregateFluent(2, "updateTimeList.status20", from, to, null);
                            var Bson2 = orderAggregateFluent2.Group(DriversCountGroup2).ToList();
                            double Sum = 0;
                            Bson2.ForEach(x =>
                            {
                                Sum = Sum + x.GetValue("OrderCount").ToDouble();
                            });
                            data.Count = OnlineDriversM==0? 0 : (Sum / SubDay) / OnlineDriversM;
                            list.Add(data);
                        }
                        else
                        {
                            orderAggregateFluent2 = GetDriverAggregateFluent(2, "updateTimeList.status20", from, to, null);
                            var Bson2 = orderAggregateFluent2.Group(DriversCountGroup2).ToList();
                            double Sum = 0;
                            Bson2.ForEach(x =>
                            {
                                Sum = Sum + x.GetValue("SumAmount").ToDouble();
                            });
                            data.Count = OnlineDriversM == 0 ? 0 : (Sum / SubDay) / OnlineDriversM;
                            list.Add(data);
                        }
                    }
                    break;
            }
            return list.OrderBy(x => x.Year).ThenBy(x=>x.Key);

        }
        #endregion

        #region 动态添加筛选条件
        protected IAggregateFluent<BsonDocument> GetDriverAggregateFluent(int? Type,string TimeKey, DateTime beginTime, DateTime endTime, int? DriverStatus)
        {
            var builderFilter = Builders<BsonDocument>.Filter;

            List<FilterDefinition<BsonDocument>> filterDefinitions = new List<FilterDefinition<BsonDocument>>();

            if (Type==1)
            {
                //上线司机数
                if (DriverStatus == 1) filterDefinitions.Add(builderFilter.Gt("attendTimes", 0));
                //满勤司机数
                if (DriverStatus == 2) filterDefinitions.Add(builderFilter.Gte("attendTimes", 8));
            }
            else
            {
                filterDefinitions.Add(builderFilter.Exists("updateTimeList.status20", true));
            }

            filterDefinitions.Add(builderFilter.Gte(TimeKey, beginTime));
            filterDefinitions.Add(builderFilter.Lte(TimeKey, endTime));

            Dictionary<string, object> DriversCountDictionary = new Dictionary<string, object>();
            DriversCountDictionary.Add("$toDate", "$date");
            var ProjectGroup = new BsonDocument
            {
                { "_id",1},
                { "onlineTimes",1},
                { "attendTimes",1},
                { "date", new BsonDocument(DriversCountDictionary) },
            };
            var OrdersProjectGroup = new BsonDocument
            {
                { "_id",1},
                { "driverId",1},
                { "updateTimeList",1},
                { "orderAmount",1 }
            };
            if (Type==1)
            {
                return _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_driver_time_statistics").Aggregate()
                    .Project(ProjectGroup)
                    .Match(builderFilter.And(filterDefinitions));
            }
            else
            {
                return _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_order").Aggregate()
                    .Project(OrdersProjectGroup)
                    .Match(builderFilter.And(filterDefinitions));
            }
        }
        #endregion
    }
}
