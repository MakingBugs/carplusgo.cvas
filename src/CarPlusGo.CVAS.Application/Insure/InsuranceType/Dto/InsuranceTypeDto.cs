using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CarPlusGo.CVAS.Insure.Dto
{
    [AutoMap(typeof(InsuranceType))]
    public class InsuranceTypeDto : FullAuditedEntityDto<long>
    {
        /// <summary>
        /// 险种名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// Is this user active?
        /// If as user is not active, he/she can not use the application.
        /// </summary>
        public virtual bool IsActive { get; set; }
    }
}
