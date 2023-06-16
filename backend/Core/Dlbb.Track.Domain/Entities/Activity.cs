using Dlbb.Track.Domain.Entities.Base;

namespace Dlbb.Track.Domain.Entities
{
    public class Activity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public List<Session> Sessions { get; } = new();
    }
}
