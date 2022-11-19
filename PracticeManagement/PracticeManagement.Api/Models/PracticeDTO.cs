using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PracticeManagement.Api.Models
{
    public class PracticeDTO
    {
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(100, ErrorMessage = "{0} cannot be greater than {1} characters.")]
        public string FirstName  { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(200, ErrorMessage = "{0} cannot be greater than {1} characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(16, ErrorMessage = "{0} cannot be greater than {1} characters.")]
        public string FiscalCode { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public IFormFile Attachment { get; set; }
    }

    

  
}
