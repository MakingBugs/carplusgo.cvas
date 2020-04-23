using Abp.Domain.Entities;
using System;

namespace CarPlusGo.CVAS.Mobile.TShareBank
{
    public class UmengApiData: Entity<long>
    {
        public DateTime Date { get; set; }
        public long ActivityUsers { get; set; }
        public long NewUsers { get; set; }
        public long UniqNewUsers { get; set; }
        public long UniqActiveUsers { get; set; }
        public long Launches { get; set; }
        public long TotalInstallUser { get; set; }
    }
}
