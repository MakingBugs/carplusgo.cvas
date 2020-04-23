using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CarPlusGo.CVAS.Car;
using CarPlusGo.CVAS.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.CarFixFile.Dto
{
    [AutoMap(typeof(CarFix))]
    public class CarFixDto : FullAuditedEntityDto<long>
    {
        public long CarBaseAuto { get; set; }
        public CarBase CarBase { get; set; }
        public long OrderAuto { get; set; }
        public long SupplierAuto { get; set; }
        public Supplier Supplier { get; set; }
        public string CarFixNo { get; set; }
        public string MakNo { get; set; }
        public string CustName { get; set; }
        public DateTime FixDt { get; set; }
        public DateTime FixDtpre { get; set; }
        public DateTime FixDtreal { get; set; }
        public int Km { get; set; }
        public int FixType { get; set; }
        public int ItemCodeFixType { get; set; }
        public ItemCode ItemCodeFixTypeData { get; set; }
        public int MainTainKm { get; set; }
        public decimal Whamount { get; set; }
        public decimal WhdisCount { get; set; }
        public decimal ItemAmount { get; set; }
        public decimal ItemDisCount { get; set; }
        public decimal RealAmount { get; set; }
        public int Status { get; set; }
        public int ItemCodeStatus { get; set; }
        public ItemCode ItemCodeStatusData { get; set; }
        public DateTime Cdt { get; set; }
        public int Muser { get; set; }
        public DateTime Mdt { get; set; }
        public long? AccBankAuto { get; set; }
        public AccBank AccBank { get; set; }
        public long? CarFixBatchAuto { get; set; }
        public CarFixBatch CarFixBatch { get; set; }
        public string Remark { get; set; }
        public long? CarFixBatchTno { get; set; }
        public int? PayMode { get; set; }
        public int? CarRepairAuto { get; set; }
        public int? NextMaintainKm { get; set; }
        public DateTime? NextMaintainDt { get; set; }
        public DateTime? PreNextMaintainDt { get; set; }

        public List<CarFixItemDto> CarFixItem { get; set; }
        public List<PRInvLink> PRInvLink { get; set; }
    }
}
