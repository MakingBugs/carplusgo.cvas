using CarPlusGo.CVAS.Mobile.MongoDB;
using CarPlusGo.CVAS.Mobile.TransportCapacity.ArriveSpendTime.Dto;
using MongoDB.Bson;
using MongoDB.Driver;
using Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarPlusGo.CVAS.Mobile.TransportCapacity.ArriveSpendTime
{
    public class ArriveSpendTimeAppService: IArriveSpendTimeAppService
    {
        private readonly IMongoDBRepository _mongoDBRepository;
        public ArriveSpendTimeAppService(IMongoDBRepository orderRepository)
        {
            _mongoDBRepository = orderRepository;
        }
        public Blanket2Dto Blanket(Blanket2ResultRequestDto input)
        {
            var builderFilter = Builders<BsonDocument>.Filter;

            #region 订单成功预估时间
            Dictionary<string, object> SuccessForecastTimeDictionary = new Dictionary<string, object>();
            var SuccessForecastTimeGroup = new BsonDocument
            {
                { "_id", 1 },
                { "AvgSpendTime", new BsonDocument("$avg","$forecastTime") },
                { "OrderCount", new BsonDocument("$sum", 1) }
            };
            var SuccessForecastTimeAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_order").Aggregate()
                .Match(builderFilter.And(builderFilter.Exists("updateTimeList.status20", true),builderFilter.Gte("updateTimeList.status1", input.From.ToLocalTime().Date), builderFilter.Lte("updateTimeList.status1", input.To.ToLocalTime().ToDayEnd())));
            var SuccessForecastTimeBsonDate = SuccessForecastTimeAggregateFluent.Group(SuccessForecastTimeGroup).FirstOrDefault();
            var SuccessForecastTimeBson = SuccessForecastTimeBsonDate == null ? 0 : SuccessForecastTimeBsonDate.GetValue("AvgSpendTime");
            return new Blanket2Dto
            {

            };
            #endregion
        }
    }
}
