using System;
using Silky.EntityFrameworkCore.Locators;

namespace Silky.EntityFrameworkCore
{
    /// <summary>
    /// Construct Sql String execution part
    /// </summary>
    public sealed partial class SqlStringExecutePart
    {
        /// <summary>
        /// set up Sql string
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public SqlStringExecutePart SetSqlString(string sql)
        {
            SqlString = sql;
            return this;
        }

        /// <summary>
        /// set up ADO.NET overtime time
        /// </summary>
        /// <param name="timeout">unit second</param>
        /// <returns></returns>
        public SqlStringExecutePart SetCommandTimeout(int timeout)
        {
            Timeout = timeout;
            return this;
        }


        /// <summary>
        /// set up数据库上下文定位器
        /// </summary>
        /// <typeparam name="TDbContextLocator"></typeparam>
        /// <returns></returns>
        public SqlStringExecutePart Change<TDbContextLocator>()
            where TDbContextLocator : class, IDbContextLocator
        {
            DbContextLocator = typeof(TDbContextLocator) ?? typeof(MasterDbContextLocator);
            return this;
        }

        /// <summary>
        /// set up数据库上下文定位器
        /// </summary>
        /// <returns></returns>
        public SqlStringExecutePart Change(Type dbContextLocator)
        {
            DbContextLocator = dbContextLocator ?? typeof(MasterDbContextLocator);
            return this;
        }
    }
}