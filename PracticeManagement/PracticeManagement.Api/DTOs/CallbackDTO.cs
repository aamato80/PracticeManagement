using PracticeManagement.Dal.Enums;

namespace PracticeManagement.Api.DTOs
{
    public class CallbackDTO
    {
        public CallbackDTO(int practiceId, PracticeStatus status, PracticeResult result)
        {
            PracticeId = practiceId;
            Status = status;
            Result = result;
        }

        public int PracticeId { get; }
        public PracticeStatus Status { get; }
        public PracticeResult Result { get; }
    }
}
