using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Silky.EntityFrameworkCore.Locators;
using Silky.EntityFrameworkCore.Values;

namespace Silky.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Sql Execute repository interface
    /// </summary>
    public interface ISqlExecutableRepository : ISqlExecutableRepository<MasterDbContextLocator>
    {
    }

    /// <summary>
    /// Sql Execute repository interface
    /// </summary>
    /// <typeparam name="TDbContextLocator">database context locator</typeparam>
    public interface ISqlExecutableRepository<TDbContextLocator> : IPrivateSqlExecutableRepository
        where TDbContextLocator : class, IDbContextLocator
    {
    }

    /// <summary>
    /// Sql Execute repository interface
    /// </summary>
    public interface IPrivateSqlExecutableRepository : IPrivateRootRepository
    {
        /// <summary>
        /// Execute the stored procedure to return DataTable
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>DataTable</returns>
        DataTable SqlProcedureQuery(string procName, params DbParameter[] parameters);

        /// <summary>
        /// Execute the stored procedure to return DataTable
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <returns>DataTable</returns>
        DataTable SqlProcedureQuery(string procName, object model);

        /// <summary>
        /// Execute the stored procedure to return DataTable
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>DataTable</returns>
        Task<DataTable> SqlProcedureQueryAsync(string procName, params DbParameter[] parameters);

        /// <summary>
        /// Execute the stored procedure to return DataTable
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>DataTable</returns>
        Task<DataTable> SqlProcedureQueryAsync(string procName, DbParameter[] parameters,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute the stored procedure to return DataTable
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>DataTable</returns>
        Task<DataTable> SqlProcedureQueryAsync(string procName, object model,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute the stored procedure to return List gather
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>List{T}</returns>
        List<T> SqlProcedureQuery<T>(string procName, params DbParameter[] parameters);

        /// <summary>
        /// Execute the stored procedure to return List gather
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <returns>List{T}</returns>
        List<T> SqlProcedureQuery<T>(string procName, object model);

        /// <summary>
        /// Execute the stored procedure to return List gather
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>List{T}</returns>
        Task<List<T>> SqlProcedureQueryAsync<T>(string procName, params DbParameter[] parameters);

        /// <summary>
        /// Execute the stored procedure to return List gather
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>List{T}</returns>
        Task<List<T>> SqlProcedureQueryAsync<T>(string procName, DbParameter[] parameters,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute the stored procedure to return List gather
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>List{T}</returns>
        Task<List<T>> SqlProcedureQueryAsync<T>(string procName, object model,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute the stored procedure to return DataSet
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>DataSet</returns>
        DataSet SqlProcedureQueries(string procName, params DbParameter[] parameters);

        /// <summary>
        /// Execute the stored procedure to return DataSet
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <returns>DataSet</returns>
        DataSet SqlProcedureQueries(string procName, object model);

        /// <summary>
        /// Execute the stored procedure to return DataSet
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>DataSet</returns>
        Task<DataSet> SqlProcedureQueriesAsync(string procName, params DbParameter[] parameters);

        /// <summary>
        /// Execute the stored procedure to return DataSet
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>DataSet</returns>
        Task<DataSet> SqlProcedureQueriesAsync(string procName, DbParameter[] parameters,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute the stored procedure to return DataSet
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>DataSet</returns>
        Task<DataSet> SqlProcedureQueriesAsync(string procName, object model,
            CancellationToken cancellationToken = default);

        /// <summary>
        ///  Execute the stored procedure to return List gather
        /// </summary>
        /// <typeparam name="T1">return type</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>List{T1}</returns>
        List<T1> SqlProcedureQueries<T1>(string procName, params DbParameter[] parameters);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        (List<T1> list1, List<T2> list2) SqlProcedureQueries<T1, T2>(string procName, params DbParameter[] parameters);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3) SqlProcedureQueries<T1, T2, T3>(string procName,
            params DbParameter[] parameters);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4) SqlProcedureQueries<T1, T2, T3, T4>(
            string procName, params DbParameter[] parameters);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)
            SqlProcedureQueries<T1, T2, T3, T4, T5>(string procName, params DbParameter[] parameters);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)
            SqlProcedureQueries<T1, T2, T3, T4, T5, T6>(string procName, params DbParameter[] parameters);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <typeparam name="T7">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7)
            SqlProcedureQueries<T1, T2, T3, T4, T5, T6, T7>(string procName, params DbParameter[] parameters);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <typeparam name="T7">tuple元素类型</typeparam>
        /// <typeparam name="T8">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7,
            List<T8> list8) SqlProcedureQueries<T1, T2, T3, T4, T5, T6, T7, T8>(string procName,
                params DbParameter[] parameters);

        /// <summary>
        ///  Execute the stored procedure to return List gather
        /// </summary>
        /// <typeparam name="T1">return type</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <returns>List{T1}</returns>
        List<T1> SqlProcedureQueries<T1>(string procName, object model);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <returns>tuple类型</returns>
        (List<T1> list1, List<T2> list2) SqlProcedureQueries<T1, T2>(string procName, object model);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <returns>tuple类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3) SqlProcedureQueries<T1, T2, T3>(string procName, object model);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <returns>tuple类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4) SqlProcedureQueries<T1, T2, T3, T4>(
            string procName, object model);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <returns>tuple类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5) SqlProcedureQueries<T1, T2, T3,
            T4, T5>(string procName, object model);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <returns>tuple类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)
            SqlProcedureQueries<T1, T2, T3, T4, T5, T6>(string procName, object model);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <typeparam name="T7">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <returns>tuple类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7)
            SqlProcedureQueries<T1, T2, T3, T4, T5, T6, T7>(string procName, object model);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <typeparam name="T7">tuple元素类型</typeparam>
        /// <typeparam name="T8">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <returns>tuple类型</returns>
        (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7> list7,
            List<T8> list8) SqlProcedureQueries<T1, T2, T3, T4, T5, T6, T7, T8>(string procName, object model);

        /// <summary>
        ///  Execute the stored procedure to return List gather
        /// </summary>
        /// <typeparam name="T1">return type</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>Task{List{T1}}</returns>
        Task<List<T1>> SqlProcedureQueriesAsync<T1>(string procName, params DbParameter[] parameters);

        /// <summary>
        ///  Execute the stored procedure to return List gather
        /// </summary>
        /// <typeparam name="T1">return type</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>Task{List{T1}}</returns>
        Task<List<T1>> SqlProcedureQueriesAsync<T1>(string procName, DbParameter[] parameters,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2)> SqlProcedureQueriesAsync<T1, T2>(string procName,
            params DbParameter[] parameters);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2)> SqlProcedureQueriesAsync<T1, T2>(string procName,
            DbParameter[] parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3)> SqlProcedureQueriesAsync<T1, T2, T3>(string procName,
            params DbParameter[] parameters);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3)> SqlProcedureQueriesAsync<T1, T2, T3>(string procName,
            DbParameter[] parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4)> SqlProcedureQueriesAsync<T1, T2, T3, T4>(
            string procName, params DbParameter[] parameters);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4)> SqlProcedureQueriesAsync<T1, T2, T3, T4>(
            string procName, DbParameter[] parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)>
            SqlProcedureQueriesAsync<T1, T2, T3, T4, T5>(string procName, params DbParameter[] parameters);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)>
            SqlProcedureQueriesAsync<T1, T2, T3, T4, T5>(string procName, DbParameter[] parameters,
                CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)>
            SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6>(string procName, params DbParameter[] parameters);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)>
            SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6>(string procName, DbParameter[] parameters,
                CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <typeparam name="T7">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7>
            list7)> SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6, T7>(string procName,
            params DbParameter[] parameters);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <typeparam name="T7">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7>
            list7)> SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6, T7>(string procName, DbParameter[] parameters,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <typeparam name="T7">tuple元素类型</typeparam>
        /// <typeparam name="T8">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7>
            list7, List<T8> list8)> SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string procName,
            params DbParameter[] parameters);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <typeparam name="T7">tuple元素类型</typeparam>
        /// <typeparam name="T8">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7>
            list7, List<T8> list8)> SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string procName,
            DbParameter[] parameters, CancellationToken cancellationToken = default);

        /// <summary>
        ///  Execute the stored procedure to return List gather
        /// </summary>
        /// <typeparam name="T1">return type</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>List{T1}</returns>
        Task<List<T1>> SqlProcedureQueriesAsync<T1>(string procName, object model,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2)> SqlProcedureQueriesAsync<T1, T2>(string procName, object model,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3)> SqlProcedureQueriesAsync<T1, T2, T3>(string procName,
            object model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4)> SqlProcedureQueriesAsync<T1, T2, T3, T4>(
            string procName, object model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)>
            SqlProcedureQueriesAsync<T1, T2, T3, T4, T5>(string procName, object model,
                CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)>
            SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6>(string procName, object model,
                CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <typeparam name="T7">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7>
            list7)> SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6, T7>(string procName, object model,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <typeparam name="T7">tuple元素类型</typeparam>
        /// <typeparam name="T8">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6, List<T7>
            list7, List<T8> list8)> SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6, T7, T8>(string procName,
            object model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute the stored procedure to return single row single column
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>object</returns>
        object SqlProcedureScalar(string procName, params DbParameter[] parameters);

        /// <summary>
        /// Execute the stored procedure to return single row single column
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <returns>object</returns>
        object SqlProcedureScalar(string procName, object model);

        /// <summary>
        /// Execute the stored procedure to return single row single column
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>object</returns>
        Task<object> SqlProcedureScalarAsync(string procName, params DbParameter[] parameters);

        /// <summary>
        /// Execute the stored procedure to return single row single column
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>object</returns>
        Task<object> SqlProcedureScalarAsync(string procName, DbParameter[] parameters,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute the stored procedure to return single row single column
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>object</returns>
        Task<object> SqlProcedureScalarAsync(string procName, object model,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute the stored procedure to return single row single column
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>TResult</returns>
        TResult SqlProcedureScalar<TResult>(string procName, params DbParameter[] parameters);

        /// <summary>
        /// Execute the stored procedure to return single row single column
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <returns>TResult</returns>
        TResult SqlProcedureScalar<TResult>(string procName, object model);

        /// <summary>
        /// Execute the stored procedure to return single row single column
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>TResult</returns>
        Task<TResult> SqlProcedureScalarAsync<TResult>(string procName, params DbParameter[] parameters);

        /// <summary>
        /// Execute the stored procedure to return single row single column
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>TResult</returns>
        Task<TResult> SqlProcedureScalarAsync<TResult>(string procName, DbParameter[] parameters,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute the stored procedure to return single row single column
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>TResult</returns>
        Task<TResult> SqlProcedureScalarAsync<TResult>(string procName, object model,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute the stored procedure with no data returned
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>int</returns>
        int SqlProcedureNonQuery(string procName, params DbParameter[] parameters);

        /// <summary>
        /// Execute the stored procedure with no data returned
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <returns>int</returns>
        int SqlProcedureNonQuery(string procName, object model);

        /// <summary>
        /// Execute the stored procedure with no data returned
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>int</returns>
        Task<int> SqlProcedureNonQueryAsync(string procName, params DbParameter[] parameters);

        /// <summary>
        /// Execute the stored procedure with no data returned
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>int</returns>
        Task<int> SqlProcedureNonQueryAsync(string procName, DbParameter[] parameters,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute the stored procedure with no data returned
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>int</returns>
        Task<int> SqlProcedureNonQueryAsync(string procName, object model,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// implement Sql no data returned
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>int</returns>
        int SqlNonQuery(string sql, params DbParameter[] parameters);

        /// <summary>
        /// implement Sql no data returned
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <returns>int</returns>
        int SqlNonQuery(string sql, object model);

        /// <summary>
        /// implement Sql no data returned
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>int</returns>
        Task<int> SqlNonQueryAsync(string sql, params DbParameter[] parameters);

        /// <summary>
        /// implement Sql no data returned
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>int</returns>
        Task<int> SqlNonQueryAsync(string sql, DbParameter[] parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// implement Sql no data returned
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>int</returns>
        Task<int> SqlNonQueryAsync(string sql, object model, CancellationToken cancellationToken = default);

        /// <summary>
        /// implement Sql return single row single column
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>object</returns>
        object SqlScalar(string sql, params DbParameter[] parameters);

        /// <summary>
        /// implement Sql return single row single column
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <returns>object</returns>
        object SqlScalar(string sql, object model);

        /// <summary>
        /// implement Sql return single row single column
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>object</returns>
        Task<object> SqlScalarAsync(string sql, params DbParameter[] parameters);

        /// <summary>
        /// implement Sql return single row single column
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>object</returns>
        Task<object> SqlScalarAsync(string sql, DbParameter[] parameters,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// implement Sql return single row single column
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>object</returns>
        Task<object> SqlScalarAsync(string sql, object model, CancellationToken cancellationToken = default);

        /// <summary>
        /// implement Sql return single row single column
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>TResult</returns>
        TResult SqlScalar<TResult>(string sql, params DbParameter[] parameters);

        /// <summary>
        /// implement Sql return single row single column
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <returns>TResult</returns>
        TResult SqlScalar<TResult>(string sql, object model);

        /// <summary>
        /// implement Sql return single row single column
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>TResult</returns>
        Task<TResult> SqlScalarAsync<TResult>(string sql, params DbParameter[] parameters);

        /// <summary>
        /// implement Sql return single row single column
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>TResult</returns>
        Task<TResult> SqlScalarAsync<TResult>(string sql, DbParameter[] parameters,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// implement Sql return single row single column
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>TResult</returns>
        Task<TResult> SqlScalarAsync<TResult>(string sql, object model, CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute the stored procedure to returnOUPUT、RETURN、result set
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>ProcedureOutput</returns>
        ProcedureOutputResult SqlProcedureOutput(string procName, DbParameter[] parameters);

        /// <summary>
        /// Execute the stored procedure to returnOUPUT、RETURN、result set
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>ProcedureOutput</returns>
        Task<ProcedureOutputResult> SqlProcedureOutputAsync(string procName, DbParameter[] parameters,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute the stored procedure to returnOUPUT、RETURN、result set
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">command model</param>
        /// <returns>ProcedureOutput</returns>
        ProcedureOutputResult SqlProcedureOutput(string procName, object model);

        /// <summary>
        /// Execute the stored procedure to returnOUPUT、RETURN、result set
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">command model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>ProcedureOutput</returns>
        Task<ProcedureOutputResult> SqlProcedureOutputAsync(string procName, object model,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute the stored procedure to returnOUPUT、RETURN、result set
        /// </summary>
        /// <typeparam name="TResult">dataset results</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>ProcedureOutput</returns>
        ProcedureOutputResult<TResult> SqlProcedureOutput<TResult>(string procName, DbParameter[] parameters);

        /// <summary>
        /// Execute the stored procedure to returnOUPUT、RETURN、result set
        /// </summary>
        /// <typeparam name="TResult">dataset results</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>ProcedureOutput</returns>
        Task<ProcedureOutputResult<TResult>> SqlProcedureOutputAsync<TResult>(string procName, DbParameter[] parameters,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Execute the stored procedure to returnOUPUT、RETURN、result set
        /// </summary>
        /// <typeparam name="TResult">dataset results</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">command model</param>
        /// <returns>ProcedureOutput</returns>
        ProcedureOutputResult<TResult> SqlProcedureOutput<TResult>(string procName, object model);

        /// <summary>
        /// Execute the stored procedure to returnOUPUT、RETURN、result set
        /// </summary>
        /// <typeparam name="TResult">dataset results</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">command model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>ProcedureOutput</returns>
        Task<ProcedureOutputResult<TResult>> SqlProcedureOutputAsync<TResult>(string procName, object model,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// implement标量函数return single row single column
        /// </summary>
        /// <param name="funcName">Function name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>object</returns>
        object SqlFunctionScalar(string funcName, params DbParameter[] parameters);

        /// <summary>
        /// implement标量函数return single row single column
        /// </summary>
        /// <param name="funcName">Function name</param>
        /// <param name="model">parametric model</param>
        /// <returns>object</returns>
        object SqlFunctionScalar(string funcName, object model);

        /// <summary>
        /// implement标量函数return single row single column
        /// </summary>
        /// <param name="funcName">Function name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>object</returns>
        Task<object> SqlFunctionScalarAsync(string funcName, params DbParameter[] parameters);

        /// <summary>
        /// implement标量函数return single row single column
        /// </summary>
        /// <param name="funcName">Function name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>object</returns>
        Task<object> SqlFunctionScalarAsync(string funcName, DbParameter[] parameters,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// implement标量函数return single row single column
        /// </summary>
        /// <param name="funcName">Function name</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>object</returns>
        Task<object> SqlFunctionScalarAsync(string funcName, object model,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// implement标量函数return single row single column
        /// </summary>
        /// <typeparam name="TResult">return值类型</typeparam>
        /// <param name="funcName">Function name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>TResult</returns>
        TResult SqlFunctionScalar<TResult>(string funcName, params DbParameter[] parameters);

        /// <summary>
        /// implement标量函数return single row single column
        /// </summary>
        /// <typeparam name="TResult">return值类型</typeparam>
        /// <param name="funcName">Function name</param>
        /// <param name="model">parametric model</param>
        /// <returns>TResult</returns>
        TResult SqlFunctionScalar<TResult>(string funcName, object model);

        /// <summary>
        /// implement标量函数return single row single column
        /// </summary>
        /// <typeparam name="TResult">return值类型</typeparam>
        /// <param name="funcName">Function name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>TResult</returns>
        Task<TResult> SqlFunctionScalarAsync<TResult>(string funcName, params DbParameter[] parameters);

        /// <summary>
        /// implement标量函数return single row single column
        /// </summary>
        /// <typeparam name="TResult">return值类型</typeparam>
        /// <param name="funcName">Function name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>TResult</returns>
        Task<TResult> SqlFunctionScalarAsync<TResult>(string funcName, DbParameter[] parameters,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// implement标量函数return single row single column
        /// </summary>
        /// <typeparam name="TResult">return值类型</typeparam>
        /// <param name="funcName">Function name</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>object</returns>
        Task<TResult> SqlFunctionScalarAsync<TResult>(string funcName, object model,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// implement表值函数return DataTable
        /// </summary>
        /// <param name="funcName">Function name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>DataTable</returns>
        DataTable SqlFunctionQuery(string funcName, params DbParameter[] parameters);

        /// <summary>
        /// implement表值函数return DataTable
        /// </summary>
        /// <param name="funcName">Function name</param>
        /// <param name="model">parametric model</param>
        /// <returns>DataTable</returns>
        DataTable SqlFunctionQuery(string funcName, object model);

        /// <summary>
        /// implement表值函数return DataTable
        /// </summary>
        /// <param name="funcName">Function name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>Task{DataTable}</returns>
        Task<DataTable> SqlFunctionQueryAsync(string funcName, params DbParameter[] parameters);

        /// <summary>
        /// implement表值函数return DataTable
        /// </summary>
        /// <param name="funcName">Function name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>Task{DataTable}</returns>
        Task<DataTable> SqlFunctionQueryAsync(string funcName, DbParameter[] parameters,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// implement表值函数return DataTable
        /// </summary>
        /// <param name="funcName">Function name</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>Task{DataTable}</returns>
        Task<DataTable> SqlFunctionQueryAsync(string funcName, object model,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// implement表值函数return List gather
        /// </summary>
        /// <typeparam name="T">return type</typeparam>
        /// <param name="funcName">Function name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>List{T}</returns>
        List<T> SqlFunctionQuery<T>(string funcName, params DbParameter[] parameters);

        /// <summary>
        /// implement表值函数return List gather
        /// </summary>
        /// <typeparam name="T">return type</typeparam>
        /// <param name="funcName">Function name</param>
        /// <param name="model">parametric model</param>
        /// <returns>List{T}</returns>
        List<T> SqlFunctionQuery<T>(string funcName, object model);

        /// <summary>
        /// implement表值函数return List gather
        /// </summary>
        /// <typeparam name="T">return type</typeparam>
        /// <param name="funcName">Function name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>Task{List{T}}</returns>
        Task<List<T>> SqlFunctionQueryAsync<T>(string funcName, params DbParameter[] parameters);

        /// <summary>
        /// implement表值函数return List gather
        /// </summary>
        /// <typeparam name="T">return type</typeparam>
        /// <param name="funcName">Function name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>Task{List{T}}</returns>
        Task<List<T>> SqlFunctionQueryAsync<T>(string funcName, DbParameter[] parameters,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// implement表值函数return List gather
        /// </summary>
        /// <typeparam name="T">return type</typeparam>
        /// <param name="funcName">Function name</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>Task{List{T}}</returns>
        Task<List<T>> SqlFunctionQueryAsync<T>(string funcName, object model,
            CancellationToken cancellationToken = default);
    }
}