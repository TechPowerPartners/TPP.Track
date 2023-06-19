using Dlbb.Track.Application.Activities.Commands.UpdateActivity;
using Dlbb.Application.Tests.Common;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using Dlbb.Track.Application.Exceptions;

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
		var result = await Context.Activities.SingleOrDefaultAsync(a =>
			a.Id == AppDbContextFactory.ActivityIdForUpdate &&
			a.Name == "Name has been updated" &&
			a.Description == "Description has been updated");

		//Assert
		result.Should().NotBeNull();
	}

	[Fact]
	public async Task UpdateActivityCommandHandler_FailOnWrongId()
	{
		//Arrange
		var handler = new UpdateActivityCommandHandler(Context);

		//Act
		var result = async () => await handler.Handle(
				new UpdateActivityCommand()
				{ Id = Guid.NewGuid() },
				CancellationToken.None
				);

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
