using DossierManagement.Dal.Enums;

namespace DossierManagement.Api.DTOs
{
    public class DossierChangeStatusDTO
    {
        public DossierChangeStatusDTO(DossierStatus status, DossierResult result, DateTime date)
        {
            Status = status;
            Result = result;
            Date = date;
        }
        public DossierStatus Status { get; }
        public DossierResult Result { get; }
        public DateTime Date { get; }
    }
}
