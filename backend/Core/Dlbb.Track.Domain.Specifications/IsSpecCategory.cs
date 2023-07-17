using System.Linq.Expressions;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Domain.Specifications.Base;

namespace Dlbb.Track.Domain.Specifications;
public class IsSpecCategory : Spec<Category>
{
	public IsSpecCategory(Expression<Func<Category, bool>> expression) : base(expression)
	{
	}

	public IsSpecCategory(Guid categoryId) : base((c)=> c.Id == categoryId)
	{
	}

	public IsSpecCategory(bool isGlobal) : base((c) => c.IsGlobal == isGlobal)
	{
	}

	public IsSpecCategory(string categoryName) : base((c) => c.Name == categoryName)
	{
	}
}
