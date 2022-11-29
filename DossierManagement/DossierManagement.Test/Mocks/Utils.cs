using DossierManagement.Api.DTOs;
using DossierManagement.Dal.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DossierManagement.Test.Mocks
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

        public static bool IsEquivalentTo(this Dossier Dossier, Dossier other)
        {
            return Dossier.Id == other.Id &&
                Dossier.FiscalCode == other.FiscalCode &&
                Dossier.FirstName == other.FirstName &&
                Dossier.LastName == other.LastName &&
                Dossier.BirthDate == other.BirthDate;
        }

        public static bool IsEquivalentTo(this CallbackDTO dto, CallbackDTO other)
        {
            return dto.DossierId == other.DossierId &&
                dto.Status == other.Status &&
                 dto.Result == other.Result;
        }
    }
}
