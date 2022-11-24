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
    internal class AddPracticeRequestMock
    {

        internal string FirstName { get; set; }

        internal string LastName { get; set; }

        internal string FiscalCode { get; set; }

        internal DateTime? BirthDate { get; set; }

        internal IFormFile Attachment { get; set; }

        internal static PracticeDTO Create()
        {
            return new PracticeDTO()
            {
                FirstName = "Test First Name",
                LastName = "Test Last Name",
                FiscalCode = "Test Fiscal Code",
                BirthDate = DateTime.Now.Date,
                Attachment = CreateFakeFormFile()
            };
        }

        private static IFormFile CreateFakeFormFile()
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
