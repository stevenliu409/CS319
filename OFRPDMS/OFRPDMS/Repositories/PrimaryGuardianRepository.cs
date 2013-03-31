using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

using OFRPDMS.Models;

namespace OFRPDMS.Repositories
{
    public class PrimaryGuardianRepository : IPrimaryGuardianRepository
    {
        private OFRPDMSContext db = new OFRPDMSContext();

        public IEnumerable<PrimaryGuardian> FindAll()
        {
            return db.PrimaryGuardians;
        }

        public IEnumerable<PrimaryGuardian> FindAllWithCenterId(int centerId)
        {
            return db.PrimaryGuardians.Where(p => p.CenterId == centerId);
        }

        public PrimaryGuardian FindById(int id)
        {
            return db.PrimaryGuardians.Find(id);
        }

        public PrimaryGuardian FindByIdAndCenterId(int id, int centerId)
        {
            return db.PrimaryGuardians.Where(p => p.CenterId == centerId).Single(p => p.Id == id);
        }

        public void Add(PrimaryGuardian p)
        {
            db.PrimaryGuardians.Add(p);
            db.SaveChanges();
        }

        public void Update(PrimaryGuardian p)
        {
            db.Entry(p).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(PrimaryGuardian p)
        {
            db.PrimaryGuardians.Remove(p);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}