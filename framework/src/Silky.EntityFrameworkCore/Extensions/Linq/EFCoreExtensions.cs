using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    /// EntityFramework Core expand
    /// </summary>
    public static class EFCoreExtensions
    {
        /// <summary>
        /// [EF Core] Rebuild according to conditions Include Inquire
        /// </summary>
        /// <typeparam name="TSource">generic type</typeparam>
        /// <typeparam name="TProperty">Generic property type</typeparam>
        /// <param name="sources">collection object</param>
        /// <param name="condition">boolean condition</param>
        /// <param name="expression">新的collection object表达式</param>
        /// <returns></returns>
        public static IIncludableQueryable<TSource, TProperty> Include<TSource, TProperty>(
            this IQueryable<TSource> sources, bool condition, Expression<Func<TSource, TProperty>> expression)
            where TSource : class
        {
            return condition ? sources.Include(expression) : (IIncludableQueryable<TSource, TProperty>)sources;
        }
    }
}