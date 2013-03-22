using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OFRPDMS.Models;
using System.Data;
using System.Data.Entity;
using System.Web.Security;

namespace OFRPDMS.Account
{
    public class AccountService : IAccountService
    {
        public int GetCurrentUserCenterId()
        {
            return AccountProfile.CurrentUser.CenterID;
        }

        public int GetUserCenterId(string UserName)
        {
            return AccountProfile.GetUser(UserName).CenterID;
        }

        public void SetCurrentUserCenterId(int Id)
        {
            AccountProfile.CurrentUser.CenterID = Id;
        }

        public void SetUserCenterId(string UserName, int Id)
        {
            AccountProfile.GetUser(UserName).CenterID = Id;
        }

        public bool RoleExists(string role)
        {
            return Roles.RoleExists(role);
        }

        public void CreateRole(string role)
        {
            Roles.CreateRole(role);
        }

        public string[] GetRolesForUser()
        {
            return Roles.GetRolesForUser();
        }



    }
}