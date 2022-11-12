using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Silky.EntityFrameworkCore.Locators;

namespace Silky.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Sql Query repository interface
    /// </summary>
    public interface ISqlReaderRepository : ISqlReaderRepository<MasterDbContextLocator>
    {
    }

    /// <summary>
    /// Sql Query repository interface
    /// </summary>
    /// <typeparam name="TDbContextLocator">database context locator</typeparam>
    public interface ISqlReaderRepository<TDbContextLocator> : IPrivateSqlReaderRepository
        where TDbContextLocator : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Sql Query repository interface
    /// </summary>
    public interface IPrivateSqlReaderRepository : IPrivateRootRepository
    {
        /// <summary>
        /// Sql query returns DataTable
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>DataTable</returns>
        DataTable SqlQuery(string sql, params DbParameter[] parameters);

        /// <summary>
        /// Sql query returns DataTable
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <returns>DataTable</returns>
        DataTable SqlQuery(string sql, object model);

        /// <summary>
        /// Sql query returns DataTable
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>Task{DataTable}</returns>
        Task<DataTable> SqlQueryAsync(string sql, params DbParameter[] parameters);

        /// <summary>
        /// Sql query returns DataTable
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>Task{DataTable}</returns>
        Task<DataTable> SqlQueryAsync(string sql, DbParameter[] parameters,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Sql query returns DataTable
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>Task{DataTable}</returns>
        Task<DataTable> SqlQueryAsync(string sql, object model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sql query returns List gather
        /// </summary>
        /// <typeparam name="T">return type</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>List{T}</returns>
        List<T> SqlQuery<T>(string sql, params DbParameter[] parameters);

        /// <summary>
        /// Sql query returns List gather
        /// </summary>
        /// <typeparam name="T">return type</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <returns>List{T}</returns>
        List<T> SqlQuery<T>(string sql, object model);

        /// <summary>
        /// Sql query returns List gather
        /// </summary>
        /// <typeparam name="T">return type</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>Task{List{T}}</returns>
        Task<List<T>> SqlQueryAsync<T>(string sql, params DbParameter[] parameters);

        /// <summary>
        /// Sql query returns List gather
        /// </summary>
        /// <typeparam name="T">return type</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>Task{List{T}}</returns>
        Task<List<T>> SqlQueryAsync<T>(string sql, DbParameter[] parameters,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Sql query returns List gather
        /// </summary>
        /// <typeparam name="T">return type</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>Task{List{T}}</returns>
        Task<List<T>> SqlQueryAsync<T>(string sql, object model, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Sql query returns DataSet
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>DataSet</returns>
        DataSet SqlQueries(string sql, params DbParameter[] parameters);

        /// <summary>
        ///  Sql query returns DataSet
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <returns>DataSet</returns>
        DataSet SqlQueries(string sql, object model);

        /// <summary>
        ///  Sql query returns DataSet
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>Task{DataSet}</returns>
        Task<DataSet> SqlQueriesAsync(string sql, params DbParameter[] parameters);

        /// <summary>
        ///  Sql query returns DataSet
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>Task{DataSet}</returns>
        Task<DataSet> SqlQueriesAsync(string sql, DbParameter[] parameters,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///  Sql query returns DataSet
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>Task{DataSet}</returns>
        Task<DataSet> SqlQueriesAsync(string sql, object model, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Sql query returns List gather
        /// </summary>
        /// <typeparam name="T1">return type</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>List{T1}</returns>
        List<T1> SqlQueries<T1>(string sql, params DbParameter[] parameters);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        (List<T1> list1, List<T2> list2) SqlQueries<T1, T2>(string sql, params DbParameter[] parameters);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3) SqlQueries<T1, T2, T3>(string sql,
            params DbParameter[] parameters);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4) SqlQueries<T1, T2, T3, T4>(string sql,
            params DbParameter[] parameters);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5) SqlQueries<T1, T2, T3, T4, T5>(
            string sql, params DbParameter[] parameters);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6) SqlQueries<T1,
            T2, T3, T4, T5, T6>(string sql, params DbParameter[] parameters);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <typeparam name="T7">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7)
            SqlQueries<T1, T2, T3, T4, T5, T6, T7>(string sql, params DbParameter[] parameters);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <typeparam name="T7">tuple元素类型</typeparam>
        /// <typeparam name="T8">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7,
            List<T8> list8) SqlQueries<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, params DbParameter[] parameters);

        /// <summary>
        ///  Sql query returns List gather
        /// </summary>
        /// <typeparam name="T1">return type</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <returns>List{T1}</returns>
        List<T1> SqlQueries<T1>(string sql, object model);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <returns>tuple类型</returns>
        (List<T1> list1, List<T2> list2) SqlQueries<T1, T2>(string sql, object model);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <returns>tuple类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3) SqlQueries<T1, T2, T3>(string sql, object model);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <returns>tuple类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4) SqlQueries<T1, T2, T3, T4>(string sql,
            object model);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <returns>tuple类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5) SqlQueries<T1, T2, T3, T4, T5>(
            string sql, object model);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <returns>tuple类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6) SqlQueries<T1,
            T2, T3, T4, T5, T6>(string sql, object model);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <typeparam name="T7">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <returns>tuple类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7)
            SqlQueries<T1, T2, T3, T4, T5, T6, T7>(string sql, object model);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <typeparam name="T7">tuple元素类型</typeparam>
        /// <typeparam name="T8">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <returns>tuple类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7,
            List<T8> list8) SqlQueries<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, object model);

        /// <summary>
        ///  Sql query returns List gather
        /// </summary>
        /// <typeparam name="T1">return type</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>Task{List{T1}}</returns>
        Task<List<T1>> SqlQueriesAsync<T1>(string sql, params DbParameter[] parameters);

        /// <summary>
        ///  Sql query returns List gather
        /// </summary>
        /// <typeparam name="T1">return type</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>Task{List{T1}}</returns>
        Task<List<T1>> SqlQueriesAsync<T1>(string sql, DbParameter[] parameters,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2)> SqlQueriesAsync<T1, T2>(string sql, params DbParameter[] parameters);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2)> SqlQueriesAsync<T1, T2>(string sql, DbParameter[] parameters,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3)> SqlQueriesAsync<T1, T2, T3>(string sql,
            params DbParameter[] parameters);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3)> SqlQueriesAsync<T1, T2, T3>(string sql,
            DbParameter[] parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4)> SqlQueriesAsync<T1, T2, T3, T4>(
            string sql, params DbParameter[] parameters);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4)> SqlQueriesAsync<T1, T2, T3, T4>(
            string sql, DbParameter[] parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)>
            SqlQueriesAsync<T1, T2, T3, T4, T5>(string sql, params DbParameter[] parameters);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)>
            SqlQueriesAsync<T1, T2, T3, T4, T5>(string sql, DbParameter[] parameters,
                CancellationToken cancellationToken = default);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)>
            SqlQueriesAsync<T1, T2, T3, T4, T5, T6>(string sql, params DbParameter[] parameters);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)>
            SqlQueriesAsync<T1, T2, T3, T4, T5, T6>(string sql, DbParameter[] parameters,
                CancellationToken cancellationToken = default);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <typeparam name="T7">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7>
            list7)> SqlQueriesAsync<T1, T2, T3, T4, T5, T6, T7>(string sql, params DbParameter[] parameters);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <typeparam name="T7">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7>
            list7)> SqlQueriesAsync<T1, T2, T3, T4, T5, T6, T7>(string sql, DbParameter[] parameters,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <typeparam name="T7">tuple元素类型</typeparam>
        /// <typeparam name="T8">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7>
            list7, List<T8> list8)> SqlQueriesAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string sql,
            params DbParameter[] parameters);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <typeparam name="T7">tuple元素类型</typeparam>
        /// <typeparam name="T8">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7>
            list7, List<T8> list8)> SqlQueriesAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string sql,
            DbParameter[] parameters, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Sql query returns List gather
        /// </summary>
        /// <typeparam name="T1">return type</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>List{T1}</returns>
        Task<List<T1>> SqlQueriesAsync<T1>(string sql, object model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2)> SqlQueriesAsync<T1, T2>(string sql, object model,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3)> SqlQueriesAsync<T1, T2, T3>(string sql, object model,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4)> SqlQueriesAsync<T1, T2, T3, T4>(
            string sql, object model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)>
            SqlQueriesAsync<T1, T2, T3, T4, T5>(string sql, object model,
                CancellationToken cancellationToken = default);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)>
            SqlQueriesAsync<T1, T2, T3, T4, T5, T6>(string sql, object model,
                CancellationToken cancellationToken = default);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <typeparam name="T7">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7>
            list7)> SqlQueriesAsync<T1, T2, T3, T4, T5, T6, T7>(string sql, object model,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <typeparam name="T7">tuple元素类型</typeparam>
        /// <typeparam name="T8">tuple元素类型</typeparam>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7>
            list7, List<T8> list8)> SqlQueriesAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string sql, object model,
            CancellationToken cancellationToken = default);
    }
}