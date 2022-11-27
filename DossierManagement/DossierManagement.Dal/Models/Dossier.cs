using DossierManagement.Dal.Enums;
using System.ComponentModel.DataAnnotations;

namespace DossierManagement.Dal.Models
{
    public class Dossier
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FiscalCode { get; set; }
        public DossierStatus Status { get; set; }
        public DossierResult Result { get; set; }
        public DateTime BirthDate { get; set; }
        public IEnumerable<DossierChangeStatus> StatusChanges { get; set; }
    }
}
