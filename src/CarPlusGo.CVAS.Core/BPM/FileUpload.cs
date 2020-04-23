using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPlusGo.CVAS.BPM
{
    [Table("FileUpload")]
    public class FileUpload : Entity<long>
    {
        [Column("FileUpload_Auto")]
        public override long Id { get; set; }
        public int Type { get; set; }
        public long DocPostId { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public int Status { get; set; }
        public string Memo { get; set; }
        public DateTime Cdt { get; set; }
        public int Cuser { get; set; }
        public DateTime? Mdt { get; set; }
        public int? Muser { get; set; }
        public int? FileSize { get; set; }
        public string OldFileName { get; set; }
    }
}
