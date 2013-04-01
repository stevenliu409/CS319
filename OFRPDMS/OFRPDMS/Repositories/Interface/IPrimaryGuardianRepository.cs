using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OFRPDMS.Models;

namespace OFRPDMS.Repositories
{
    public interface IPrimaryGuardianRepository
    {
        PrimaryGuardian FindById(int id);

        IEnumerable<PrimaryGuardian> FindAll();

        IEnumerable<PrimaryGuardian> FindAllWithCenterId(int centerId);

        PrimaryGuardian FindByIdAndCenterId(int id, int centerId);

        void Add(PrimaryGuardian p);

        void Update(PrimaryGuardian p);

        void Delete(PrimaryGuardian p);

        void Dispose();
    }
}
