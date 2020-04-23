using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CarPlusGo.CVAS.Car;
using CarPlusGo.CVAS.Common;
using CarPlusGo.CVAS.LocationFile;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.UseCarApplyFile.Dto
{
    [AutoMap(typeof(UseCarApply))]
    public class UseCarApplyDto : FullAuditedEntityDto<long>
    {
        public long UseReason { get; set; }
        public ItemCode UserReasonData { get; set; }
        public long AreaID { get; set; }
        public Location Location { get; set; }
        public DateTime? PreUseDT { get; set; }
        public DateTime? PreReutrnDT { get; set; }
        public long PreClasen { get; set; }
        public Clasen Clasen { get; set; }
        public int? CC { get; set; }
        public int EmpID { get; set; }
        public string User { get; set; }
        public string Tel { get; set; }
        public string CustomerName { get; set; }
        public int Status { get; set; }
        public long RepositoryID { get; set; }
        public Repository Repository { get; set; }
        public string MakNo { get; set; }
        public long? CarBase { get; set; }
        public CarBase CarBaseData { get; set; }
        public string RejectMemo { get; set; }
        public DateTime? OutDT { get; set; }
        public int? OutKM { get; set; }
        public string Taker { get; set; }
        public string OutProvider { get; set; }
        public string OutMemo { get; set; }
        public DateTime? ReutrnDT { get; set; }
        public string Returner { get; set; }
        public string ReturnPervider { get; set; }
        public string ReturnMemo { get; set; }
        public string UseMemo { get; set; }
        public int ItemType { get; set; }
        public object ProposerData { get; set; }
        public object UserData { get; set; }
    }
}
