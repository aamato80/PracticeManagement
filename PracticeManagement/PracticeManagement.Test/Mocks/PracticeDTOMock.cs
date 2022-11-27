using Microsoft.AspNetCore.Http;
using PracticeManagement.Api.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace PracticeManagement.Test.Mocks
{
    internal class PracticeDTOMock
    {

        internal static PracticeDTO Create(
            int? id = null,
            string firstname = null,
            string lastname = null,
            string fiscalcode = null,
            DateTime? birthdate = null,
            IFormFile? attachment = null)
        {
            return new PracticeDTO()
            {
                Id = id ?? 10,
                FirstName = firstname ?? "Test First Name",
                LastName = lastname ?? "Test Last Name",
                FiscalCode = fiscalcode ?? "Test Fiscal Code",
                BirthDate = birthdate ?? DateTime.Now.Date,
                Attachment = attachment ?? CreateFakeFormFile()
            };
        }

        internal static PracticeDTO CreateRandom(int? id = null,
            string firstname = null,
            string lastname = null,
            string fiscalcode = null,
            DateTime? birthdate = null,
            IFormFile? attachment = null)
        {
            return new PracticeDTO()
            {
                Id = id ?? Utils.CreateRandomNumber(100),
                FirstName = firstname ?? Utils.CreateRandomString(20),
                LastName = lastname ?? Utils.CreateRandomString(30),
                FiscalCode = fiscalcode ?? Utils.CreateRandomString(16),
                BirthDate = birthdate ?? Utils.CreateRandomDate(null, null),
                Attachment = attachment ?? CreateFakeFormFile()
            };
        }


        public static IFormFile CreateFakeFormFile()
        {
            var contentFile = "Test Content File";
            var fileName = "test.txt";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(contentFile);
            writer.Flush();
            stream.Position = 0;


            return new FormFile(stream, 0, stream.Length, "attachment", fileName);
        }
    }
}
