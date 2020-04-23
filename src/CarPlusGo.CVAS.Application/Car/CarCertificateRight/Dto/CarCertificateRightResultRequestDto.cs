using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace CarPlusGo.CVAS.Car.Dto
{
    public class CarCertificateRightResultRequestDto: PagedResultRequestDto
    {
        public long? CarCertificateID { get; set; }//证件ID
        public int? Selected { get; set; }//是否停用
    }
}
