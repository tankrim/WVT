namespace WVTLib.Models
{
    [Serializable]
    public class LocalObjectiveCompletionModel : IEquatable<LocalObjectiveCompletionModel>
    {
        public string AccountName { get; set; } = string.Empty;
        public string Endpoint { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public string UniqueId => $"{AccountName}_{Endpoint}_{Title}";

        public LocalObjectiveCompletionModel() { }

        public LocalObjectiveCompletionModel(string accountName, string endpoint, string title, bool isCompleted)
        {
            AccountName = accountName ?? throw new ArgumentNullException(nameof(accountName));
            Endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
            Title = title ?? throw new ArgumentNullException(nameof(title));
            IsCompleted = isCompleted;

            if (string.IsNullOrWhiteSpace(accountName)) throw new ArgumentException("AccountName cannot be empty", nameof(accountName));
            if (string.IsNullOrWhiteSpace(endpoint)) throw new ArgumentException("Endpoint cannot be empty", nameof(endpoint));
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title cannot be empty", nameof(title));
        }

        public override bool Equals(object? obj) => Equals(obj as LocalObjectiveCompletionModel);

        public bool Equals(LocalObjectiveCompletionModel? other)
        {
            return other != null &&
                   AccountName == other.AccountName &&
                   Endpoint == other.Endpoint &&
                   Title == other.Title &&
                   IsCompleted == other.IsCompleted;
        }

        public override int GetHashCode() => HashCode.Combine(AccountName, Endpoint, Title, IsCompleted);

        public static bool operator ==(LocalObjectiveCompletionModel? left, LocalObjectiveCompletionModel? right)
        {
            return EqualityComparer<LocalObjectiveCompletionModel>.Default.Equals(left, right);
        }

        public static bool operator !=(LocalObjectiveCompletionModel? left, LocalObjectiveCompletionModel? right)
        {
            return !(left == right);
        }
    }
}
