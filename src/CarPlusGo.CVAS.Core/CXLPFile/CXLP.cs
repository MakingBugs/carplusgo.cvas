using Abp.Domain.Entities.Auditing;
using CarPlusGo.CVAS.Car;
using CarPlusGo.CVAS.Common;
using CarPlusGo.CVAS.Insure;
using CarPlusGo.CVAS.OrderFile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPlusGo.CVAS.CXLPFile
{
    [Table("CXLP")]
    public class CXLP : FullAuditedEntity<long>
    {
        [Column("CXLP_Auto")]
        public override long Id { get; set; }
        public string CXLPNO { get; set; }
        public string MakNo { get; set; }
        public DateTime CaseTime { get; set; }
        public string CaseAddr { get; set; }
        public string Driver { get; set; }
        public string Reporter { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public int LossValue { get; set; }
        public string LossOtherRemark { get; set; }
        public long? CaseType { get; set; }
        public long? CaseStatus { get; set; }
        public string InsureNo { get; set; }
        public int AccidentType { get; set; }
        public int IsInjured { get; set; }
        public string QJDBRemark { get; set; }
        public string SUser { get; set; }
        public DateTime? SDT { get; set; }
        public string SRemark { get; set; }
        public string AccountName { get; set; }
        public string AcountBank { get; set; }
        public string BankAccount { get; set; }
        public int? ZFContractors { get; set; }
        public DateTime? ZFDT { get; set; }
        public int? CauseType { get; set; }
        public long JFNeedFile { get; set; }
        public long JFMakeFile { get; set; }
        public long JFNeedFileNum { get; set; }
        public long JFMakeFileNum { get; set; }
        public long WFNeedFile { get; set; }
        public long WFMakeFile { get; set; }
        public long WFNeedFileNum { get; set; }
        public long WFMakeFileNum { get; set; }
        public string tijiaoren { get; set; }
        public DateTime? tijiaoDt { get; set; }
        public string jieshouren { get; set; }
        public DateTime? jieshouDt { get; set; }
        public decimal InsurePFAmount { get; set; }
        public decimal InsureDPFamount { get; set; }
        public string SSPFRemark { get; set; }
        public decimal InsureDZAmount { get; set; }
        public DateTime? InsureDZDT { get; set; }
        public string InsureDZAccount { get; set; }
        public decimal? InsureDDZAmount { get; set; }
        public DateTime? InsureDDZDT { get; set; }
        public string InsureDDZAccount { get; set; }
        public decimal DZFPFAmount { get; set; }
        public DateTime? DZFPFDT { get; set; }
        public string DZPFRemark { get; set; }
        public decimal? QuerenAmount { get; set; }
        public DateTime? DaoZhangDT { get; set; }
        public int? ZhuanFuType { get; set; }
        public DateTime? ZhuanFuDT { get; set; }
        public string ShouKuanRen { get; set; }
        public string ShouKuanRenID { get; set; }
        public string ShouKuanFang { get; set; }
        public string ShouKuanBank { get; set; }
        public string ShouKuanAccount { get; set; }
        public int Status { get; set; }
        public long CUser { get; set; }
        public DateTime CDT { get; set; }
        public long MUser { get; set; }
        public DateTime? MDT { get; set; }
        public DateTime? YiJueDt { get; set; }
        public long? TJCLFile { get; set; }
        public long? TJCLNum { get; set; }
        public long? QJDBFile { get; set; }
        public long? QJDBNum { get; set; }
        public string HH { get; set; }
        public string MM { get; set; }
        public string WFPercent { get; set; }
        [Column("order_auto")]
        public long? OrderAuto { get; set; }
        public DateTime? EndDT { get; set; }
        [Column("Insure_Auto")]
        public long? InsureAuto { get; set; }
        [Column("CarBase_Auto")]
        public long CarBaseAuto { get; set; }
        public int? RequestStatus { get; set; }
        public DateTime? RequestDT { get; set; }
        public long? RequestUser { get; set; }
        public int? IsTrn { get; set; }
        public int? LossValueN { get; set; }
        public int? CSIC { get; set; }
        public int? CTIC { get; set; }
        [ForeignKey("CarBaseAuto")]
        public CarBase CarBase { get; set; }
        [ForeignKey("InsureAuto")]
        public InsurancePolicy InsurancePolicy { get; set; }
        [ForeignKey("OrderAuto")]
        public Order Order { get; set; }
        [ForeignKey("CSIC,CaseStatus")]
        public ItemCode CaseStatusItemCode { get; set; }
        [ForeignKey("CTIC,CaseType")]
        public ItemCode CaseTypeItemCode { get; set; }
        [ForeignKey("RequestUser")]
        public VEmp VEmp { get; set; }
    }
}
