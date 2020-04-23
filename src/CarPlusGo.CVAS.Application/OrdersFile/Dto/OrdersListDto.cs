using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CarPlusGo.CVAS.Car;
using CarPlusGo.CVAS.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.OrdersFile.Dto
{
    [AutoMap(typeof(Orders))]
    public class OrdersListDto : EntityDto<long>
    {
        public Inc Inc { get; set; }
        //public long TradeItemAuto { get; set; }
        public TradeItem TradeItem { get; set; }
        public long CarBaseAuto { get; set; }
        public long SalesAuto { get; set; }
        public int CustSource { get; set; } 
        public int CarSource { get; set; }
        //public long FactoryBrandAuto { get; set; }
        public FactoryBrand FactoryBrand { get; set; }
        //public long BrandAuto { get; set; }
        public Brand Brand { get; set; }
        //public long ClasenAuto { get; set; }
        public Clasen Clasen { get; set; } 
        public DateTime? CarDt { get; set; } 
        public string MakNo { get; set; }
        public decimal ListPrice { get; set; }
        public decimal DisPrice { get; set; }
        public decimal GetPrice { get; set; }
        public decimal Accessary { get; set; }  
        public decimal CarCost { get; set; }
        public decimal OverAmt { get; set; }
        public decimal DptAmt { get; set; } 
        public decimal MakNoCost { get; set; }
        public int OrderType { get; set; } 
        public decimal RentAmt { get; set; }
        public int RentType { get; set; }    
        public decimal FinalMcost { get; set; }  
        public decimal Mamt { get; set; }
        public decimal? RentAmtT { get; set; }
        public decimal RentRate { get; set; } 
        public int OrderStatus { get; set; }
        public string OrderStatusName { get; set; }
        public string Memo { get; set; }  
        public DateTime Cdt { get; set; }  
        public int PostType { get; set; } 
        public int? IsCustomerCare { get; set; }
        public decimal RateRate { get; set; }

    }
}
