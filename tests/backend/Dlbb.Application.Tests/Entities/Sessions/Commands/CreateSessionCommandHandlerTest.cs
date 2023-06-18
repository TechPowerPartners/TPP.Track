
using Dlbb.Application.Tests.Common;
using Dlbb.Track.Application.Exceptions;
using Dlbb.Track.Application.Sessions.Commands.CreateSession;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Application.Tests.Entities.Sessions.Commands;
public class CreateSessionCommandHandlerTest : TestCommandBase
{
	[Fact]
	public async Task CreateSessionCommandHandler_Success()
	{
		//Arrange
		var handler = new CreateSessionCommandHandler(Context);
		var command = new CreateSessionCommand()
		{
			StartTime = new DateTime(15, 12, 17),
			ActivityId = AppDbContextFactory.ActivityIdForGet
		};

		//Act
		var sessionId = await handler.Handle(command, CancellationToken.None);
		var session = await Context.Sessions.SingleOrDefaultAsync(s =>
				s.Id == sessionId && s.ActivityId == AppDbContextFactory.ActivityIdForGet);

		//Assert
		session.Should().NotBeNull();
		session?.EndTime.Should().BeNull();
	}

	[Fact]
	public async Task CreateSessionCommandHandler_FailOnWrongActivityId()
	{
		//Arrange
		var handler = new CreateSessionCommandHandler(Context);

		//Act
		var result = async () => await handler.Handle(
				new CreateSessionCommand()
				{
					ActivityId = Guid.NewGuid(),
					StartTime = new DateTime(15, 12, 17)
				},
				CancellationToken.None);

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
