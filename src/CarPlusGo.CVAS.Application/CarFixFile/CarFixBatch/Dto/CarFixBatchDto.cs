using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CarPlusGo.CVAS.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.CarFixFile.Dto
{
    [AutoMap(typeof(CarFixBatch))]
    public class CarFixBatchDto : FullAuditedEntityDto<long>
    {
        public decimal TotalAmt { get; set; }
        public long SupplierAuto { get; set; }
        public Supplier Supplier { get; set; }
        public long IncAuto { get; set; }
        public Inc Inc { get; set; }
        public int Status { get; set; }
        public long? AccBankAuto { get; set; }
        public AccBank AccBank { get; set; }
        public DateTime? PAyDT { get; set; }
        public int IsPAy { get; set; }
        public int BankTab { get; set; }
        public long CUser { get; set; }
        public DateTime CDT { get; set; }
        public long? MUser { get; set; }
        public DateTime? MDT { get; set; }
        public int? RequestStatus { get; set; }
        public string AccountName { get; set; }
        public string AccountBank { get; set; }
        public string BankAccount { get; set; }
        public int? BankType { get; set; }
        public int? IsS { get; set; }
        public int? PayMode { get; set; }
        public long? CarFixBatchTNO { get; set; }
        public int? IsTab { get; set; }
    }
}
