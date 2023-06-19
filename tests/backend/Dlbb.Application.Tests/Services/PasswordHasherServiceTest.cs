using Dlbb.Track.Persistence.Services;
using FluentAssertions;

namespace Dlbb.Application.Tests.Services;
public class PasswordHasherServiceTest
{
	[Fact]
	public void PasswordHasherVerify_Succes()
	{
		//Arrange
		var hasher = new PasswordHasher();
		var password = "password";

		//Act
		var hashedPassword = hasher.Hash(password);
		var result = hasher.Verify(password, hashedPassword);

		//Assert
		result.Should().BeTrue();
	}

	[Fact]
	public void PasswordHasherVerify_Fail()
	{
		//Arrange
		var hasher = new PasswordHasher();
		var password = "password";

		//Act
		var hashedPassword = hasher.Hash(password);
		var result = hasher.Verify("wrongPassword", hashedPassword);

		//Assert
		result.Should().BeFalse();
	}
}
