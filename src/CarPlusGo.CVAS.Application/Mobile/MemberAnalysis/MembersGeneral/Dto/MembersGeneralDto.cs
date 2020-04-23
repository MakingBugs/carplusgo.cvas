using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Mobile.MemberAnalysis.MembersGeneral.Dto
{
    public class MembersGeneralDto
    {
        /// <summary>
        /// 日活跃用户数
        /// </summary>
        public int ActiveUers { get; set; }
        /// <summary>
        /// 新增下载量
        /// </summary>
        public int NewDownloads { get; set; }
        /// <summary>
        /// 新增注册用户数
        /// </summary>
        public int NewRegisterUsers { get; set; }
        /// <summary>
        /// 新增充值用户数
        /// </summary>
        public int NewRechargeUsers { get; set; }
        /// <summary>
        /// 新增消费用户数
        /// </summary>
        public int NewConsumeUsers { get; set; }
        /// <summary>
        /// 累计下载量
        /// </summary>
        public int TotalDownloads { get; set; }
        /// <summary>
        /// 累计注册用户数
        /// </summary>
        public int TotalRegisterUsers { get; set; }
        /// <summary>
        /// 累计充值用户数
        /// </summary>
        public int TotalRechargeUsers { get; set; }
        /// <summary>
        /// 累计消费用户数
        /// </summary>
        public int TotalConsumeUsers { get; set; }
    }
}
