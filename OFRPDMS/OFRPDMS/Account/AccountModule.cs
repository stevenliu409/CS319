using System;
using System.Web;

using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;

using OFRPDMS.Account;

namespace OFRPDMS.Account
{
    public class AccountModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind(typeof(IAccountService)).To(typeof(AccountService));
        }
    }
}