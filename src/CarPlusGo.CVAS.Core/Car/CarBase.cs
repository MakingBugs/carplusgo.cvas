using Abp.Domain.Entities;
using CarPlusGo.CVAS.CarFixFile;
using CarPlusGo.CVAS.Common;
using CarPlusGo.CVAS.LocationFile;
using CarPlusGo.CVAS.OrderFile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Car
{
    [Table("CarBase")]
    public class CarBase : Entity<long>
    {
        [Column("CarBase_Auto")]
        public override long Id { get; set; }
        [ForeignKey("Inc")]
        [Column("Inc_Auto")]
        public long? IncAuto { get; set; }
        public Inc Inc { get; set; }
        [Column("Dep_Auto")]
        public long DepAuto { get; set; }
        [Column("SG_Auto")]
        public long SgAuto { get; set; }
        public int Km { get; set; }
        public string MakNo { get; set; }
        public string MakColor { get; set; }
        public string LinceNo { get; set; }
        public DateTime? LinceDt { get; set; }
        [ForeignKey("Brand")]
        [Column("Brand_Auto")]
        public long BrandAuto { get; set; }
        public Brand Brand { get; set; }
        [ForeignKey("Clasen")]
        [Column("Clasen_Auto")]
        public long? ClasenAuto { get; set; }
        public Clasen Clasen { get; set; }
        public string ClasenCode { get; set; }
        public DateTime? MakDt { get; set; }
        public int Cc { get; set; }
        public int Percnt { get; set; }
        public int Wheel { get; set; }
        public string CarColor { get; set; }
        public string EngNo { get; set; }
        public string CarNo { get; set; }
        public DateTime? CarDt { get; set; }
        public int Oil { get; set; }
        public decimal ListPrice { get; set; }
        public int CarType { get; set; }
        public string ClasenVer { get; set; }
        public long Manufacturer { get; set; }
        public long Supplier { get; set; }
        public string CreatePlace { get; set; }
        public long? UseType { get; set; }
        public int? ItemType { get; set; }
        [ForeignKey("ItemType,UseType")]
        public ItemCode ItemCode { get; set; }
        public int Category { get; set; }
        public int InsurePercnt { get; set; }
        public int IsBusiness { get; set; }
        public int KeyCount { get; set; }
        public string UsePlace { get; set; }
        public string Memo { get; set; }
        public int IsImport { get; set; }
        public int MtnFirst { get; set; }
        public int MtnSecond { get; set; }
        public int MtnCycle { get; set; }
        public DateTime? YearCheckDt { get; set; }
        public DateTime? RoadCheckDt { get; set; }
        public int MtnFirstMonth { get; set; }
        public int MtnSecondMonth { get; set; }
        public int Status { get; set; }
        public int Cuser { get; set; }
        public DateTime Cdt { get; set; }
        public int Muser { get; set; }
        public DateTime Mdt { get; set; }
        public string DeptNo { get; set; }
        public decimal GetAmt { get; set; }
        public decimal CarTax { get; set; }
        public decimal Accessary { get; set; }
        public decimal MarketValue { get; set; }
        public DateTime? MarketDt { get; set; }
        public decimal SellAmt { get; set; }
        public DateTime? SellDt { get; set; }
        public int Es { get; set; }
        public int Bstype { get; set; }
        public DateTime? Jcdt { get; set; }
        public DateTime? Bkdt { get; set; }
        public string NewBrand { get; set; }
        public string NewClasen { get; set; }
        public string NewFactory { get; set; }
        [ForeignKey("FactoryBrand")]
        [Column("FactoryBrand_Auto")]
        public long? FactoryBrandAuto { get; set; }
        public FactoryBrand FactoryBrand { get; set; }
        public decimal? RentCarType { get; set; }
        public int? MakStatus { get; set; }
        public string SellInvoice { get; set; }
        public int? IsYearCheck { get; set; }
        public int? CarTaxMode { get; set; }
        public decimal? Ton { get; set; }
        public string Esremark { get; set; }
        public int? IsEas { get; set; }
        public int? EasAsstAct { get; set; }
        [Column("EasCarbase_Auto")]
        public int? EasCarbaseAuto { get; set; }
        public long? ProvinceCode { get; set; }
        [ForeignKey("ProvinceCode")]
        public CreditProvince CreditProvince { get; set; }
        public long? CityCode { get; set; }
        [ForeignKey("CityCode")]
        public CreditCity CreditCity { get; set; }

        /// <summary>
        /// 契约档
        /// </summary>
        [ForeignKey("CarBaseAuto")]
        public virtual ICollection<Order> Order { get; set; }
        public long? RepositoryID { get; set; }
        [ForeignKey("RepositoryID")]
        public Repository Repository { get; set; }
    }
}
