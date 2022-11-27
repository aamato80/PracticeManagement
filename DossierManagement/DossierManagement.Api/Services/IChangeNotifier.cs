using DossierManagement.Api.DTOs;

namespace DossierManagement.Api.Services
{
    public interface IChangeNotifier
    {
        Task Notify(CallbackDTO callbackDTO);
    }
}
