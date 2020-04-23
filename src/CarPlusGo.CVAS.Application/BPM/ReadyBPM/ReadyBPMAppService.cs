using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using CarPlusGo.CVAS.Authorization.Users;
using CarPlusGo.CVAS.BPM.Dto;
using System;
using System.Threading.Tasks;

namespace CarPlusGo.CVAS.BPM
{
    public class ReadyBPMAppService
        : AsyncCrudAppService<ReadyBPM, ReadyBPMDto, string, PagedResultRequestDto, CreateReadyBPMDto, ReadyBPMDto>, IReadyBPMAppService
    {
        private readonly IRepository<FormFlow, int> _formFlowRepository;
        private readonly IRepository<UserInfo, long> _userInfoRepository;
        private readonly UserManager _userManager;
        public ReadyBPMAppService(IRepository<ReadyBPM, string> repository, IRepository<FormFlow, int> formFlowRepository, IRepository<UserInfo, long> userInfoRepository, UserManager userManager)
            : base(repository)
        {
            _formFlowRepository = formFlowRepository;
            _userInfoRepository = userInfoRepository;
            _userManager = userManager;
        }

        public override async Task<ReadyBPMDto> Create(CreateReadyBPMDto input)
        {
            CheckCreatePermission();

            var user = await _userManager.GetUserByIdAsync(AbpSession.UserId.Value);
            var formFlow = _formFlowRepository.FirstOrDefault(x => x.FormName == input.FormName && x.FlowId == input.FlowId);
            var bpmUser = _userInfoRepository.FirstOrDefault(x => x.BPMUserID.ToLower() == user.UserName.ToLower());
            var entity = new ReadyBPM()
            {
                DiagramId = formFlow.FlowId,
                ApplicantDept = bpmUser.BPMDeptID,
                ApplicantDeptName = bpmUser.BPMDeptName,
                ApplicantId = bpmUser.BPMUserID,
                ApplicantName = bpmUser.BPMUserName,
                FillerId = bpmUser.BPMUserID,
                FillerName = bpmUser.BPMUserName,
                ApplicantDateTime = DateTime.Now,
                Priority = 2,
                DraftFlag = 0,
                FlowActivated = 0,
                TagId = input.Id,
            };

            await Repository.InsertAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }
    }
}
