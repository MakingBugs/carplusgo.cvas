using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.RepositoryOutCar.Dto
{
    [AutoMap(typeof(RepositoryOut))]//出入库记录档
    public class CreateRepositoryOutDto : FullAuditedEntityDto<long>
    {
        public long CarTakeApplyID { get; set; }//车辆提领申请编号
        public long CarTakeID { get; set; }//车辆提领编号
        public long CarBaseAuto { get; set; }
        public string MakNo { get; set; }//车牌号
        public long OutRepositoryID { get; set; }//出库仓库
        public int OutReason { get; set; }//出库原因
        public DateTime OutDate { get; set; }//出库日期
        public int OutKM { get; set; }//出库里程
        public int OutCondition { get; set; }//出库车况
        public string OutMemo { get; set; }//出库备注
        public long InRepositoryID { get; set; }//入库仓库
        public int InReason { get; set; }//入库原因
        public DateTime InDate { get; set; }//入库日期
        public int InKM { get; set; }//入库里程
        public int InCondition { get; set; }//入库车况
        public string InMemo { get; set; }//入库备注
        public string RejectMemo { get; set; }//取消原因
        public long Status { get; set; }//出库状态1608
        public int ItemStatus { get; set; }
        public List<RepositoryOutCarPartDto> RepositoryOutCarPartList { get; set; }//出入库部位记录
        public List<RepositoryOutAccessoryDto> RepositoryOutAccessorieList { get; set; }//出入库配件记录
        public List<RepositoryOutCertificateDto> RepositoryOutCertificateList { get; set; }//出入库证件记录
        public List<RepositoryOutFileDto> RepositoryOutFileList { get; set; }//出入库附件记录
    }
}
