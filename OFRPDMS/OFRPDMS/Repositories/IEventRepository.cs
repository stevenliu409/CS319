using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OFRPDMS.Models;

namespace OFRPDMS.Repositories
{
    public interface IEventRepository : IRepository<Event>
    {
        IEnumerable<Event> FindAllWithCenterId(int centerId);

        Event FindByIdAndCenterId(int id, int centerID);

        Event FindById(int id);
    }
}
