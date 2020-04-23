using Abp.Application.Services;
using CarPlusGo.CVAS.CXLPFile.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.CXLPFile
{
    public interface ICXLPDZFAppService
    : IAsyncCrudAppService<CXLPDZFDto, long, CXLPDZFResultRequestDto, CXLPDZFDto, CXLPDZFDto>
    {
    }
}
