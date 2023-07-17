using System.Security.Claims;
using AutoMapper;
using Dlbb.Track.Application.Exceptions;
using Dlbb.Track.Common.Exceptions.Extensions;
using Dlbb.Track.Domain.Abstractions.Repositories.Base;
using Dlbb.Track.Domain.Specifications;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Accounts.Queries.GetUser;
public class GetUserQueryHandler : IRequestHandler<GetUserQuery, AppUserVM>
{
	private readonly IUserRepository _userRep;
	private readonly IMapper _mapper;

	public GetUserQueryHandler(IUserRepository userRep, IMapper mapper)
	{
		_userRep = userRep;
		_mapper = mapper;
	}

	public async Task<AppUserVM> Handle
		(GetUserQuery request,
		CancellationToken cancellationToken)
	{
		var id = request.Id;

		var userDb = await _userRep.GetSingleUserAsync
			(new IsSpecUser(id), cancellationToken);

		userDb!.ThrowUserFriendlyExceptionIfNull
			(Status.NotFound, $"Not Found \"AppUserId\" : {id}");

		var result = _mapper.Map<AppUserVM>(userDb);

		return result;
	}
}
