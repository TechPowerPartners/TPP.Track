using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dlbb.Application.Tests.Common;
using Dlbb.Track.Application.Exceptions;
using Dlbb.Track.Application.Sessions.Queries.GetSession;
using Dlbb.Track.Persistence.Contexts;
using FluentAssertions;

namespace Dlbb.Application.Tests.Entities.Sessions.Queries;

[Collection("QueryCollection")]
public class GetSessionQueryHandlerTest
{
	private readonly AppDbContext Context;
	private readonly IMapper Mapper;

	public GetSessionQueryHandlerTest(QueryTestFixture fixture)
	{
		Context = fixture.Context;
		Mapper = fixture.Mapper;
	}

	[Fact]
	public async Task GetSessionQueryHandler_Success()
	{
		//Arrange
		var handler = new GetSessionQueryHandler(Context,Mapper);
		var query = new GetSessionQuery()
		{
			Id = AppDbContextFactory.SessionIdForGet
		};

		//Act
		var result = await handler.Handle(query, CancellationToken.None);

		//Assert
		result.Should().NotBeNull();
		result.Should().BeOfType<SessionVm>();
		result.StartTime.Should().Be(AppDbContextFactory.SessionStartTimeForGet);
		result.AppUserId.Should().Be(AppDbContextFactory.UserBId);
	}

	[Fact]
	public async Task GetSessionQueryHandler_FailOnWrongSessionId()
	{
		//Arrange
		var handler = new GetSessionQueryHandler(Context, Mapper);
		var query = new GetSessionQuery()
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
