namespace WVTLib.Models
{
    [Serializable]
    public class LocalObjectiveCompletionModel
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
        }
    }
}
