using Abp.Domain.Repositories;
using System.Threading.Tasks;

namespace CarPlusGo.CVAS.BPM
{
    public interface IFileUploadRepository : IRepository<FileUpload, long>
    {
        Task<int> SaveBPMFile(BPMFile bpmFile);
    }
}
