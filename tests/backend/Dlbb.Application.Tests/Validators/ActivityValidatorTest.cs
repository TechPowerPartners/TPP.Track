using Dlbb.Track.Application.Validators;
using Dlbb.Track.Domain.Entities;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace Dlbb.Application.Tests.Validators;

[TestFixture]
public class ActivityValidatorTest
{
	private readonly ActivityValidator _validator = new();

	[Test]
	public void Should_have_error_when_Name_is_null()
	{
		var model = new Activity() { Name = null };
		var result = _validator.TestValidate(model);
		result.ShouldHaveValidationErrorFor(a => a.Name);
	}
	
	[Test]
	public void Should_have_error_when_Name_is_empty()
	{
		var model = new Activity() { Name = "" };
		var result = _validator.TestValidate(model);
		result.ShouldHaveValidationErrorFor(a => a.Name);
	}
	
	[Test]
	public void Should_have_error_when_Name_have_only_spaces()
	{
		var model = new Activity() { Name = "    " };
		var result = _validator.TestValidate(model);
		result.ShouldHaveValidationErrorFor(a => a.Name);
	}
	
	[Test]
	public void Should_not_have_error_with_name()
	{
		var model = new Activity() { Name = "Test" };
		var result = _validator.TestValidate(model);
		result.ShouldNotHaveValidationErrorFor(a => a.Name);
	}
}
