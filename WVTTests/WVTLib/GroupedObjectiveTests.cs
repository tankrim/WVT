using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVTLib.Models;

namespace WVTTests.WVTLib
{
    public class GroupedObjectiveTests
    {
        [Fact]
        public void Constructor_WithValidInputs_ShouldCreateInstance()
        {
            var accounts = new List<string> { "Account1", "Account2" };
            var objective = new GroupedObjective("Daily", "PvE", "Test Objective", accounts);

            objective.Endpoint.Should().Be("Daily");
            objective.Track.Should().Be("PvE");
            objective.Title.Should().Be("Test Objective");
            objective.Accounts.Should().BeEquivalentTo(accounts);
        }

        [Theory]
        [InlineData(null, "PvE", "Title")]
        [InlineData("", "PvE", "Title")]
        [InlineData("Daily", null, "Title")]
        [InlineData("Daily", "", "Title")]
        [InlineData("Daily", "PvE", null)]
        [InlineData("Daily", "PvE", "")]
        public void Constructor_WithInvalidInputs_ShouldThrowArgumentException(string endpoint, string track, string title)
        {
            var accounts = new List<string> { "Account1" };
            Action act = () => new GroupedObjective(endpoint, track, title, accounts);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Constructor_WithNullAccounts_ShouldThrowArgumentNullException()
        {
            Action act = () => new GroupedObjective("Daily", "PvE", "Title", null);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Equals_WithSameValues_ShouldBeTrue()
        {
            var accounts1 = new List<string> { "Account1", "Account2" };
            var accounts2 = new List<string> { "Account2", "Account1" };
            var obj1 = new GroupedObjective("Daily", "PvE", "Test", accounts1);
            var obj2 = new GroupedObjective("Daily", "PvE", "Test", accounts2);

            obj1.Should().Be(obj2);
            (obj1 == obj2).Should().BeTrue();
        }

        [Fact]
        public void Equals_WithDifferentValues_ShouldBeFalse()
        {
            var accounts = new List<string> { "Account1" };
            var obj1 = new GroupedObjective("Daily", "PvE", "Test1", accounts);
            var obj2 = new GroupedObjective("Weekly", "PvE", "Test2", accounts);

            obj1.Should().NotBe(obj2);
            (obj1 != obj2).Should().BeTrue();
        }

        [Fact]
        public void GetHashCode_WithSameValues_ShouldBeEqual()
        {
            var accounts1 = new List<string> { "Account1", "Account2" };
            var accounts2 = new List<string> { "Account2", "Account1" };
            var obj1 = new GroupedObjective("Daily", "PvE", "Test", accounts1);
            var obj2 = new GroupedObjective("Daily", "PvE", "Test", accounts2);

            obj1.GetHashCode().Should().Be(obj2.GetHashCode());
        }

        [Fact]
        public void Equals_WithDifferentAccountOrder_ShouldBeTrue()
        {
            var obj1 = new GroupedObjective("Daily", "PvE", "Test", new List<string> { "Account1", "Account2", "Account3" });
            var obj2 = new GroupedObjective("Daily", "PvE", "Test", new List<string> { "Account3", "Account1", "Account2" });

            obj1.Should().Be(obj2);
        }

        [Fact]
        public void GetHashCode_WithDifferentAccountOrder_ShouldBeEqual()
        {
            var obj1 = new GroupedObjective("Daily", "PvE", "Test", new List<string> { "Account1", "Account2", "Account3" });
            var obj2 = new GroupedObjective("Daily", "PvE", "Test", new List<string> { "Account3", "Account1", "Account2" });

            obj1.GetHashCode().Should().Be(obj2.GetHashCode());
        }
    }
}
