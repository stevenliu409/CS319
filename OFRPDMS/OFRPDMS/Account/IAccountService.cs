using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OFRPDMS.Account
{
    public interface IAccountService
    {
        int GetCurrentUserCenterId();

        int GetUserCenterId(string UserName);

        void SetCurrentUserCenterId(int Id);

        void SetUserCenterId(string UserName, int Id);

        bool RoleExists(string role);

        void CreateRole(string role);

        string[] GetRolesForUser();

        void AddUserToRole(string username, string role);
    }
}
