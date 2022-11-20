using PracticeManagement.Dal.Enums;
using System.ComponentModel.DataAnnotations;

namespace PracticeManagement.Dal.Models
{
    public class Practice
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FiscalCode { get; set; }
        public PracticeStatus Status { get; set; }
        public PracticeResult Result { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
