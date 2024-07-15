namespace WVTLib.Models
{
    public class ObjectiveModel : IEquatable<ObjectiveModel>
    {
        public string Account { get; }
        public string Title { get; }
        public string Track { get; }

        public ObjectiveModel(string account, string title, string track)
        {
            Account = account ?? throw new ArgumentNullException(nameof(account));
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Track = track ?? throw new ArgumentNullException(nameof(track));

            if (string.IsNullOrWhiteSpace(account)) throw new ArgumentException("Account cannot be empty", nameof(account));
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title cannot be empty", nameof(title));
            if (string.IsNullOrWhiteSpace(track)) throw new ArgumentException("Track cannot be empty", nameof(track));
        }

        public override string ToString() => $"Account: {Account}, Track: {Track}, Title: {Title}";

        public override bool Equals(object? obj) => Equals(obj as ObjectiveModel);

        public bool Equals(ObjectiveModel? other)
        {
            return other != null &&
                   Account == other.Account &&
                   Title == other.Title &&
                   Track == other.Track;
        }

        public override int GetHashCode() => HashCode.Combine(Account, Title, Track);

        public static bool operator ==(ObjectiveModel? left, ObjectiveModel? right)
        {
            return EqualityComparer<ObjectiveModel>.Default.Equals(left, right);
        }

        public static bool operator !=(ObjectiveModel? left, ObjectiveModel? right)
        {
            return !(left == right);
        }
    }
}
