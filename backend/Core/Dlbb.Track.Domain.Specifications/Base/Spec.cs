using System.Linq.Expressions;
using Dlbb.Track.Domain.Specifications.Extensions;

namespace Dlbb.Track.Domain.Specifications.Base
{
	public class Spec<T>
	{
			private readonly Expression<Func<T, bool>> _expression;

			public Expression<Func<T, bool>> Expression => _expression;

			public bool IsSatisfiedBy(T obj) => _expression.Compile()(obj);

			public static Spec<T> operator |
				(Spec<T> left, Spec<T> right) =>
				new(left._expression.Or(right));

			public static Spec<T> operator &
				(Spec<T> left, Spec<T> right) =>
				new(left._expression.And(right));

			public static bool operator false(Spec<T> left) => false;

			public static bool operator true(Spec<T> left) => false;

			public Spec(Expression<Func<T, bool>> expression)
			{
				_expression = expression;
			}

			public static implicit operator Expression<Func<T, bool>>
				(Spec<T> specification) =>
				specification.Expression;

			public static implicit operator Spec<T>
				(Expression<Func<T, bool>> expression) =>
				new(expression);
	}
}
