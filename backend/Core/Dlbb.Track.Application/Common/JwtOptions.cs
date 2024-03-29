﻿using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace zgmapi.Data;

public class JwtOptions
{
	public const string ISSUER = "Dlbb"; // издатель токена
	public const string AUDIENCE = "Angular"; // потребитель токена
	private static string KEY = "LongSecretKey333_danger!!!_warning!!!_need_long_key";
	public static void SetKey(string configKey)
	{
		KEY = configKey;
	}
	public static SymmetricSecurityKey GetSymmetricSecurityKey()
	{
		return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
	}
}
