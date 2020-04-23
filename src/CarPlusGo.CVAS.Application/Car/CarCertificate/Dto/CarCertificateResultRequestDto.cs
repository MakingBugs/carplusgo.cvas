using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace CarPlusGo.CVAS.Car.Dto
{
    public class CarCertificateResultRequestDto:PagedResultRequestDto
    {
        public string CarCertificateName { get; set; }//证件名称
        public int? IsStop { get; set; }//是否停用
    }
}
