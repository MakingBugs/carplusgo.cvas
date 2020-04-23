using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Car.Dto
{
    [AutoMap(typeof(CarMemo))]
    public class CarMemoDto : EntityDto<long>
    {
        public long OrderAuto { get; set; }
        public string CarMakNo { get; set; }
        public string CarMemoText { get; set; }
        public int Cuser { get; set; }
        public DateTime Cdate { get; set; }
    }
}
