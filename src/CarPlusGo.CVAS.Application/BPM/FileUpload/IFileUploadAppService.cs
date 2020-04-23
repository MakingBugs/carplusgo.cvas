using Abp.Application.Services;
using CarPlusGo.CVAS.BPM.Dto;

namespace CarPlusGo.CVAS.BPM
{
    public interface IFileUploadAppService
        : IAsyncCrudAppService<FileUploadDto, long, PagedFileUploadResultRequestDto, FileUploadDto, FileUploadDto>
    {
    }
}
