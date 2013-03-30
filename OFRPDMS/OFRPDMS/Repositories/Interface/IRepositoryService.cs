using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OFRPDMS.Repositories
{
    public interface IRepositoryService
    {
        IEventRepository eventRepo { get; set; }

        ICenterRepository centerRepo { get; set; }

        ILibraryRepository libraryRepo { get; set; }

        ISpecialEventRepository specialEventRepo { get; set; }

        IPrimaryGuardianBorrowsRepository primaryGuardianBorrowsRepo { get; set; }

        ISignInRepository signInRepo { get; set; }
    }
}
