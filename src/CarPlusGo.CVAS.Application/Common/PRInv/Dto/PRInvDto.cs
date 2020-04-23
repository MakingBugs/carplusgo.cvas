﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPlusGo.CVAS.Common.Dto
{
    [AutoMap(typeof(PRInv))]
    public class PRInvDto : FullAuditedEntityDto<long>
    {
        public long IncAuto { get; set; }
        public Inc Inc { get; set; }
        public long SupplierAuto { get; set; }
        public Supplier Supplier { get; set; }
        public string InvCode { get; set; }
        public string InvNo { get; set; }
        public DateTime InvDt { get; set; }
        public decimal Amount { get; set; }
        public DateTime? PayDt { get; set; }
        public double PayAmt { get; set; }
        public int Status { get; set; }
        public int Cuser { get; set; }
        public DateTime Cdt { get; set; }
        public int Muser { get; set; }
        public DateTime Mdt { get; set; }
        public int? InvType { get; set; }
        public long? PrinvLinkAuto { get; set; }
        public PRInvLink PRInvLink { get; set; }
        public string AccountName { get; set; }
        public string AccountBank { get; set; }
        public string BankAccount { get; set; }
        public int? BankType { get; set; }
        public BankType BankTypeData { get; set; }
        public decimal? InvRealAmt { get; set; }
    }
}
