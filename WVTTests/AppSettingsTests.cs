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
        public void ApiKeys_ShouldAllowAdding()
        {
            var settings = new AppSettings();
            var apiKeyA = new ApiKeyModel("ATestName", "ATestToken");
            var apiKeyB = new ApiKeyModel("BTestName", "BTestToken");

            settings.ApiKeys.Add(apiKeyA);

            settings.ApiKeys.Should().HaveCount(1);
            settings.ApiKeys[0].Name.Should().Be("ATestName");
            settings.ApiKeys[0].Token.Should().Be("ATestToken");

            settings.ApiKeys.Add(apiKeyB);
            settings.ApiKeys.Should().HaveCount(2);
            settings.ApiKeys[1].Name.Should().Be("BTestName");
            settings.ApiKeys[1].Token.Should().Be("BTestToken");
        }

        [Fact]
        public void ApiKeys_ShouldAllowRemoving()
        {
            var settings = new AppSettings();
            var apiKeyA = new ApiKeyModel("ATestName", "ATestToken");
            var apiKeyB = new ApiKeyModel("BTestName", "BTestToken");
            settings.ApiKeys.Add(apiKeyA);
            settings.ApiKeys.Add(apiKeyB);
            settings.ApiKeys.Should().HaveCount(2);

            settings.ApiKeys.Remove(apiKeyA);
            settings.ApiKeys.Remove(apiKeyB);
            settings.ApiKeys.Should().HaveCount(0);
        }
    }
}