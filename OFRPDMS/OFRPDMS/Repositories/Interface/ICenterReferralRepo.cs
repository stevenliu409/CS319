using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OFRPDMS.Models;

namespace OFRPDMS.Repositories
{
    public interface ICenterReferralRepo
    {
        IEnumerable<CenterReferral> FindAll();

        IEnumerable<CenterReferral> FindByIdAndCenterId(int id, int centerid);

        IEnumerable<CenterReferral> FindAllWithCenterId(int centerId);

        void Insert(CenterReferral c);

        CenterReferral FindById(int id);

        void Update(CenterReferral c);

        void Delete(CenterReferral c);

        void Insert(Referral r);

        void Dispose();
    }
}