using System;

namespace Touch.Server.Android.Extensions
{
	public static class PlatformExtensions
	{
		public static bool IsMacOSX(this PlatformID platform)
		{
			return platform == PlatformID.MacOSX || platform == PlatformID.Unix;
		}
	}
}
