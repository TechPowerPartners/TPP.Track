﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dlbb.Track.Domain.Entities.Base;
public abstract class BaseActivity: BaseEntity
{
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; } = string.Empty;
}