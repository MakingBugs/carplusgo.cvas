using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CarPlusGo.CVAS.Common;
using CarPlusGo.CVAS.Common.Dto;
using CarPlusGo.CVAS.OrderFile;
using CarPlusGo.CVAS.OrderFile.Dto;
using System;
using System.Collections.Generic;

namespace CarPlusGo.CVAS.Car.Dto
{
    [AutoMap(typeof(CarBase))]
    public class UpdateCarBaseDto : EntityDto<long>
    {
        public long IncAuto { get; set; }
        public long? FactoryBrandAuto { get; set; }
        public long ClasenAuto { get; set; }
        public long ProvinceCode { get; set; }
        public long? CityCode { get; set; }
        public long DepAuto { get; set; }
        public long SgAuto { get; set; }
        public int Km { get; set; }
        public string MakNo { get; set; }
        public string MakColor { get; set; }
        public string LinceNo { get; set; }
        public DateTime? LinceDt { get; set; }
        public long BrandAuto { get; set; }
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
        public long UseType { get; set; }
        public int Category { get; set; }
        public int InsurePercnt { get; set; }
        public int IsBusiness { get; set; }
        public int KeyCount { get; set; }
        //public string UsePlace { get; set; }
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
        //public int Cuser { get; set; }
        //public DateTime Cdt { get; set; }
        public int Muser { get; set; }
        public DateTime Mdt { get; set; }
        //public string DeptNo { get; set; }
        //public decimal GetAmt { get; set; }
        //public decimal CarTax { get; set; }
        //public decimal Accessary { get; set; }
        //public decimal MarketValue { get; set; }
        //public DateTime? MarketDt { get; set; }
        //public decimal SellAmt { get; set; }
        //public DateTime? SellDt { get; set; }
        public int Es { get; set; }
        public int Bstype { get; set; }
        public DateTime? Jcdt { get; set; }
        public DateTime? Bkdt { get; set; }
        //public string NewBrand { get; set; }
        //public string NewClasen { get; set; }
        //public string NewFactory { get; set; }
        public decimal? RentCarType { get; set; }
        //public int? MakStatus { get; set; }
        //public string SellInvoice { get; set; }
        public int? IsYearCheck { get; set; }
        public int? CarTaxMode { get; set; }
        public decimal? Ton { get; set; }
        public string Esremark { get; set; }
        //public int? IsEas { get; set; }
        //public int? EasAsstAct { get; set; }
        public int ItemType { get; set; }
        public long? RepositoryID { get; set; }
    }
}
