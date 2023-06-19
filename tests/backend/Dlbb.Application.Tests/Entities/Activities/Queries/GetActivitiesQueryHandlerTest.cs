
using Dlbb.Track.Application.Activities.Queries.GetActivities;
using Dlbb.Application.Tests.Common;
using AutoMapper;
using Dlbb.Track.Persistence.Contexts;
using FluentAssertions;
using Dlbb.Track.Application.Activities.Queries.GetActivity;

namespace Dlbb.Application.Tests.Entities.Activities.Queries;

[Collection("QueryCollection")]
public class GetActivitiesQueryHandlerTest
{
	private readonly AppDbContext Context;
	private readonly IMapper Mapper;

	public GetActivitiesQueryHandlerTest(QueryTestFixture fixture)
	{
		Context = fixture.Context;
		Mapper = fixture.Mapper;
	}

	[Fact]
	public async Task GetActivitiesQueryHandler_Succes()
	{
		//Arrange
		var handler = new GetActivitiesQueryHandler(Context, Mapper);
		var query = new GetActivitiesQuery();

		//Act
		var result = await handler.Handle(query, CancellationToken.None);

		//Assert
		result.Should().BeOfType<List<ActivityVm>>();
		result.Count.Should().Be(4);
	}
}
