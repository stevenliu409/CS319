using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OFRPDMS.Models;
namespace OFRPDMS.Repositories
{
    public interface IPrimaryGuardianBorrowsRepository
    {
        PrimaryGuardianBorrow FindById(int id);

        IEnumerable<PrimaryGuardianBorrow> FindAllWithCenterId(int centerId);

        PrimaryGuardianBorrow FindByIdAndCenterId(int id, int centerId);

        IEnumerable<PrimaryGuardianBorrow> FindAllWithLibraryResourceId(int id);

        void Insert(PrimaryGuardianBorrow e);

        void Update(PrimaryGuardianBorrow e);

        void Delete(PrimaryGuardianBorrow e);

        void Dispose();
    }
}