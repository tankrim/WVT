namespace WVTrackerLibrary
{
    public class ApiKeyModel
    {
        public ApiKeyModel(string name, string token)
        {
            Name = name;
            Token = token;
        }

        // Parameterless constructor for serialization purposes
        public ApiKeyModel() { }

        public string Name { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public bool IsValid { get; set; } = true;

        public override string ToString()
        {
            return $"{Name} {(IsValid ? "" : "(Invalid)")}";
        }
    }
}
