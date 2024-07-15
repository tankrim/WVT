namespace WVTApp.Models
{
    public class GroupedObjective : IEquatable<GroupedObjective>
    {
        public string Endpoint { get; set; } = string.Empty;
        public string Track { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public List<string> Accounts { get; set; } = [];

        public GroupedObjective() { }

        public GroupedObjective(string endpoint, string track, string title, List<string> accounts)
        {
            Endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
            Track = track ?? throw new ArgumentNullException(nameof(track));
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Accounts = accounts ?? throw new ArgumentNullException(nameof(accounts));

            if (string.IsNullOrWhiteSpace(endpoint)) throw new ArgumentException("Endpoint cannot be empty", nameof(endpoint));
            if (string.IsNullOrWhiteSpace(track)) throw new ArgumentException("Track cannot be empty", nameof(track));
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title cannot be empty", nameof(title));
        }

        public override bool Equals(object? obj) => Equals(obj as GroupedObjective);

        public bool Equals(GroupedObjective? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Endpoint == other.Endpoint &&
                   Track == other.Track &&
                   Title == other.Title &&
                   Accounts.Count == other.Accounts.Count &&
                   Accounts.All(a => other.Accounts.Contains(a));
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Endpoint);
            hashCode.Add(Track);
            hashCode.Add(Title);
            foreach (var account in Accounts.OrderBy(a => a))
            {
                hashCode.Add(account);
            }
            return hashCode.ToHashCode();
        }

        public static bool operator ==(GroupedObjective? left, GroupedObjective? right)
        {
            if (left is null) return right is null;
            return left.Equals(right);
        }

        public static bool operator !=(GroupedObjective? left, GroupedObjective? right) => !(left == right);
    }

    public class DisplayObjective : IEquatable<DisplayObjective>
    {
        public string Account { get; set; } = string.Empty;
        public string Endpoint { get; set; } = string.Empty;
        public string Track { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public bool Completed { get; set; }
        public string Others { get; set; } = string.Empty;

        public DisplayObjective() { }

        public DisplayObjective(string account, string endpoint, string track, string title, bool completed, string others)
        {
            Account = account ?? throw new ArgumentNullException(nameof(account));
            Endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
            Track = track ?? throw new ArgumentNullException(nameof(track));
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Completed = completed;
            Others = others ?? throw new ArgumentNullException(nameof(others));

            if (string.IsNullOrWhiteSpace(account)) throw new ArgumentException("Account cannot be empty", nameof(account));
            if (string.IsNullOrWhiteSpace(endpoint)) throw new ArgumentException("Endpoint cannot be empty", nameof(endpoint));
            if (string.IsNullOrWhiteSpace(track)) throw new ArgumentException("Track cannot be empty", nameof(track));
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title cannot be empty", nameof(title));
        }

        public override bool Equals(object? obj) => Equals(obj as DisplayObjective);

        public bool Equals(DisplayObjective? other)
        {
            return other != null &&
                   Account == other.Account &&
                   Endpoint == other.Endpoint &&
                   Track == other.Track &&
                   Title == other.Title &&
                   Completed == other.Completed &&
                   Others == other.Others;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Account, Endpoint, Track, Title, Completed, Others);
        }

        public static bool operator ==(DisplayObjective? left, DisplayObjective? right)
        {
            return EqualityComparer<DisplayObjective>.Default.Equals(left, right);
        }

        public static bool operator !=(DisplayObjective? left, DisplayObjective? right)
        {
            return !(left == right);
        }
    }
}
