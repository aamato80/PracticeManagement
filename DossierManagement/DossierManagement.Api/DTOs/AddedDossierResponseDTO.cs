using DossierManagement.Dal.Models;

namespace DossierManagement.Api.DTOs
{
    public class AddedDossierResponseDTO
    {
        public AddedDossierResponseDTO(int id,
            string firstName,
            string lastName,
            string fiscalCode,
            DateTime birhtDate
            )
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            FiscalCode = fiscalCode;
            BirthDate = birhtDate;
        }

        public int Id { get;  }
        public string FirstName { get;  }
        public string LastName { get;  }
        public string FiscalCode { get;  }
        public DateTime BirthDate { get;  }
        public IEnumerable<DossierChangeStatus> StatusChanges { get; set; }
    }
}
