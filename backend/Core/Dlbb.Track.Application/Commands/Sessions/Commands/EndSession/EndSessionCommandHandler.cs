using AutoMapper;
using Dlbb.Track.Application.Exceptions;
using Dlbb.Track.Common.Exceptions.Extensions;
using Dlbb.Track.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Track.Application.Sessions.Commands.EndSession;
public class EndSessionCommandHandler : IRequestHandler<EndSessionCommand>
{
	private readonly IMapper _mapper;
	private readonly AppDbContext _dbContext;

	public EndSessionCommandHandler(AppDbContext dbContext, IMapper mapper)
	{
		_mapper = mapper;
		_dbContext = dbContext;
	}

	public async Task<Unit> Handle
		(EndSessionCommand request,
		CancellationToken cancellationToken)
	{
		var session = await _dbContext.Sessions.SingleOrDefaultAsync
			(s => s.Id == request.Id, cancellationToken);

		session!.ThrowUserFriendlyExceptionIfNull
			(status: Status.NotFound,
			message: $"Not found \"Id\" : {request.Id}");

		session = _mapper.Map(request, session);

		await _dbContext.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
