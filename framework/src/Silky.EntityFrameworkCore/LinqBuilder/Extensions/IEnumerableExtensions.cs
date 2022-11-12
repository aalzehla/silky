using System.Collections.Generic;
using System.Linq.Expressions;
using Silky.EntityFrameworkCore.LinqBuilder;

namespace System.Linq
{
    /// <summary>
    /// IEnumerable expand
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// [EF Core] Rebuild according to conditions Where Inquire
        /// </summary>
        /// <typeparam name="TSource">generic type</typeparam>
        /// <param name="sources">collection object</param>
        /// <param name="condition">boolean condition</param>
        /// <param name="expression">expression</param>
        /// <returns>新的collection object</returns>
        public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> sources, bool condition,
            Expression<Func<TSource, bool>> expression)
        {
            return condition ? Queryable.Where(sources, expression) : sources;
        }

        /// <summary>
        /// [EF Core] Rebuild according to conditions Where Inquire，indexer support
        /// </summary>
        /// <typeparam name="TSource">generic type</typeparam>
        /// <param name="sources">collection object</param>
        /// <param name="condition">boolean condition</param>
        /// <param name="expression">expression</param>
        /// <returns>新的collection object</returns>
        public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> sources, bool condition,
            Expression<Func<TSource, int, bool>> expression)
        {
            return condition ? Queryable.Where(sources, expression) : sources;
        }

        /// <summary>
        /// [EF Core] 与操作合并多个expression
        /// </summary>
        /// <typeparam name="TSource">generic type</typeparam>
        /// <param name="sources">collection object</param>
        /// <param name="expressions">expression数组</param>
        /// <returns>新的collection object</returns>
        public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> sources,
            params Expression<Func<TSource, bool>>[] expressions)
        {
            if (expressions == null || !expressions.Any()) return sources;
            if (expressions.Length == 1) return Queryable.Where(sources, expressions[0]);

            var expression = LinqExpression.Or<TSource>();
            foreach (var _expression in expressions)
            {
                expression = expression.Or(_expression);
            }

            return Queryable.Where(sources, expression);
        }

        /// <summary>
        /// [EF Core] 与操作合并多个expression，indexer support
        /// </summary>
        /// <typeparam name="TSource">generic type</typeparam>
        /// <param name="sources">collection object</param>
        /// <param name="expressions">expression数组</param>
        /// <returns>新的collection object</returns>
        public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> sources,
            params Expression<Func<TSource, int, bool>>[] expressions)
        {
            if (expressions == null || !expressions.Any()) return sources;
            if (expressions.Length == 1) return Queryable.Where(sources, expressions[0]);

            var expression = LinqExpression.IndexOr<TSource>();
            foreach (var _expression in expressions)
            {
                expression = expression.Or(_expression);
            }

            return Queryable.Where(sources, expression);
        }

        /// <summary>
        /// [EF Core] Rebuild according to conditions WhereOr Inquire
        /// </summary>
        /// <typeparam name="TSource">generic type</typeparam>
        /// <param name="sources">collection object</param>
        /// <param name="conditionExpressions">条件expression</param>
        /// <returns>新的collection object</returns>
        public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> sources,
            params (bool condition, Expression<Func<TSource, bool>> expression)[] conditionExpressions)
        {
            var expressions = new List<Expression<Func<TSource, bool>>>();
            foreach (var (condition, expression) in conditionExpressions)
            {
                if (condition) expressions.Add(expression);
            }

            return Where(sources, expressions.ToArray());
        }

        /// <summary>
        /// [EF Core] Rebuild according to conditions WhereOr Inquire，indexer support
        /// </summary>
        /// <typeparam name="TSource">generic type</typeparam>
        /// <param name="sources">collection object</param>
        /// <param name="conditionExpressions">条件expression</param>
        /// <returns>新的collection object</returns>
        public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> sources,
            params (bool condition, Expression<Func<TSource, int, bool>> expression)[] conditionExpressions)
        {
            var expressions = new List<Expression<Func<TSource, int, bool>>>();
            foreach (var (condition, expression) in conditionExpressions)
            {
                if (condition) expressions.Add(expression);
            }

            return Where(sources, expressions.ToArray());
        }

        /// <summary>
        /// Rebuild according to conditions Where Inquire
        /// </summary>
        /// <typeparam name="TSource">generic type</typeparam>
        /// <param name="sources">collection object</param>
        /// <param name="condition">boolean condition</param>
        /// <param name="expression">expression</param>
        /// <returns>新的collection object</returns>
        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> sources, bool condition,
            Func<TSource, bool> expression)
        {
            return condition ? sources.Where(expression) : sources;
        }

        /// <summary>
        /// Rebuild according to conditions Where Inquire，indexer support
        /// </summary>
        /// <typeparam name="TSource">generic type</typeparam>
        /// <param name="sources">collection object</param>
        /// <param name="condition">boolean condition</param>
        /// <param name="expression">expression</param>
        /// <returns>新的collection object</returns>
        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> sources, bool condition,
            Func<TSource, int, bool> expression)
        {
            return condition ? sources.Where(expression) : sources;
        }
    }
}