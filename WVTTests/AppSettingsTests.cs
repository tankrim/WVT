using FluentAssertions;
using WVTLib;
using WVTLib.Models;

namespace WVTTests
{
    public class AppSettingsTests
    {
        [Fact]
        public void ApiKeys_ShouldBeInitiallyEmpty()
        {
            var settings = new AppSettings();
            settings.ApiKeys.Should().BeEmpty();
        }

        [Fact]
        public void ApiKeys_ShouldAllowAddingAndRetrieving()
        {
            var settings = new AppSettings();
            var apiKey = new ApiKeyModel("TestAccount", "TestToken");

            settings.ApiKeys.Add(apiKey);

            settings.ApiKeys.Should().HaveCount(1);
            settings.ApiKeys[0].Name.Should().Be("TestAccount");
            settings.ApiKeys[0].Token.Should().Be("TestToken");
        }
    }
}