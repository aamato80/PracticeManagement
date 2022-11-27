using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DossierManagement.Api.DTOs
{
    public class DossierDto
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FiscalCode { get; set; }
        public DateTime? BirthDate { get; set; }
        public IFormFile? Attachment { get; set; }
    }
}
