using System.Linq.Expressions;

namespace Dlbb.Track.Domain.Specifications.Extensions;
public static class ExpressionExtensions
{
	public static Expression<Func<T, bool>> Or<T>
	(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right) =>
	left.Compose(right, Expression.OrElse);

	public static Expression<Func<T, bool>> And<T>
		(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right) =>
		left.Compose(right, Expression.AndAlso);

	private static Expression<Func<T, bool>> Compose<T>
		(this Expression<Func<T, bool>> left,
		Expression<Func<T, bool>> right,
		Func<Expression, Expression, Expression> op)
	{
		var param = Expression.Parameter(typeof(T));
		var body = op(Expression.Invoke(left, param), Expression.Invoke(right, param));
		return Expression.Lambda<Func<T, bool>>(body, param);
	}
}
