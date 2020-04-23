using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Mobile.MemberAnalysis.MembersGeneral.Dto
{
    public class MembersRisingTendencyDto
    {
     // 业绩指标类型 1日活跃用户 2新增注册用户 3新增充值用户数 4新增消费用户 5新增下载量

        /// <summary>
        /// 日活跃用户数
        /// </summary>
        public IEnumerable<MembersRisingTendencyDetailDto> ActiveUers { get; set; }
        /// <summary>
        /// 新增注册用户数
        /// </summary>
        public IEnumerable<MembersRisingTendencyDetailDto> NewRegisterUsers { get; set; }
        /// <summary>
        /// 新增充值用户数
        /// </summary>
        public IEnumerable<MembersRisingTendencyDetailDto> NewRechargeUsers { get; set; }
        /// <summary>
        /// 新增消费用户数
        /// </summary>
        public IEnumerable<MembersRisingTendencyDetailDto> NewConsumeUsers { get; set; }
        /// <summary>
        /// 新增下载量
        /// </summary>
        public IEnumerable<MembersRisingTendencyDetailDto> NewDownloads { get; set; }
    }
}
