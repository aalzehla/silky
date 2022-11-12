using System.Collections.Generic;
using System.Linq.Expressions;

namespace Silky.EntityFrameworkCore.LinqBuilder.Visitors
{
    /// <summary>
    /// deal with Lambda parameter inconsistency
    /// </summary>
    internal sealed class ParameterReplaceExpressionVisitor : ExpressionVisitor
    {
        /// <summary>
        /// A collection of parameter expression mappings
        /// </summary>
        private readonly Dictionary<ParameterExpression, ParameterExpression> parameterExpressionSetter;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parameterExpressionSetter">A collection of parameter expression mappings</param>
        public ParameterReplaceExpressionVisitor(
            Dictionary<ParameterExpression, ParameterExpression> parameterExpressionSetter)
        {
            this.parameterExpressionSetter =
                parameterExpressionSetter ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        /// <summary>
        /// Substitute expression arguments
        /// </summary>
        /// <param name="parameterExpressionSetter">A collection of parameter expression mappings</param>
        /// <param name="expression">expression</param>
        /// <returns>新的expression</returns>
        public static Expression ReplaceParameters(
            Dictionary<ParameterExpression, ParameterExpression> parameterExpressionSetter, Expression expression)
        {
            return new ParameterReplaceExpressionVisitor(parameterExpressionSetter).Visit(expression);
        }

        /// <summary>
        /// Override base class parameter accessors
        /// </summary>
        /// <param name="parameterExpression"></param>
        /// <returns></returns>
        protected override Expression VisitParameter(ParameterExpression parameterExpression)
        {
            if (parameterExpressionSetter.TryGetValue(parameterExpression, out var replacement))
            {
                parameterExpression = replacement;
            }

            return base.VisitParameter(parameterExpression);
        }
    }
}