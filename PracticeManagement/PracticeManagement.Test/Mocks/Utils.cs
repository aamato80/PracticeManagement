using PracticeManagement.Dal.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PracticeManagement.Test.Mocks
{
    public static class Utils
    {
        private static Random random = new Random();
        public static string CreateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static int CreateRandomNumber(int maxNumber=1)
        {
            return random.Next(maxNumber);
        }

        public static DateTime CreateRandomDate(DateTime? from, DateTime? to)
        {
            from = from ?? DateTime.Today.AddYears(-10);
            to = to ?? DateTime.Today;
            int range = (to.Value - from.Value).Days;
            return from.Value.AddDays(random.Next(range));
        }

        public static bool IsEquivalentTo(this Practice practice, Practice other)
        {
            return practice.Id == other.Id &&
                practice.FiscalCode == other.FiscalCode &&
                practice.FirstName == other.FirstName &&
                practice.LastName == other.LastName &&
                practice.BirthDate == other.BirthDate;
        }
    }
}
