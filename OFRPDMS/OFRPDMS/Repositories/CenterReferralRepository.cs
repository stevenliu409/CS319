using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OFRPDMS.Models;
using System.Data;

namespace OFRPDMS.Repositories
{
    public class CenterReferralRepository : ICenterReferralRepo
    {
        OFRPDMSContext db = new OFRPDMSContext();

        public IEnumerable<CenterReferral> FindAll()
        {
            return db.CenterReferrals;       
        }

        public IEnumerable<CenterReferral> FindByIdAndCenterId(int id, int centerid)
        {
            return db.CenterReferrals.Where(cr => cr.Id == id && cr.CenterId == centerid);
        }

        public IEnumerable<CenterReferral> FindAllWithCenterId(int centerId)
        {
            return db.CenterReferrals.Where(cr => cr.CenterId == centerId);
        }

        public void Insert(CenterReferral c)
        {
            db.CenterReferrals.Add(c);
            db.SaveChanges();
        }

        public void Insert(Referral r)
        {
            db.Referrals.Add(r);
            db.SaveChanges();
        }

        public CenterReferral FindById(int id) 
        {
            return db.CenterReferrals.Find(id);
        }

        public void Update(CenterReferral c) 
        {
            db.Entry(c).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(CenterReferral c) 
        {
            db.CenterReferrals.Remove(c);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}