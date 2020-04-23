using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarPlusGo.CVAS.Car
{
    [Table("CarMemo")]
    public class CarMemo : Entity<long>
    {
        [Column("CarMemo_Auto")]
        public override long Id { get; set; }
        [Column("Order_Auto")]
        public long OrderAuto { get; set; }
        public string CarMakNo { get; set; }
        public string CarMemoText { get; set; }
        public int Cuser { get; set; }
        public DateTime Cdate { get; set; }
    }
}
