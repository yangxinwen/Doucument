using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    /// <summary>
    /// 分页参数
    /// </summary>
    public class PagerParam
    {
        /// <summary>
        /// 当前页索引
        /// </summary>
        public int PageIndex { get; set; } = 1;
        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { get; set; } = 100;
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalRecord { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage
        {
            get
            {
                if (PageSize == 0 || TotalRecord == 0)
                    return 1;

                var pageCount = TotalRecord / PageSize;
                if (TotalRecord % PageSize > 0)
                    pageCount++;
                return pageCount;
            }
        }
    }
}
