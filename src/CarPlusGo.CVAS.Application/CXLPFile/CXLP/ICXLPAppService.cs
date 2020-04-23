using Abp.Application.Services;
using CarPlusGo.CVAS.CXLPFile.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.CXLPFile
{
    public interface ICXLPAppService
    : IAsyncCrudAppService<CXLPDto, long, CXLPResultRequestDto, CXLPDto, CXLPDto>
    {

    }
}
