using AutoMapper;
using Dlbb.Application.Tests.Common;
using Dlbb.Track.Application.Accounts.Queries.Login;
using Dlbb.Track.Application.Exceptions;
using Dlbb.Track.Persistence.Contexts;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Application.Tests.Entities.Users;
[Collection("QueryCollection")]
public class LoginUserQueryHandlerTest
{
	private readonly AppDbContext _context;
	private readonly IMapper _mapper;

	public LoginUserQueryHandlerTest(QueryTestFixture fixture)
	{
		_context = fixture.Context;
		_mapper = fixture.Mapper;
	}

	[Fact]
	public async Task LoginUserQueryHandler_Success()
	{
		//Arrange
		var handler = new LoginQueryHandler(_context, _mapper, new());
		var query = new LoginQuery()
		{
			ExpectedEmail = "zalupa@gmail.com",
			ExpectedPassword = "UserAPassword",
		};

		//Act
		var jwtToken = await handler.Handle(query, CancellationToken.None);
		var result = await _context.AppUsers.SingleOrDefaultAsync(u =>
		u.Id == AppDbContextFactory.UserAId);

		//Assert
		jwtToken.Should().NotBeNull();
		result.Should().NotBeNull();
	}

	[Fact]
	public async Task LoginUserQueryHandler_FailOnWrongEmail()
	{
		//Arrange
		var handler = new LoginQueryHandler(_context, _mapper, new());
		var query = new LoginQuery()
		{
			ExpectedEmail = "asdfasdfasdf",
			ExpectedPassword = "UserAPassword",
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

	[Fact]
	public async Task LoginUserQueryHandler_FailOnWrongPassword()
	{
		//Arrange
		var handler = new LoginQueryHandler(_context, _mapper, new());
		var query = new LoginQuery()
		{
			ExpectedEmail = "zalupa@gmail.com",
			ExpectedPassword = "asdfasdfafd",
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
