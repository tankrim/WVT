using FluentAssertions;
using WVTApp.Models;

namespace TestWVT.WVTApp
{
    public class DisplayObjectiveTests
    {
        [Fact]
        public void Constructor_WithValidInputs_ShouldCreateInstance()
        {
            var objective = new DisplayObjective("Account1", "Daily", "PvE", "Test Objective", true, "Account2, Account3");

            objective.Account.Should().Be("Account1");
            objective.Endpoint.Should().Be("Daily");
            objective.Track.Should().Be("PvE");
            objective.Title.Should().Be("Test Objective");
            objective.Completed.Should().BeTrue();
            objective.Others.Should().Be("Account2, Account3");
        }

        [Theory]
        [InlineData(null, "Daily", "PvE", "Title")]
        [InlineData("", "Daily", "PvE", "Title")]
        [InlineData("Account", null, "PvE", "Title")]
        [InlineData("Account", "", "PvE", "Title")]
        [InlineData("Account", "Daily", null, "Title")]
        [InlineData("Account", "Daily", "", "Title")]
        [InlineData("Account", "Daily", "PvE", null)]
        [InlineData("Account", "Daily", "PvE", "")]
        public void Constructor_WithInvalidInputs_ShouldThrowArgumentException(string account, string endpoint, string track, string title)
        {
            Action act = () => new DisplayObjective(account, endpoint, track, title, true, "Others");

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Constructor_WithNullOthers_ShouldThrowArgumentNullException()
        {
            Action act = () => new DisplayObjective("Account", "Daily", "PvE", "Title", true, null);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Equals_WithSameValues_ShouldBeTrue()
        {
            var obj1 = new DisplayObjective("Account1", "Daily", "PvE", "Test", true, "Account2");
            var obj2 = new DisplayObjective("Account1", "Daily", "PvE", "Test", true, "Account2");

            obj1.Should().Be(obj2);
            (obj1 == obj2).Should().BeTrue();
        }

        [Fact]
        public void Equals_WithDifferentValues_ShouldBeFalse()
        {
            var obj1 = new DisplayObjective("Account1", "Daily", "PvE", "Test1", true, "Account2");
            var obj2 = new DisplayObjective("Account1", "Weekly", "PvP", "Test2", false, "Account3");

            obj1.Should().NotBe(obj2);
            (obj1 != obj2).Should().BeTrue();
        }

        [Fact]
        public void GetHashCode_WithSameValues_ShouldBeEqual()
        {
            var obj1 = new DisplayObjective("Account1", "Daily", "PvE", "Test", true, "Account2");
            var obj2 = new DisplayObjective("Account1", "Daily", "PvE", "Test", true, "Account2");

            obj1.GetHashCode().Should().Be(obj2.GetHashCode());
        }
    }
}
