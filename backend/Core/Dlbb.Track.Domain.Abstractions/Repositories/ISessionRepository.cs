﻿using System.Linq.Expressions;
using Dlbb.Track.Domain.Abstractions.Repositories.Base;
using Dlbb.Track.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Dlbb.Track.Domain.Abstractions.Repositories;
public interface ISessionRepository : IGenericRepository<Session>
{
}
