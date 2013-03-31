using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

using OFRPDMS.Models;

namespace OFRPDMS.Repositories
{
    public class SecondaryGuardianRepository : ISecondaryGuardianRepository
    {
        private OFRPDMSContext db = new OFRPDMSContext();

        public IEnumerable<SecondaryGuardian> FindAll()
        {
            return db.SecondaryGuardians;
        }

        public SecondaryGuardian FindById(int id)
        {
            return db.SecondaryGuardians.Find(id);
        }

        public void Add(SecondaryGuardian p)
        {
            db.SecondaryGuardians.Add(p);
            db.SaveChanges();
        }

        public void Update(SecondaryGuardian p)
        {
            db.Entry(p).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(SecondaryGuardian p)
        {
            db.SecondaryGuardians.Remove(p);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}