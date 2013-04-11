using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

using OFRPDMS.Models;

namespace OFRPDMS.Repositories
{
    public class CenterRepository : ICenterRepository
    {
        private OFRPDMSContext db = new OFRPDMSContext();

        public IEnumerable<Center> FindAll()
        {
            return db.Centers;
        }

        public Center FindById(int id)
        {
            return db.Centers.Find(id);
        }

        public IEnumerable<Center> FindListById(int id)
        {
            return db.Centers.Where(c => c.Id == id);
        }


        public void Insert(Center e)
        {
            db.Centers.Add(e);
            db.SaveChanges();
        }

        public void Update(Center e)
        {
            db.Entry(e).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Center e)
        {
            db.Centers.Remove(e);
            db.SaveChanges();
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}