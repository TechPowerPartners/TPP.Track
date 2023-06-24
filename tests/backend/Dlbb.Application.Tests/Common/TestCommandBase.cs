using Dlbb.Track.Persistence.Contexts;
namespace Dlbb.Application.Tests.Common;
public abstract class TestCommandBase : IDisposable
{
	protected readonly AppDbContext Context;

	public TestCommandBase()
	{
		Context = AppDbContextFactory.Create();
	}

	public void Dispose()
	{
		AppDbContextFactory.Destroy(Context);
	}
}
