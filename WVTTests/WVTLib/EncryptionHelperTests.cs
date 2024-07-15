using FluentAssertions;
using WVTLib;

namespace TestWVT.WVTLib
{
    public class EncryptionHelperTests
    {
        [Fact]
        public void EncryptString_WithValidInput_ShouldReturnNonEmptyString()
        {
            string input = "Test123";
            string encrypted = EncryptionHelper.EncryptString(input);

            encrypted.Should().NotBeNullOrEmpty();
            encrypted.Should().NotBe(input);
        }

        [Fact]
        public void EncryptString_WithNullInput_ShouldThrowArgumentNullException()
        {
            Action act = () => EncryptionHelper.EncryptString(null);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void EncryptString_WithEmptyString_ShouldReturnNonEmptyString()
        {
            string input = string.Empty;
            string encrypted = EncryptionHelper.EncryptString(input);

            encrypted.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void DecryptString_WithValidInput_ShouldReturnOriginalString()
        {
            string original = "Test123";
            string encrypted = EncryptionHelper.EncryptString(original);
            string decrypted = EncryptionHelper.DecryptString(encrypted);

            decrypted.Should().Be(original);
        }

        [Fact]
        public void DecryptString_WithNullInput_ShouldReturnEmptyString()
        {
            string decrypted = EncryptionHelper.DecryptString(null);

            decrypted.Should().BeEmpty();
        }

        [Fact]
        public void DecryptString_WithEmptyString_ShouldReturnEmptyString()
        {
            string decrypted = EncryptionHelper.DecryptString(string.Empty);

            decrypted.Should().BeEmpty();
        }

        [Fact]
        public void DecryptString_WithInvalidInput_ShouldReturnEmptyString()
        {
            string decrypted = EncryptionHelper.DecryptString("NotValidBase64String");

            decrypted.Should().BeEmpty();
        }

        [Fact]
        public void EncryptAndDecrypt_WithSpecialCharacters_ShouldReturnOriginalString()
        {
            string original = "Test123!@#$%^&*()_+";
            string encrypted = EncryptionHelper.EncryptString(original);
            string decrypted = EncryptionHelper.DecryptString(encrypted);

            decrypted.Should().Be(original);
        }

        [Fact]
        public void EncryptAndDecrypt_WithUnicodeCharacters_ShouldReturnOriginalString()
        {
            string original = "Test123 测试 こんにちは";
            string encrypted = EncryptionHelper.EncryptString(original);
            string decrypted = EncryptionHelper.DecryptString(encrypted);

            decrypted.Should().Be(original);
        }
    }
}