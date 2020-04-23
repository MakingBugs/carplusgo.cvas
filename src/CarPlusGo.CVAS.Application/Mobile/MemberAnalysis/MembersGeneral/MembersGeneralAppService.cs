using CarPlusGo.CVAS.Mobile.MemberAnalysis.MembersGeneral.Dto;
using CarPlusGo.CVAS.Mobile.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using Abp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using CarPlusGo.CVAS.Mobile.TShareBank;

namespace CarPlusGo.CVAS.Mobile.MemberAnalysis.MembersGeneral
{
    public class MembersGeneralAppService:IMembersGeneralAppService
    {
        private readonly IMongoDBRepository _mongoDBRepository;
        private readonly IRepository<UmengApiData, long> _umengApiDataRepository;
        public MembersGeneralAppService(IMongoDBRepository orderRepository, IRepository<UmengApiData, long> umengApiDataRepository)
        {
            _mongoDBRepository = orderRepository;
            _umengApiDataRepository = umengApiDataRepository;
        }

        #region 用户概况
        public MembersGeneralDto MembersGeneral(MembersGeneralResultRequestDto input)
        {
            var builderFilter = Builders<BsonDocument>.Filter;
            var subDate = input.To.Subtract(input.From).Days + 1;//所选时间段的天数
            var Group = new BsonDocument
            {
                { "_id", "$uid" },
                { "Count", new BsonDocument("$sum", 1) }
            };
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
            #region 新增下载量
            var NewDownloadsApiDataQuery = _umengApiDataRepository.GetAll().Where(x => x.Date >= input.From.ToLocalTime().Date && x.Date <= input.To.ToLocalTime().ToDayEnd());
            var NewDownloads = NewDownloadsApiDataQuery.Count() > 0 ? NewDownloadsApiDataQuery.Sum(x => x.TotalInstallUser) : 0;
            #endregion

            #region 日均活跃用户数
            var umengApiDataQuery = _umengApiDataRepository.GetAll().Where(x => x.Date >= input.From.ToLocalTime().Date && x.Date <= input.To.ToLocalTime().ToDayEnd());
            var activeUser = umengApiDataQuery.Count() > 0 ? umengApiDataQuery.Average(x => x.UniqActiveUsers) : 0;

            //var ActiveUersAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("login_history").Aggregate()
            //    .Match(builderFilter.And(builderFilter.Gte("createdTime", input.From.ToLocalTime().Date), builderFilter.Lte("createdTime", input.To.ToLocalTime().ToDayEnd()),builderFilter.Eq("status",1),builderFilter.Eq("role", "mobile")));
            //var ActiveUersBsonDate = ActiveUersAggregateFluent.Group(Group).Group(Group2).FirstOrDefault();
            //var ActiveUers = ActiveUersBsonDate == null ? 0 : ActiveUersBsonDate.GetValue("Count");
            #endregion

            #region 新增注册用户数
            var NewRegisterUsersAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_user").Aggregate()
               .Match(builderFilter.And(builderFilter.Gte("createTime", input.From.ToLocalTime().Date), builderFilter.Lte("createTime", input.To.ToLocalTime().ToDayEnd()),builderFilter.Eq("roles", "mobile")));
            var NewRegisterUsersBsonDate = NewRegisterUsersAggregateFluent.Group(Group2).FirstOrDefault();
            var NewRegisterUsers = NewRegisterUsersBsonDate == null ? 0 : NewRegisterUsersBsonDate.GetValue("Count");
            #endregion

            #region 新增充值用户数
            var NewRechargeUsersAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_spending_detail").Aggregate()
               .Match(builderFilter.And(builderFilter.Eq("refundStatus", "3"), builderFilter.Ne("payWay", "6"), builderFilter.Ne("payWay", "10"), builderFilter.Lt("operateTime", input.From.ToLocalTime().Date)));
            var NewRechargeUsersBsonDate = NewRechargeUsersAggregateFluent.Group(Group1).Group(Group2).FirstOrDefault();
            var NewRechargeUsers = NewRechargeUsersBsonDate == null ? 0 : NewRechargeUsersBsonDate.GetValue("Count");
            #endregion

            #region 新增消费用户数
            var NewConsumeUsersAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_spending_detail").Aggregate()
              .Match(builderFilter.And(builderFilter.Eq("type", "8"), builderFilter.Lt("operateTime", input.From.ToLocalTime().Date)));
            var NewConsumeUsersBsonDate = NewConsumeUsersAggregateFluent.Group(Group1).Group(Group2).FirstOrDefault();
            var NewConsumeUsers = NewConsumeUsersBsonDate == null ? 0 : NewConsumeUsersBsonDate.GetValue("Count");
            #endregion

            #region 累计下载量
            var TotalDownloadsApiDataQuery = _umengApiDataRepository.GetAll().Where(x => x.Date <= input.To.ToLocalTime().ToDayEnd());
            var TotalDownloads = TotalDownloadsApiDataQuery.Count() > 0 ? TotalDownloadsApiDataQuery.Sum(x => x.TotalInstallUser) : 0;
            #endregion

            #region 累计注册用户数
            var TotalRegisterUsersAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_user").Aggregate()
               .Match(builderFilter.And(builderFilter.Lte("createTime", input.To.ToLocalTime().ToDayEnd()), builderFilter.Eq("roles", "mobile")));
            var TotalRegisterUsersBsonDate = TotalRegisterUsersAggregateFluent.Group(Group2).FirstOrDefault();
            var TotalRegisterUsers = TotalRegisterUsersBsonDate == null ? 0 : TotalRegisterUsersBsonDate.GetValue("Count");
            #endregion

            #region 累计充值用户数
            var TotalRechargeUsersAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_spending_detail").Aggregate()
               .Match(builderFilter.And(builderFilter.Eq("refundStatus","3"), builderFilter.Ne("payWay", "6"), builderFilter.Ne("payWay", "10"), builderFilter.Lte("operateTime", input.To.ToLocalTime().ToDayEnd())));
            var TotalRechargeUsersBsonDate = TotalRechargeUsersAggregateFluent.Group(Group1).Group(Group2).FirstOrDefault();
            var TotalRechargeUsers = TotalRechargeUsersBsonDate == null ? 0 : TotalRechargeUsersBsonDate.GetValue("Count");
            #endregion

            #region 累计消费用户数
            var TotalConsumeUsersAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_spending_detail").Aggregate()
              .Match(builderFilter.And(builderFilter.Eq("type", "8"), builderFilter.Lte("operateTime", input.To.ToLocalTime().ToDayEnd())));
            var TotalConsumeUsersBsonDate = TotalConsumeUsersAggregateFluent.Group(Group1).Group(Group2).FirstOrDefault();
            var TotalConsumeUsers = TotalConsumeUsersBsonDate == null ? 0 : TotalConsumeUsersBsonDate.GetValue("Count");
            #endregion

            return new MembersGeneralDto
            {
                ActiveUers = Convert.ToInt32(activeUser), //ActiveUers.ToInt32()/ subDate,
                NewDownloads = Convert.ToInt32(NewDownloads),
                NewRegisterUsers = NewRegisterUsers.ToInt32(),
                NewRechargeUsers = TotalRechargeUsers.ToInt32() - NewRechargeUsers.ToInt32(),
                NewConsumeUsers = TotalConsumeUsers.ToInt32() - NewConsumeUsers.ToInt32(),
                TotalDownloads = Convert.ToInt32(TotalDownloads),
                TotalRegisterUsers = TotalRegisterUsers.ToInt32(),
                TotalRechargeUsers= TotalRechargeUsers.ToInt32(),
                TotalConsumeUsers= TotalConsumeUsers.ToInt32()
            };
        }
        #endregion

        #region 用户增长趋势-接口
        public MembersRisingTendencyDto MembersRisingTendency(MembersRisingTendencyResultRequestDto input)
        {
            var data = new MembersRisingTendencyDto();
            foreach (var i in input.Types)
            {
                switch (i)
                {
                    case 1:
                        data.ActiveUers = GetOrderResult(i, input.Period);
                        break;
                    case 2:
                        data.NewRegisterUsers = GetOrderResult(i, input.Period);
                        break;
                    case 3:
                        data.NewRechargeUsers = GetOrderResult(i, input.Period);
                        break;
                    case 4:
                        data.NewConsumeUsers = GetOrderResult(i, input.Period);
                        break;
                    case 5:
                        data.NewDownloads = GetOrderResult(i, input.Period);
                        break;
                }
            }
            return data;
        }
        #endregion

        #region 动态周期
        protected IOrderedEnumerable<MembersRisingTendencyDetailDto> GetOrderResult(int Type, int Period)
        {
            DateTime beginTime = DateTime.Now.AddDays(-7).Date;
            DateTime endTime = DateTime.Now.AddDays(-1).ToDayEnd();
            switch (Period)
            {
                case 3:
                    beginTime = (new DateTime(DateTime.Now.AddMonths(-6).Year, DateTime.Now.AddMonths(-6).Month, 1)).Date;
                    endTime = (new DateTime(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, DateTime.Now.AddMonths(-1).TotalDaysInMonth())).ToDayEnd();
                    return Group(Type, "%Y-%m", beginTime, endTime, Period);
                case 2:
                    beginTime = DateTime.Now.StartOfWeek(DayOfWeek.Friday).AddDays(-49).Date;
                    endTime = DateTime.Now.StartOfWeek(DayOfWeek.Friday).AddDays(-1).Date;
                    return Group(Type, "%Y-%m-%d", beginTime, endTime, Period);
                case 1:
                default:
                    return Group(Type, "%Y-%m-%d", beginTime, endTime, Period);
            }
        }
        #endregion

        #region 动态创建Group对象
        protected IOrderedEnumerable<MembersRisingTendencyDetailDto> Group(int Type, string dateFormat, DateTime beginTime, DateTime endTime,int Period)
        {
            var builderFilter = Builders<BsonDocument>.Filter;
            var Bson = new List<BsonDocument>();
            IAggregateFluent<BsonDocument> AggregateFluent;
            Dictionary<string, object> dateToStringKeyValuePairs = new Dictionary<string, object>();
            dateToStringKeyValuePairs.Add("format", dateFormat);
            if (Type==1) dateToStringKeyValuePairs.Add("date", "$createdTime");
            else dateToStringKeyValuePairs.Add("date", "$createTime");
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
                    { "UserId","$uid"}
                } },
                { "Value", new BsonDocument("$sum", 1) }
            };
            var Group2 = new BsonDocument
            {
                { "_id", "$_id.Day"},
                { "Value", new BsonDocument("$sum", 1) }
            };
            var Group3 = new BsonDocument
            {
                { "_id", ""},
                { "Value", new BsonDocument("$sum", 1) }
            };
            var Group5 = new BsonDocument
            {
                { "_id", "$userId" },
                { "Count", new BsonDocument("$sum", 1) }
            };
            var Group6 = new BsonDocument
            {
                { "_id", 1 },
                { "Count", new BsonDocument("$sum", 1) }
            };

            var list = new List<MembersRisingTendencyDetailDto>();

            switch (Type)
            {
                case 1:
                    IQueryable<UmengApiData> umengApiDataQuery;
                    Double activeUser;
                    if (Period == 1)
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            var data = new MembersRisingTendencyDetailDto();
                            var from = beginTime.AddDays(i);
                            var to = beginTime.AddDays(i).ToDayEnd();
                            umengApiDataQuery = _umengApiDataRepository.GetAll().Where(x => x.Date >= from && x.Date <= to);
                            activeUser = umengApiDataQuery.Count() > 0 ? umengApiDataQuery.Average(x => x.UniqActiveUsers) : 0;
                            data.Key= from.ToString("yyyy-MM-dd");
                            data.Value = activeUser;
                            data.Year = to.Year;
                            list.Add(data);
                        }
                    }
                    else if (Period == 2)
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            var data = new MembersRisingTendencyDetailDto();
                            var from = beginTime.AddDays(i * 7);
                            var to = beginTime.AddDays(6).AddDays(i * 7).ToDayEnd();
                            umengApiDataQuery = _umengApiDataRepository.GetAll().Where(x => x.Date >= from && x.Date <= to);
                            activeUser = umengApiDataQuery.Count() > 0 ? umengApiDataQuery.Average(x => x.UniqActiveUsers) : 0;
                            data.Key = string.Format(@"{0:D2}W{1}", from.AddDays(6).Month, from.AddDays(6).WeekDayInstanceOfMonth());
                            data.Value = activeUser;
                            data.Year = to.Year;
                            list.Add(data);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            var data = new MembersRisingTendencyDetailDto();
                            var from = beginTime.AddMonths(i);
                            var to = (new DateTime(beginTime.Year, beginTime.Month, beginTime.TotalDaysInMonth())).ToDayEnd().AddMonths(i);
                            umengApiDataQuery = _umengApiDataRepository.GetAll().Where(x => x.Date >= from && x.Date <= to);
                            activeUser = umengApiDataQuery.Count() > 0 ? umengApiDataQuery.Average(x => x.UniqActiveUsers) : 0;
                            data.Key = from.AddMonths(i).ToString("yyyy-MM");
                            data.Value = activeUser;
                            data.Year = to.Year;
                            list.Add(data);
                        }
                    }
                    break;
                case 2:
                    if (Period == 2)
                    {
                        for (int i = 1; i < 7; i++)
                        {
                            var data = new MembersRisingTendencyDetailDto();
                            var from = beginTime.AddDays(i * 7);
                            var to = beginTime.AddDays(6).AddDays(i * 7).ToDayEnd();
                            AggregateFluent = GetAggregateFluent(Type, "createTime", from, to);
                            data.Key = string.Format(@"{0:D2}W{1}", from.AddDays(6).Month, from.AddDays(6).WeekDayInstanceOfMonth());
                            var result = AggregateFluent.Group(Group3).FirstOrDefault();
                            data.Value = result == null ? 0 : result.GetValue("Value").ToDouble();
                            data.Year = to.Year;
                            list.Add(data);
                        }
                    }
                    else
                    {
                        AggregateFluent = GetAggregateFluent(Type, "createTime", beginTime, endTime);
                        Bson = AggregateFluent.Group(Group).ToList();
                    }
                    break;
                case 3:
                    if (Period==1)
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            var data = new MembersRisingTendencyDetailDto();
                            var from = beginTime.AddDays(i);
                            var to = beginTime.AddDays(i).ToDayEnd();
                            var NewRechargeUsersAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_spending_detail").Aggregate()
                            .Match(builderFilter.And(builderFilter.Eq("refundStatus", "3"), builderFilter.Ne("payWay", "6"), builderFilter.Ne("payWay", "10"), builderFilter.Lt("operateTime", from)));
                            var NewRechargeUsersBsonDate = NewRechargeUsersAggregateFluent.Group(Group5).Group(Group6).FirstOrDefault();
                            var NewRechargeUsers = NewRechargeUsersBsonDate == null ? 0 : NewRechargeUsersBsonDate.GetValue("Count");

                            var TotalRechargeUsersAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_spending_detail").Aggregate()
                            .Match(builderFilter.And(builderFilter.Eq("refundStatus", "3"), builderFilter.Ne("payWay", "6"), builderFilter.Ne("payWay", "10"), builderFilter.Lte("operateTime", to)));
                            var TotalRechargeUsersBsonDate = TotalRechargeUsersAggregateFluent.Group(Group5).Group(Group6).FirstOrDefault();
                            var TotalRechargeUsers = TotalRechargeUsersBsonDate == null ? 0 : TotalRechargeUsersBsonDate.GetValue("Count");
                            data.Key = from.ToString("yyyy-MM-dd");
                            data.Value = TotalRechargeUsers.ToInt32() - NewRechargeUsers.ToInt32();
                            data.Year = to.Year;
                            list.Add(data);
                        }
                        
                    }
                    else if (Period == 2)
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            var data = new MembersRisingTendencyDetailDto();
                            var from = beginTime.AddDays(i*7);
                            var to = beginTime.AddDays(6).AddDays(i*7).ToDayEnd();
                            var NewRechargeUsersAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_spending_detail").Aggregate()
                            .Match(builderFilter.And(builderFilter.Eq("refundStatus", "3"), builderFilter.Ne("payWay", "6"), builderFilter.Ne("payWay", "10"), builderFilter.Lt("operateTime", from)));
                            var NewRechargeUsersBsonDate = NewRechargeUsersAggregateFluent.Group(Group5).Group(Group6).FirstOrDefault();
                            var NewRechargeUsers = NewRechargeUsersBsonDate == null ? 0 : NewRechargeUsersBsonDate.GetValue("Count");

                            var TotalRechargeUsersAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_spending_detail").Aggregate()
                            .Match(builderFilter.And(builderFilter.Eq("refundStatus", "3"), builderFilter.Ne("payWay", "6"), builderFilter.Ne("payWay", "10"), builderFilter.Lte("operateTime", to)));
                            var TotalRechargeUsersBsonDate = TotalRechargeUsersAggregateFluent.Group(Group5).Group(Group6).FirstOrDefault();
                            var TotalRechargeUsers = TotalRechargeUsersBsonDate == null ? 0 : TotalRechargeUsersBsonDate.GetValue("Count");
                            data.Key = string.Format(@"{0:D2}W{1}", from.AddDays(6).Month, from.AddDays(6).WeekDayInstanceOfMonth());
                            data.Value = TotalRechargeUsers.ToInt32() - NewRechargeUsers.ToInt32();
                            data.Year = to.Year;
                            list.Add(data);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            var data = new MembersRisingTendencyDetailDto();
                            var from = beginTime.AddMonths(i);
                            var to = (new DateTime(beginTime.Year, beginTime.Month, beginTime.TotalDaysInMonth())).ToDayEnd().AddMonths(i);

                            var NewRechargeUsersAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_spending_detail").Aggregate()
                            .Match(builderFilter.And(builderFilter.Eq("refundStatus", "3"), builderFilter.Ne("payWay", "6"), builderFilter.Ne("payWay", "10"), builderFilter.Lt("operateTime", from)));
                            var NewRechargeUsersBsonDate = NewRechargeUsersAggregateFluent.Group(Group5).Group(Group6).FirstOrDefault();
                            var NewRechargeUsers = NewRechargeUsersBsonDate == null ? 0 : NewRechargeUsersBsonDate.GetValue("Count");

                            var TotalRechargeUsersAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_spending_detail").Aggregate()
                            .Match(builderFilter.And(builderFilter.Eq("refundStatus", "3"), builderFilter.Ne("payWay", "6"), builderFilter.Ne("payWay", "10"), builderFilter.Lte("operateTime", to)));
                            var TotalRechargeUsersBsonDate = TotalRechargeUsersAggregateFluent.Group(Group5).Group(Group6).FirstOrDefault();
                            var TotalRechargeUsers = TotalRechargeUsersBsonDate == null ? 0 : TotalRechargeUsersBsonDate.GetValue("Count");
                            data.Key = from.ToString("yyyy-MM");
                            data.Value = TotalRechargeUsers.ToInt32() - NewRechargeUsers.ToInt32();
                            data.Year = to.Year;
                            list.Add(data);
                        }
                    }
                    break;
                case 4:
                    if (Period == 1)
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            var data = new MembersRisingTendencyDetailDto();
                            var from = beginTime.AddDays(i);
                            var to = beginTime.AddDays(i).ToDayEnd();
                            var NewConsumeUsersAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_spending_detail").Aggregate()
                            .Match(builderFilter.And(builderFilter.Eq("type", "8"), builderFilter.Lt("operateTime", from)));
                            var NewConsumeUsersBsonDate = NewConsumeUsersAggregateFluent.Group(Group5).Group(Group6).FirstOrDefault();
                            var NewConsumeUsers = NewConsumeUsersBsonDate == null ? 0 : NewConsumeUsersBsonDate.GetValue("Count");

                            var TotalConsumeUsersAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_spending_detail").Aggregate()
                            .Match(builderFilter.And(builderFilter.Eq("type", "8"), builderFilter.Lte("operateTime", to)));
                            var TotalConsumeUsersBsonDate = TotalConsumeUsersAggregateFluent.Group(Group5).Group(Group6).FirstOrDefault();
                            var TotalConsumeUsers = TotalConsumeUsersBsonDate == null ? 0 : TotalConsumeUsersBsonDate.GetValue("Count");
                            data.Key = from.ToString("yyyy-MM-dd");
                            data.Value = TotalConsumeUsers.ToInt32() - NewConsumeUsers.ToInt32();
                            data.Year = to.Year;
                            list.Add(data);
                        }

                    }
                    else if (Period == 2)
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            var data = new MembersRisingTendencyDetailDto();
                            var from = beginTime.AddDays(i * 7);
                            var to = beginTime.AddDays(6).AddDays(i * 7).ToDayEnd();
                            var NewConsumeUsersAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_spending_detail").Aggregate()
                            .Match(builderFilter.And(builderFilter.Eq("type", "8"), builderFilter.Lt("operateTime", from)));
                            var NewConsumeUsersBsonDate = NewConsumeUsersAggregateFluent.Group(Group5).Group(Group6).FirstOrDefault();
                            var NewConsumeUsers = NewConsumeUsersBsonDate == null ? 0 : NewConsumeUsersBsonDate.GetValue("Count");

                            var TotalConsumeUsersAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_spending_detail").Aggregate()
                            .Match(builderFilter.And(builderFilter.Eq("type", "8"), builderFilter.Lte("operateTime", to)));
                            var TotalConsumeUsersBsonDate = TotalConsumeUsersAggregateFluent.Group(Group5).Group(Group6).FirstOrDefault();
                            var TotalConsumeUsers = TotalConsumeUsersBsonDate == null ? 0 : TotalConsumeUsersBsonDate.GetValue("Count");
                            data.Key = string.Format(@"{0:D2}W{1}", from.AddDays(6).Month, from.AddDays(6).WeekDayInstanceOfMonth());
                            data.Value = TotalConsumeUsers.ToInt32() - NewConsumeUsers.ToInt32();
                            data.Year = to.Year;
                            list.Add(data);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            var data = new MembersRisingTendencyDetailDto();
                            var from = beginTime.AddMonths(i);
                            var to = (new DateTime(beginTime.Year, beginTime.Month, beginTime.TotalDaysInMonth())).ToDayEnd().AddMonths(i);
                            var NewConsumeUsersAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_spending_detail").Aggregate()
                            .Match(builderFilter.And(builderFilter.Eq("type", "8"), builderFilter.Lt("operateTime", from)));
                            var NewConsumeUsersBsonDate = NewConsumeUsersAggregateFluent.Group(Group5).Group(Group6).FirstOrDefault();
                            var NewConsumeUsers = NewConsumeUsersBsonDate == null ? 0 : NewConsumeUsersBsonDate.GetValue("Count");

                            var TotalConsumeUsersAggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_spending_detail").Aggregate()
                            .Match(builderFilter.And(builderFilter.Eq("type", "8"), builderFilter.Lte("operateTime", to)));
                            var TotalConsumeUsersBsonDate = TotalConsumeUsersAggregateFluent.Group(Group5).Group(Group6).FirstOrDefault();
                            var TotalConsumeUsers = TotalConsumeUsersBsonDate == null ? 0 : TotalConsumeUsersBsonDate.GetValue("Count");
                            data.Key = from.ToString("yyyy-MM");
                            data.Value = TotalConsumeUsers.ToInt32() - NewConsumeUsers.ToInt32();
                            data.Year = to.Year;
                            list.Add(data);
                        }
                    }
                    break;
                case 5:
                    IQueryable<UmengApiData> umengApiDataQuery2;
                    Double NewDownloads;
                    if (Period == 1)
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            var data = new MembersRisingTendencyDetailDto();
                            var from = beginTime.AddDays(i);
                            var to = beginTime.AddDays(i).ToDayEnd();
                            umengApiDataQuery2 = _umengApiDataRepository.GetAll().Where(x => x.Date >= from && x.Date <= to);
                            NewDownloads = umengApiDataQuery2.Count() > 0 ? umengApiDataQuery2.Sum(x => x.TotalInstallUser) : 0;
                            data.Key = from.ToString("yyyy-MM-dd");
                            data.Value = NewDownloads;
                            data.Year = to.Year;
                            list.Add(data);
                        }
                    }
                    else if (Period == 2)
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            var data = new MembersRisingTendencyDetailDto();
                            var from = beginTime.AddDays(i * 7);
                            var to = beginTime.AddDays(6).AddDays(i * 7).ToDayEnd();
                            umengApiDataQuery2 = _umengApiDataRepository.GetAll().Where(x => x.Date >= from && x.Date <= to);
                            NewDownloads = umengApiDataQuery2.Count() > 0 ? umengApiDataQuery2.Sum(x => x.TotalInstallUser) : 0;
                            data.Key = string.Format(@"{0:D2}W{1}", from.AddDays(6).Month, from.AddDays(6).WeekDayInstanceOfMonth());
                            data.Value = NewDownloads;
                            data.Year = to.Year;
                            list.Add(data);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            var data = new MembersRisingTendencyDetailDto();
                            var from = beginTime.AddMonths(i);
                            var to = (new DateTime(beginTime.Year, beginTime.Month, beginTime.TotalDaysInMonth())).ToDayEnd().AddMonths(i);
                            umengApiDataQuery2 = _umengApiDataRepository.GetAll().Where(x => x.Date >= from && x.Date <= to);
                            NewDownloads = umengApiDataQuery2.Count() > 0 ? umengApiDataQuery2.Sum(x => x.TotalInstallUser) : 0;
                            data.Key = from.ToString("yyyy-MM");
                            data.Value = NewDownloads;
                            data.Year = to.Year;
                            list.Add(data);
                        }
                    }
                    break;
            }

            Bson.ForEach(x =>
            {
                var data = new MembersRisingTendencyDetailDto();
                data.Key = x.GetValue("_id").ToString();
                data.Value = x.GetValue("Value").ToDouble();
                data.Year = endTime.Year;
                list.Add(data);
            });
            return list.OrderBy(x=>x.Year).ThenBy(x => x.Key);
        }
        #endregion

        #region 动态添加筛选条件
        protected IAggregateFluent<BsonDocument> GetAggregateFluent(int Type, string TimeKey, DateTime beginTime, DateTime endTime)
        {
            var builderFilter = Builders<BsonDocument>.Filter;
            var AggregateFluent = _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_spending_detail").Aggregate();
            if (Type==1)
            {
                AggregateFluent=_mongoDBRepository.Database.GetCollection<BsonDocument>("login_history").Aggregate()
                .Match(builderFilter.And(builderFilter.Gte(TimeKey, beginTime), builderFilter.Lte(TimeKey, endTime), builderFilter.Eq("status", 1), builderFilter.Eq("role", "mobile")));
            }
            else
            {
                AggregateFluent= _mongoDBRepository.Database.GetCollection<BsonDocument>("tshare_user").Aggregate().Match(builderFilter.And(builderFilter.Gte(TimeKey, beginTime), builderFilter.Lte(TimeKey, endTime), builderFilter.Eq("roles", "mobile")));
            }
            return AggregateFluent;
        }
        #endregion
    }
}
