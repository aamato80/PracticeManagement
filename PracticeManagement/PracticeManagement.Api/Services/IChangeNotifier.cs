using PracticeManagement.Api.DTOs;

namespace PracticeManagement.Api.Services
{
    public interface IChangeNotifier
    {
        Task Notify(CallbackDTO callbackDTO);
    }
}
