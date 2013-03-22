using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OFRPDMS.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> FindAll();

        void Insert(T t);

        void Update(T t);

        void Delete(T t);

        void Dispose();
    }
}