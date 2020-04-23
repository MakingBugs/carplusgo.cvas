using CarPlusGo.CVAS.Mobile.MongoDB;
using Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using CarPlusGo.CVAS.Mobile.MemberAnalysis.RechargeGeneral.Dto;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;

namespace CarPlusGo.CVAS.Mobile.MemberAnalysis.RechargeGeneral
{
    public class RechargeGeneralAppService: IRechargeGeneralAppService
    {
        private readonly IMongoDBRepository _mongoDBRepository;
        public RechargeGeneralAppService(IMongoDBRepository orderRepository)
        {
            _mongoDBRepository = orderRepository;
        }

        public RechargeGeneralDto RechargeGeneral(RechargeGeneralResultRequestDto input)
        {
            var builderFilter = Builders<BsonDocument>.Filter;
            var subDate = input.To.Subtract(input.From).Days + 1;//所选时间段的天数
            var Group1 = new BsonDocument
            {
                { "_id", "$userId" },
                { "Count", new BsonDocument("$sum", 1) }
            };
            var Group2 = new BsonDocument
            {
                { "_id", 1 },
                { "Count", new BsonDocument("$sum", 1) }
            };
            var RechargeGroup = new BsonDocument
            {
                { "_id", 1 },
                { "Amount", new BsonDocument("$sum", "$rechargeMoney") }
            };
            var PresenterGroup = new BsonDocument
            {
                { "_id", 1 },
                { "Amount", new BsonDocument("$sum", "$givenMoney") }
            };
            var TotalBalanceGroup = new BsonDocument
            {
                { "_id", 1 },
                { "Amount", new BsonDocument("$sum", "$balance") }
            };
            var CapitalBalanceGroup = new BsonDocument
            {
                { "_id", 1 },
                { "Amount", new BsonDocument("$sum", "$rechargeMoney") }
            };
            var PresenterBalanceGroup = new BsonDocument
            {
                { "_id", 1 },
                { "Amount", new BsonDocument("$sum", "$givenMoney") }
            };

            #region 充值用户个数
            var RechargeUsersAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_spending_detail").Aggregate()
               .Match(builderFilter.And(builderFilter.Eq("refundStatus", "3"), builderFilter.In("payWay", new BsonArray(new List<string>() { "1", "2" })), builderFilter.Eq("type", "1"), builderFilter.Gte("operateTime",input.From.ToLocalTime().Date), builderFilter.Lte("operateTime", input.To.ToLocalTime().ToDayEnd())));
            var RechargeUsersBsonDate = RechargeUsersAggregateFluent.Group(Group1).Group(Group2).FirstOrDefault();
            var RechargeUsers = RechargeUsersBsonDate == null ? 0 : RechargeUsersBsonDate.GetValue("Count");
            #endregion

            #region 充值订单数
            var RechargeOrdersAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_spending_detail").Aggregate()
               .Match(builderFilter.And(builderFilter.Eq("refundStatus", "3"), builderFilter.In("payWay", new BsonArray(new List<string>() { "1", "2" })), builderFilter.Eq("type", "1"), builderFilter.Gte("operateTime", input.From.ToLocalTime().Date), builderFilter.Lte("operateTime", input.To.ToLocalTime().ToDayEnd())));
            var RechargeOrdersBsonDate = RechargeOrdersAggregateFluent.Group(Group2).FirstOrDefault();
            var RechargeOrders = RechargeOrdersBsonDate == null ? 0 : RechargeOrdersBsonDate.GetValue("Count");
            #endregion

            #region 充值金额
            var RechargeAmountAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_spending_detail").Aggregate()
               .Match(builderFilter.And(builderFilter.Eq("refundStatus", "3"), builderFilter.In("payWay", new BsonArray(new List<string>() { "1", "2" })), builderFilter.Eq("type", "1"), builderFilter.Gte("operateTime", input.From.ToLocalTime().Date), builderFilter.Lte("operateTime", input.To.ToLocalTime().ToDayEnd())));
            var RechargeAmountBsonDate = RechargeAmountAggregateFluent.Group(RechargeGroup).FirstOrDefault();
            var RechargeAmount = RechargeAmountBsonDate == null ? 0 : RechargeAmountBsonDate.GetValue("Amount");
            #endregion

            #region 赠送金额
            var PresenterAmountAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_spending_detail").Aggregate()
               .Match(builderFilter.And(builderFilter.Eq("refundStatus", "3"), builderFilter.In("payWay", new BsonArray(new List<string>() { "1", "2" })), builderFilter.Eq("type", "1"), builderFilter.Gte("operateTime", input.From.ToLocalTime().Date), builderFilter.Lte("operateTime", input.To.ToLocalTime().ToDayEnd())));
            var PresenterAmountBsonDate = PresenterAmountAggregateFluent.Group(PresenterGroup).FirstOrDefault();
            var PresenterAmount = PresenterAmountBsonDate == null ? 0 : PresenterAmountBsonDate.GetValue("Amount");
            #endregion

            #region 余额账户数
            var BalanceAmountsAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_user_account").Aggregate()
               .Match(builderFilter.And(builderFilter.Gt("balance", 0)));
            var BalanceAmountsBsonDate = BalanceAmountsAggregateFluent.Group(Group2).FirstOrDefault();
            var BalanceAmounts = BalanceAmountsBsonDate == null ? 0 : BalanceAmountsBsonDate.GetValue("Count");
            #endregion

            #region 账户总余额
            var TotalBalanceAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_user_account").Aggregate();
            var TotalBalanceBsonDate = TotalBalanceAggregateFluent.Group(TotalBalanceGroup).FirstOrDefault();
            var TotalBalance = TotalBalanceBsonDate == null ? 0 : TotalBalanceBsonDate.GetValue("Amount");
            #endregion

            #region 本金余额
            var CapitalBalanceAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_user_account").Aggregate();
            var CapitalBalanceBsonDate = CapitalBalanceAggregateFluent.Group(CapitalBalanceGroup).FirstOrDefault();
            var CapitalBalance = CapitalBalanceBsonDate == null ? 0 : CapitalBalanceBsonDate.GetValue("Amount");
            #endregion

            #region 赠送余额
            var PresenterBalanceAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_user_account").Aggregate();
            var PresenterBalanceBsonDate = PresenterBalanceAggregateFluent.Group(PresenterBalanceGroup).FirstOrDefault();
            var PresenterBalance = PresenterBalanceBsonDate == null ? 0 : PresenterBalanceBsonDate.GetValue("Amount");
            #endregion

            return new RechargeGeneralDto
            {
                RechargeUsers= RechargeUsers.ToInt32(),
                RechargeOrders= RechargeOrders.ToInt32(),
                RechargeAmount= RechargeAmount.ToDouble(),
                PresenterAmount= PresenterAmount.ToDouble(),
                BalanceAmounts= BalanceAmounts.ToInt32(),
                TotalBalance= TotalBalance.ToDouble(),
                CapitalBalance= CapitalBalance.ToDouble(),
                PresenterBalance= PresenterBalance.ToDouble()
            };
        }

        #region 充值趋势-接口
        public RechargeTendencyDto RechargeTendency(RechargeTendencyResultRequestDto input)
        {
            var data = new RechargeTendencyDto();
            foreach (var i in input.Types)
            {
                switch (i)
                {
                    case 1:
                        data.RechargeUsers = GetOrderResult(i, input.Period);
                        break;
                    case 2:
                        data.RechargeOrders = GetOrderResult(i, input.Period);
                        break;
                    case 3:
                        data.RechargeAmount = GetOrderResult(i, input.Period);
                        break;
                    case 4:
                        data.PresenterAmount = GetOrderResult(i, input.Period);
                        break;
                }
            }
            return data;
        }
        #endregion

        #region 动态周期
        protected IOrderedEnumerable<RechargeTendencyDetailDto> GetOrderResult(int Type, int Period)
        {
            DateTime beginTime = DateTime.Now.AddDays(-7).Date;
            DateTime endTime = DateTime.Now.AddDays(-1).ToDayEnd();
            switch (Period)
            {
                case 3:
                    beginTime = (new DateTime(DateTime.Now.AddMonths(-6).Year, DateTime.Now.AddMonths(-6).Month, 1)).Date;
                    endTime = (new DateTime(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, DateTime.Now.AddMonths(-1).TotalDaysInMonth())).ToDayEnd();
                    return Group(Type, "%Y-%m", beginTime, endTime,false);
                case 2:
                    beginTime = DateTime.Now.StartOfWeek(DayOfWeek.Friday).AddDays(-49).Date;
                    endTime = DateTime.Now.StartOfWeek(DayOfWeek.Friday).AddDays(-1).ToDayEnd();
                    return Group(Type, "%Y-%m-%d", beginTime, endTime,true);
                case 1:
                default:
                    return Group(Type, "%Y-%m-%d", beginTime, endTime,false);
            }
        }
        #endregion


        #region 动态创建Group对象
        protected IOrderedEnumerable<RechargeTendencyDetailDto> Group(int Type,string dateFormat, DateTime beginTime, DateTime endTime,bool IsWeek)
        {
            var builderFilter = Builders<BsonDocument>.Filter;
            var Bson = new List<BsonDocument>();
            Dictionary<string, object> dateToStringKeyValuePairs = new Dictionary<string, object>();
            dateToStringKeyValuePairs.Add("format", dateFormat);
            dateToStringKeyValuePairs.Add("date", "$operateTime");
            dateToStringKeyValuePairs.Add("timezone", "+08:00");

            var Group = new BsonDocument
            {
                { "_id", new BsonDocument("$dateToString",new BsonDocument(dateToStringKeyValuePairs))},
                { "Value", new BsonDocument("$sum", 1) }
            };
            var Group1 = new BsonDocument
            {
                { "_id", new BsonDocument{ 
                    { "Day",new BsonDocument{{"$dateToString",new BsonDocument(dateToStringKeyValuePairs) } } },
                    { "UserId","$userId"} 
                } },
                { "Value", new BsonDocument("$sum", 1) }
            };
            var Group2 = new BsonDocument
            {
                { "_id", "$_id.Day"},
                { "Value", new BsonDocument("$sum", 1) }
            };
            var RechargeGroup = new BsonDocument
            {
                { "_id", new BsonDocument("$dateToString",new BsonDocument(dateToStringKeyValuePairs)) },
                { "Value", new BsonDocument("$sum", "$rechargeMoney") }
            };
            var PresenterGroup = new BsonDocument
            {
                { "_id", new BsonDocument("$dateToString",new BsonDocument(dateToStringKeyValuePairs)) },
                { "Value", new BsonDocument("$sum", "$givenMoney") }
            };

            var WeekGroup1 = new BsonDocument
            {
                { "_id", "$userId"},
                { "Value", new BsonDocument("$sum", 1) }
            };
            var WeekGroup2 = new BsonDocument
            {
                { "_id", ""},
                { "Value", new BsonDocument("$sum", 1) }
            };
            var WeekGroup3 = new BsonDocument
            {
                { "_id", "" },
                { "Value", new BsonDocument("$sum", "$rechargeMoney") }
            };
            var WeekGroup4 = new BsonDocument
            {
                { "_id", "" },
                { "Value", new BsonDocument("$sum", "$givenMoney") }
            };

            IAggregateFluent<BsonDocument> AggregateFluent;
            var list = new List<RechargeTendencyDetailDto>();

            if (Type == 1)
            {
                if (IsWeek)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        var data = new RechargeTendencyDetailDto();
                        var from = beginTime;
                        var to = beginTime.AddDays(6);
                        AggregateFluent = GetAggregateFluent(Type, "operateTime", from.AddDays(i * 7), to.AddDays(i * 7));
                        data.Key = string.Format(@"{0:D2}W{1}", from.AddDays(i * 7).AddDays(6).Month, from.AddDays(i * 7).AddDays(6).WeekDayInstanceOfMonth());
                        var result = AggregateFluent.Group(WeekGroup1).Group(WeekGroup2).FirstOrDefault();
                        data.Value = result==null? 0:result.GetValue("Value").ToDouble();
                        data.Year = to.AddDays(i * 7).Year;
                        list.Add(data);
                    }
                }
                else
                {
                    AggregateFluent = GetAggregateFluent(Type, "operateTime", beginTime, endTime);
                    Bson = AggregateFluent.Group(Group1).Group(Group2).ToList();
                }
            }
            if (Type == 2)
            {
                if (IsWeek)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        var data = new RechargeTendencyDetailDto();
                        var from = beginTime;
                        var to = beginTime.AddDays(6);
                        AggregateFluent = GetAggregateFluent(Type, "operateTime", from.AddDays(i * 7), to.AddDays(i * 7));
                        data.Key = string.Format(@"{0:D2}W{1}", from.AddDays(i * 7).AddDays(6).Month, from.AddDays(i * 7).AddDays(6).WeekDayInstanceOfMonth());
                        var result = AggregateFluent.Group(WeekGroup2).FirstOrDefault();
                        data.Value = result == null ? 0 : result.GetValue("Value").ToDouble();
                        data.Year = to.AddDays(i * 7).Year;
                        list.Add(data);
                    }
                }
                else
                {
                    AggregateFluent = GetAggregateFluent(Type, "operateTime", beginTime, endTime);
                    Bson = AggregateFluent.Group(Group).ToList();
                }
            }
            if (Type == 3)
            {
                if (IsWeek)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        var data = new RechargeTendencyDetailDto();
                        var from = beginTime;
                        var to = beginTime.AddDays(6);
                        AggregateFluent = GetAggregateFluent(Type, "operateTime", from.AddDays(i * 7), to.AddDays(i * 7));
                        data.Key = string.Format(@"{0:D2}W{1}", from.AddDays(i * 7).AddDays(6).Month, from.AddDays(i * 7).AddDays(6).WeekDayInstanceOfMonth());
                        var result = AggregateFluent.Group(WeekGroup3).FirstOrDefault();
                        data.Value = result == null ? 0 : result.GetValue("Value").ToDouble();
                        data.Year = to.AddDays(i * 7).Year;
                        list.Add(data);
                    }
                }
                else
                {
                    AggregateFluent = GetAggregateFluent(Type, "operateTime", beginTime, endTime);
                    Bson = AggregateFluent.Group(RechargeGroup).ToList();
                }
            }
            if (Type == 4)
            {
                if (IsWeek)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        var data = new RechargeTendencyDetailDto();
                        var from = beginTime;
                        var to = beginTime.AddDays(6);
                        AggregateFluent = GetAggregateFluent(Type, "operateTime", from.AddDays(i * 7), to.AddDays(i * 7));
                        data.Key = string.Format(@"{0:D2}W{1}", from.AddDays(i * 7).AddDays(6).Month, from.AddDays(i * 7).AddDays(6).WeekDayInstanceOfMonth());
                        var result = AggregateFluent.Group(WeekGroup4).FirstOrDefault();
                        data.Value = result == null ? 0 : result.GetValue("Value").ToDouble();
                        data.Year = to.AddDays(i * 7).Year;
                        list.Add(data);
                    }
                }
                else
                {
                    AggregateFluent = GetAggregateFluent(Type, "operateTime", beginTime, endTime);
                    Bson = AggregateFluent.Group(PresenterGroup).ToList();
                }
            }

            Bson.ForEach(x =>
            {
                var data = new RechargeTendencyDetailDto();
                data.Key = x.GetValue("_id").ToString();
                data.Value = x.GetValue("Value").ToDouble();
                data.Year = endTime.Year;
                list.Add(data);
            });
            return list.OrderBy(x => x.Year).ThenBy(x=>x.Key);
        }
        #endregion

        #region 动态添加筛选条件
        protected IAggregateFluent<BsonDocument> GetAggregateFluent(int Type, string TimeKey, DateTime beginTime, DateTime endTime)
        {
            var builderFilter = Builders<BsonDocument>.Filter;
            var AggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_spending_detail").Aggregate();

            return AggregateFluent.Match(builderFilter.And(builderFilter.Eq("refundStatus", "3"), builderFilter.In("payWay", new BsonArray(new List<string>() { "1", "2" })), builderFilter.Eq("type", "1"), builderFilter.Gte(TimeKey, beginTime), builderFilter.Lte(TimeKey, endTime)));
        }
        #endregion
    }
}
