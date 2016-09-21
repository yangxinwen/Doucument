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
            this.ExecuteTransaction(() =>
            {
                var newInfo = new UserInfo();
                newInfo.userName = "yxwtest";
                this.Add(newInfo);

                return true;
            });
            return true;
        }

    }
}
