using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace CarPlusGo.CVAS.OrderFile.Dto
{
    [AutoMap(typeof(Order))]
    public class OrderDto : EntityDto<long>
    {
    }
}
