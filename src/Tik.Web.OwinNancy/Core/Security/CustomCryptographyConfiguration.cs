using System;
using Nancy.Cryptography;

namespace Tik.Web.OwinNancy.Core.Security
{
	public static class CustomCryptographyConfiguration
	{
		public static CryptographyConfiguration Default
		{
			get{ return DefaultConfiguration.Value; }
		}

		public static CryptographyConfiguration NoEncryption
		{
			get{ return NoEncryptionConfiguration.Value; }
		}

		static readonly Lazy<CryptographyConfiguration> 
		DefaultConfiguration = 
			new Lazy<CryptographyConfiguration> (() =>
				new CryptographyConfiguration (
					new RijndaelEncryptionProvider (KeyGenerator.Value), 
					new DefaultHmacProvider (KeyGenerator.Value))
			);



		static readonly Lazy<CryptographyConfiguration> 
		NoEncryptionConfiguration = 
			new Lazy<CryptographyConfiguration> (() =>
				new CryptographyConfiguration (
					new NoEncryptionProvider(), 
					new DefaultHmacProvider (KeyGenerator.Value))
			);

		static readonly Lazy<IKeyGenerator> KeyGenerator = 
			new Lazy<IKeyGenerator> (() => 
				new PassphraseKeyGenerator (
					SecurityConfiguration.GetPassphrase (), 
					Convert.FromBase64String (SecurityConfiguration.GetSalt ())
				)
			);


	}
}







