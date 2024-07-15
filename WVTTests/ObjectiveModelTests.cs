using FluentAssertions;
using WVTLib.Models;

namespace WVTTests
{
    public class ObjectiveModelTests
    {
        [Fact]
        public void Constructor_WithValidInputs_ShouldCreateInstance()
        {
            var model = new ObjectiveModel("TestAccount", "TestTitle", "TestTrack");

            model.Account.Should().Be("TestAccount");
            model.Title.Should().Be("TestTitle");
            model.Track.Should().Be("TestTrack");
        }

        [Theory]
        [InlineData(null, "TestTitle", "TestTrack")]
        [InlineData("", "TestTitle", "TestTrack")]
        [InlineData(" ", "TestTitle", "TestTrack")]
        [InlineData("TestAccount", null, "TestTrack")]
        [InlineData("TestAccount", "", "TestTrack")]
        [InlineData("TestAccount", " ", "TestTrack")]
        [InlineData("TestAccount", "TestTitle", null)]
        [InlineData("TestAccount", "TestTitle", "")]
        [InlineData("TestAccount", "TestTitle", " ")]
        public void Constructor_WithInvalidInputs_ShouldThrowArgumentException(string account, string title, string track)
        {
            Action act = () => new ObjectiveModel(account, title, track);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void ToString_ShouldReturnCorrectFormat()
        {
            var model = new ObjectiveModel("TestAccount", "TestTitle", "TestTrack");
            model.ToString().Should().Be("Account: TestAccount, Track: TestTrack, Title: TestTitle");
        }

        [Fact]
        public void Equals_WithSameValues_ShouldBeTrue()
        {
            var model1 = new ObjectiveModel("TestAccount", "TestTitle", "TestTrack");
            var model2 = new ObjectiveModel("TestAccount", "TestTitle", "TestTrack");

            model1.Should().Be(model2);
            (model1 == model2).Should().BeTrue();
        }

        [Fact]
        public void Equals_WithDifferentValues_ShouldBeFalse()
        {
            var model1 = new ObjectiveModel("TestAccount1", "TestTitle1", "TestTrack1");
            var model2 = new ObjectiveModel("TestAccount2", "TestTitle2", "TestTrack2");

            model1.Should().NotBe(model2);
            (model1 != model2).Should().BeTrue();
        }

        [Fact]
        public void GetHashCode_WithSameValues_ShouldBeEqual()
        {
            var model1 = new ObjectiveModel("TestAccount", "TestTitle", "TestTrack");
            var model2 = new ObjectiveModel("TestAccount", "TestTitle", "TestTrack");

            model1.GetHashCode().Should().Be(model2.GetHashCode());
        }
    }
}
