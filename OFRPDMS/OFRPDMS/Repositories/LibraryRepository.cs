using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

using OFRPDMS.Models;

namespace OFRPDMS.Repositories
{

    public class LibraryRepository : ILibraryRepository
    {
        private OFRPDMSContext db = new OFRPDMSContext();

        public IEnumerable<LibraryResource> FindAll()
        {
            return db.LibraryResources;
        }

        public IEnumerable<LibraryResource> FindAllWithCenterId(int centerId)
        {
            return db.LibraryResources.Where(LibraryResource => LibraryResource.CenterId == centerId);
        }

        public LibraryResource FindById(int id)
        {
            return db.LibraryResources.Find(id);
        }

        public LibraryResource FindByIdAndCenterId(int id, int centerId)
        {
            return db.LibraryResources.Where(e => e.CenterId == centerId).Single(e => e.Id == id);
        }

        public void Add(LibraryResource e)
        {
            db.LibraryResources.Add(e);
            db.SaveChanges();
        }

        public void Update(LibraryResource e)
        {
            db.Entry(e).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(LibraryResource e)
        {
            db.LibraryResources.Remove(e);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}