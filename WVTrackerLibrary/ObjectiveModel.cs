namespace WVTrackerLibrary
{
    public class ObjectiveModel
    {
        public string Account {  get; private set; }
        public string Title { get; private set; }
        public string Track { get; private set; }
        public bool Claimed { get; private set; }

        public ObjectiveModel(string account, string title, string track, bool claimed)
        {
            Account = account;
            Title = title;
            Track = track;
            Claimed = claimed;
        }

        public override string ToString()
        {
            return $"Account: {Account}, Track: {Track}, Title: {Title}, Claimed: {Claimed}";
        }
    }
}
