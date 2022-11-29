using DossierManagement.Dal.Enums;

namespace DossierManagement.Api.DTOs
{
    public class CallbackDTO
    {
        public CallbackDTO(int dossierId, DossierStatus status, DossierResult result)
        {
            DossierId = dossierId;
            Status = status;
            Result = result;
        }

        public int DossierId { get; }
        public DossierStatus Status { get; }
        public DossierResult Result { get; }
    }
}
