using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OFRPDMS.Models;
namespace OFRPDMS.Repositories
{
    public interface ISignInRepository : IRepository<EventParticipant>
    {
        IEnumerable<EventParticipant> FindAllWithCenterId(int centerId);

        IEnumerable<EventParticipant> FindByEventIdAndCenterId(int id, int centerID);

        IEnumerable<EventParticipant> FindAllWithEventId(int eventId);

        EventParticipant FindById(int id);
    }
}