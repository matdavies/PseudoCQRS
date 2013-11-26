using System;
using System.Linq.Expressions;

namespace PseudoCQRS
{
    public static class ExpressionExtensionMethods
    {
        public static string GetMemberNameFromExpression<T>( this Expression<Func<T, object>> expression )
        {
            string result = String.Empty;
            var unary = expression.Body as UnaryExpression;
            if ( unary != null )
                result = ( (MemberExpression)unary.Operand ).Member.Name;
            else
                result = ( (MemberExpression)expression.Body ).Member.Name;

            return result;
        }
    }
}