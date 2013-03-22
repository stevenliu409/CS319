using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OFRPDMS.Models;

namespace OFRPDMS.Repositories
{
    public interface ICenterRepository
    {
        IEnumerable<Center> FindAll();

        IEnumerable<Center> FindListById(int id);

        void Insert(Center c);

        Center FindById(int id);

        void Update(Center c);

        void Delete(Center c);
    }
}
