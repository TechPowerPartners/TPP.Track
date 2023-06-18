using Dlbb.Application.Tests.Common;
using Dlbb.Track.Application.Exceptions;
using Dlbb.Track.Application.Sessions.Commands.EndSession;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Application.Tests.Entities.Sessions.Commands;
public class EndSessionCommandHandlerTest : TestCommandBase
{
	[Fact]
	public async Task EndSessionCommandHandler_Success()
	{
		//Arrange
		var handler = new EndSessionCommandHandler(Context);
		var command = new EndSessionCommand()
		{
			Id = AppDbContextFactory.SessionIdForEnd,
			Duration = new TimeOnly(5, 23)
		};

		//Act
		await handler.Handle(command, CancellationToken.None);
		var result = await Context.Sessions.SingleOrDefaultAsync(s =>
			s.Id == AppDbContextFactory.SessionIdForEnd &&
			s.Duration == command.Duration &&
			s.EndTime == s.StartTime + command.Duration.ToTimeSpan());

		//Assert
		result.Should().NotBeNull();
	}

	[Fact]
	public async Task EndSessionCommandHandler_FailOnWrongSessionId()
	{
		//Arrange
		var handler = new EndSessionCommandHandler(Context);

		//Act
		var result = async () => await handler.Handle(
				new EndSessionCommand()
				{
					Id = Guid.NewGuid(),
					Duration = new TimeOnly(5, 23)
				}, CancellationToken.None);

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
