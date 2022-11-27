using DossierManagement.Dal.Enums;
using DossierManagement.Dal.Models;

namespace DossierManagement.Api.DTOs
{
    public class GetDossierResponseDTO
    {
        public GetDossierResponseDTO(int id,
            string firstName,
            string lastName,
            string fiscalCode,
            DateTime birhtDate,
            DossierStatus status,
            DossierResult result,
            IEnumerable<DossierChangeStatusDTO> dossierChanges)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            FiscalCode = fiscalCode;
            BirthDate = birhtDate;
            Status = status;
            Result = result;
            StatusChanges = dossierChanges;
        }

        public int Id { get;  }
        public string FirstName { get;  }
        public string LastName { get;  }
        public string FiscalCode { get;  }
        public DateTime BirthDate { get;  }
        public DossierStatus Status { get;  }
        public DossierResult Result { get;  }
        public IEnumerable<DossierChangeStatusDTO> StatusChanges { get;  }
    }
}
