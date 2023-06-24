
using Dlbb.Track.Application.Activities.Commands.DeleteActivity;
using Dlbb.Application.Tests.Common;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using Dlbb.Track.Application.Exceptions;

namespace Dlbb.Application.Tests.Entities.Activities.Commands;
public class DeleteActivityCommandHandlerTest: TestCommandBase
{
	[Fact]
	public async Task DeleteActivityCommandHandler_Success()
	{
		//Arrange
		var handler = new DeleteActivityCommandHandler(Context);
		var command = new DeleteActivityCommand()
		{
			Id = AppDbContextFactory.ActivityIdForDelete
		};

		//Act
		await handler.Handle(command, CancellationToken.None);
		var result = await Context.Activities.SingleOrDefaultAsync(a =>
			a.Id == AppDbContextFactory.ActivityIdForDelete);

		//Assert
		result.Should().BeNull();
	}

	[Fact]
	public async Task DeleteActivityCommandHandlerText_FailOnWrongId()
	{
		//Arrange
		var handler = new DeleteActivityCommandHandler(Context);

		//Act
		var result = async ()=> await handler.Handle(
				new DeleteActivityCommand
				{
					Id = Guid.NewGuid(),
				},
				CancellationToken.None);

		//Assert
		await result.Should().ThrowAsync<UserFriendlyException>();

		try
		{
			await result();
		}
		catch(UserFriendlyException e) 
		{
			e.Status.Should().Be(Status.NotFound);
		}
	}
}
