﻿using Dlbb.Track.Domain.Entities.Base;

namespace Dlbb.Track.Domain.Entities
{
	public class Session : BaseEntity
	{
		public string? Description { get; set; }
		public TimeOnly? Duration { get; set; }
		public DateTime StartTime { get; set; }
		public Guid ActivityId { get; set; }
		public Activity Activity { get; set; } = new();
		public Guid AppUserId { get; set; }
		public AppUser AppUser { get; set; }
	}
}
