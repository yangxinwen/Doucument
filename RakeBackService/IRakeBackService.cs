using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using DAL;

namespace RakeBackService
{
    [ServiceContract]
    public interface IRakeBackService
    {
        [OperationContract]
        bool AddUserInfo(UserInfo info);
    }
}
