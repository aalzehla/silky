using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Silky.Core;
using Silky.Core.Configuration;
using Silky.Core.Extensions;
using Silky.EntityFrameworkCore.Helpers;

namespace Silky.EntityFrameworkCore.Extensions.DatabaseFacade
{
    /// <summary>
    /// DatabaseFacade Extended class
    /// </summary>
    public static class DbObjectExtensions
    {
        /// <summary>
        /// MiniProfiler Category name
        /// </summary>
        private const string MiniProfilerCategory = "connection";

        /// <summary>
        /// Is it a development environment
        /// </summary>
        private static readonly bool IsDevelopment;

        /// <summary>
        /// whether to record EFCore implement sql command print log
        /// </summary>
        private static readonly bool IsLogEntityFrameworkCoreSqlExecuteCommand;

        /// <summary>
        /// Constructor
        /// </summary>
        static DbObjectExtensions()
        {
            IsDevelopment = EngineContext.Current.HostEnvironment.IsDevelopment();
            AppSettingsOptions appsettings = default;
            appsettings = EngineContext.Current.GetOptionsMonitor<AppSettingsOptions>(((options, s) =>
            {
                appsettings = options;
            }));
            IsLogEntityFrameworkCoreSqlExecuteCommand = appsettings.LogEntityFrameworkCoreSqlExecuteCommand ?? false;
        }

        /// <summary>
        /// Initialize the database command object
        /// </summary>
        /// <param name="databaseFacade">ADO.NET database object</param>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="commandType">Command type</param>
        /// <returns>(DbConnection dbConnection, DbCommand dbCommand)</returns>
        public static (DbConnection dbConnection, DbCommand dbCommand) PrepareDbCommand(
            this Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade databaseFacade, string sql,
            DbParameter[] parameters = null, CommandType commandType = CommandType.Text)
        {
            // Support read configuration rendering
            var realSql = sql.Render();

            // Create database connection objects and database command objects
            var (dbConnection, dbCommand) = databaseFacade.CreateDbCommand(realSql, commandType);
            SetDbParameters(databaseFacade.ProviderName, ref dbCommand, parameters);

            // Open database connection
            OpenConnection(databaseFacade, dbConnection, dbCommand);

            // return
            return (dbConnection, dbCommand);
        }

        /// <summary>
        /// Initialize the database command object
        /// </summary>
        /// <param name="databaseFacade">ADO.NET database object</param>
        /// <param name="sql">sql statement</param>
        /// <param name="model">command model</param>
        /// <param name="commandType">Command type</param>
        /// <returns>(DbConnection dbConnection, DbCommand dbCommand, DbParameter[] dbParameters)</returns>
        public static (DbConnection dbConnection, DbCommand dbCommand, DbParameter[] dbParameters) PrepareDbCommand(
            this Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade databaseFacade, string sql, object model,
            CommandType commandType = CommandType.Text)
        {
            // Support read configuration rendering
            var realSql = sql.Render();

            // Create database connection objects and database command objects
            var (dbConnection, dbCommand) = databaseFacade.CreateDbCommand(realSql, commandType);
            SetDbParameters(databaseFacade.ProviderName, ref dbCommand, model, out var dbParameters);

            // Open database connection
            OpenConnection(databaseFacade, dbConnection, dbCommand);

            // return
            return (dbConnection, dbCommand, dbParameters);
        }

        /// <summary>
        /// Initialize the database command object
        /// </summary>
        /// <param name="databaseFacade">ADO.NET database object</param>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="commandType">Command type</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>(DbConnection dbConnection, DbCommand dbCommand)</returns>
        public static async Task<(DbConnection dbConnection, DbCommand dbCommand)> PrepareDbCommandAsync(
            this Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade databaseFacade, string sql,
            DbParameter[] parameters = null, CommandType commandType = CommandType.Text,
            CancellationToken cancellationToken = default)
        {
            // Support read configuration rendering
            var realSql = sql.Render();

            // Create database connection objects and database command objects
            var (dbConnection, dbCommand) = databaseFacade.CreateDbCommand(realSql, commandType);
            SetDbParameters(databaseFacade.ProviderName, ref dbCommand, parameters);

            // Open database connection
            await OpenConnectionAsync(databaseFacade, dbConnection, dbCommand, cancellationToken);

            // return
            return (dbConnection, dbCommand);
        }

        /// <summary>
        /// Initialize the database command object
        /// </summary>
        /// <param name="databaseFacade">ADO.NET database object</param>
        /// <param name="sql">sql statement</param>
        /// <param name="model">command model</param>
        /// <param name="commandType">Command type</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>(DbConnection dbConnection, DbCommand dbCommand, DbParameter[] dbParameters)</returns>
        public static async Task<(DbConnection dbConnection, DbCommand dbCommand, DbParameter[] dbParameters)>
            PrepareDbCommandAsync(this Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade databaseFacade,
                string sql, object model, CommandType commandType = CommandType.Text,
                CancellationToken cancellationToken = default)
        {
            // Support read configuration rendering
            var realSql = sql.Render();

            // Create database connection objects and database command objects
            var (dbConnection, dbCommand) = databaseFacade.CreateDbCommand(realSql, commandType);
            SetDbParameters(databaseFacade.ProviderName, ref dbCommand, model, out var dbParameters);

            // Open database connection
            await OpenConnectionAsync(databaseFacade, dbConnection, dbCommand, cancellationToken);

            // return
            return (dbConnection, dbCommand, dbParameters);
        }

        /// <summary>
        /// Create database command object
        /// </summary>
        /// <param name="databaseFacade">ADO.NET database object</param>
        /// <param name="sql">sql statement</param>
        /// <param name="commandType">Command type</param>
        /// <returns>(DbConnection dbConnection, DbCommand dbCommand)</returns>
        private static (DbConnection dbConnection, DbCommand dbCommand) CreateDbCommand(
            this Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade databaseFacade, string sql,
            CommandType commandType = CommandType.Text)
        {
            // Check if stored procedures are supported
            // DbProvider.CheckStoredProcedureSupported(databaseFacade.ProviderName, commandType);

            // Determine whether to enable MiniProfiler components，If there is，then the packaging link
            var dbConnection = databaseFacade.GetDbConnection();

            // Create database command object
            var dbCommand = dbConnection.CreateCommand();

            // Set basic parameters
            dbCommand.Transaction = databaseFacade.CurrentTransaction?.GetDbTransaction();
            dbCommand.CommandType = commandType;
            dbCommand.CommandText = sql;

            // set timeout
            var commandTimeout = databaseFacade.GetCommandTimeout();
            if (commandTimeout != null) dbCommand.CommandTimeout = commandTimeout.Value;

            // return
            return (dbConnection, dbCommand);
        }

        /// <summary>
        /// Open database connection
        /// </summary>
        /// <param name="databaseFacade">ADO.NET database object</param>
        /// <param name="dbConnection">database connection object</param>
        /// <param name="dbCommand"></param>
        private static void OpenConnection(Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade databaseFacade,
            DbConnection dbConnection, DbCommand dbCommand)
        {
            // Check if the connection string is closed，in the case of，then on
            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }

            // Record Sql implement命令日志
            LogSqlExecuteCommand(databaseFacade, dbCommand);
        }

        /// <summary>
        /// Open database connection
        /// </summary>
        /// <param name="databaseFacade">ADO.NET database object</param>
        /// <param name="dbConnection">database connection object</param>
        /// <param name="dbCommand"></param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns></returns>
        private static async Task OpenConnectionAsync(
            Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade databaseFacade, DbConnection dbConnection,
            DbCommand dbCommand, CancellationToken cancellationToken = default)
        {
            // Check if the connection string is closed，in the case of，then on
            if (dbConnection.State == ConnectionState.Closed)
            {
                await dbConnection.OpenAsync(cancellationToken);
            }

            // Record Sql implement命令日志
            LogSqlExecuteCommand(databaseFacade, dbCommand);
        }

        /// <summary>
        /// Set database command object parameters
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="dbCommand">database command object</param>
        /// <param name="parameters">Command parameters</param>
        private static void SetDbParameters(string providerName, ref DbCommand dbCommand,
            DbParameter[] parameters = null)
        {
            if (parameters == null || parameters.Length == 0) return;

            // 添加Command parameters前缀
            foreach (var parameter in parameters)
            {
                parameter.ParameterName = DbHelpers.FixSqlParameterPlaceholder(providerName, parameter.ParameterName);
                dbCommand.Parameters.Add(parameter);
            }
        }

        /// <summary>
        /// Set database command object parameters
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="dbCommand">database command object</param>
        /// <param name="model">parametric model</param>
        /// <param name="dbParameters">Command parameters</param>
        private static void SetDbParameters(string providerName, ref DbCommand dbCommand, object model,
            out DbParameter[] dbParameters)
        {
            dbParameters = DbHelpers.ConvertToDbParameters(model, dbCommand);
            SetDbParameters(providerName, ref dbCommand, dbParameters);
        }


        /// <summary>
        /// Record Sql implement命令日志
        /// </summary>
        /// <param name="databaseFacade"></param>
        /// <param name="dbCommand"></param>
        private static void LogSqlExecuteCommand(
            Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade databaseFacade, DbCommand dbCommand)
        {
            // 打印implement SQL
            //App.PrintToMiniProfiler("sql", "Execution", dbCommand.CommandText);

            // Determine whether to enable
            if (!IsLogEntityFrameworkCoreSqlExecuteCommand) return;

            // get log object
            var logger = databaseFacade.GetService<ILogger<Microsoft.EntityFrameworkCore.Database.SqlExecuteCommand>>();

            // build log content
            var sqlLogBuilder = new StringBuilder();
            sqlLogBuilder.Append(@"Executed DbCommand (NaN) ");
            sqlLogBuilder.Append(@" [Parameters=[");

            // 拼接Command parameters
            var parameters = dbCommand.Parameters;
            for (var i = 0; i < parameters.Count; i++)
            {
                var parameter = parameters[i];
                var parameterType = parameter.GetType();

                // deal with OracleParameter parameter print
                var dbType = parameterType.FullName.Equals("Oracle.ManagedDataAccess.Client.OracleParameter",
                    StringComparison.OrdinalIgnoreCase)
                    ? parameterType.GetProperty("OracleDbType").GetValue(parameter)
                    : parameter.DbType;

                sqlLogBuilder.Append(
                    $"{parameter.ParameterName}='{parameter.Value}' (Size = {parameter.Size}) (DbType = {dbType})");
                if (i < parameters.Count - 1) sqlLogBuilder.Append(", ");
            }

            sqlLogBuilder.Append(
                @$"], CommandType='{dbCommand.CommandType}', CommandTimeout='{dbCommand.CommandTimeout}']");
            sqlLogBuilder.Append("\r\n");
            sqlLogBuilder.Append(dbCommand.CommandType == CommandType.StoredProcedure
                ? "EXEC " + dbCommand.CommandText
                : dbCommand.CommandText);

            // print log
            logger.LogInformation(sqlLogBuilder.ToString());
        }
    }
}