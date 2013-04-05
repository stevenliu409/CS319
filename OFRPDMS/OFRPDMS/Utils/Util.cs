using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using OFRPDMS.Models;

namespace OFRPDMS.Utils
{
    public static class Util
    {
        private static Random random = new Random((int)DateTime.Now.Ticks);

        public static string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + (Probability(0.5) ? 97 : 65))));                 
                builder.Append(ch);
            }

            return builder.ToString();
        }


        public static DateTime RandomDate()
        {
            DateTime date = DateTime.Now;

            double dayChange = Math.Floor(-365 * random.NextDouble());

            date = date.AddDays(dayChange);

            return date;
        }

        public static PrimaryGuardian RandomPrimaryGuardian(int Id, int CenterId)
        {
            PrimaryGuardian pg = new PrimaryGuardian
            {
                Id = Id,
                CenterId = CenterId,
                FirstName = Probability(0.8) ? RandomString(3) : null,
                LastName = Probability(0.8) ? RandomString(3) : null,
                Allergies = Probability(0.8) ? RandomString(3) : null,
                Country = Probability(0.8) ? RandomString(3) : null,
                Email = Probability(0.8) ? RandomString(3) : null,
                Language = Probability(0.8) ? RandomString(3) : null,
                PostalCodePrefix = Probability(0.8) ? RandomString(3) : null,
                Phone = Probability(0.8) ? RandomString(3) : null,
                DateCreated = RandomDate()
            };

            return pg;
        }

        public static Child RandomChild(int Id, int primaryGuardianId)
        {
           Child c = new Child
            {
                Id = Id,
                FirstName = Probability(0.8) ? RandomString(3) : null,
                LastName = Probability(0.8) ? RandomString(3) : null,
                Allergies = Probability(0.8) ? RandomString(3) : null,
                Birthdate = RandomDate(),
                DateCreated = RandomDate(),
                PrimaryGuardianId = primaryGuardianId
            };

            return c;
        }

        public static SecondaryGuardian RandomSecondaryGuardian(int Id, int primaryGuardianId)
        {
            SecondaryGuardian sg = new SecondaryGuardian
            {
                Id = Id,
                FirstName = Probability(0.8) ? RandomString(3) : null,
                LastName = Probability(0.8) ? RandomString(3) : null,
                Phone = Probability(0.8) ? RandomString(3) : null,
                RelationshipToChild = Probability(0.8) ? RandomString(3) : null,
                PrimaryGuardianId = primaryGuardianId
            };

            return sg;
        }

        private static bool Probability(double prob)
        {
            return random.NextDouble() < prob;
        }
    }


}