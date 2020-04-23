using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CarPlusGo.CVAS.Car;
using CarPlusGo.CVAS.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.CarFixFile.Dto
{
    [AutoMap(typeof(CarRepair))]
    public class CarRepairDto : FullAuditedEntityDto<long>
    {
        public long CarBaseAuto { get; set; }
        public CarBase CarBase { get; set; }
        public long OrderAuto { get; set; }
        public DateTime RepairDt { get; set; }
        public long RepairType { get; set; }
        public int Km { get; set; }
        public DateTime RepairDtpre { get; set; }
        public string RepairName { get; set; }
        public string ContactNumber { get; set; }
        public long PayMode { get; set; }
        public long SupplierAuto { get; set; }
        public Supplier Supplier { get; set; }
        public string SystemP { get; set; }
        public string RepairProblem { get; set; }
        public string OperatingItem { get; set; }
        public string RepairRecommendation { get; set; }
        public decimal EstimatedTimeFee { get; set; }
        public decimal EstimatedPartFee { get; set; }
        public decimal EstimatedTotalFee { get; set; }
        public int Status { get; set; }
        public DateTime? FinishDt { get; set; }
        public int AddStatus { get; set; }
        public long Cuser { get; set; }
        public DateTime Cdt { get; set; }
        public long? Muser { get; set; }
        public DateTime? Mdt { get; set; }
        public int RepairTypeItemType { get; set; }
        public ItemCode RepairTypeItemCode { get; set; }
        public int PayModeItemType { get; set; }
        public ItemCode PayModeItemCode { get; set; }
        public int CarFixType { get; set; }
    }
}
