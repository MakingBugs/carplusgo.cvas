using Abp.Application.Services;
using CarPlusGo.CVAS.BPM.Dto;
using System.Linq;
using Abp.Linq.Extensions;
using System.Threading.Tasks;
using CarPlusGo.CVAS.Authorization.Users;

namespace CarPlusGo.CVAS.BPM
{
    public class FileUploadAppService
        : AsyncCrudAppService<FileUpload, FileUploadDto, long, PagedFileUploadResultRequestDto, FileUploadDto, FileUploadDto>, IFileUploadAppService
    {
        private readonly IFileUploadRepository _fileUploadrepository;
        private readonly UserManager _userManager;
        public FileUploadAppService(IFileUploadRepository repository, UserManager userManager)
            : base(repository)
        {
            _fileUploadrepository = repository;
            _userManager = userManager;
        }

        protected override IQueryable<FileUpload> CreateFilteredQuery(PagedFileUploadResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(input.DocPostID.HasValue, x => x.DocPostId == input.DocPostID);
        }

        public async Task<int> SaveBPMFile(BPMFileDto input)
        {
            var entity = ObjectMapper.Map<BPMFile>(input);
            entity.AccountID = (await _userManager.GetUserByIdAsync(AbpSession.UserId.Value)).UserName;
            return await _fileUploadrepository.SaveBPMFile(entity);
        }
    }
}
