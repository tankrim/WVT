using System.Runtime.Serialization;

namespace WVTLib.Models
{
    [Serializable]
    public class ApiKeyModel : ISerializable, IEquatable<ApiKeyModel>
    {
        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => _name = value?.Trim() ?? string.Empty;
        }

        private string _encryptedToken = string.Empty;
        public bool IsValid { get; set; } = true;
        public string DisplayName => $"{Name}{(IsValid ? "" : " (Invalid)")}";

        public string Token
        {
            get => EncryptionHelper.DecryptString(_encryptedToken);
            set => _encryptedToken = EncryptionHelper.EncryptString(value.Trim() ?? throw new ArgumentNullException(nameof(value)));
        }

        public ApiKeyModel(string name, string token)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or whitespace.", nameof(name));
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Token cannot be null or whitespace.", nameof(token));

            Name = name;
            Token = token;
        }

        // Parameterless constructor for serialization purposes
        public ApiKeyModel() { }

        public override string ToString() => DisplayName;

        protected ApiKeyModel(SerializationInfo info, StreamingContext context)
        {
            Name = info.GetString(nameof(Name)) ?? string.Empty;
            Token = info.GetString(nameof(Token)) ?? string.Empty;
            IsValid = info.GetBoolean(nameof(IsValid));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Name), Name);
            info.AddValue(nameof(Token), Token);
            info.AddValue(nameof(IsValid), IsValid);
        }

        public override bool Equals(object? obj) => Equals(obj as ApiKeyModel);

        public bool Equals(ApiKeyModel? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return other != null &&
                   Name == other.Name &&
                   Token == other.Token &&
                   IsValid == other.IsValid;
        }

        public override int GetHashCode() => HashCode.Combine(Name, Token, IsValid);
        
        public static bool operator ==(ApiKeyModel? left, ApiKeyModel? right)
        {
            if (left is null) return right is null;
            return EqualityComparer<ApiKeyModel>.Default.Equals(left, right);
        }

        public static bool operator !=(ApiKeyModel? left, ApiKeyModel? right)
        {
            return !(left == right);
        }
    }
}
