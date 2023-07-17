using Dlbb.Track.Persistence.Contexts;

namespace Dlbb.Track.Persistence.Services;
public interface ISeedingService
{
	Task Initialize();
	Task ReInit();
}
