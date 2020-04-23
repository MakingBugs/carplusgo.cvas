using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CarPlusGo.CVAS.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Insure.Dto
{
    [AutoMap(typeof(Insure2))]
    public class Insure2Dto : FullAuditedEntityDto<long>
    {
        public long OrderAuto { get; set; }
        public long CarBaseAuto { get; set; }
        public long Supplier { get; set; }
        public string InsureNo { get; set; }
        public decimal GetPrice { get; set; }
        public decimal InsureDisCount { get; set; }
        public decimal CarTax { get; set; }
        public decimal OrgAmt { get; set; }
        public decimal RateAmt { get; set; }
        public decimal FinalAmt { get; set; }
        public decimal RealAmt { get; set; }
        public decimal PayAmt { get; set; }
        public DateTime? StartDt { get; set; }
        public DateTime? EndDt { get; set; }
        public DateTime? RescissionDt { get; set; }
        public int IsBusiness { get; set; }
        public int Year { get; set; }
        public int InsIsRebates { get; set; }
        public string InsRate { get; set; }
        public decimal InsReAmt { get; set; }
        public string InsRemark { get; set; }
        public int InsDisRebates { get; set; }
        public string InsDrate { get; set; }
        public decimal InsDreAmt { get; set; }
        public string InsDremark { get; set; }
        public int Status { get; set; }
        public int Cuser { get; set; }
        public DateTime Cdt { get; set; }
        public int Muser { get; set; }
        public DateTime Mdt { get; set; }
        public long Batch { get; set; }
        public int? TrnType { get; set; }
        public DateTime? TrnDate { get; set; }
        public decimal? InsureLimit { get; set; }
        public int? InsureType { get; set; }
        public int? InsurePercnt { get; set; }
        public decimal? DetailAmt { get; set; }
        public decimal? OfferAmt { get; set; }
        public string InsPairate { get; set; }
        public Supplier SupplierData { get; set; }
    }
}
