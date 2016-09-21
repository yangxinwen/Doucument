using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class PagerResult<T>
    {
        /// <summary>
        /// 分页信息
        /// </summary>
        public PagerParam PageInfo { get; set; }

        public IEnumerable<T> Content { get; set; }
    }
}
