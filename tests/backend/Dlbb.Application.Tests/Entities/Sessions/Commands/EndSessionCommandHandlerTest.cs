using Dlbb.Application.Tests.Common;
using Dlbb.Track.Application.Sessions.Commands.EndSession;
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
		Assert.NotNull(result);
	}

	[Fact]
	public async Task EndSessionCommandHandler_FailOnWrongSessionId()
	{
		//Arrange
		var handler = new EndSessionCommandHandler(Context);

		//Act

		//Assert
		await Assert.ThrowsAsync<Exception>(async () =>
			await handler.Handle(
				new EndSessionCommand()
				{
					Id = Guid.NewGuid(),
					Duration = new TimeOnly(5, 23)
				}, CancellationToken.None));
	}
}
