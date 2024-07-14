namespace WVTLib.Models
{
    [Serializable]
    public class LocalObjectiveCompletionModel
    {
        public string AccountName { get; set; }
        public string Endpoint { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }

        // Unique identifier for the objective
        public string UniqueId => $"{AccountName}_{Endpoint}_{Title}";
    }
}
