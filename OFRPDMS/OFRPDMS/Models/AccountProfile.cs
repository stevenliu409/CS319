using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Profile;
using System.Web.Security;

namespace OFRPDMS.Models
{
    public class AccountProfile : ProfileBase
    {
        static public AccountProfile CurrentUser
        {
            get
            {
                MembershipUser currentUser = Membership.GetUser();
                return (AccountProfile)
                       ProfileBase.Create(Membership.GetUser().UserName);
            }
        }

        static public AccountProfile GetUser(string username)
        {
            {
                MembershipUser user = Membership.GetUser(username);
                return (AccountProfile)
                    ProfileBase.Create(user.UserName);
            }
        }

        public int CenterID
        {
            get { return ((int)(base["CenterID"])); }
            set { base["CenterID"] = value; Save(); }
        }

        // add additional properties here
    }
}