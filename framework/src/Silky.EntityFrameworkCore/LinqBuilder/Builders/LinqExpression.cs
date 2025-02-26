using System;
using System.Linq.Expressions;

namespace Silky.EntityFrameworkCore.LinqBuilder
{
    /// <summary>
    /// EF Core Linq expand
    /// </summary>
    public static class LinqExpression
    {
        /// <summary>
        /// [EF Core] create Linq/Lambda expression
        /// </summary>
        /// <typeparam name="TSource">generic type</typeparam>
        /// <param name="expression">expression</param>
        /// <returns>新的expression</returns>
        public static Expression<Func<TSource, bool>> Create<TSource>(Expression<Func<TSource, bool>> expression)
        {
            return expression;
        }

        /// <summary>
        /// [EF Core] create Linq/Lambda expression，indexer support
        /// </summary>
        /// <typeparam name="TSource">generic type</typeparam>
        /// <param name="expression">expression</param>
        /// <returns>新的expression</returns>
        public static Expression<Func<TSource, int, bool>> Create<TSource>(
            Expression<Func<TSource, int, bool>> expression)
        {
            return expression;
        }

        /// <summary>
        /// [EF Core] create And expression
        /// </summary>
        /// <typeparam name="TSource">generic type</typeparam>
        /// <returns>新的expression</returns>
        public static Expression<Func<TSource, bool>> And<TSource>()
        {
            return u => true;
        }

        /// <summary>
        /// [EF Core] create And expression，indexer support
        /// </summary>
        /// <typeparam name="TSource">generic type</typeparam>
        /// <returns>新的expression</returns>
        public static Expression<Func<TSource, int, bool>> IndexAnd<TSource>()
        {
            return (u, i) => true;
        }

        /// <summary>
        /// [EF Core] create Or expression
        /// </summary>
        /// <typeparam name="TSource">generic type</typeparam>
        /// <returns>新的expression</returns>
        public static Expression<Func<TSource, bool>> Or<TSource>()
        {
            return u => false;
        }

        /// <summary>
        /// [EF Core] create Or expression，indexer support
        /// </summary>
        /// <typeparam name="TSource">generic type</typeparam>
        /// <returns>新的expression</returns>
        public static Expression<Func<TSource, int, bool>> IndexOr<TSource>()
        {
            return (u, i) => false;
        }
    }
}