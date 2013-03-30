using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using OFRPDMS.Models;

namespace OFRPDMS.Repositories
{
    public interface ILibraryRepository
    {

        LibraryResource FindById(int id);

        IEnumerable<LibraryResource> FindAllWithCenterId(int centerId);

        LibraryResource FindByIdAndCenterId(int id, int centerId);

        void Add(LibraryResource e);

        void Update(LibraryResource e);

        void Delete(LibraryResource e);

        void Dispose();
    }
}