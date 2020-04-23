using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using CarPlusGo.CVAS.Common.Dto;

namespace CarPlusGo.CVAS.Car.Dto
{
    [AutoMap(typeof(CarCertificateRight))]//适用随车证件表
    public class CarCertificateRightDto:FullAuditedEntityDto<long>
    {
        public long CarCertificateID { get; set; }//证件ID
        public long? OilID { get; set; }//燃油类型
        public int Selected { get; set; }//是否停用
        public int? OilType { get; set; }//231
        public ItemCodeDto ItemCode { get; set; }
        public CarCertificateDto CarCertificate { get; set; }
    }
}
