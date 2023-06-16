using Dlbb.Track.Domain.Entities.Base;

namespace Dlbb.Track.Domain.Entities
{
    public class Session :BaseEntity
    {
        public TimeOnly Duration { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Activity Activity { get; } = new();
    }
}