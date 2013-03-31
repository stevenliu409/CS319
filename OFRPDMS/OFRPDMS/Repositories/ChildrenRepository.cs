using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

using OFRPDMS.Models;

namespace OFRPDMS.Repositories
{
    public class ChildRepository : IChildRepository
    {
        private OFRPDMSContext db = new OFRPDMSContext();

        public IEnumerable<Child> FindAll()
        {
            return db.Children;
        }

        public Child FindById(int id)
        {
            return db.Children.Find(id);
        }

        public void Add(Child c)
        {
            db.Children.Add(c);
            db.SaveChanges();
        }

        public void Update(Child c)
        {
            db.Entry(c).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Child c)
        {
            db.Children.Remove(c);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}