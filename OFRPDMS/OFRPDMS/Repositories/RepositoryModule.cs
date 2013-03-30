using System;
using System.Web;

using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;

using OFRPDMS.Repositories;

public class RepositoryModule : NinjectModule
{
    public override void Load()
    {
        this.Bind(typeof(ICenterRepository)).To(typeof(CenterRepository));

        this.Bind(typeof(IEventRepository)).To(typeof(EventRepository));

        this.Bind(typeof(ILibraryRepository)).To(typeof(LibraryRepository));

        this.Bind(typeof(IRepositoryService)).To(typeof(DbRepositoryService));

        this.Bind(typeof(ISpecialEventRepository)).To(typeof(SpecialEventRepository));

        this.Bind(typeof(IPrimaryGuardianBorrowsRepository)).To(typeof(PrimaryGuardianBorrowsRepository));

        this.Bind(typeof(ISignInRepository)).To(typeof(SignInRepository));
    }
}