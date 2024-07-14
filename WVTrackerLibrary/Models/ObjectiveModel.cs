namespace WVTLib.Models
{
    public class ObjectiveModel(string account, string title, string track)
    {
        public string Account { get; private set; } = account;
        public string Title { get; private set; } = title;
        public string Track { get; private set; } = track;

        public override string ToString()
        {
            return $"Account: {Account}, Track: {Track}, Title: {Title}";
        }
    }
}
