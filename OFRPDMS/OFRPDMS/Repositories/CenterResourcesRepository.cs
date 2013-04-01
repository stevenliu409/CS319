using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OFRPDMS.Models;
using System.Data;

namespace OFRPDMS.Repositories
{
    public class CenterResourcesRepository : ICenterResourcesRepository
    {
        OFRPDMSContext db = new OFRPDMSContext();

        public IEnumerable<CenterFreeResource> FindAll()
        {
            return db.CenterFreeResources;
        }

        public IEnumerable<CenterFreeResource> FindByIdAndCenterId(int id, int centerid)
        {
            return db.CenterFreeResources.Where(cr => cr.Id == id && cr.CenterId == centerid);
        }

        public IEnumerable<CenterFreeResource> FindAllWithCenterId(int centerId)
        {
            return db.CenterFreeResources.Where(cr => cr.CenterId == centerId);
        }

        public void Insert(CenterFreeResource c)
        {
            db.CenterFreeResources.Add(c);
            db.SaveChanges();
        }

        public void Insert(GivenResource r)
        {
            db.GivenResources.Add(r);
            db.SaveChanges();
        }

        public CenterFreeResource FindById(int id)
        {
            return db.CenterFreeResources.Find(id);
        }

        public void Update(CenterFreeResource c)
        {
            db.Entry(c).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(CenterFreeResource c)
        {
            db.CenterFreeResources.Remove(c);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}