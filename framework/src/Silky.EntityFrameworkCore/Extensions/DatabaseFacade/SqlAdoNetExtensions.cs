using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Silky.EntityFrameworkCore.Extensions.DatabaseFacade
{
    /// <summary>
    /// ADONET Extended class
    /// </summary>
    public static class SqlAdoNetExtensions
    {
        /// <summary>
        /// implement Sql return DataTable
        /// </summary>
        /// <param name="databaseFacade">ADO.NET database object</param>
        /// <param name="sql">sql statement</param>
        /// <param name="commandType">Command type</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="behavior">Behavior</param>
        /// <returns>DataTable</returns>
        public static DataTable ExecuteReader(
            this Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade databaseFacade, string sql,
            DbParameter[] parameters = null, CommandType commandType = CommandType.Text,
            CommandBehavior behavior = CommandBehavior.Default)
        {
            // Initialize database connection object and database command object
            var (_, dbCommand) = databaseFacade.PrepareDbCommand(sql, parameters, commandType);

            // read data
            using var dbDataReader = dbCommand.ExecuteReader(behavior);

            // fill to DataTable
            var dataTable = dbDataReader.ToDataTable();

            // release command object
            dbCommand.Dispose();

            return dataTable;
        }

        /// <summary>
        /// implement Sql return DataTable
        /// </summary>
        /// <param name="databaseFacade">ADO.NET database object</param>
        /// <param name="sql">sql statement</param>
        /// <param name="commandType">Command type</param>
        /// <param name="model">command model</param>
        /// <param name="behavior">Behavior</param>
        /// <returns>(DataTable dataTable, DbParameter[] dbParameters)</returns>
        public static (DataTable dataTable, DbParameter[] dbParameters) ExecuteReader(
            this Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade databaseFacade, string sql, object model,
            CommandType commandType = CommandType.Text, CommandBehavior behavior = CommandBehavior.Default)
        {
            // Initialize database connection object and database command object
            var (_, dbCommand, dbParameters) = databaseFacade.PrepareDbCommand(sql, model, commandType);

            // read data
            using var dbDataReader = dbCommand.ExecuteReader(behavior);

            // fill to DataTable
            var dataTable = dbDataReader.ToDataTable();

            // release command object
            dbCommand.Dispose();

            return (dataTable, dbParameters);
        }

        /// <summary>
        /// implement Sql return DataTable
        /// </summary>
        /// <param name="databaseFacade">ADO.NET database object</param>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="commandType">Command type</param>
        /// <param name="behavior">Behavior</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>DataTable</returns>
        public static async Task<DataTable> ExecuteReaderAsync(
            this Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade databaseFacade, string sql,
            DbParameter[] parameters = null, CommandType commandType = CommandType.Text,
            CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default)
        {
            // Initialize database connection object and database command object
            var (_, dbCommand) =
                await databaseFacade.PrepareDbCommandAsync(sql, parameters, commandType, cancellationToken);

            // read data
            using var dbDataReader = await dbCommand.ExecuteReaderAsync(behavior, cancellationToken);

            // fill to DataTable
            var dataTable = dbDataReader.ToDataTable();

            // release command object
            await dbCommand.DisposeAsync();

            return dataTable;
        }

        /// <summary>
        /// implement Sql return DataTable
        /// </summary>
        /// <param name="databaseFacade">ADO.NET database object</param>
        /// <param name="sql">sql statement</param>
        /// <param name="model">command model</param>
        /// <param name="commandType">Command type</param>
        /// <param name="behavior">Behavior</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>(DataTable dataTable, DbParameter[] dbParameters)</returns>
        public static async Task<(DataTable dataTable, DbParameter[] dbParameters)> ExecuteReaderAsync(
            this Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade databaseFacade, string sql, object model,
            CommandType commandType = CommandType.Text, CommandBehavior behavior = CommandBehavior.Default,
            CancellationToken cancellationToken = default)
        {
            // Initialize database connection object and database command object
            var (_, dbCommand, dbParameters) =
                await databaseFacade.PrepareDbCommandAsync(sql, model, commandType, cancellationToken);

            // read data
            using var dbDataReader = await dbCommand.ExecuteReaderAsync(behavior, cancellationToken);

            // fill to DataTable
            var dataTable = dbDataReader.ToDataTable();

            // release command object
            await dbCommand.DisposeAsync();

            return (dataTable, dbParameters);
        }

        /// <summary>
        /// implement Sql statementreturnAffected rows
        /// </summary>
        /// <param name="databaseFacade">ADO.NET database object</param>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="commandType">Command type</param>
        /// <returns>Affected rows</returns>
        public static int ExecuteNonQuery(
            this Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade databaseFacade, string sql,
            DbParameter[] parameters = null, CommandType commandType = CommandType.Text)
        {
            // Initialize database connection object and database command object
            var (_, dbCommand) = databaseFacade.PrepareDbCommand(sql, parameters, commandType);

            // implementreturnAffected rows
            var rowEffects = dbCommand.ExecuteNonQuery();

            // release command object
            dbCommand.Dispose();

            return rowEffects;
        }

        /// <summary>
        /// implement Sql statementreturnAffected rows
        /// </summary>
        /// <param name="databaseFacade">ADO.NET database object</param>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <param name="commandType">Command type</param>
        /// <returns>(int rowEffects, DbParameter[] dbParameters)</returns>
        public static (int rowEffects, DbParameter[] dbParameters) ExecuteNonQuery(
            this Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade databaseFacade, string sql, object model,
            CommandType commandType = CommandType.Text)
        {
            // Initialize database connection object and database command object
            var (_, dbCommand, dbParameters) = databaseFacade.PrepareDbCommand(sql, model, commandType);

            // implementreturnAffected rows
            var rowEffects = dbCommand.ExecuteNonQuery();

            // release command object
            dbCommand.Dispose();

            return (rowEffects, dbParameters);
        }

        /// <summary>
        /// implement Sql statementreturnAffected rows
        /// </summary>
        /// <param name="databaseFacade">ADO.NET database object</param>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="commandType">Command type</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>Affected rows</returns>
        public static async Task<int> ExecuteNonQueryAsync(
            this Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade databaseFacade, string sql,
            DbParameter[] parameters = null, CommandType commandType = CommandType.Text,
            CancellationToken cancellationToken = default)
        {
            // Initialize database connection object and database command object
            var (_, dbCommand) =
                await databaseFacade.PrepareDbCommandAsync(sql, parameters, commandType, cancellationToken);

            // implementreturnAffected rows
            var rowEffects = await dbCommand.ExecuteNonQueryAsync(cancellationToken);

            // release command object
            await dbCommand.DisposeAsync();

            return rowEffects;
        }

        /// <summary>
        /// implement Sql statementreturnAffected rows
        /// </summary>
        /// <param name="databaseFacade">ADO.NET database object</param>
        /// <param name="sql">sql statement</param>
        /// <param name="model">command model</param>
        /// <param name="commandType">Command type</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>(int rowEffects, DbParameter[] dbParameters)</returns>
        public static async Task<(int rowEffects, DbParameter[] dbParameters)> ExecuteNonQueryAsync(
            this Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade databaseFacade, string sql, object model,
            CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
        {
            // Initialize database connection object and database command object
            var (_, dbCommand, dbParameters) =
                await databaseFacade.PrepareDbCommandAsync(sql, model, commandType, cancellationToken);

            // implementreturnAffected rows
            var rowEffects = await dbCommand.ExecuteNonQueryAsync(cancellationToken);

            // release command object
            await dbCommand.DisposeAsync();

            return (rowEffects, dbParameters);
        }

        /// <summary>
        /// implement Sql returnsingle row and single column value
        /// </summary>
        /// <param name="databaseFacade">ADO.NET database object</param>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="commandType">Command type</param>
        /// <returns>single row and single column value</returns>
        public static object ExecuteScalar(
            this Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade databaseFacade, string sql,
            DbParameter[] parameters = null, CommandType commandType = CommandType.Text)
        {
            // Initialize database connection object and database command object
            var (_, dbCommand) = databaseFacade.PrepareDbCommand(sql, parameters, commandType);

            // implementreturnsingle row and single column value
            var result = dbCommand.ExecuteScalar();

            // release command object
            dbCommand.Dispose();

            return result != DBNull.Value ? result : default;
        }

        /// <summary>
        /// implement Sql returnsingle row and single column value
        /// </summary>
        /// <param name="databaseFacade">ADO.NET database object</param>
        /// <param name="sql">sql statement</param>
        /// <param name="model">command model</param>
        /// <param name="commandType">Command type</param>
        /// <returns>(object result, DbParameter[] dbParameters)</returns>
        public static (object result, DbParameter[] dbParameters) ExecuteScalar(
            this Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade databaseFacade, string sql, object model,
            CommandType commandType = CommandType.Text)
        {
            // Initialize database connection object and database command object
            var (_, dbCommand, dbParameters) = databaseFacade.PrepareDbCommand(sql, model, commandType);

            // implementreturnsingle row and single column value
            var result = dbCommand.ExecuteScalar();

            // release command object
            dbCommand.Dispose();

            return (result != DBNull.Value ? result : default, dbParameters);
        }

        /// <summary>
        /// implement Sql returnsingle row and single column value
        /// </summary>
        /// <param name="databaseFacade">ADO.NET database object</param>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="commandType">Command type</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>single row and single column value</returns>
        public static async Task<object> ExecuteScalarAsync(
            this Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade databaseFacade, string sql,
            DbParameter[] parameters = null, CommandType commandType = CommandType.Text,
            CancellationToken cancellationToken = default)
        {
            // Initialize database connection object and database command object
            var (_, dbCommand) =
                await databaseFacade.PrepareDbCommandAsync(sql, parameters, commandType, cancellationToken);

            // implementreturnsingle row and single column value
            var result = await dbCommand.ExecuteScalarAsync(cancellationToken);

            // release command object
            await dbCommand.DisposeAsync();

            return result != DBNull.Value ? result : default;
        }

        /// <summary>
        /// implement Sql returnsingle row and single column value
        /// </summary>
        /// <param name="databaseFacade">ADO.NET database object</param>
        /// <param name="sql">sql statement</param>
        /// <param name="model">command model</param>
        /// <param name="commandType">Command type</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>(object result, DbParameter[] dbParameters)</returns>
        public static async Task<(object result, DbParameter[] dbParameters)> ExecuteScalarAsync(
            this Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade databaseFacade, string sql, object model,
            CommandType commandType = CommandType.Text, CancellationToken cancellationToken = default)
        {
            // Initialize database connection object and database command object
            var (_, dbCommand, dbParameters) =
                await databaseFacade.PrepareDbCommandAsync(sql, model, commandType, cancellationToken);

            // implementreturnsingle row and single column value
            var result = await dbCommand.ExecuteScalarAsync(cancellationToken);

            // release command object
            await dbCommand.DisposeAsync();

            return (result != DBNull.Value ? result : default, dbParameters);
        }

        /// <summary>
        /// implement Sql return DataSet
        /// </summary>
        /// <param name="databaseFacade">ADO.NET database object</param>
        /// <param name="sql">sql statement</param>
        /// <param name="commandType">Command type</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="behavior">Behavior</param>
        /// <returns>DataSet</returns>
        public static DataSet DataAdapterFill(
            this Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade databaseFacade, string sql,
            DbParameter[] parameters = null, CommandType commandType = CommandType.Text,
            CommandBehavior behavior = CommandBehavior.Default)
        {
            // Initialize database connection object and database command object
            var (_, dbCommand) = databaseFacade.PrepareDbCommand(sql, parameters, commandType);

            // read data
            using var dbDataReader = dbCommand.ExecuteReader(behavior);

            // fill to DataSet
            var dataSet = dbDataReader.ToDataSet();

            // release command object
            dbCommand.Dispose();

            return dataSet;
        }

        /// <summary>
        /// implement Sql return DataSet
        /// </summary>
        /// <param name="databaseFacade">ADO.NET database object</param>
        /// <param name="sql">sql statement</param>
        /// <param name="commandType">Command type</param>
        /// <param name="model">command model</param>
        /// <param name="behavior">Behavior</param>
        /// <returns>(DataSet dataSet, DbParameter[] dbParameters)</returns>
        public static (DataSet dataSet, DbParameter[] dbParameters) DataAdapterFill(
            this Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade databaseFacade, string sql, object model,
            CommandType commandType = CommandType.Text, CommandBehavior behavior = CommandBehavior.Default)
        {
            // Initialize database connection object and database command object
            var (_, dbCommand, dbParameters) = databaseFacade.PrepareDbCommand(sql, model, commandType);

            // read data
            using var dbDataReader = dbCommand.ExecuteReader(behavior);

            // fill to DataSet
            var dataSet = dbDataReader.ToDataSet();

            // release command object
            dbCommand.Dispose();

            return (dataSet, dbParameters);
        }

        /// <summary>
        /// implement Sql return DataSet
        /// </summary>
        /// <param name="databaseFacade">ADO.NET database object</param>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="commandType">Command type</param>
        /// <param name="behavior">Behavior</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>DataSet</returns>
        public static async Task<DataSet> DataAdapterFillAsync(
            this Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade databaseFacade, string sql,
            DbParameter[] parameters = null, CommandType commandType = CommandType.Text,
            CommandBehavior behavior = CommandBehavior.Default, CancellationToken cancellationToken = default)
        {
            // Initialize database connection object and database command object
            var (_, dbCommand) =
                await databaseFacade.PrepareDbCommandAsync(sql, parameters, commandType, cancellationToken);

            // read data
            using var dbDataReader = await dbCommand.ExecuteReaderAsync(behavior, cancellationToken);

            // fill to DataSet
            var dataSet = dbDataReader.ToDataSet();

            // release command object
            await dbCommand.DisposeAsync();

            return dataSet;
        }

        /// <summary>
        /// implement Sql return DataSet
        /// </summary>
        /// <param name="databaseFacade">ADO.NET database object</param>
        /// <param name="sql">sql statement</param>
        /// <param name="model">command model</param>
        /// <param name="commandType">Command type</param>
        /// <param name="behavior">Behavior</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>(DataSet dataSet, DbParameter[] dbParameters)</returns>
        public static async Task<(DataSet dataSet, DbParameter[] dbParameters)> DataAdapterFillAsync(
            this Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade databaseFacade, string sql, object model,
            CommandType commandType = CommandType.Text, CommandBehavior behavior = CommandBehavior.Default,
            CancellationToken cancellationToken = default)
        {
            // Initialize database connection object and database command object
            var (_, dbCommand, dbParameters) =
                await databaseFacade.PrepareDbCommandAsync(sql, model, commandType, cancellationToken);

            // read data
            using var dbDataReader = await dbCommand.ExecuteReaderAsync(behavior, cancellationToken);

            // fill to DataSet
            var dataSet = dbDataReader.ToDataSet();

            // release command object
            await dbCommand.DisposeAsync();

            return (dataSet, dbParameters);
        }
    }
}