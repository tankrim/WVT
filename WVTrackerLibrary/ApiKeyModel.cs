using System.Runtime.Serialization;

namespace WVTrackerLibrary
{
    public class ApiKeyModel : ISerializable
    {
        public string Name { get; set; } = string.Empty;
        public string _encryptedToken;
        public bool IsValid { get; set; } = true;
        public string DisplayName => $"{Name} {(IsValid ? "" : "(Invalid)")}";

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
            return DisplayName;
        }

        protected ApiKeyModel(SerializationInfo info, StreamingContext context)
        {
            Name = info.GetString(nameof(Name));
            Token = info.GetString(nameof(Token));
            IsValid = info.GetBoolean(nameof(IsValid));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Name), Name);
            info.AddValue(nameof(Token), Token);
            info.AddValue(nameof(IsValid), IsValid);
        }
    }
}
