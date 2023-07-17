
using Dlbb.Application.Tests.Common;
using Dlbb.Track.Application.Accounts.Commands.Register;
using Dlbb.Track.Persistence.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Application.Tests.Entities.Users;
public class RegisterUserCommandHandlerTest : TestCommandBase
{
	[Fact]
	public async Task RegisterUserCommandHandler_Success()
	{
		//Arrange
		var hasher = new PasswordHasher();
		var handler = new RegisterCommandHandler(Context, hasher);
		var command = new RegisterUserCommand()
		{
			Email = "yandex@gmail.com",
			Password = "Password",
			UserName = "Loh"
		};

		//Act
		var jwtToken = await handler.Handle(command, CancellationToken.None);
		var result = await Context.AppUsers.SingleOrDefaultAsync
			(u => u.Email == "yandex@gmail.com" &&
			hasher.Verify("Password", u.PassworHash) &&
			u.UserName == "Loh");


		//Assert
		jwtToken.Should().NotBeNull();
		Context.AppUsers.Count().Should().Be(3);
		result.Should().NotBeNull();
	}
}
