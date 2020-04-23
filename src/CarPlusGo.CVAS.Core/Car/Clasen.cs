using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Car
{
    [Table("Clasen")]
    public class Clasen : Entity<long>
    {
        [Column("Clasen_Auto")]
        public override long Id { get; set; }
        [ForeignKey("Brand")]
        [Column("Brand_Auto")]
        public long BrandAuto { get; set; }
        public Brand Brand { get; set; }
        public string ClasenName { get; set; }
        public DateTime Cdt { get; set; }
        public long Cuser { get; set; }
        public DateTime Mdt { get; set; }
        public long Muser { get; set; }

        public decimal OverP_0_5 { get; set; }

        public decimal OverP_1 { get; set; }

        public decimal OverP_1_5 { get; set; }

        public decimal OverP_2 { get; set; }

        public decimal OverP_2_5 { get; set; }

        public decimal OverP_3 { get; set; }

        public decimal OverP_3_5 { get; set; }

        public decimal OverP_4 { get; set; }

        public decimal OverP_4_5 { get; set; }

        public decimal OverP_5 { get; set; }

        public decimal OverP_5_5 { get; set; }

        public decimal OverP_6 { get; set; }

        public decimal OverP_6_5 { get; set; }

        public decimal OverP_7 { get; set; }

        public decimal OverP_7_5 { get; set; }

        public decimal OverP_8 { get; set; }

        public decimal RateKM_0_5W { get; set; }

        public decimal RateKM_1W { get; set; }

        public decimal RateKM_1_5W { get; set; }

        public decimal RateKM_2W { get; set; }

        public decimal RateKM_2_5W { get; set; }

        public decimal RateKM_3W { get; set; }

        public decimal RateKM_3_5W { get; set; }

        public decimal RateKM_4W { get; set; }

        public decimal RateKM_4_5W { get; set; }

        public decimal RateKM_5W { get; set; }

        public decimal RateKM_5_5W { get; set; }

        public decimal RateKM_6W { get; set; }

        public decimal RateKM_6_5W { get; set; }

        public decimal RateKM_7W { get; set; }

        public decimal RateKM_7_5W { get; set; }

        public decimal RateKM_8W { get; set; }

        public decimal RateKM_8_5W { get; set; }

        public decimal RateKM_9W { get; set; }

        public decimal RateKM_9_5W { get; set; }

        public decimal RateKM_10W { get; set; }

        public decimal RateKM_10_5W { get; set; }

        public decimal RateKM_11W { get; set; }

        public decimal RateKM_11_5W { get; set; }

        public decimal RateKM_12W { get; set; }

        public decimal RateKM_12_5W { get; set; }

        public decimal RateKM_13W { get; set; }

        public decimal RateKM_13_5W { get; set; }

        public decimal RateKM_14W { get; set; }

        public decimal RateKM_14_5W { get; set; }

        public decimal RateKM_15W { get; set; }

        public decimal RateKM_15_5W { get; set; }

        public decimal RateKM_16W { get; set; }

        public decimal RateKM_16_5W { get; set; }

        public decimal RateKM_17W { get; set; }

        public decimal RateKM_17_5W { get; set; }

        public decimal RateKM_18W { get; set; }

        public decimal RateKM_18_5W { get; set; }

        public decimal RateKM_19W { get; set; }

        public decimal RateKM_19_5W { get; set; }

        public decimal RateKM_20W { get; set; }

        public decimal RateKM_20_5W { get; set; }

        public decimal RateKM_21W { get; set; }

        public decimal RateKM_21_5W { get; set; }

        public decimal RateKM_22W { get; set; }

        public decimal RateKM_22_5W { get; set; }

        public decimal RateKM_23W { get; set; }

        public decimal RateKM_23_5W { get; set; }

        public decimal RateKM_24W { get; set; }

        public decimal RateKM_24_5W { get; set; }

        public decimal RateKM_25W { get; set; }

        public decimal RateKM_25_5W { get; set; }

        public decimal RateKM_26W { get; set; }

        public decimal RateKM_26_5W { get; set; }

        public decimal RateKM_27W { get; set; }

        public decimal RateKM_27_5W { get; set; }

        public decimal RateKM_28W { get; set; }

        public decimal RateKM_28_5W { get; set; }

        public decimal RateKM_29W { get; set; }

        public decimal RateKM_29_5W { get; set; }

        public decimal RateKM_30W { get; set; }
        public string Memo { get; set; }
    }
}
