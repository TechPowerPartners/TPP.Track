
using Dlbb.Application.Tests.Common;
using Dlbb.Track.Application.Activities.Commands.CreateActivity;
using Dlbb.Track.Application.Commands.Activities.Commands.CreateActivity;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Dlbb.Application.Tests.Entities.Activities.Commands;
public class CreateActivityCommandHandlerTest:TestCommandBase
{
	[Fact]
	public async Task CreateActivityCommandHandler_Succes()
	{
		//Arrange
		var handler = new CreateActivityCommandHandler(Context);
		var command = new CreateActivityCommand()
		{
			Name = "dota 2",
			Description = "igrau",
		};

		//Act
		var activityId = await handler.Handle(command, CancellationToken.None);
		var result = await Context.Activities.SingleOrDefaultAsync(a =>
			a.Id == activityId &&
			a.Name == command.Name &&
			a.Description == command.Description);

		//Assert
		result.Should().NotBeNull();
	}
}
