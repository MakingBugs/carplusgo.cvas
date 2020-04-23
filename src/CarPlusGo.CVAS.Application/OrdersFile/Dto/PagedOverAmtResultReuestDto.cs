using System;
using System.Collections.Generic;
using System.Text;

namespace CarPlusGo.CVAS.OrdersFile.Dto
{
    public class PagedOverAmtResultReuestDto
    {
        /// <summary>
        /// 車型代碼
        /// </summary>
        public long Clasen_Auto { get; set; }
        /// <summary>
        /// 期數
        /// </summary>
        public int MM { get; set; }
        /// <summary>
        /// 租賃性質
        /// </summary>
        public int RentType { get; set; }
        /// <summary>
        /// 營業類型
        /// </summary>
        public int OrderType { get; set; }

        public int MakNoType { get; set; }
        /// <summary>
        /// 排檔類型
        /// </summary>
        public int BsType { get; set; }
        /// <summary>
        /// 合約總里程
        /// </summary>
        public int KM { get; set; }
        /// <summary>
        /// 牌價
        /// </summary>
        public int ListPrice { get; set; }
    }
}
