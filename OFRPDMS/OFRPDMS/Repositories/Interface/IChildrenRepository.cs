using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OFRPDMS.Models;

namespace OFRPDMS.Repositories
{
    public interface IChildRepository
    {
        IEnumerable<Child> FindAll();

        Child FindById(int id);

        void Add(Child c);

        void Update(Child c);

        void Delete(Child c);

        void Dispose();
    }
}
