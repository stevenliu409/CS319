using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OFRPDMS.Repositories
{
    public class DbRepositoryService : IRepositoryService
    {
        // ninject depedency injection
        public DbRepositoryService(IEventRepository eventRepo, ICenterRepository centerRepo, ILibraryRepository libraryRepo, ISpecialEventRepository specialEventRepo)
        {
            this.eventRepo = eventRepo;
            this.centerRepo = centerRepo;
            this.libraryRepo = libraryRepo;
            this.specialEventRepo = specialEventRepo;
        }

        public IEventRepository eventRepo { get; set;  }

        public ICenterRepository centerRepo { get; set;  }

        public ILibraryRepository libraryRepo { get; set; }

        public ISpecialEventRepository specialEventRepo {get; set;}
    }
}