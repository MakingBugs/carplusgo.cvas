using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace CarPlusGo.CVAS.RepositoryOutCar.Dto
{
    public class RepositoryOutCarPartResultRequestDto : PagedResultRequestDto
    {
        public long[] Ids { get; set; }
    }
}
