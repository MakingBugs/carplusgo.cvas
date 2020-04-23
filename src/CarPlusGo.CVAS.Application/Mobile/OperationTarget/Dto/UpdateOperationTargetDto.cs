using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CarPlusGo.CVAS.Mobile.TShareBank;

namespace CarPlusGo.CVAS.Mobile.Dto
{
    [AutoMap(typeof(OperationTarget))]
    public class UpdateOperationTargetDto : EntityDto<long>
    {
        public long OnDutyDriverCount { get; set; }
        public long RecruitDriverCount { get; set; }
        public long QuitDriverCount { get; set; }
        public long CarCount { get; set; }
        public long OperationCarCount { get; set; }
        public long AccidentCarCount { get; set; }
        public long MaintenanceCarCount { get; set; }
        public long ComplaintDriverUserCount { get; set; }
        public long ComplaintAppUserCount { get; set; }
        public long ComplaintCarUserCount { get; set; }
        public string MainWorkItems { get; set; }
    }
}
