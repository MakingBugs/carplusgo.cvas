using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CarPlusGo.CVAS.BPM.Dto;

namespace CarPlusGo.CVAS.BPM
{
    public interface IReadyBPMAppService 
        : IAsyncCrudAppService<ReadyBPMDto, string, PagedResultRequestDto, CreateReadyBPMDto, ReadyBPMDto>
    {
    }
}
