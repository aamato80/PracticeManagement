using Microsoft.Extensions.Configuration;
using NSubstitute;
using PracticeManagement.Api;
using PracticeManagement.Api.DTOs;
using PracticeManagement.Api.CustomHttpClient;
using PracticeManagement.Api.Services;
using PracticeManagement.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PracticeManagement.Test.UnitTests.Services
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
            var dto = CallbackDTOMock.Create(101, Dal.Enums.PracticeStatus.Created, Dal.Enums.PracticeResult.None);
            await notifier.Notify(dto);
            await  _httpClient.Received().PostAsJsonAsync(url, dto);
        }
    }
}
