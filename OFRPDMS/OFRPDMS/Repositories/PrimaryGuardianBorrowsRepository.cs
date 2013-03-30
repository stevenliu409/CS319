using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

using OFRPDMS.Models;
namespace OFRPDMS.Repositories
{
  
    public class PrimaryGuardianBorrowsRepository : IPrimaryGuardianBorrowsRepository
    {
        private OFRPDMSContext db = new OFRPDMSContext();
        

        public IEnumerable<PrimaryGuardianBorrow> FindAllWithCenterId(int centerId) {

            return db.PrimaryGuardianBorrows.Where(pgb => pgb.PrimaryGuardian.CenterId == centerId); 
        }

        public PrimaryGuardianBorrow FindById(int id)
        {
            return db.PrimaryGuardianBorrows.Find(id);
        }

        public PrimaryGuardianBorrow FindByIdAndCenterId(int id, int centerId)
        {
            return db.PrimaryGuardianBorrows.Single(c => c.Id == id && c.PrimaryGuardian.CenterId == centerId);
        }


        public void Insert(PrimaryGuardianBorrow e)
        {
            db.PrimaryGuardianBorrows.Add(e);
            db.SaveChanges();
        }

        public void Update(PrimaryGuardianBorrow e)
        {
            db.Entry(e).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(PrimaryGuardianBorrow e)
        {
            db.PrimaryGuardianBorrows.Remove(e);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }


    }
}