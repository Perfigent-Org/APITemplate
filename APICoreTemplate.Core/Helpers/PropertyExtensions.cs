using System;
using System.Linq.Expressions;

namespace APICoreTemplate.Core.Helpers
{
    public static class PropertyExtensions
	{
		public static string GetName<T>(this Expression<Func<T, object>> action)
		{
			return GetNameFromMemberExpression(action.Body);
		}

		static string GetNameFromMemberExpression(Expression expression)
		{
			if (expression is MemberExpression)
			{
				return (expression as MemberExpression).Member.Name;
			}
			else if (expression is UnaryExpression)
			{
				return GetNameFromMemberExpression((expression as UnaryExpression).Operand);
			}

			return null;
		}
	}
}
