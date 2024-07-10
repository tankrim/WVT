namespace WVTrackerLibrary
{
    public class ApiKeyModel
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty or whitespace.");
                _name = value;
            }
        }

        private string _token;
        public string Token
        {
            get => _token;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Token cannot be empty or whitespace.");
                _token = value;
            }
        }

        public ApiKeyModel(string name, string token)
        {
            Name = name;
            Token = token;
        }

        // Parameterless constructor for serialization purposes
        public ApiKeyModel() { }
    }
}
