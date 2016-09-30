using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace DAL
{
    public class UserInfoDal : EntityBase<UserInfo>
    {
        public bool Test1()
        {

            var context = this.GetDbContext();
            var dal = context.Set<UserInfo>();
            var roleDal = context.Set<RoleInfo>();

            var linq = from u in dal
                       join r in roleDal on u.roleId equals r.RoleId
                       select new
                       {
                           roleId=u.roleId,
                           userId=u.userId,
                           userName=u.userName
                       };

            var result=linq.SelectMany(r => dal.Where(a=>a.roleId==r.roleId));

            foreach (var item in linq)
            {
                System.Diagnostics.Debug.WriteLine(item.roleId + ":"+item.userId + ":" + item.userName);
            }

            System.Diagnostics.Debug.WriteLine(linq.Count());
            return true;
        }
    }
}
