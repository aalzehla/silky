namespace Microsoft.EntityFrameworkCore.Query
{
    /// <summary>
    /// SqlServer query conversion factory（deal with SqlServer 2008 pagination problem）
    /// </summary>
    public class SqlServer2008QueryTranslationPostprocessorFactory : IQueryTranslationPostprocessorFactory
    {
        /// <summary>
        /// Query Transformation Dependency Collection
        /// </summary>
        private readonly QueryTranslationPostprocessorDependencies _dependencies;

        /// <summary>
        /// 关系Query Transformation Dependency Collection
        /// </summary>
        private readonly RelationalQueryTranslationPostprocessorDependencies _relationalDependencies;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dependencies"></param>
        /// <param name="relationalDependencies"></param>
        public SqlServer2008QueryTranslationPostprocessorFactory(QueryTranslationPostprocessorDependencies dependencies,
            RelationalQueryTranslationPostprocessorDependencies relationalDependencies)
        {
            _dependencies = dependencies;
            _relationalDependencies = relationalDependencies;
        }

        /// <summary>
        /// Create a query transformation instance factory
        /// </summary>
        /// <param name="queryCompilationContext"></param>
        /// <returns></returns>
        public virtual QueryTranslationPostprocessor Create(QueryCompilationContext queryCompilationContext)
        {
            return new SqlServer2008QueryTranslationPostprocessor(
                _dependencies,
                _relationalDependencies,
                queryCompilationContext);
        }
    }
}