using CarPlusGo.CVAS.Mobile.MongoDB;
using CarPlusGo.CVAS.Mobile.TransportCapacity.OrderMap.Dto;
using CarPlusGo.CVAS.Mobile.TShareBank.Enum;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;

namespace CarPlusGo.CVAS.Mobile.TransportCapacity.OrderMap
{
    public class OrderMapAppService
        : IOrderMapAppService
    {
        private readonly IMongoDBRepository _mongoDBRepository;
        public OrderMapAppService(IMongoDBRepository orderRepository)
        {
            _mongoDBRepository = orderRepository;
        }

        public async Task<HeatDataDto> HeatData(HeatDataResultRequestDto input)
        {
            var cellCount = new int[] { input.SegmentationNumber, input.SegmentationNumber };
            var lngExtent = new double[] { 29.8, 30.8 };
            var latExtent = new double[] { 119.7, 120.6 };
            var lngCellSizeCoord = (lngExtent[1] - lngExtent[0]) / cellCount[0];
            var latCellSizeCoord = (latExtent[1] - latExtent[0]) / cellCount[1];
            var mapData = new List<int[]>();
            var rangeList = new List<int>();

            var builderFilter = Builders<BsonDocument>.Filter;
            List<FilterDefinition<BsonDocument>> filterDefinitions = new List<FilterDefinition<BsonDocument>>();
            CreateTimeFilter(input, builderFilter, filterDefinitions);
            CreateOrderTypeFilter(input, builderFilter, filterDefinitions);
            CreateOrderStatusFilter(input, builderFilter, filterDefinitions);

            var orderAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tab_order").Aggregate()
                .Match(builderFilter.And(filterDefinitions));

            orderAggregateFluent = PlaceTypeFilter(input, orderAggregateFluent);

            var orderBson = await GeoGroup(lngExtent, latExtent, lngCellSizeCoord, latCellSizeCoord, orderAggregateFluent);
            int max = GetMapDataAndMax(cellCount, mapData, orderBson);

            GetRangeList(rangeList, max);

            return new HeatDataDto
            {
                CellCount = cellCount,
                LngExtent = lngExtent,
                LatExtent = latExtent,
                MapData = mapData.ToArray(),
                RangeList = rangeList.ToArray(),
            };
        }

        private static void GetRangeList(List<int> rangeList, int max)
        {
            if (max < 10)
            {
                rangeList.AddRange(new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
                return;
            }
            var figures = Math.Abs(max).ToString().Length;
            var firstNumFigures = figures > 2 ? figures / 2 : 1;
            var firstNum = Convert.ToInt32(max.ToString().Substring(0, firstNumFigures));
            var secondNum = Convert.ToInt32(max.ToString().Substring(firstNumFigures, 1));
            var topNum = secondNum < 5 ? firstNum + "5" : (firstNum + 1) + "0";
            var maxStr = new StringBuilder(topNum);
            for (int i = 0; i < figures - firstNumFigures - 1; i++)
            {
                maxStr.Append('0');
            }
            var rangeMax = Convert.ToInt32(maxStr.ToString());
            rangeList.Add(1);
            for (int i = 1; i < 9; i++)
            {
                rangeList.Add(Convert.ToInt32(rangeMax * 0.125 * i));
            }
        }

        private static int GetMapDataAndMax(int[] cellCount, List<int[]> mapData, List<BsonDocument> orderBson)
        {
            int max = 0;
            for (int i = 0; i < cellCount[0]; i++)
            {
                for (int j = 0; j < cellCount[1]; j++)
                {
                    var element = orderBson.FirstOrDefault(o => o.GetValue("_id").ToBsonDocument().GetValue("lngIndex") == i && o.GetValue("_id").ToBsonDocument().GetValue("latIndex") == j);
                    var value = element != null ? element.GetValue("Count").ToInt32() : 0;
                    var point = new int[3] { i, j, value };
                    max = value > max ? value : max;
                    mapData.Add(point);
                }
            }

            return max;
        }

        private static async Task<List<BsonDocument>> GeoGroup(double[] lngExtent, double[] latExtent, double lngCellSizeCoord, double latCellSizeCoord, IAggregateFluent<BsonDocument> orderAggregateFluent)
        {
            var orderGroup = new BsonDocument
            {
                { "_id", new BsonDocument{
                    { "lngIndex", new BsonDocument
                    {
                        { "$floor", new BsonDocument
                        {
                            { "$divide",new BsonArray(new List<BsonValue>()
                            {
                                new BsonDocument
                                {
                                    { "$subtract", new BsonArray(new List<BsonValue>() { "$lat", latExtent[0] }) }
                                } , lngCellSizeCoord
                            }) }
                        } }
                    } },
                    { "latIndex",new BsonDocument
                    {
                        { "$floor", new BsonDocument
                        {
                            { "$divide",new BsonArray(new List<BsonValue>()
                            {
                                new BsonDocument
                                {
                                    { "$subtract", new BsonArray(new List<BsonValue>() { "$lng", lngExtent[0] }) }
                                } , latCellSizeCoord
                            }) }
                        } }
                    } },
                }},
                { "Count", new BsonDocument("$sum", 1) },
            };
            var orderBson = await orderAggregateFluent.Group(orderGroup).ToListAsync();
            return orderBson;
        }

        private static IAggregateFluent<BsonDocument> PlaceTypeFilter(HeatDataResultRequestDto input, IAggregateFluent<BsonDocument> orderAggregateFluent)
        {
            string poinitLocationName = "$startPoinitLocation";
            switch (input.PlaceType)
            {
                case InputPlaceType.ExpectedGetOnLocation:
                default:
                    break;
                case InputPlaceType.ExpectedGetOutLocation:
                    poinitLocationName = "$endPointLocation";
                    break;
                case InputPlaceType.ActualGetOnLocation:
                    poinitLocationName = "$startPoinitLocationReal";
                    break;
                case InputPlaceType.ActualGetOutLocation:
                    poinitLocationName = "$endPointLocationReal";
                    break;
            }

            orderAggregateFluent = orderAggregateFluent.Project(new BsonDocument {
                {"lat",new BsonDocument
                {
                    {
                        "$arrayElemAt",new BsonArray(new List<BsonValue>(){ poinitLocationName,0 })
                    }
                } },
                {"lng",new BsonDocument
                {
                    {
                        "$arrayElemAt",new BsonArray(new List<BsonValue>(){ poinitLocationName,1 })
                    }
                } },
            });
            return orderAggregateFluent;
        }

        private static void CreateOrderStatusFilter(HeatDataResultRequestDto input, FilterDefinitionBuilder<BsonDocument> builderFilter, List<FilterDefinition<BsonDocument>> filterDefinitions)
        {
            List<FilterDefinition<BsonDocument>> orderStatusFilterDefinitions = new List<FilterDefinition<BsonDocument>>();
            input.OrderStatus.ForEach(x =>
            {
                switch (x)
                {
                    case InputOrderStatus.TotalStatus:
                    default:
                        break;
                    case InputOrderStatus.CancelPlaceOrder:
                        orderStatusFilterDefinitions.Add(builderFilter.And(builderFilter.In("orderStatus", new List<int>() { 30, 40, 45 }), builderFilter.Exists("updateTimeList.status5", false)));
                        break;
                    case InputOrderStatus.CancelAcceptOrder:
                        orderStatusFilterDefinitions.Add(builderFilter.And(builderFilter.In("orderStatus", new List<int>() { 30, 40, 45 }), builderFilter.Exists("updateTimeList.status5", true)));
                        break;
                    case InputOrderStatus.CompletedOrder:
                        orderStatusFilterDefinitions.Add(builderFilter.Exists("updateTimeList.status20", true));
                        break;
                    case InputOrderStatus.ToBeCompletedOrder:
                        orderStatusFilterDefinitions.Add(builderFilter.And(builderFilter.Exists("updateTimeList.status20", false), builderFilter.Not(builderFilter.In("orderStatus", new List<int>() { 30, 40, 45 }))));
                        break;
                }
            });
            if (orderStatusFilterDefinitions.Count > 0)
            {
                filterDefinitions.Add(builderFilter.Or(orderStatusFilterDefinitions));
            }
        }

        private static void CreateOrderTypeFilter(HeatDataResultRequestDto input, FilterDefinitionBuilder<BsonDocument> builderFilter, List<FilterDefinition<BsonDocument>> filterDefinitions)
        {
            List<FilterDefinition<BsonDocument>> orderTypeFilterDefinitions = new List<FilterDefinition<BsonDocument>>();
            input.OrderType.ForEach(x =>
            {
                switch (x)
                {
                    case InputOrderType.TotalOrder:
                    default:
                        break;
                    case InputOrderType.ImmediateOrder:
                        orderTypeFilterDefinitions.Add(builderFilter.Eq("orderType", OrderType.Immediate));
                        break;
                    case InputOrderType.BookingOrder:
                        orderTypeFilterDefinitions.Add(builderFilter.And(builderFilter.Eq("orderType", OrderType.Booking), builderFilter.Eq("orderType2", BookingOrderType.Normal)));
                        break;
                    case InputOrderType.PickupOrder:
                        orderTypeFilterDefinitions.Add(builderFilter.Eq("orderType2", BookingOrderType.Pickup));
                        break;
                    case InputOrderType.DropoffOrder:
                        orderTypeFilterDefinitions.Add(builderFilter.Eq("orderType2", BookingOrderType.Dropoff));
                        break;
                }
            });
            if (orderTypeFilterDefinitions.Count > 0)
            {
                filterDefinitions.Add(builderFilter.Or(orderTypeFilterDefinitions));
            }
        }

        private static void CreateTimeFilter(HeatDataResultRequestDto input, FilterDefinitionBuilder<BsonDocument> builderFilter, List<FilterDefinition<BsonDocument>> filterDefinitions)
        {
            List<FilterDefinition<BsonDocument>> timeFilterDefinitions = new List<FilterDefinition<BsonDocument>>();
            input.Dates.ForEach(x =>
            {
                var date = x.ToLocalTime();
                timeFilterDefinitions.Add(builderFilter.And(new List<FilterDefinition<BsonDocument>>()
                {
                    builderFilter.Gte("updateTimeList.status1",new DateTime(date.Year, date.Month, date.Day, input.Times[0].Hour, input.Times[0].Minute, input.Times[0].Second, DateTimeKind.Local)),
                    builderFilter.Lte("updateTimeList.status1",new DateTime(date.Year, date.Month, date.Day, input.Times[1].Hour, input.Times[1].Minute, input.Times[1].Second, DateTimeKind.Local)),
                }));
            });
            if (timeFilterDefinitions.Count > 0)
            {
                filterDefinitions.Add(builderFilter.Or(timeFilterDefinitions));
            }
        }
    }
}
