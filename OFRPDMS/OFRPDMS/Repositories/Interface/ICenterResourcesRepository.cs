using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OFRPDMS.Models;

namespace OFRPDMS.Repositories
{
    public interface ICenterResourcesRepository
    {
        IEnumerable<CenterFreeResource> FindAll();

        IEnumerable<CenterFreeResource> FindByIdAndCenterId(int id, int centerid);

        IEnumerable<CenterFreeResource> FindAllWithCenterId(int centerId);

        void Insert(CenterFreeResource c);

        CenterFreeResource FindById(int id);

        void Update(CenterFreeResource c);

        void Delete(CenterFreeResource c);

        void Insert(GivenResource r);

        void Dispose();
    }
}