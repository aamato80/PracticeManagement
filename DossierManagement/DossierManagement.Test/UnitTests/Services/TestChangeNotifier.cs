using Microsoft.Extensions.Configuration;
using NSubstitute;
using DossierManagement.Api;
using DossierManagement.Api.DTOs;
using DossierManagement.Api.CustomHttpClient;
using DossierManagement.Api.Services;
using DossierManagement.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DossierManagement.Test.UnitTests.Services
{
    public class TestChangeNotifier
    {
        private IConfiguration _configuration;
        private ICustomHttpClient _httpClient;
        public TestChangeNotifier() {
            var fakeSettings = new Dictionary<string, string> {
                    {"Callbacks:Url", "localhost:8080"}
    
            };
            _configuration = new ConfigurationBuilder().AddInMemoryCollection(fakeSettings).Build();
            _httpClient = Substitute.For<ICustomHttpClient>();
        }

        [Fact]
        public async Task ChangeNotifier_Notify_ShouldBeCalledCorrectly()
        {
            var url = _configuration.GetValue<string>(AppConfigConst.CallbacksUrl);
            var notifier = new ChangeNotifier(_httpClient,_configuration);
            var dto = CallbackDTOMock.Create(101, Dal.Enums.DossierStatus.Created, Dal.Enums.DossierResult.None);
            await notifier.Notify(dto);
            await  _httpClient.Received().PostAsJsonAsync(url, dto);
        }
    }
}
