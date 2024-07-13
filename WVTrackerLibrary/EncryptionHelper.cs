namespace WVTrackerLibrary
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public static class EncryptionHelper
    {
        private static readonly byte[] Entropy = Encoding.Unicode.GetBytes("WVTSalt");

        public static string EncryptString(string input)
        {
            if (input != null)
            {
                byte[] encryptedData = ProtectedData.Protect(
                    Encoding.Unicode.GetBytes(input),
                    Entropy,
                    DataProtectionScope.CurrentUser);
                return Convert.ToBase64String(encryptedData);
            }

            throw new ArgumentNullException(nameof(input));
        }

        public static string DecryptString(string? encryptedData)
        {
            if (string.IsNullOrEmpty(encryptedData))
            {
                return string.Empty;
            }

            try
            {
                byte[] decryptedData = ProtectedData.Unprotect(
                    Convert.FromBase64String(encryptedData),
                    Entropy,
                    DataProtectionScope.CurrentUser);
                return Encoding.Unicode.GetString(decryptedData);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
