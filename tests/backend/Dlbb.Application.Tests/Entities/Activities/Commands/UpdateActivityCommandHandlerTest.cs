using Dlbb.Track.Application.Activities.Commands.UpdateActivity;
using Dlbb.Application.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Application.Tests.Entities.Activities.Commands;
public class UpdateActivityCommandHandlerTest : TestCommandBase
{
	[Fact]
	public async Task UpdateCommandHandler_Succes()
	{
		//Arrange
		var handler = new UpdateActivityCommandHandler(Context);
		var command = new UpdateActivityCommand()
		{
			Id = AppDbContextFactory.ActivityIdForUpdate,
			Name = "Name has been updated",
			Description = "Description has been updated"
		};

		//Act
		await handler.Handle(command, CancellationToken.None);

		//Assert
		Assert.NotNull(await Context.Activities.SingleOrDefaultAsync(a =>
			a.Id == AppDbContextFactory.ActivityIdForUpdate &&
			a.Name == "Name has been updated" &&
			a.Description == "Description has been updated"));
	}

	[Fact]
	public async Task UpdateActivityCommandHandler_FailOnWrongId()
	{
		//Arrange
		var handler = new UpdateActivityCommandHandler(Context);

		//Act

		//Assert
		await Assert.ThrowsAsync<Exception>(async () =>
			await handler.Handle(
				new UpdateActivityCommand()
				{ Id = Guid.NewGuid() },
				CancellationToken.None
				));
	}
}
