using AutoMapper;
using Dlbb.Track.Application.Exceptions;
using Dlbb.Track.Common.Exceptions.Extensions;
using Dlbb.Track.Domain.Specifications;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Sessions.Queries.GetSession;
public class GetSessionQueryHandler : IRequestHandler<GetSessionQuery, SessionVm>
{
	private readonly AppDbContext _dbContext;
	private readonly IMapper _mapper;

	public GetSessionQueryHandler(AppDbContext dbContext, IMapper mapper)
	{
		_dbContext = dbContext;
		_mapper = mapper;
	}

	public async Task<SessionVm> Handle(GetSessionQuery request, CancellationToken cancellationToken)
	{
		var session = await _dbContext.Sessions.SingleOrDefaultAsync
			(new IsSpecSession(request.Id), cancellationToken);

		session!.ThrowUserFriendlyExceptionIfNull
			(status: Status.NotFound,
			message: $"Not found \"Id\" : {request.Id}");

		var result = _mapper.Map<SessionVm>(session);

		return result;
	}
}
