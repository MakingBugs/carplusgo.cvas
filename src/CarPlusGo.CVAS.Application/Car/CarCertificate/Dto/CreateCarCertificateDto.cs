using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Car.Dto
{
    [AutoMap(typeof(CarCertificate))]
    public class CreateCarCertificateDto : FullAuditedEntityDto<long>
    {
        public string CarCertificateName { get; set; }//证件名称
        public int Qty { get; set; }//数量
        public int IsStop { get; set; }//是否停用
        public List<CarCertificateRightDto> CarCertificateRightList { get; set; }
    }
}
