using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.Mobile.MemberAnalysis.MembersGeneral.Dto
{
    public class MembersRisingTendencyResultRequestDto
    {
        /// <summary>
        /// 统计周期：1日 2周 3月
        /// </summary>
        public int Period { get; set; }
        /// <summary>
        /// 业绩指标类型 1日活跃用户 2新增注册用户 3新增充值用户数 4新增消费用户 5新增下载量
        /// </summary>
        public int[] Types { get; set; }
    }
}
