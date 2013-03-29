using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OFRPDMS.Models;

namespace OFRPDMS.Repositories
{
    public interface ISpecialEventRepository : IRepository<SpecialEvent>
    {
        IEnumerable<SpecialEvent> FindAllWithCenterId(int centerId);

        SpecialEvent FindByIdAndCenterId(int id, int centerID);

        SpecialEvent FindById(int id);
    }
}
