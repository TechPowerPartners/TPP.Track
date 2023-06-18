
using Dlbb.Track.Application.Activities.Commands.DeleteActivity;
using Dlbb.Application.Tests.Common;

namespace Dlbb.Application.Tests.Entities.Activities.Commands;
public class DeleteActivityCommandHandlerTest: TestCommandBase
{
	[Fact]
	public async Task DeleteActivityCommandHandler_Succes()
	{
		//Arrange
		var handler = new DeleteActivityCommandHandler(Context);
		var command = new DeleteActivityCommand()
		{
			Id = AppDbContextFactory.ActivityIdForDelete
		};

		//Act
		await handler.Handle(command, CancellationToken.None);

		//Assert
		Assert.Null(Context.Activities.SingleOrDefault(a=>
			a.Id == AppDbContextFactory.ActivityIdForDelete));
	}

	[Fact]
	public async Task DeleteActivityCommandHandlerText_FailOnWrongId()
	{
		//Arrange
		var handler = new DeleteActivityCommandHandler(Context);

		//Act

		//Assert
		await Assert.ThrowsAsync<Exception>(async () =>
			await handler.Handle(
				new DeleteActivityCommand
				{
					Id = Guid.NewGuid(),
				},
				CancellationToken.None));
	}
}
