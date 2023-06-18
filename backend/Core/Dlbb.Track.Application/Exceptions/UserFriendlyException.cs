using System.Net;

namespace Dlbb.Track.Application.Exceptions;
public class UserFriendlyException : Exception
{
	public Status Status { get; init; }
	public UserFriendlyException( Status status, string message)
		: base(message)
	{
		Status = status;
	}
}
