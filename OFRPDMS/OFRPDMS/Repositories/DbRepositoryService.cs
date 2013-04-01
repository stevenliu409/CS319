using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OFRPDMS.Repositories
{
    public class DbRepositoryService : IRepositoryService
    {
        // ninject depedency injection
        public DbRepositoryService(IEventRepository eventRepo, ICenterRepository centerRepo,
            ILibraryRepository libraryRepo, ISpecialEventRepository specialEventRepo,
            IPrimaryGuardianBorrowsRepository primaryGuardianBorrowsRepo, ISignInRepository signInRepo,
            IPrimaryGuardianRepository primaryGuardianRepo, ISecondaryGuardianRepository secondaryGuardianRepo,
            IChildRepository childRepo, ICenterReferralRepo centerReferralRepo, ICenterResourcesRepository centerResourcesRepo)
        {
            this.eventRepo = eventRepo;
            this.centerRepo = centerRepo;
            this.libraryRepo = libraryRepo;
            this.specialEventRepo = specialEventRepo;
            this.primaryGuardianBorrowsRepo = primaryGuardianBorrowsRepo;
            this.signInRepo = signInRepo;
            this.primaryGuardianRepo = primaryGuardianRepo;
            this.secondaryGuardianRepo = secondaryGuardianRepo;
            this.childRepo = childRepo;
            this.centerReferralRepo = centerReferralRepo;
            this.centerResourcesRepo = centerResourcesRepo;
        }

        public IEventRepository eventRepo { get; set;  }

        public ICenterRepository centerRepo { get; set;  }

        public ILibraryRepository libraryRepo { get; set; }

        public ISpecialEventRepository specialEventRepo {get; set;}

        public IPrimaryGuardianBorrowsRepository primaryGuardianBorrowsRepo { get; set; }

        public ISignInRepository signInRepo { get; set; }

        public IPrimaryGuardianRepository primaryGuardianRepo { get; set; }

        public ISecondaryGuardianRepository secondaryGuardianRepo { get; set; }

        public IChildRepository childRepo { get; set; }

        public ICenterReferralRepo centerReferralRepo { get; set; }

        public ICenterResourcesRepository centerResourcesRepo { get; set; }
    }
}