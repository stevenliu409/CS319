using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

using OFRPDMS.Models;

namespace OFRPDMS.Repositories
{
    public class EventRepository : IEventRepository
    {
        private OFRPDMSContext db = new OFRPDMSContext();

        public IEnumerable<Event> FindAll()
        {
            return db.Events;
        }

        public IEnumerable<Event> FindAllWithCenterId(int centerId)
        {
            return db.Events.Where(Event => Event.CenterId == centerId);
        }

        public Event FindById(int id)
        {
            return db.Events.Find(id);
        }

        public Event FindByIdAndCenterId(int id, int centerId)
        {
            return db.Events.Where(e => e.CenterId == centerId).Single(e => e.Id == id);
        }

        public void Insert(Event e)
        {
            db.Events.Add(e);
            db.SaveChanges();
        }

        public void Update(Event e)
        {
            db.Entry(e).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Event e)
        {
            db.Events.Remove(e);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}