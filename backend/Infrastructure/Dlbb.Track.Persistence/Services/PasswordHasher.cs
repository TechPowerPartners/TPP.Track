using System.Security.Cryptography;

namespace Dlbb.Track.Persistence.Services;
public class PasswordHasher
{
	private int SaltSize = 16;

	private int HashSize = 20;

	/// <summary>
	/// Создание хэша с несколькими итерациями
	/// </summary>
	/// <param name="password">Пароль</param>
	/// <param name="iterations">Количество итераций</param>
	/// <returns>The hash.</returns>
	public string Hash(string password, int iterations)
	{
		// Create salt
		byte[] salt;
		new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

		// Create hash
		var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
		var hash = pbkdf2.GetBytes(HashSize);

		// Combine salt and hash
		var hashBytes = new byte[SaltSize + HashSize];
		Array.Copy(salt, 0, hashBytes, 0, SaltSize);
		Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

		// Convert to base64
		var base64Hash = Convert.ToBase64String(hashBytes);

		// Format hash with extra information
		return string.Format("$MYHASH$V1${0}${1}", iterations, base64Hash);
	}

	/// <summary>
	/// Создание хэша с 1000 итерациями
	/// </summary>
	/// <param name="password">Пароль</param>
	/// <returns>Хэш</returns>
	public string Hash(string password)
	{
		return Hash(password, 1000);
	}

	/// <summary>
	/// Проверка хэширован ли строка
	/// </summary>
	/// <param name="hashString">Хэш</param>
	/// <returns>Поддерживаеться?</returns>
	public bool IsHashSupported(string hashString)
	{
		return hashString.Contains("$MYHASH$V1$");
	}

	/// <summary>
	/// Проверка соответсвия строки к хэшу
	/// </summary>
	/// <param name="password">Предпологаемый пароль</param>
	/// <param name="hashedPassword">Хэш</param>
	/// <returns>Хэши совпали?</returns>
	public bool Verify(string password, string hashedPassword)
	{
		// Check hash
		if (!IsHashSupported(hashedPassword))
		{
			throw new NotSupportedException("The hashtype is not supported");
		}

		var splittedHashString = hashedPassword.Replace("$MYHASH$V1$", "").Split('$');
		var iterations = int.Parse(splittedHashString[0]);
		var base64Hash = splittedHashString[1];

		var hashBytes = Convert.FromBase64String(base64Hash);

		var salt = new byte[SaltSize];
		Array.Copy(hashBytes, 0, salt, 0, SaltSize);

		var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
		byte[] hash = pbkdf2.GetBytes(HashSize);

		for (var i = 0; i < HashSize; i++)
		{
			if (hashBytes[i + SaltSize] != hash[i])
			{
				return false;
			}
		}
		return true;
	}
}
