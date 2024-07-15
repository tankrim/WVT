using FluentAssertions;
using WVTLib.Models;

namespace TestWVT.WVTLib
{
    public class LocalObjectiveCompletionModelTests
    {
        [Fact]
        public void Constructor_WithValidInputs_ShouldCreateInstance()
        {
            var model = new LocalObjectiveCompletionModel("TestAccount", "TestEndpoint", "TestTitle", true);

            model.AccountName.Should().Be("TestAccount");
            model.Endpoint.Should().Be("TestEndpoint");
            model.Title.Should().Be("TestTitle");
            model.IsCompleted.Should().BeTrue();
        }

        [Theory]
        [InlineData(null, "TestEndpoint", "TestTitle")]
        [InlineData("", "TestEndpoint", "TestTitle")]
        [InlineData(" ", "TestEndpoint", "TestTitle")]
        [InlineData("TestAccount", null, "TestTitle")]
        [InlineData("TestAccount", "", "TestTitle")]
        [InlineData("TestAccount", " ", "TestTitle")]
        [InlineData("TestAccount", "TestEndpoint", null)]
        [InlineData("TestAccount", "TestEndpoint", "")]
        [InlineData("TestAccount", "TestEndpoint", " ")]
        public void Constructor_WithInvalidInputs_ShouldThrowArgumentException(string accountName, string endpoint, string title)
        {
            Action act = () => new LocalObjectiveCompletionModel(accountName, endpoint, title, true);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void UniqueId_ShouldReturnCorrectFormat()
        {
            var model = new LocalObjectiveCompletionModel("TestAccount", "TestEndpoint", "TestTitle", true);
            model.UniqueId.Should().Be("TestAccount_TestEndpoint_TestTitle");
        }

        [Fact]
        public void Equals_WithSameValues_ShouldBeTrue()
        {
            var model1 = new LocalObjectiveCompletionModel("TestAccount", "TestEndpoint", "TestTitle", true);
            var model2 = new LocalObjectiveCompletionModel("TestAccount", "TestEndpoint", "TestTitle", true);

            model1.Should().Be(model2);
            (model1 == model2).Should().BeTrue();
        }

        [Fact]
        public void Equals_WithDifferentValues_ShouldBeFalse()
        {
            var model1 = new LocalObjectiveCompletionModel("TestAccount1", "TestEndpoint1", "TestTitle1", true);
            var model2 = new LocalObjectiveCompletionModel("TestAccount2", "TestEndpoint2", "TestTitle2", false);

            model1.Should().NotBe(model2);
            (model1 != model2).Should().BeTrue();
        }

        [Fact]
        public void GetHashCode_WithSameValues_ShouldBeEqual()
        {
            var model1 = new LocalObjectiveCompletionModel("TestAccount", "TestEndpoint", "TestTitle", true);
            var model2 = new LocalObjectiveCompletionModel("TestAccount", "TestEndpoint", "TestTitle", true);

            model1.GetHashCode().Should().Be(model2.GetHashCode());
        }
    }
}
