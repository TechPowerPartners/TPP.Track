using Dlbb.Track.Domain.Entities.Base;

namespace Dlbb.Track.Domain.Entities;
public class GlobalActivity : BaseActivity
{
	public ICollection<GlobalSessions> GlobalSessions { get; set; }
}
