﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CarPlusGo.CVAS.Finance.Enum;
using System.Collections.Generic;

namespace CarPlusGo.CVAS.Finance.Dto
{
    [AutoMap(typeof(AccountingEntryConfig))]
    public class AccountingEntryConfigDto : FullAuditedEntityDto<long>
    {
        /// <summary>
        /// 配置名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 上级标识
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 是否为主配置
        /// </summary>
        public bool IsMaster { get; set; }

        /// <summary>
        /// 会计分录配置明细
        /// </summary>
        public virtual List<AccountingEntryConfigDto> Children { get; set; }

        /// <summary>
        /// 会计要素改变方向
        /// </summary>
        public ElementChangeDirection? ElementChangeDirection { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }

        public long? AccountingTitleId { get; set; }

        /// <summary>
        /// 会计科目
        /// </summary>
        public AccountingTitleDto AccountingTitle { get; set; }

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
