using System.Security.Cryptography;
using System.Text;

namespace PublishingHouse.Services.Infrastructure;

public static class PasswordHashService
{
	public static (string Hash, string Key) GenHashPassword(string password)
	{
		var passwordKey = Guid.NewGuid().ToString("N");
		return (BitConverter.ToString(
			SHA256.Create().ComputeHash(
				Encoding.UTF8.GetBytes(password + passwordKey))), passwordKey);
	}

	public static string GetHashPassword(string password, string key)
	{
		return BitConverter.ToString(
			SHA256.Create().ComputeHash(
				Encoding.UTF8.GetBytes(password + key)));
	}
}