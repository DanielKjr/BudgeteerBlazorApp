using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;

namespace Budgeteer.Plugins.Encryption
{
	/// <summary>
	/// Provides randomized Salt and Hashes the password with salt
	/// </summary>
	public class EncryptionHandler
	{

		private static int saltLengthLimit = 32;
		
		private static string GetRandomSalt()
		{
			return GetRandomSalt(saltLengthLimit);
		}


		/// <summary>
		/// Returns a salt.
		/// Meaning random collection of numbers and letters
		/// </summary>
		/// <param name="size"></param>
		/// <returns></returns>
		public static string GetRandomSalt(int size =32)
		{
			
			byte[]salt = new byte[size];

			RandomNumberGenerator.Create().GetBytes(salt, 0, salt.Length);
			
			return Convert.ToBase64String(salt);
		}
	

		/// <summary>
		/// Hash the password with salt
		/// </summary>
		/// <param name="password"></param>
		/// <param name="salt"></param>
		/// <returns></returns>
		public static string HashPassword(string password, string salt)
		{
			string combinedPassword = string.Concat(password, salt);

			byte[] bytes = UTF8Encoding.UTF8.GetBytes(combinedPassword);

			byte[] hash = SHA256.Create().ComputeHash(bytes);


			return Convert.ToBase64String(hash);
		}
	}
}