
namespace Tik.Web.OwinNancy.Core.Security
{

	public static class SecurityConfiguration{
		public static string GetSalt ()
		{
			// Generate a random salt string and Base 64 encode
			return "Z2VuZXJhdGUgYSByYW5kb20gc2FsdCBhbmQgYmFzZTY0IGVuY29kZQo=";
		}

		public static string GetPassphrase ()
		{
			// Create a random passphrase as well, I just generate a random string
			return "P4s430hU681A+qVq3RtSHvz2YKY+ljbxScNncbpHKge7OLu1x/0jL69zHokX89Gb";
		}
	}
}

