using DossierManagement.Api.DTOs;
using DossierManagement.Api.CustomHttpClient;

namespace DossierManagement.Api.Services
{
    public class ChangeNotifier : IChangeNotifier
    {
        private readonly ICustomHttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public ChangeNotifier(ICustomHttpClient httpClient,IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task Notify(CallbackDTO callbackDTO)
        {
            var callbackUrl = _configuration.GetValue<string>(AppConfigConst.CallbacksUrl);
            await _httpClient.PostAsJsonAsync(callbackUrl, callbackDTO);
        }
    }
}
