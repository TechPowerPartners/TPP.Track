using System.Linq.Expressions;
using Dlbb.Track.Domain.Entities;
using Dlbb.Track.Domain.Specifications.Base;

namespace Dlbb.Track.Domain.Specifications.Categorys;
public class CategoryByGlobalSpec : Spec<Category>
{
	public CategoryByGlobalSpec(bool isGlobal) : base((c) => c.IsGlobal == isGlobal)
	{
	}
}
