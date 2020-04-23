using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CarPlusGo.CVAS.Finance.Enum;

namespace CarPlusGo.CVAS.Finance.Dto
{
    [AutoMap(typeof(AccountingTitle))]
    public class AccountingTitleDto : FullAuditedEntityDto<long>
    {
        /// <summary>
        /// 会计要素类别
        /// </summary>
        public AccountingElementType AccountingElementType { get; set; }

        /// <summary>
        /// 科目名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 科目编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 科目级次
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Tenant Id of this user.
        /// </summary>
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// Is this user active?
        /// If as user is not active, he/she can not use the application.
        /// </summary>
        public virtual bool IsActive { get; set; }
    }
}
