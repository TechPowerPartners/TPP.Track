﻿using Dlbb.Track.Domain.Entities.Base;

namespace Dlbb.Track.Domain.Entities;
public class Category : BaseEntity
{
	public string Name { get; set; }
	public string? Description { get; set; }
	public ICollection<Activity> Activities { get; set; }
	public bool IsGlobal { get; set; } = false;
	public AppUser AppUser { get; set; }
	public Guid AppUserId { get; set; }
}
