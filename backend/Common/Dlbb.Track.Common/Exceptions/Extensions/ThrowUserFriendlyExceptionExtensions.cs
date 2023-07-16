using Dlbb.Track.Application.Exceptions;

namespace Dlbb.Track.Common.Exceptions.Extensions
{
	public static class ThrowUserFriendlyExceptionExtensions
	{
		public static void ThrowUserFriendlyExceptionIfTrue
			(this bool condition,
			Status status,
			string message)
		{
			if (condition)
			{
				throw new UserFriendlyException(status, message);
			}
		}

		public static void ThrowUserFriendlyExceptionIfNull
			(this object obj,
			Status status,
			string message)
		{
			if (obj is null)
			{
				throw new UserFriendlyException(status, message);
			}
		}
	}
}
