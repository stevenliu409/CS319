using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OFRPDMS.Models;
using System.Data;

namespace OFRPDMS.Repositories
{
    public class SignInRepository : ISignInRepository
    {
        private OFRPDMSContext db = new OFRPDMSContext();

        public IEnumerable<EventParticipant> FindAll()
        {
            return db.EventParticipants;
        }

        public IEnumerable<EventParticipant> FindAllWithCenterId(int centerId)
        {
            return db.EventParticipants.Where(EventParticipant => EventParticipant.Event.CenterId == centerId);
        }

        public IEnumerable<EventParticipant> FindAllWithEventId(int eventId) {

            return db.EventParticipants.Where(e => e.EventId == eventId);
        }
        public EventParticipant FindById(int id)
        {
            return db.EventParticipants.Find(id);
        }

        public IEnumerable<EventParticipant> FindByEventIdAndCenterId(int id, int centerId)
        {
            return db.EventParticipants.Where(e => e.Event.CenterId == centerId && e.EventId == id);
        }

        public IEnumerable<EventParticipant> FindPrimaryGuardianByIdAndEventId(int id, int eventId) 
        {
            return db.EventParticipants.Where(eps => eps.PrimaryGuardianId == id && eps.EventId == eventId);
        }

        public IEnumerable<EventParticipant> FindChildByIdAndEventId(int id, int eventId)
        {
            return db.EventParticipants.Where(eps => eps.ChildId == id && eps.EventId == eventId);
        }

        public IEnumerable<EventParticipant> FindSecondaryGuardianByIdAndEventId(int id, int eventId)
        {
            return db.EventParticipants.Where(eps => eps.SecondaryGuardianId == id && eps.EventId == eventId);
        }
        public void Insert(EventParticipant e)
        {
            db.EventParticipants.Add(e);
            db.SaveChanges();
        }

        public void Update(EventParticipant e)
        {
            db.Entry(e).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(EventParticipant e)
        {
            db.EventParticipants.Remove(e);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

       
    }
}