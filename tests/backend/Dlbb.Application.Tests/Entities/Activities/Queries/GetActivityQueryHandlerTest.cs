

using AutoMapper;
using Dlbb.Application.Tests.Common;
using Dlbb.Track.Application.Activities.Queries.GetActivity;
using Dlbb.Track.Application.Exceptions;
using Dlbb.Track.Persistence.Contexts;
using FluentAssertions;

namespace Dlbb.Application.Tests.Entities.Activities.Queries;
[Collection("QueryCollection")]
public class GetActivityQueryHandlerTest
{
	private readonly AppDbContext Context;
	private readonly IMapper Mapper;

	public GetActivityQueryHandlerTest(QueryTestFixture fixture)
	{
		Context = fixture.Context;
		Mapper = fixture.Mapper;
	}

	[Fact]
	public async Task GetActivityQueryHandler_Success()
	{
		//Arrange
		var handler = new GetActivityQueryHandler(Context, Mapper);
		var query = new GetActivityQuery()
		{
			Id = AppDbContextFactory.ActivityIdForGet,
		};

		//Act
		var result = await handler.Handle(query, CancellationToken.None);

		//Assert
		result.Should().BeOfType<ActivityVm>();
		result.Name.Should().Be("Cleaning");
		result.Description.Should().Be("ubiraus");
	}

	[Fact]
	public async Task GetActivityQueryHandler_FailOnWrongId()
	{
		//Arrange
		var handler = new GetActivityQueryHandler(Context, Mapper);
		var query = new GetActivityQuery()
		{
			Id = Guid.NewGuid()
		};

		//Act
		var result = async () => await handler.Handle(query, CancellationToken.None);

		//Assert
		await result.Should().ThrowAsync<UserFriendlyException>();

		try
		{
			await result();
		}
		catch (UserFriendlyException e)
		{
			e.Status.Should().Be(Status.NotFound);
		}
	}
}
