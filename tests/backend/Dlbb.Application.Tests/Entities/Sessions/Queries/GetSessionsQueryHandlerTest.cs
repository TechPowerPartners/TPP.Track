using AutoMapper;
using Dlbb.Application.Tests.Common;
using Dlbb.Track.Application.Sessions.Queries.GetSession;
using Dlbb.Track.Application.Sessions.Queries.GetSessions;
using Dlbb.Track.Persistence.Contexts;
using FluentAssertions;

namespace Dlbb.Application.Tests.Entities.Sessions.Queries;

[Collection("QueryCollection")]
public class GetSessionsQueryHandlerTest
{
	private readonly IMapper Mapper;
	private readonly AppDbContext Context;

	public GetSessionsQueryHandlerTest(QueryTestFixture fixture)
	{
		Context = fixture.Context;
		Mapper = fixture.Mapper;
	}

	[Fact]
	public async Task GetSessionsQueryHandler_Success()
	{
		//Arrange
		var handler = new GetSessionsQueryHandler(Context, Mapper);
		var query = new GetSessionsQuery();

		//Act
		var result = await handler.Handle(query, CancellationToken.None);

		//Assert
		result.Should().NotBeNull();
		result.Should().BeOfType<List<SessionVm>>();
		result.Count.Should().Be(14);
	}
}
