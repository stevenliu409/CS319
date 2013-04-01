using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OFRPDMS.Models;

namespace OFRPDMS.Repositories
{
    public interface ISecondaryGuardianRepository
    {
        IEnumerable<SecondaryGuardian> FindAll();

        SecondaryGuardian FindById(int id);

        void Add(SecondaryGuardian s);

        void Update(SecondaryGuardian s);

        void Delete(SecondaryGuardian s);

        void Dispose();
    }
}
