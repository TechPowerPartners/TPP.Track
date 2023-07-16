
using AutoMapper;
using Dlbb.Application.Tests.Common;
using Dlbb.Track.Application.Accounts.Queries.GetUser;
using Dlbb.Track.Application.Accounts.Shared;
using Dlbb.Track.Application.Exceptions;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Domain.Enums;
using Dlbb.Track.Persistence.Contexts;
using FluentAssertions;

namespace Dlbb.Application.Tests.Entities.Users;

[Collection("QueryCollection")]
public class GetUserQueryHandlerTest
{
	private readonly IMapper Mapper;
	private readonly AppDbContext Context;

	public GetUserQueryHandlerTest(QueryTestFixture fixture)
	{
		Mapper = fixture.Mapper;
		Context = fixture.Context;
	}

	[Fact]
	public async Task GetUserQueryHandler_Success()
	{
		//Arrange
		var handler = new GetUserQueryHandler(Context, Mapper);
		var query = new GetUserQuery()
		{
			Claims = AppDbContextFactory.UserAClaims,
		};

		//Act
		var user = await handler.Handle(query, CancellationToken.None);

		//Assert
		user.Should().NotBeNull();
		user.Should().BeOfType<AppUserVM>();
		user.Email.Should().Be("zalupa@gmail.com");
		user.UserName.Should().Be("Stas");
		user.Role.Should().Be(RoleEnum.User);
	}

	[Fact]
	public async Task GetUserQueryHandler_FailOnWrongUserClaims()
	{
		//Arrange
		var handler = new GetUserQueryHandler(Context, Mapper);
		var query = new GetUserQuery()
		{
			Claims = AutorizeUtils.GetClaimsFor(new AppUser()
			{
				UserName = "asdfa",
				Email = "ASDASD",
				PassworHash = "ASDASDASD",
				Role = RoleEnum.Admin
			})
		};

		//Act
		var result  = async () => await handler.Handle(query, CancellationToken.None);

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
