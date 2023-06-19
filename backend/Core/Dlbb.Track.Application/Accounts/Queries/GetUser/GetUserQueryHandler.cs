using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dlbb.Track.Application.Sessions.Queries.GetSession;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Accounts.Queries.GetUser;
public class GetUserQueryHandler : IRequestHandler<GetUserQuery, AppUserVM>
{
	private readonly AppDbContext _dbContext;
	private readonly IMapper _mapper;

	public GetUserQueryHandler(AppDbContext dbContext, IMapper mapper)
	{
		_dbContext = dbContext;
		_mapper = mapper;
	}


	public async Task<AppUserVM> Handle(GetUserQuery request, CancellationToken cancellationToken)
	{
		var id = request.Claims.First(c => ClaimTypes.IsPersistent == c.Type)!.Value;

		var userDb = _dbContext.AppUsers.FirstOrDefault(u => u.Id == Guid.Parse(id));

		var result = _mapper.Map<AppUserVM>(userDb);

		return result;
	}
}
