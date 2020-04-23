using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.Common
{
    [Table("BankType")]
    public class BankType : Entity<int>
    {
        [Column("BankType")]
        public override int Id { get; set; }
        public string BankNameT { get; set; }
        public int Invisible { get; set; }
    }
}
