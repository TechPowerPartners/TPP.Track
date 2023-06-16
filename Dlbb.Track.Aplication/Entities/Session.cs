namespace Dlbb.Track.Aplication.Entities
{
    public class Session
    {
        public Guid Id { get; set; }
        public TimeOnly Duration { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<Activity> Activities { get; } = new();
    }
}