using CarPlusGo.CVAS.Mobile.MemberAnalysis.Expenditure.Dto;
using CarPlusGo.CVAS.Mobile.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Extensions;
using System.Linq;
using CarPlusGo.CVAS.Mobile.TShareBank.Enum;

namespace CarPlusGo.CVAS.Mobile.MemberAnalysis.Expenditure
{
    public class ExpenditureAppService : IExpenditureAppService
    {
        private readonly IMongoDBRepository _mongoDBRepository;
        public ExpenditureAppService(IMongoDBRepository orderRepository)
        {
            _mongoDBRepository = orderRepository;
        }

        #region 消费概况
        public async Task<ExpenditureOverviewDto> ExpenditureOverview(ExpenditureOverviewResultRequestDto input)
        {
            var orderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status35", input.From.ToLocalTime().Date, input.To.ToLocalTime().ToDayEnd());
            var allOrderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status35", new DateTime(2019, 9, 6, 0, 0, 0, DateTimeKind.Local), DateTime.Now.ToLocalTime().ToDayEnd());
            var orderGroup = new BsonDocument
            {
                { "_id", 1 },
                { "UserCount", new BsonDocument("$addToSet", "$passengerId") },
                { "OrderCount", new BsonDocument("$sum", 1) },
                { "PayAmount", new BsonDocument("$sum", "$payAmount") },
                { "CashPayAmount",new BsonDocument("$sum","$amountPayment") }
            };
            var orderBson = await orderAggregateFluent.Group(orderGroup).FirstOrDefaultAsync();
            var allOrderBson = await allOrderAggregateFluent.Group(orderGroup).FirstOrDefaultAsync();
            return new ExpenditureOverviewDto
            {
                SearchResult = new ExpenditureDto
                {
                    UserCount = orderBson != null ? orderBson.GetValue("UserCount").AsBsonArray.LongCount() : 0,
                    OrderCount = orderBson != null ? orderBson.GetValue("OrderCount").ToInt64() : 0,
                    PayAmount = orderBson != null ? orderBson.GetValue("PayAmount").ToDecimal() : 0.00m,
                    CashPayAmount = orderBson != null ? orderBson.GetValue("CashPayAmount").ToDecimal() : 0.00m,
                },
                Total = new ExpenditureDto
                {
                    UserCount = allOrderBson != null ? allOrderBson.GetValue("UserCount").AsBsonArray.LongCount() : 0,
                    OrderCount = allOrderBson != null ? allOrderBson.GetValue("OrderCount").ToInt64() : 0,
                    PayAmount = allOrderBson != null ? allOrderBson.GetValue("PayAmount").ToDecimal() : 0.00m,
                    CashPayAmount = allOrderBson != null ? allOrderBson.GetValue("CashPayAmount").ToDecimal() : 0.00m,
                }
            };
        }

        protected IAggregateFluent<BsonDocument> GetOrderAggregateFluent(string timeKey, DateTime beginTime, DateTime endTime, params FilterDefinition<BsonDocument>[] otherFilterDefinitions)
        {
            var builderFilter = Builders<BsonDocument>.Filter;
            List<FilterDefinition<BsonDocument>> filterDefinitions = new List<FilterDefinition<BsonDocument>>();
            filterDefinitions.Add(builderFilter.Gte(timeKey, beginTime));
            filterDefinitions.Add(builderFilter.Lte(timeKey, endTime));
            if (otherFilterDefinitions.Length > 0)
            {
                filterDefinitions.AddRange(otherFilterDefinitions);
            }
            return _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_order").Aggregate()
                .Match(builderFilter.And(filterDefinitions));
        }
        #endregion


        #region 消费趋势
        public async Task<IEnumerable<ExpenditureDto>> ExpenditureTrend(ExpenditureTrendResultRequestDto input)
        {
            DateTime beginTime = DateTime.Now.AddDays(-7).Date;
            DateTime endTime = DateTime.Now.AddDays(-1).ToDayEnd();
            switch (input.Unit)
            {
                case Unit.Month:
                    beginTime = new DateTime(DateTime.Now.AddMonths(-6).Year, DateTime.Now.AddMonths(-6).Month, 1, 0, 0, 0, DateTimeKind.Local).Date;
                    endTime = new DateTime(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, DateTime.Now.AddMonths(-1).TotalDaysInMonth(), 0, 0, 0, DateTimeKind.Local).ToDayEnd();
                    return await GetExpenditureTrends("%Y-%m", beginTime, endTime);
                case Unit.Week:
                    beginTime = DateTime.Now.StartOfWeek(DayOfWeek.Friday).AddDays(-49);
                    endTime = DateTime.Now.StartOfWeek(DayOfWeek.Friday).AddDays(-1).ToDayEnd();
                    return await GetWeekExpenditureTrends(beginTime, endTime);
                case Unit.Day:
                default:
                    return await GetExpenditureTrends("%Y-%m-%d", beginTime, endTime);
            }
        }

        protected async Task<IOrderedEnumerable<ExpenditureDto>> GetWeekExpenditureTrends(DateTime beginTime, DateTime endTime)
        {
            var returnOrderTrendDetails = new List<ExpenditureDto>();
            var expenditureTrends = await GetExpenditureTrends("%Y-%m-%d", beginTime, endTime);
            for (int i = 0; i < expenditureTrends.Count();)
            {
                var weekBegin = Convert.ToDateTime(expenditureTrends.ElementAtOrDefault(i).Key).StartOfWeek(DayOfWeek.Friday);
                var weekList = expenditureTrends.Where(x => Convert.ToDateTime(x.Key) >= weekBegin && Convert.ToDateTime(x.Key) < weekBegin.AddDays(7));
                var orderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status35", weekBegin.ToLocalTime().Date, weekBegin.ToLocalTime().AddDays(6).ToDayEnd());
                var orderGroup = new BsonDocument
                {
                    { "_id", 1 },
                    { "UserCount", new BsonDocument("$addToSet", "$passengerId") }
                };
                var orderBson = await orderAggregateFluent.Group(orderGroup).FirstOrDefaultAsync();
                returnOrderTrendDetails.Add(new ExpenditureDto
                {
                    Year = weekBegin.AddDays(6).Year,
                    Key = string.Format(@"{0:D2}W{1}", weekBegin.AddDays(6).Month, weekBegin.AddDays(6).WeekDayInstanceOfMonth()),
                    OrderCount = weekList.Sum(x => x.OrderCount),
                    PayAmount = weekList.Sum(x => x.PayAmount),
                    CashPayAmount = weekList.Sum(x => x.CashPayAmount),
                    UserCount = orderBson != null ? orderBson.GetValue("UserCount").AsBsonArray.LongCount() : 0,
                });
                i += weekList.Count();
            }
            return returnOrderTrendDetails.OrderBy(x=>x.Year).ThenBy(x => x.Key);
        }

        protected async Task<IOrderedEnumerable<ExpenditureDto>> GetExpenditureTrends(string dateFormat, DateTime beginTime, DateTime endTime)
        {
            var expenditures = new List<ExpenditureDto>();
            var orderAggregateFluent = GetOrderAggregateFluent("updateTimeList.status35", beginTime.ToLocalTime().Date, endTime.ToLocalTime().ToDayEnd());
            var orderGroup = new BsonDocument
            {
                { "_id", new BsonDocument("$dateToString",new BsonDocument{
                    { "format",dateFormat },{ "date", "$updateTimeList.status35" },{ "timezone", "+08:00" }
                }) },
                { "UserCount", new BsonDocument("$addToSet", "$passengerId") },
                { "OrderCount", new BsonDocument("$sum", 1) },
                { "PayAmount", new BsonDocument("$sum", "$payAmount") },
                { "CashPayAmount",new BsonDocument("$sum","$amountPayment") }
            };
            var orderBson = await orderAggregateFluent.Group(orderGroup).ToListAsync();
            orderBson.ForEach(x =>
            {
                var expenditure = new ExpenditureDto();
                expenditure.Key = x.GetValue("_id").ToString();
                expenditure.UserCount = x.GetValue("UserCount").AsBsonArray.LongCount();
                expenditure.OrderCount = x.GetValue("OrderCount").ToInt64();
                expenditure.PayAmount = x.GetValue("PayAmount").ToDecimal();
                expenditure.CashPayAmount = x.GetValue("CashPayAmount").ToDecimal();
                expenditures.Add(expenditure);
            });
            return expenditures.OrderBy(x => x.Key);
        }
        #endregion

    }
}
