using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Silky.EntityFrameworkCore.LinqBuilder.Visitors;

namespace Silky.EntityFrameworkCore.LinqBuilder
{
    /// <summary>
    /// Expression extension class
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// combine two expressions
        /// </summary>
        /// <typeparam name="TSource">generic type</typeparam>
        /// <param name="expression">expression1</param>
        /// <param name="extendExpression">expression2</param>
        /// <param name="mergeWay">combination</param>
        /// <returns>新的expression</returns>
        public static Expression<TSource> Compose<TSource>(this Expression<TSource> expression,
            Expression<TSource> extendExpression, Func<Expression, Expression, Expression> mergeWay)
        {
            var parameterExpressionSetter = expression.Parameters
                .Select((u, i) => new { u, Parameter = extendExpression.Parameters[i] })
                .ToDictionary(d => d.Parameter, d => d.u);

            var extendExpressionBody =
                ParameterReplaceExpressionVisitor.ReplaceParameters(parameterExpressionSetter, extendExpression.Body);
            return Expression.Lambda<TSource>(mergeWay(expression.Body, extendExpressionBody), expression.Parameters);
        }

        /// <summary>
        /// 与操作合并两个expression
        /// </summary>
        /// <typeparam name="TSource">generic type</typeparam>
        /// <param name="expression">expression1</param>
        /// <param name="extendExpression">expression2</param>
        /// <returns>新的expression</returns>
        public static Expression<Func<TSource, bool>> And<TSource>(this Expression<Func<TSource, bool>> expression,
            Expression<Func<TSource, bool>> extendExpression)
        {
            return expression.Compose(extendExpression, Expression.AndAlso);
        }

        /// <summary>
        /// 与操作合并两个expression，indexer support
        /// </summary>
        /// <typeparam name="TSource">generic type</typeparam>
        /// <param name="expression">expression1</param>
        /// <param name="extendExpression">expression2</param>
        /// <returns>新的expression</returns>
        public static Expression<Func<TSource, int, bool>> And<TSource>(
            this Expression<Func<TSource, int, bool>> expression, Expression<Func<TSource, int, bool>> extendExpression)
        {
            return expression.Compose(extendExpression, Expression.AndAlso);
        }

        /// <summary>
        /// 根据条件成立再与操作合并两个expression
        /// </summary>
        /// <typeparam name="TSource">generic type</typeparam>
        /// <param name="expression">expression1</param>
        /// <param name="condition">boolean condition</param>
        /// <param name="extendExpression">expression2</param>
        /// <returns>新的expression</returns>
        public static Expression<Func<TSource, bool>> AndIf<TSource>(this Expression<Func<TSource, bool>> expression,
            bool condition, Expression<Func<TSource, bool>> extendExpression)
        {
            return condition ? expression.Compose(extendExpression, Expression.AndAlso) : expression;
        }

        /// <summary>
        /// 根据条件成立再与操作合并两个expression，indexer support
        /// </summary>
        /// <typeparam name="TSource">generic type</typeparam>
        /// <param name="expression">expression1</param>
        /// <param name="condition">boolean condition</param>
        /// <param name="extendExpression">expression2</param>
        /// <returns>新的expression</returns>
        public static Expression<Func<TSource, int, bool>> AndIf<TSource>(
            this Expression<Func<TSource, int, bool>> expression, bool condition,
            Expression<Func<TSource, int, bool>> extendExpression)
        {
            return condition ? expression.Compose(extendExpression, Expression.AndAlso) : expression;
        }

        /// <summary>
        /// 或操作合并两个expression
        /// </summary>
        /// <typeparam name="TSource">generic type</typeparam>
        /// <param name="expression">expression1</param>
        /// <param name="extendExpression">expression2</param>
        /// <returns>新的expression</returns>
        public static Expression<Func<TSource, bool>> Or<TSource>(this Expression<Func<TSource, bool>> expression,
            Expression<Func<TSource, bool>> extendExpression)
        {
            return expression.Compose(extendExpression, Expression.OrElse);
        }

        /// <summary>
        /// 或操作合并两个expression，indexer support
        /// </summary>
        /// <typeparam name="TSource">generic type</typeparam>
        /// <param name="expression">expression1</param>
        /// <param name="extendExpression">expression2</param>
        /// <returns>新的expression</returns>
        public static Expression<Func<TSource, int, bool>> Or<TSource>(
            this Expression<Func<TSource, int, bool>> expression, Expression<Func<TSource, int, bool>> extendExpression)
        {
            return expression.Compose(extendExpression, Expression.OrElse);
        }

        /// <summary>
        /// 根据条件成立再或操作合并两个expression
        /// </summary>
        /// <typeparam name="TSource">generic type</typeparam>
        /// <param name="expression">expression1</param>
        /// <param name="condition">boolean condition</param>
        /// <param name="extendExpression">expression2</param>
        /// <returns>新的expression</returns>
        public static Expression<Func<TSource, bool>> OrIf<TSource>(this Expression<Func<TSource, bool>> expression,
            bool condition, Expression<Func<TSource, bool>> extendExpression)
        {
            return condition ? expression.Compose(extendExpression, Expression.OrElse) : expression;
        }

        /// <summary>
        /// 根据条件成立再或操作合并两个expression，indexer support
        /// </summary>
        /// <typeparam name="TSource">generic type</typeparam>
        /// <param name="expression">expression1</param>
        /// <param name="condition">boolean condition</param>
        /// <param name="extendExpression">expression2</param>
        /// <returns>新的expression</returns>
        public static Expression<Func<TSource, int, bool>> OrIf<TSource>(
            this Expression<Func<TSource, int, bool>> expression, bool condition,
            Expression<Func<TSource, int, bool>> extendExpression)
        {
            return condition ? expression.Compose(extendExpression, Expression.OrElse) : expression;
        }

        /// <summary>
        /// ObtainLambdaexpressionproperty name，only u=>u.Property expression
        /// </summary>
        /// <typeparam name="TSource">generic type</typeparam>
        /// <param name="expression">expression</param>
        /// <returns>property name</returns>
        public static string GetExpressionPropertyName<TSource>(this Expression<Func<TSource, object>> expression)
        {
            if (expression.Body is UnaryExpression unaryExpression)
            {
                return ((MemberExpression)unaryExpression.Operand).Member.Name;
            }
            else if (expression.Body is MemberExpression memberExpression)
            {
                return memberExpression.Member.Name;
            }
            else if (expression.Body is ParameterExpression parameterExpression)
            {
                return parameterExpression.Type.Name;
            }

            throw new InvalidCastException(nameof(expression));
        }
        
    }
}