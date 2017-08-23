using System;
using CryptSharp;

namespace Tik.Web.OwinNancy.Core.Security
{
	public static class CrypterExtensions
	{
		public static string BCrypt (this string password)
		{
            var options = 	new CrypterOptions(){
				// specify the number of rounds to iterate (2^n) between 2^4 an 2^31
				// 2^14 = 16384 iterations
				{ CrypterOption.Rounds, 14} 
			};
			var crytpedPassword = Crypter.Blowfish.Crypt (password, options);
			return crytpedPassword;
		}

		public static bool Validate (this string password, string correctHash)
		{
			return Crypter.CheckPassword (password, correctHash);
		}

	}
}

