using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace CarPlusGo.CVAS.Common.Dto
{
    [AutoMap(typeof(Inc))]
    public class CreateOrUpdateIncDto : EntityDto<long>
    {
        public long TradeItemAuto { get; set; }
        public string Fname { get; set; }
        public string Sname { get; set; }
        public string TaxCode { get; set; }
        public int CarTaxMode { get; set; }
        public string AccMemo { get; set; }
        public int Status { get; set; }
        public DateTime Cdt { get; set; }
        public int Cuser { get; set; }
        public DateTime Mdt { get; set; }
        public int Muser { get; set; }
        public string IncVirBankNo { get; set; }
        public string IncVirBankNm { get; set; }
        public string OldBankNo { get; set; }
        public string OldBankNm { get; set; }
        public string LicensePlate { get; set; }
        public string Eascode { get; set; }
        public int? IslimitedLicense { get; set; }
        public int? Area { get; set; }
        public string IncAddr { get; set; }
        public string IncTel { get; set; }
        public string IncFax { get; set; }
        public string CityCode { get; set; }
    }
}
