using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using CarPlusGo.CVAS.UseCarFiles.Dto;

namespace CarPlusGo.CVAS.UseCarFile
{
    public interface IUseCarPartAppService:IAsyncCrudAppService<UseCarPartDto, long, UseCarPartResultRequestDto, UseCarPartDto, UseCarPartDto>
    { 
    }
}
