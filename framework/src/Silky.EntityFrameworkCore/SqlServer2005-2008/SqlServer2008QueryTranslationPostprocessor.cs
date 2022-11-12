using System.Linq.Expressions;

namespace Microsoft.EntityFrameworkCore.Query
{
    /// <summary>
    /// SqlServer query converter
    /// </summary>
    internal class SqlServer2008QueryTranslationPostprocessor : RelationalQueryTranslationPostprocessor
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="relationalDependencies"></param>
        /// <param name="queryCompilationContext"></param>
        public SqlServer2008QueryTranslationPostprocessor(QueryTranslationPostprocessorDependencies dependencies,
            RelationalQueryTranslationPostprocessorDependencies relationalDependencies,
            QueryCompilationContext queryCompilationContext)
            : base(dependencies, relationalDependencies, queryCompilationContext)
        {
        }

        /// <summary>
        /// Replace pagination statement
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public override Expression Process(Expression query)
        {
            query = base.Process(query);
            query = new SqlServer2008OffsetToRowNumberConvertVisitor(query, RelationalDependencies.SqlExpressionFactory)
                .Visit(query);
            return query;
        }
    }
}