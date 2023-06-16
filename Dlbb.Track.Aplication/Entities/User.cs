namespace Dlbb.Track.Aplication.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Password { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public List<Session> Sessions { get; } = new();
        public List<Activity> Activities { get; } = new();

    }
}
