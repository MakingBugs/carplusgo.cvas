using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace CarPlusGo.CVAS.BPM.Dto
{
    [AutoMap(typeof(FileUpload))]
    public class FileUploadDto:EntityDto<long>
    {
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
