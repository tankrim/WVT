namespace VWTracker
{
    public class GroupedObjective
    {
        public string Endpoint { get; set; } = string.Empty;
        public string Track { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public List<string> Accounts { get; set; } = [];
        public bool Completed { get; set; }
    }

    public class DisplayObjective
    {
        public string Account { get; set; } = string.Empty;
        public string Endpoint { get; set; } = string.Empty;
        public string Track { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public bool Completed { get; set; }
        public string Others { get; set; } = string.Empty;
    }
}
