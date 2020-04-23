using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace CarPlusGo.CVAS.Common.Dto
{
    [AutoMap(typeof(Customer))]
    public class CustomerDto : EntityDto<long>
    {
        public long TradeItemAuto { get; set; }
        public string CustId { get; set; }
        public int CustType { get; set; }
        public int IncType { get; set; }
        public string BossName { get; set; }
        public int BossIdtype { get; set; }
        public string BossFid { get; set; }
        public DateTime? BossBirthday { get; set; }
        public long SalesAuto { get; set; }
        public int Status { get; set; }
        public DateTime Cdt { get; set; }
        public int Cuser { get; set; }
        public DateTime Mdt { get; set; }
        public int Muser { get; set; }
        public long AreaAuto { get; set; }
        public Area Area { get; set; }
        public int IncLev { get; set; }
        public int Sex { get; set; }
        public int IdenType { get; set; }
        public int? IsVip { get; set; }
        public string Vipmemo { get; set; }
        public int TaxType { get; set; }
        public string TaxBankNm { get; set; }
        public string TaxBankNo { get; set; }
        public int? RegCapital { get; set; }
        public int? RecCapital { get; set; }
        public int? MoneyType { get; set; }
        public int? MakInvPro { get; set; }
        public int? MakInvCity { get; set; }
        public int? MakInvArea { get; set; }
        public string MakInvAddr { get; set; }
        public string MakInvTel { get; set; }
        public int? RegType { get; set; }
    }
}