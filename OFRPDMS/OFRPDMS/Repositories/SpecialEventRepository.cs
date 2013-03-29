using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

using OFRPDMS.Models;

namespace OFRPDMS.Repositories
{
    public class SpecialEventRepository : ISpecialEventRepository
    {
        private OFRPDMSContext db = new OFRPDMSContext();

        public IEnumerable<SpecialEvent> FindAll()
        {
            return db.SpecialEvents;
        }

        public IEnumerable<SpecialEvent> FindAllWithCenterId(int centerId)
        {
            return db.SpecialEvents.Where(SpecialEvent => SpecialEvent.CenterId == centerId);
        }

        public SpecialEvent FindById(int id)
        {
            return db.SpecialEvents.Find(id);
        }

        public SpecialEvent FindByIdAndCenterId(int id, int centerId)
        {
            return db.SpecialEvents.Where(e => e.CenterId == centerId).Single(e => e.Id == id);
        }

        public void Insert(SpecialEvent e)
        {
            db.SpecialEvents.Add(e);
            db.SaveChanges();
        }

        public void Update(SpecialEvent e)
        {
            db.Entry(e).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(SpecialEvent e)
        {
            db.SpecialEvents.Remove(e);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}