//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class MenuInfo
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public Nullable<int> IsEnable { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> MenuDate { get; set; }
        public Nullable<int> AddUser { get; set; }
        public Nullable<int> OrderNum { get; set; }
    }
}