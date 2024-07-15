using FluentAssertions;
using System.Text.Json;
using WVTLib.Models;

namespace TestWVT.WVTLib
{
    public class ApiKeyModelTests
    {
        [Fact]
        public void Constructor_WithValidInputs_ShouldCreateInstance()
        {
            var model = new ApiKeyModel("TestName ", "TestToken");

            model.Name.Should().Be("TestName");
            model.Token.Should().Be("TestToken");
            model.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData(null, "TestToken")]
        [InlineData("", "TestToken")]
        [InlineData(" ", "TestToken")]
        [InlineData("TestName", null)]
        [InlineData("TestName", "")]
        [InlineData("TestName", " ")]
        public void Constructor_WithInvalidInputs_ShouldThrowArgumentException(string name, string token)
        {
            Action act = () => new ApiKeyModel(name, token);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void DisplayName_ShouldReturnCorrectFormat()
        {
            var model = new ApiKeyModel("TestName ", "TestToken");
            model.DisplayName.Should().Be("TestName");

            model.IsValid = false;
            model.DisplayName.Should().Be("TestName (Invalid)");
        }

        [Fact]
        public void ToString_ShouldReturnDisplayName()
        {
            var model = new ApiKeyModel("TestName ", "TestToken");
            model.ToString().Should().Be("TestName");
        }

        [Fact]
        public void Equals_WithSameValues_ShouldBeTrue()
        {
            var model1 = new ApiKeyModel("TestName ", "TestToken");
            var model2 = new ApiKeyModel("TestName", "TestToken");

            model1.Should().Be(model2);
            (model1 == model2).Should().BeTrue();
        }

        [Fact]
        public void Equals_WithDifferentValues_ShouldBeFalse()
        {
            var model1 = new ApiKeyModel("TestName1", "TestToken1");
            var model2 = new ApiKeyModel("TestName2", "TestToken2");

            model1.Should().NotBe(model2);
            (model1 != model2).Should().BeTrue();
        }

        [Fact]
        public void GetHashCode_WithSameValues_ShouldBeEqual()
        {
            var model1 = new ApiKeyModel("TestName ", "TestToken");
            var model2 = new ApiKeyModel("TestName", "TestToken");

            model1.GetHashCode().Should().Be(model2.GetHashCode());
        }

        [Fact]
        public void JsonSerialization_ShouldPreserveValues()
        {
            var model = new ApiKeyModel("TestName", "TestToken") { IsValid = false };

            var jsonString = JsonSerializer.Serialize(model);
            var deserializedModel = JsonSerializer.Deserialize<ApiKeyModel>(jsonString);

            deserializedModel.Should().NotBeNull();
            deserializedModel!.Name.Should().Be(model.Name);
            deserializedModel.Token.Should().Be(model.Token);
            deserializedModel.IsValid.Should().Be(model.IsValid);
        }

        [Fact]
        public void Token_ShouldBeConsistentAfterReassignment()
        {
            var model = new ApiKeyModel("TestName", "TestToken");
            var initialToken = model.Token;

            model.Token = "NewTestToken";
            var newToken = model.Token;

            initialToken.Should().Be("TestToken");
            newToken.Should().Be("NewTestToken");
        }

        [Fact]
        public void Token_ShouldNotBeStoredAsPlainText()
        {
            var model = new ApiKeyModel("TestName", "TestToken");
            var tokenFieldValue = GetPrivateField(model, "_encryptedToken") as string;

            tokenFieldValue.Should().NotBeNull();
            tokenFieldValue.Should().NotBe("TestToken");
        }

        private object? GetPrivateField(object obj, string fieldName)
        {
            var field = obj.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return field?.GetValue(obj);
        }
    }
}