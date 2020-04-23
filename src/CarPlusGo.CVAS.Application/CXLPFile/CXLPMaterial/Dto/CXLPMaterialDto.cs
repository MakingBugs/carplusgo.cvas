using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.CXLPFile.Dto
{
    [AutoMap(typeof(CXLPMaterial))]
    public class CXLPMaterialDto : FullAuditedEntityDto<long>
    {
        public long CXLPAuto { get; set; }
        public string CXLPMaterialName { get; set; }
        public string CXLPMaterialURL { get; set; }
        public int CXLPMaterialType { get; set; }
        public long CUser { get; set; }
        public DateTime CDT { get; set; }
        public long MUser { get; set; }
        public DateTime? MDT { get; set; }
        public string FileSize { get; set; }
        public string OldFileName { get; set; }
    }
}
