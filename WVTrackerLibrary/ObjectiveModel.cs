namespace WVTrackerLibrary
{
    public class ObjectiveModel(string account, string title, string track, bool completed)
    {
        public string Account { get; private set; } = account;
        public string Title { get; private set; } = title;
        public string Track { get; private set; } = track;
        public bool Completed { get; private set; } = completed;

        public override string ToString()
        {
            return $"Account: {Account}, Track: {Track}, Title: {Title}, Completed: {Completed}";
        }
    }
}
