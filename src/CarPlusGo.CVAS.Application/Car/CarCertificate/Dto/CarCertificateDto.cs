using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace CarPlusGo.CVAS.Car.Dto
{
    [AutoMap(typeof(CarCertificate))]//随车证件表
    public class CarCertificateDto:FullAuditedEntityDto<long>
    {
        public string CarCertificateName { get; set; }//证件名称
        public int Qty { get; set; }//数量
        public int IsStop { get; set; }//是否停用
    }
}
