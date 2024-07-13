namespace WVTrackerLibrary
{
    public class ApiKeyModel
    {
        public string Name { get; set; } = string.Empty;
        public string _encryptedToken;
        public bool IsValid { get; set; } = true;

        public string Token
        {
            get { return EncryptionHelper.DecryptString(_encryptedToken); }
            set { _encryptedToken = EncryptionHelper.EncryptString(value); }
        }

        public ApiKeyModel(string name, string token)
        {
            Name = name;
            Token = token;
        }

        // Parameterless constructor for serialization purposes
        public ApiKeyModel() { }

        public override string ToString()
        {
            return $"{Name} {(IsValid ? "" : "(Invalid)")}";
        }
    }
}
