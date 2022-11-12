using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Silky.Core.Extensions;
using Silky.EntityFrameworkCore.Extensions.DatabaseFacade;
using Silky.EntityFrameworkCore.Helpers;
using Silky.EntityFrameworkCore.Values;

namespace Silky.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Sql Execute warehouse division class
    /// </summary>
    public partial class PrivateSqlRepository
    {
        /// <summary>
        /// Execute the stored procedure to return DataTable
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>DataTable</returns>
        public virtual DataTable SqlProcedureQuery(string procName, params DbParameter[] parameters)
        {
            return Database.ExecuteReader(procName, parameters, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Execute the stored procedure to return DataTable
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <returns>DataTable</returns>
        public virtual DataTable SqlProcedureQuery(string procName, object model)
        {
            return Database.ExecuteReader(procName, model, CommandType.StoredProcedure).dataTable;
        }

        /// <summary>
        /// Execute the stored procedure to return DataTable
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>DataTable</returns>
        public virtual Task<DataTable> SqlProcedureQueryAsync(string procName, params DbParameter[] parameters)
        {
            return Database.ExecuteReaderAsync(procName, parameters, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Execute the stored procedure to return DataTable
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>DataTable</returns>
        public virtual Task<DataTable> SqlProcedureQueryAsync(string procName, DbParameter[] parameters,
            CancellationToken cancellationToken = default)
        {
            return Database.ExecuteReaderAsync(procName, parameters, CommandType.StoredProcedure,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Execute the stored procedure to return DataTable
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>DataTable</returns>
        public virtual async Task<DataTable> SqlProcedureQueryAsync(string procName, object model,
            CancellationToken cancellationToken = default)
        {
            var (dataTable, _) = await Database.ExecuteReaderAsync(procName, model, CommandType.StoredProcedure,
                cancellationToken: cancellationToken);
            return dataTable;
        }

        /// <summary>
        /// Execute the stored procedure to return List gather
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>List{T}</returns>
        public virtual List<T> SqlProcedureQuery<T>(string procName, params DbParameter[] parameters)
        {
            return Database.ExecuteReader(procName, parameters, CommandType.StoredProcedure).ToList<T>();
        }

        /// <summary>
        /// Execute the stored procedure to return List gather
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <returns>List{T}</returns>
        public virtual List<T> SqlProcedureQuery<T>(string procName, object model)
        {
            return Database.ExecuteReader(procName, model, CommandType.StoredProcedure).dataTable.ToList<T>();
        }

        /// <summary>
        /// Execute the stored procedure to return List gather
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>List{T}</returns>
        public virtual async Task<List<T>> SqlProcedureQueryAsync<T>(string procName, params DbParameter[] parameters)
        {
            var dataTable = await Database.ExecuteReaderAsync(procName, parameters, CommandType.StoredProcedure);
            return dataTable.ToList<T>();
        }

        /// <summary>
        /// Execute the stored procedure to return List gather
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>List{T}</returns>
        public virtual async Task<List<T>> SqlProcedureQueryAsync<T>(string procName, DbParameter[] parameters,
            CancellationToken cancellationToken = default)
        {
            var dataTable = await Database.ExecuteReaderAsync(procName, parameters, CommandType.StoredProcedure,
                cancellationToken: cancellationToken);
            return dataTable.ToList<T>();
        }

        /// <summary>
        /// Execute the stored procedure to return List gather
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>List{T}</returns>
        public virtual async Task<List<T>> SqlProcedureQueryAsync<T>(string procName, object model,
            CancellationToken cancellationToken = default)
        {
            var (dataTable, _) = await Database.ExecuteReaderAsync(procName, model, CommandType.StoredProcedure,
                cancellationToken: cancellationToken);
            return dataTable.ToList<T>();
        }

        /// <summary>
        /// Execute the stored procedure to return DataSet
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>DataSet</returns>
        public virtual DataSet SqlProcedureQueries(string procName, params DbParameter[] parameters)
        {
            return Database.DataAdapterFill(procName, parameters, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Execute the stored procedure to return DataSet
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <returns>DataSet</returns>
        public virtual DataSet SqlProcedureQueries(string procName, object model)
        {
            return Database.DataAdapterFill(procName, model, CommandType.StoredProcedure).dataSet;
        }

        /// <summary>
        /// Execute the stored procedure to return DataSet
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>DataSet</returns>
        public virtual Task<DataSet> SqlProcedureQueriesAsync(string procName, params DbParameter[] parameters)
        {
            return Database.DataAdapterFillAsync(procName, parameters, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Execute the stored procedure to return DataSet
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>DataSet</returns>
        public virtual Task<DataSet> SqlProcedureQueriesAsync(string procName, DbParameter[] parameters,
            CancellationToken cancellationToken = default)
        {
            return Database.DataAdapterFillAsync(procName, parameters, CommandType.StoredProcedure,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Execute the stored procedure to return DataSet
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>DataSet</returns>
        public virtual async Task<DataSet> SqlProcedureQueriesAsync(string procName, object model,
            CancellationToken cancellationToken = default)
        {
            var (dataSet, _) = await Database.DataAdapterFillAsync(procName, model, CommandType.StoredProcedure,
                cancellationToken: cancellationToken);
            return dataSet;
        }

        /// <summary>
        ///  Execute the stored procedure to return List gather
        /// </summary>
        /// <typeparam name="T1">return type</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>List{T1}</returns>
        public virtual List<T1> SqlProcedureQueries<T1>(string procName, params DbParameter[] parameters)
        {
            return Database.DataAdapterFill(procName, parameters, CommandType.StoredProcedure).ToList<T1>();
        }

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        public virtual (List<T1> list1, List<T2> list2) SqlProcedureQueries<T1, T2>(string procName,
            params DbParameter[] parameters)
        {
            return Database.DataAdapterFill(procName, parameters, CommandType.StoredProcedure).ToList<T1, T2>();
        }

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        public virtual (List<T1> list1, List<T2> list2, List<T3> list3) SqlProcedureQueries<T1, T2, T3>(string procName,
            params DbParameter[] parameters)
        {
            return Database.DataAdapterFill(procName, parameters, CommandType.StoredProcedure).ToList<T1, T2, T3>();
        }

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
        public virtual (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4)
            SqlProcedureQueries<T1, T2, T3, T4>(string procName, params DbParameter[] parameters)
        {
            return Database.DataAdapterFill(procName, parameters, CommandType.StoredProcedure).ToList<T1, T2, T3, T4>();
        }

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
        public virtual (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)
            SqlProcedureQueries<T1, T2, T3, T4, T5>(string procName, params DbParameter[] parameters)
        {
            return Database.DataAdapterFill(procName, parameters, CommandType.StoredProcedure)
                .ToList<T1, T2, T3, T4, T5>();
        }

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
        public virtual (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)
            SqlProcedureQueries<T1, T2, T3, T4, T5, T6>(string procName, params DbParameter[] parameters)
        {
            return Database.DataAdapterFill(procName, parameters, CommandType.StoredProcedure)
                .ToList<T1, T2, T3, T4, T5, T6>();
        }

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
        public virtual (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6,
            List<T7> list7) SqlProcedureQueries<T1, T2, T3, T4, T5, T6, T7>(string procName,
                params DbParameter[] parameters)
        {
            return Database.DataAdapterFill(procName, parameters, CommandType.StoredProcedure)
                .ToList<T1, T2, T3, T4, T5, T6, T7>();
        }

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
        public virtual (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6,
            List<T7> list7, List<T8> list8) SqlProcedureQueries<T1, T2, T3, T4, T5, T6, T7, T8>(string procName,
                params DbParameter[] parameters)
        {
            return Database.DataAdapterFill(procName, parameters, CommandType.StoredProcedure)
                .ToList<T1, T2, T3, T4, T5, T6, T7, T8>();
        }

        /// <summary>
        ///  Execute the stored procedure to return List gather
        /// </summary>
        /// <typeparam name="T1">return type</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <returns>List{T1}</returns>
        public virtual List<T1> SqlProcedureQueries<T1>(string procName, object model)
        {
            return Database.DataAdapterFill(procName, model, CommandType.StoredProcedure).dataSet.ToList<T1>();
        }

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <returns>tuple类型</returns>
        public virtual (List<T1> list1, List<T2> list2) SqlProcedureQueries<T1, T2>(string procName, object model)
        {
            return Database.DataAdapterFill(procName, model, CommandType.StoredProcedure).dataSet.ToList<T1, T2>();
        }

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <returns>tuple类型</returns>
        public virtual (List<T1> list1, List<T2> list2, List<T3> list3) SqlProcedureQueries<T1, T2, T3>(string procName,
            object model)
        {
            return Database.DataAdapterFill(procName, model, CommandType.StoredProcedure).dataSet.ToList<T1, T2, T3>();
        }

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
        public virtual (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4) SqlProcedureQueries<T1, T2, T3,
            T4>(string procName, object model)
        {
            return Database.DataAdapterFill(procName, model, CommandType.StoredProcedure).dataSet
                .ToList<T1, T2, T3, T4>();
        }

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
        public virtual (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)
            SqlProcedureQueries<T1, T2, T3, T4, T5>(string procName, object model)
        {
            return Database.DataAdapterFill(procName, model, CommandType.StoredProcedure).dataSet
                .ToList<T1, T2, T3, T4, T5>();
        }

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
        public virtual (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)
            SqlProcedureQueries<T1, T2, T3, T4, T5, T6>(string procName, object model)
        {
            return Database.DataAdapterFill(procName, model, CommandType.StoredProcedure).dataSet
                .ToList<T1, T2, T3, T4, T5, T6>();
        }

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
        public virtual (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6,
            List<T7> list7) SqlProcedureQueries<T1, T2, T3, T4, T5, T6, T7>(string procName, object model)
        {
            return Database.DataAdapterFill(procName, model, CommandType.StoredProcedure).dataSet
                .ToList<T1, T2, T3, T4, T5, T6, T7>();
        }

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
        public virtual (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6,
            List<T7> list7, List<T8> list8) SqlProcedureQueries<T1, T2, T3, T4, T5, T6, T7, T8>(string procName,
                object model)
        {
            return Database.DataAdapterFill(procName, model, CommandType.StoredProcedure).dataSet
                .ToList<T1, T2, T3, T4, T5, T6, T7, T8>();
        }

        /// <summary>
        ///  Execute the stored procedure to return List gather
        /// </summary>
        /// <typeparam name="T1">return type</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>Task{List{T1}}</returns>
        public virtual async Task<List<T1>> SqlProcedureQueriesAsync<T1>(string procName,
            params DbParameter[] parameters)
        {
            var dataset = await Database.DataAdapterFillAsync(procName, parameters, CommandType.StoredProcedure);
            return dataset.ToList<T1>();
        }

        /// <summary>
        ///  Execute the stored procedure to return List gather
        /// </summary>
        /// <typeparam name="T1">return type</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>Task{List{T1}}</returns>
        public virtual async Task<List<T1>> SqlProcedureQueriesAsync<T1>(string procName, DbParameter[] parameters,
            CancellationToken cancellationToken = default)
        {
            var dataset = await Database.DataAdapterFillAsync(procName, parameters, CommandType.StoredProcedure,
                cancellationToken: cancellationToken);
            return dataset.ToList<T1>();
        }

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        public virtual async Task<(List<T1> list1, List<T2> list2)> SqlProcedureQueriesAsync<T1, T2>(string procName,
            params DbParameter[] parameters)
        {
            var dataset = await Database.DataAdapterFillAsync(procName, parameters, CommandType.StoredProcedure);
            return dataset.ToList<T1, T2>();
        }

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        public virtual async Task<(List<T1> list1, List<T2> list2)> SqlProcedureQueriesAsync<T1, T2>(string procName,
            DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            var dataset = await Database.DataAdapterFillAsync(procName, parameters, CommandType.StoredProcedure,
                cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2>();
        }

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        public virtual async Task<(List<T1> list1, List<T2> list2, List<T3> list3)>
            SqlProcedureQueriesAsync<T1, T2, T3>(string procName, params DbParameter[] parameters)
        {
            var dataset = await Database.DataAdapterFillAsync(procName, parameters, CommandType.StoredProcedure);
            return dataset.ToList<T1, T2, T3>();
        }

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
        public virtual async Task<(List<T1> list1, List<T2> list2, List<T3> list3)>
            SqlProcedureQueriesAsync<T1, T2, T3>(string procName, DbParameter[] parameters,
                CancellationToken cancellationToken = default)
        {
            var dataset = await Database.DataAdapterFillAsync(procName, parameters, CommandType.StoredProcedure,
                cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3>();
        }

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
        public virtual async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4)>
            SqlProcedureQueriesAsync<T1, T2, T3, T4>(string procName, params DbParameter[] parameters)
        {
            var dataset = await Database.DataAdapterFillAsync(procName, parameters, CommandType.StoredProcedure);
            return dataset.ToList<T1, T2, T3, T4>();
        }

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
        public virtual async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4)>
            SqlProcedureQueriesAsync<T1, T2, T3, T4>(string procName, DbParameter[] parameters,
                CancellationToken cancellationToken = default)
        {
            var dataset = await Database.DataAdapterFillAsync(procName, parameters, CommandType.StoredProcedure,
                cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3, T4>();
        }

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
        public virtual async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)>
            SqlProcedureQueriesAsync<T1, T2, T3, T4, T5>(string procName, params DbParameter[] parameters)
        {
            var dataset = await Database.DataAdapterFillAsync(procName, parameters, CommandType.StoredProcedure);
            return dataset.ToList<T1, T2, T3, T4, T5>();
        }

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
        public virtual async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)>
            SqlProcedureQueriesAsync<T1, T2, T3, T4, T5>(string procName, DbParameter[] parameters,
                CancellationToken cancellationToken = default)
        {
            var dataset = await Database.DataAdapterFillAsync(procName, parameters, CommandType.StoredProcedure,
                cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3, T4, T5>();
        }

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
        public virtual async
            Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)>
            SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6>(string procName, params DbParameter[] parameters)
        {
            var dataset = await Database.DataAdapterFillAsync(procName, parameters, CommandType.StoredProcedure);
            return dataset.ToList<T1, T2, T3, T4, T5, T6>();
        }

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
        public virtual async
            Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)>
            SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6>(string procName, DbParameter[] parameters,
                CancellationToken cancellationToken = default)
        {
            var dataset = await Database.DataAdapterFillAsync(procName, parameters, CommandType.StoredProcedure,
                cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3, T4, T5, T6>();
        }

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
        public virtual async
            Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6,
                List<T7> list7)> SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6, T7>(string procName,
                params DbParameter[] parameters)
        {
            var dataset = await Database.DataAdapterFillAsync(procName, parameters, CommandType.StoredProcedure);
            return dataset.ToList<T1, T2, T3, T4, T5, T6, T7>();
        }

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
        public virtual async
            Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6,
                List<T7> list7)> SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6, T7>(string procName,
                DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            var dataset = await Database.DataAdapterFillAsync(procName, parameters, CommandType.StoredProcedure,
                cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3, T4, T5, T6, T7>();
        }

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
        public virtual async
            Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6,
                List<T7> list7, List<T8> list8)> SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6, T7, T8>(
                string procName, params DbParameter[] parameters)
        {
            var dataset = await Database.DataAdapterFillAsync(procName, parameters, CommandType.StoredProcedure);
            return dataset.ToList<T1, T2, T3, T4, T5, T6, T7, T8>();
        }

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
        public virtual async
            Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6,
                List<T7> list7, List<T8> list8)> SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6, T7, T8>(
                string procName, DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            var dataset = await Database.DataAdapterFillAsync(procName, parameters, CommandType.StoredProcedure,
                cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3, T4, T5, T6, T7, T8>();
        }

        /// <summary>
        ///  Execute the stored procedure to return List gather
        /// </summary>
        /// <typeparam name="T1">return type</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>List{T1}</returns>
        public virtual async Task<List<T1>> SqlProcedureQueriesAsync<T1>(string procName, object model,
            CancellationToken cancellationToken = default)
        {
            var (dataset, _) = await Database.DataAdapterFillAsync(procName, model, CommandType.StoredProcedure,
                cancellationToken: cancellationToken);
            return dataset.ToList<T1>();
        }

        /// <summary>
        /// Execute the stored procedure to return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        public virtual async Task<(List<T1> list1, List<T2> list2)> SqlProcedureQueriesAsync<T1, T2>(string procName,
            object model, CancellationToken cancellationToken = default)
        {
            var (dataset, _) = await Database.DataAdapterFillAsync(procName, model, CommandType.StoredProcedure,
                cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2>();
        }

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
        public virtual async Task<(List<T1> list1, List<T2> list2, List<T3> list3)>
            SqlProcedureQueriesAsync<T1, T2, T3>(string procName, object model,
                CancellationToken cancellationToken = default)
        {
            var (dataset, _) = await Database.DataAdapterFillAsync(procName, model, CommandType.StoredProcedure,
                cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3>();
        }

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
        public virtual async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4)>
            SqlProcedureQueriesAsync<T1, T2, T3, T4>(string procName, object model,
                CancellationToken cancellationToken = default)
        {
            var (dataset, _) = await Database.DataAdapterFillAsync(procName, model, CommandType.StoredProcedure,
                cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3, T4>();
        }

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
        public virtual async Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)>
            SqlProcedureQueriesAsync<T1, T2, T3, T4, T5>(string procName, object model,
                CancellationToken cancellationToken = default)
        {
            var (dataset, _) = await Database.DataAdapterFillAsync(procName, model, CommandType.StoredProcedure,
                cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3, T4, T5>();
        }

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
        public virtual async
            Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)>
            SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6>(string procName, object model,
                CancellationToken cancellationToken = default)
        {
            var (dataset, _) = await Database.DataAdapterFillAsync(procName, model, CommandType.StoredProcedure,
                cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3, T4, T5, T6>();
        }

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
        public virtual async
            Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6,
                List<T7> list7)> SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6, T7>(string procName, object model,
                CancellationToken cancellationToken = default)
        {
            var (dataset, _) = await Database.DataAdapterFillAsync(procName, model, CommandType.StoredProcedure,
                cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3, T4, T5, T6, T7>();
        }

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
        public virtual async
            Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6,
                List<T7> list7, List<T8> list8)> SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6, T7, T8>(
                string procName, object model, CancellationToken cancellationToken = default)
        {
            var (dataset, _) = await Database.DataAdapterFillAsync(procName, model, CommandType.StoredProcedure,
                cancellationToken: cancellationToken);
            return dataset.ToList<T1, T2, T3, T4, T5, T6, T7, T8>();
        }

        /// <summary>
        /// Execute the stored procedure to return single row single column
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>object</returns>
        public virtual object SqlProcedureScalar(string procName, params DbParameter[] parameters)
        {
            return Database.ExecuteScalar(procName, parameters, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Execute the stored procedure to return single row single column
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <returns>object</returns>
        public virtual object SqlProcedureScalar(string procName, object model)
        {
            return Database.ExecuteScalar(procName, model, CommandType.StoredProcedure).result;
        }

        /// <summary>
        /// Execute the stored procedure to return single row single column
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>object</returns>
        public virtual Task<object> SqlProcedureScalarAsync(string procName, params DbParameter[] parameters)
        {
            return Database.ExecuteScalarAsync(procName, parameters, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Execute the stored procedure to return single row single column
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>object</returns>
        public virtual Task<object> SqlProcedureScalarAsync(string procName, DbParameter[] parameters,
            CancellationToken cancellationToken = default)
        {
            return Database.ExecuteScalarAsync(procName, parameters, CommandType.StoredProcedure,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Execute the stored procedure to return single row single column
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>object</returns>
        public virtual async Task<object> SqlProcedureScalarAsync(string procName, object model,
            CancellationToken cancellationToken = default)
        {
            var (result, _) = await Database.ExecuteScalarAsync(procName, model, CommandType.StoredProcedure,
                cancellationToken: cancellationToken);
            return result;
        }

        /// <summary>
        /// Execute the stored procedure to return single row single column
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>TResult</returns>
        public virtual TResult SqlProcedureScalar<TResult>(string procName, params DbParameter[] parameters)
        {
            return Database.ExecuteScalar(procName, parameters, CommandType.StoredProcedure).ChangeType<TResult>();
        }

        /// <summary>
        /// Execute the stored procedure to return single row single column
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <returns>TResult</returns>
        public virtual TResult SqlProcedureScalar<TResult>(string procName, object model)
        {
            return Database.ExecuteScalar(procName, model, CommandType.StoredProcedure).result.ChangeType<TResult>();
        }

        /// <summary>
        /// Execute the stored procedure to return single row single column
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>TResult</returns>
        public virtual async Task<TResult> SqlProcedureScalarAsync<TResult>(string procName,
            params DbParameter[] parameters)
        {
            var result = await Database.ExecuteScalarAsync(procName, parameters, CommandType.StoredProcedure);
            return result.ChangeType<TResult>();
        }

        /// <summary>
        /// Execute the stored procedure to return single row single column
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>TResult</returns>
        public virtual async Task<TResult> SqlProcedureScalarAsync<TResult>(string procName, DbParameter[] parameters,
            CancellationToken cancellationToken = default)
        {
            var result = await Database.ExecuteScalarAsync(procName, parameters, CommandType.StoredProcedure,
                cancellationToken: cancellationToken);
            return result.ChangeType<TResult>();
        }

        /// <summary>
        /// Execute the stored procedure to return single row single column
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>TResult</returns>
        public virtual async Task<TResult> SqlProcedureScalarAsync<TResult>(string procName, object model,
            CancellationToken cancellationToken = default)
        {
            var (result, _) = await Database.ExecuteScalarAsync(procName, model, CommandType.StoredProcedure,
                cancellationToken: cancellationToken);
            return result.ChangeType<TResult>();
        }

        /// <summary>
        /// Execute the stored procedure with no data returned
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>int</returns>
        public virtual int SqlProcedureNonQuery(string procName, params DbParameter[] parameters)
        {
            return Database.ExecuteNonQuery(procName, parameters, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Execute the stored procedure with no data returned
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <returns>int</returns>
        public virtual int SqlProcedureNonQuery(string procName, object model)
        {
            return Database.ExecuteNonQuery(procName, model, CommandType.StoredProcedure).rowEffects;
        }

        /// <summary>
        /// Execute the stored procedure with no data returned
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>int</returns>
        public virtual Task<int> SqlProcedureNonQueryAsync(string procName, params DbParameter[] parameters)
        {
            return Database.ExecuteNonQueryAsync(procName, parameters, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Execute the stored procedure with no data returned
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>int</returns>
        public virtual Task<int> SqlProcedureNonQueryAsync(string procName, DbParameter[] parameters,
            CancellationToken cancellationToken = default)
        {
            return Database.ExecuteNonQueryAsync(procName, parameters, CommandType.StoredProcedure,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Execute the stored procedure with no data returned
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>int</returns>
        public virtual async Task<int> SqlProcedureNonQueryAsync(string procName, object model,
            CancellationToken cancellationToken = default)
        {
            var (rowEffects, _) = await Database.ExecuteNonQueryAsync(procName, model, CommandType.StoredProcedure,
                cancellationToken: cancellationToken);
            return rowEffects;
        }

        /// <summary>
        /// implement Sql no data returned
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>int</returns>
        public virtual int SqlNonQuery(string sql, params DbParameter[] parameters)
        {
            return Database.ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// implement Sql no data returned
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <returns>int</returns>
        public virtual int SqlNonQuery(string sql, object model)
        {
            return Database.ExecuteNonQuery(sql, model).rowEffects;
        }

        /// <summary>
        /// implement Sql no data returned
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>int</returns>
        public virtual Task<int> SqlNonQueryAsync(string sql, params DbParameter[] parameters)
        {
            return Database.ExecuteNonQueryAsync(sql, parameters);
        }

        /// <summary>
        /// implement Sql no data returned
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>int</returns>
        public virtual Task<int> SqlNonQueryAsync(string sql, DbParameter[] parameters,
            CancellationToken cancellationToken = default)
        {
            return Database.ExecuteNonQueryAsync(sql, parameters, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// implement Sql no data returned
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>int</returns>
        public virtual async Task<int> SqlNonQueryAsync(string sql, object model,
            CancellationToken cancellationToken = default)
        {
            var (rowEffects, _) = await Database.ExecuteNonQueryAsync(sql, model, cancellationToken: cancellationToken);
            return rowEffects;
        }

        /// <summary>
        /// implement Sql return single row single column
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>object</returns>
        public virtual object SqlScalar(string sql, params DbParameter[] parameters)
        {
            return Database.ExecuteScalar(sql, parameters);
        }

        /// <summary>
        /// implement Sql return single row single column
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <returns>object</returns>
        public virtual object SqlScalar(string sql, object model)
        {
            return Database.ExecuteScalar(sql, model).result;
        }

        /// <summary>
        /// implement Sql return single row single column
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>object</returns>
        public virtual Task<object> SqlScalarAsync(string sql, params DbParameter[] parameters)
        {
            return Database.ExecuteScalarAsync(sql, parameters);
        }

        /// <summary>
        /// implement Sql return single row single column
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>object</returns>
        public virtual Task<object> SqlScalarAsync(string sql, DbParameter[] parameters,
            CancellationToken cancellationToken = default)
        {
            return Database.ExecuteScalarAsync(sql, parameters, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// implement Sql return single row single column
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>object</returns>
        public virtual async Task<object> SqlScalarAsync(string sql, object model,
            CancellationToken cancellationToken = default)
        {
            var (result, _) = await Database.ExecuteScalarAsync(sql, model, cancellationToken: cancellationToken);
            return result;
        }

        /// <summary>
        /// implement Sql return single row single column
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>TResult</returns>
        public virtual TResult SqlScalar<TResult>(string sql, params DbParameter[] parameters)
        {
            return Database.ExecuteScalar(sql, parameters).ChangeType<TResult>();
        }

        /// <summary>
        /// implement Sql return single row single column
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <returns>TResult</returns>
        public virtual TResult SqlScalar<TResult>(string sql, object model)
        {
            return Database.ExecuteScalar(sql, model).result.ChangeType<TResult>();
        }

        /// <summary>
        /// implement Sql return single row single column
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>TResult</returns>
        public virtual async Task<TResult> SqlScalarAsync<TResult>(string sql, params DbParameter[] parameters)
        {
            var result = await Database.ExecuteScalarAsync(sql, parameters);
            return result.ChangeType<TResult>();
        }

        /// <summary>
        /// implement Sql return single row single column
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>TResult</returns>
        public virtual async Task<TResult> SqlScalarAsync<TResult>(string sql, DbParameter[] parameters,
            CancellationToken cancellationToken = default)
        {
            var result = await Database.ExecuteScalarAsync(sql, parameters, cancellationToken: cancellationToken);
            return result.ChangeType<TResult>();
        }

        /// <summary>
        /// implement Sql return single row single column
        /// </summary>
        /// <param name="sql">sql statement</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>TResult</returns>
        public virtual async Task<TResult> SqlScalarAsync<TResult>(string sql, object model,
            CancellationToken cancellationToken = default)
        {
            var (result, _) = await Database.ExecuteScalarAsync(sql, model, cancellationToken: cancellationToken);
            return result.ChangeType<TResult>();
        }

        /// <summary>
        /// Execute the stored procedure to returnOUPUT、RETURN、result set
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>ProcedureOutput</returns>
        public virtual ProcedureOutputResult SqlProcedureOutput(string procName, DbParameter[] parameters)
        {
            parameters ??= Array.Empty<DbParameter>();

            // implement存储过程
            var dataSet = Database.DataAdapterFill(procName, parameters, CommandType.StoredProcedure);

            // 包装result set
            return DbHelpers.WrapperProcedureOutput(Database.ProviderName, parameters, dataSet);
        }

        /// <summary>
        /// Execute the stored procedure to returnOUPUT、RETURN、result set
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>ProcedureOutput</returns>
        public virtual async Task<ProcedureOutputResult> SqlProcedureOutputAsync(string procName,
            DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            parameters ??= Array.Empty<DbParameter>();

            // implement存储过程
            var dataSet = await Database.DataAdapterFillAsync(procName, parameters, CommandType.StoredProcedure,
                cancellationToken: cancellationToken);

            // 包装result set
            return DbHelpers.WrapperProcedureOutput(Database.ProviderName, parameters, dataSet);
        }

        /// <summary>
        /// Execute the stored procedure to returnOUPUT、RETURN、result set
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">command model</param>
        /// <returns>ProcedureOutput</returns>
        public virtual ProcedureOutputResult SqlProcedureOutput(string procName, object model)
        {
            // implement存储过程
            var (dataSet, parameters) = Database.DataAdapterFill(procName, model, CommandType.StoredProcedure);

            // 包装result set
            return DbHelpers.WrapperProcedureOutput(Database.ProviderName, parameters, dataSet);
        }

        /// <summary>
        /// Execute the stored procedure to returnOUPUT、RETURN、result set
        /// </summary>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">command model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>ProcedureOutput</returns>
        public virtual async Task<ProcedureOutputResult> SqlProcedureOutputAsync(string procName, object model,
            CancellationToken cancellationToken = default)
        {
            // implement存储过程
            var (dataSet, parameters) = await Database.DataAdapterFillAsync(procName, model,
                CommandType.StoredProcedure, cancellationToken: cancellationToken);

            // 包装result set
            return DbHelpers.WrapperProcedureOutput(Database.ProviderName, parameters, dataSet);
        }

        /// <summary>
        /// Execute the stored procedure to returnOUPUT、RETURN、result set
        /// </summary>
        /// <typeparam name="TResult">dataset results</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>ProcedureOutput</returns>
        public virtual ProcedureOutputResult<TResult> SqlProcedureOutput<TResult>(string procName,
            DbParameter[] parameters)
        {
            parameters ??= Array.Empty<DbParameter>();

            // implement存储过程
            var dataSet = Database.DataAdapterFill(procName, parameters, CommandType.StoredProcedure);

            // 包装result set
            return DbHelpers.WrapperProcedureOutput<TResult>(Database.ProviderName, parameters, dataSet);
        }

        /// <summary>
        /// Execute the stored procedure to returnOUPUT、RETURN、result set
        /// </summary>
        /// <typeparam name="TResult">dataset results</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>ProcedureOutput</returns>
        public virtual async Task<ProcedureOutputResult<TResult>> SqlProcedureOutputAsync<TResult>(string procName,
            DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            parameters ??= Array.Empty<DbParameter>();

            // implement存储过程
            var dataSet = await Database.DataAdapterFillAsync(procName, parameters, CommandType.StoredProcedure,
                cancellationToken: cancellationToken);

            // 包装result set
            return DbHelpers.WrapperProcedureOutput<TResult>(Database.ProviderName, parameters, dataSet);
        }

        /// <summary>
        /// Execute the stored procedure to returnOUPUT、RETURN、result set
        /// </summary>
        /// <typeparam name="TResult">dataset results</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">command model</param>
        /// <returns>ProcedureOutput</returns>
        public virtual ProcedureOutputResult<TResult> SqlProcedureOutput<TResult>(string procName, object model)
        {
            // implement存储过程
            var (dataSet, parameters) = Database.DataAdapterFill(procName, model, CommandType.StoredProcedure);

            // 包装result set
            return DbHelpers.WrapperProcedureOutput<TResult>(Database.ProviderName, parameters, dataSet);
        }

        /// <summary>
        /// Execute the stored procedure to returnOUPUT、RETURN、result set
        /// </summary>
        /// <typeparam name="TResult">dataset results</typeparam>
        /// <param name="procName">stored procedure name</param>
        /// <param name="model">command model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>ProcedureOutput</returns>
        public virtual async Task<ProcedureOutputResult<TResult>> SqlProcedureOutputAsync<TResult>(string procName,
            object model, CancellationToken cancellationToken = default)
        {
            // implement存储过程
            var (dataSet, parameters) = await Database.DataAdapterFillAsync(procName, model,
                CommandType.StoredProcedure, cancellationToken: cancellationToken);

            // 包装result set
            return DbHelpers.WrapperProcedureOutput<TResult>(Database.ProviderName, parameters, dataSet);
        }

        /// <summary>
        /// implement标量函数return single row single column
        /// </summary>
        /// <param name="funcName">Function name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>object</returns>
        public virtual object SqlFunctionScalar(string funcName, params DbParameter[] parameters)
        {
            var sql = DbHelpers.GenerateFunctionSql(Database.ProviderName, DbFunctionType.Scalar, funcName, parameters);
            return Database.ExecuteScalar(sql, parameters);
        }

        /// <summary>
        /// implement标量函数return single row single column
        /// </summary>
        /// <param name="funcName">Function name</param>
        /// <param name="model">parametric model</param>
        /// <returns>object</returns>
        public virtual object SqlFunctionScalar(string funcName, object model)
        {
            var sql = DbHelpers.GenerateFunctionSql(Database.ProviderName, DbFunctionType.Scalar, funcName, model);
            return Database.ExecuteScalar(sql, model).result;
        }

        /// <summary>
        /// implement标量函数return single row single column
        /// </summary>
        /// <param name="funcName">Function name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>object</returns>
        public virtual Task<object> SqlFunctionScalarAsync(string funcName, params DbParameter[] parameters)
        {
            var sql = DbHelpers.GenerateFunctionSql(Database.ProviderName, DbFunctionType.Scalar, funcName, parameters);
            return Database.ExecuteScalarAsync(sql, parameters);
        }

        /// <summary>
        /// implement标量函数return single row single column
        /// </summary>
        /// <param name="funcName">Function name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>object</returns>
        public virtual Task<object> SqlFunctionScalarAsync(string funcName, DbParameter[] parameters,
            CancellationToken cancellationToken = default)
        {
            var sql = DbHelpers.GenerateFunctionSql(Database.ProviderName, DbFunctionType.Scalar, funcName, parameters);
            return Database.ExecuteScalarAsync(sql, parameters, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// implement标量函数return single row single column
        /// </summary>
        /// <param name="funcName">Function name</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>object</returns>
        public virtual async Task<object> SqlFunctionScalarAsync(string funcName, object model,
            CancellationToken cancellationToken = default)
        {
            var sql = DbHelpers.GenerateFunctionSql(Database.ProviderName, DbFunctionType.Scalar, funcName, model);
            var (result, _) = await Database.ExecuteScalarAsync(sql, model, cancellationToken: cancellationToken);
            return result;
        }

        /// <summary>
        /// implement标量函数return single row single column
        /// </summary>
        /// <typeparam name="TResult">return值类型</typeparam>
        /// <param name="funcName">Function name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>TResult</returns>
        public virtual TResult SqlFunctionScalar<TResult>(string funcName, params DbParameter[] parameters)
        {
            var sql = DbHelpers.GenerateFunctionSql(Database.ProviderName, DbFunctionType.Scalar, funcName, parameters);
            return Database.ExecuteScalar(sql, parameters).ChangeType<TResult>();
        }

        /// <summary>
        /// implement标量函数return single row single column
        /// </summary>
        /// <typeparam name="TResult">return值类型</typeparam>
        /// <param name="funcName">Function name</param>
        /// <param name="model">parametric model</param>
        /// <returns>TResult</returns>
        public virtual TResult SqlFunctionScalar<TResult>(string funcName, object model)
        {
            var sql = DbHelpers.GenerateFunctionSql(Database.ProviderName, DbFunctionType.Scalar, funcName, model);
            return Database.ExecuteScalar(sql, model).result.ChangeType<TResult>();
        }

        /// <summary>
        /// implement标量函数return single row single column
        /// </summary>
        /// <typeparam name="TResult">return值类型</typeparam>
        /// <param name="funcName">Function name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>TResult</returns>
        public virtual async Task<TResult> SqlFunctionScalarAsync<TResult>(string funcName,
            params DbParameter[] parameters)
        {
            var sql = DbHelpers.GenerateFunctionSql(Database.ProviderName, DbFunctionType.Scalar, funcName, parameters);
            var result = await Database.ExecuteScalarAsync(sql, parameters);
            return result.ChangeType<TResult>();
        }

        /// <summary>
        /// implement标量函数return single row single column
        /// </summary>
        /// <typeparam name="TResult">return值类型</typeparam>
        /// <param name="funcName">Function name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>TResult</returns>
        public virtual async Task<TResult> SqlFunctionScalarAsync<TResult>(string funcName, DbParameter[] parameters,
            CancellationToken cancellationToken = default)
        {
            var sql = DbHelpers.GenerateFunctionSql(Database.ProviderName, DbFunctionType.Scalar, funcName, parameters);
            var result = await Database.ExecuteScalarAsync(sql, parameters, cancellationToken: cancellationToken);
            return result.ChangeType<TResult>();
        }

        /// <summary>
        /// implement标量函数return single row single column
        /// </summary>
        /// <typeparam name="TResult">return值类型</typeparam>
        /// <param name="funcName">Function name</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>object</returns>
        public virtual async Task<TResult> SqlFunctionScalarAsync<TResult>(string funcName, object model,
            CancellationToken cancellationToken = default)
        {
            var sql = DbHelpers.GenerateFunctionSql(Database.ProviderName, DbFunctionType.Scalar, funcName, model);
            var (result, _) = await Database.ExecuteScalarAsync(sql, model, cancellationToken: cancellationToken);
            return result.ChangeType<TResult>();
        }

        /// <summary>
        /// implement表值函数return DataTable
        /// </summary>
        /// <param name="funcName">Function name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>DataTable</returns>
        public virtual DataTable SqlFunctionQuery(string funcName, params DbParameter[] parameters)
        {
            var sql = DbHelpers.GenerateFunctionSql(Database.ProviderName, DbFunctionType.Table, funcName, parameters);
            return Database.ExecuteReader(sql, parameters);
        }

        /// <summary>
        /// implement表值函数return DataTable
        /// </summary>
        /// <param name="funcName">Function name</param>
        /// <param name="model">parametric model</param>
        /// <returns>DataTable</returns>
        public virtual DataTable SqlFunctionQuery(string funcName, object model)
        {
            var sql = DbHelpers.GenerateFunctionSql(Database.ProviderName, DbFunctionType.Table, funcName, model);
            return Database.ExecuteReader(sql, model).dataTable;
        }

        /// <summary>
        /// implement表值函数return DataTable
        /// </summary>
        /// <param name="funcName">Function name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>Task{DataTable}</returns>
        public virtual Task<DataTable> SqlFunctionQueryAsync(string funcName, params DbParameter[] parameters)
        {
            var sql = DbHelpers.GenerateFunctionSql(Database.ProviderName, DbFunctionType.Table, funcName, parameters);
            return Database.ExecuteReaderAsync(sql, parameters);
        }

        /// <summary>
        /// implement表值函数return DataTable
        /// </summary>
        /// <param name="funcName">Function name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>Task{DataTable}</returns>
        public virtual Task<DataTable> SqlFunctionQueryAsync(string funcName, DbParameter[] parameters,
            CancellationToken cancellationToken = default)
        {
            var sql = DbHelpers.GenerateFunctionSql(Database.ProviderName, DbFunctionType.Table, funcName, parameters);
            return Database.ExecuteReaderAsync(sql, parameters, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// implement表值函数return DataTable
        /// </summary>
        /// <param name="funcName">Function name</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>Task{DataTable}</returns>
        public virtual async Task<DataTable> SqlFunctionQueryAsync(string funcName, object model,
            CancellationToken cancellationToken = default)
        {
            var sql = DbHelpers.GenerateFunctionSql(Database.ProviderName, DbFunctionType.Table, funcName, model);
            var (dataTable, _) = await Database.ExecuteReaderAsync(sql, model, cancellationToken: cancellationToken);
            return dataTable;
        }

        /// <summary>
        /// implement表值函数return List gather
        /// </summary>
        /// <typeparam name="T">return type</typeparam>
        /// <param name="funcName">Function name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>List{T}</returns>
        public virtual List<T> SqlFunctionQuery<T>(string funcName, params DbParameter[] parameters)
        {
            var sql = DbHelpers.GenerateFunctionSql(Database.ProviderName, DbFunctionType.Table, funcName, parameters);
            return Database.ExecuteReader(sql, parameters).ToList<T>();
        }

        /// <summary>
        /// implement表值函数return List gather
        /// </summary>
        /// <typeparam name="T">return type</typeparam>
        /// <param name="funcName">Function name</param>
        /// <param name="model">parametric model</param>
        /// <returns>List{T}</returns>
        public virtual List<T> SqlFunctionQuery<T>(string funcName, object model)
        {
            var sql = DbHelpers.GenerateFunctionSql(Database.ProviderName, DbFunctionType.Table, funcName, model);
            return Database.ExecuteReader(sql, model).dataTable.ToList<T>();
        }

        /// <summary>
        /// implement表值函数return List gather
        /// </summary>
        /// <typeparam name="T">return type</typeparam>
        /// <param name="funcName">Function name</param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>Task{List{T}}</returns>
        public virtual async Task<List<T>> SqlFunctionQueryAsync<T>(string funcName, params DbParameter[] parameters)
        {
            var sql = DbHelpers.GenerateFunctionSql(Database.ProviderName, DbFunctionType.Table, funcName, parameters);
            var dataTable = await Database.ExecuteReaderAsync(sql, parameters);
            return dataTable.ToList<T>();
        }

        /// <summary>
        /// implement表值函数return List gather
        /// </summary>
        /// <typeparam name="T">return type</typeparam>
        /// <param name="funcName">Function name</param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>Task{List{T}}</returns>
        public virtual async Task<List<T>> SqlFunctionQueryAsync<T>(string funcName, DbParameter[] parameters,
            CancellationToken cancellationToken = default)
        {
            var sql = DbHelpers.GenerateFunctionSql(Database.ProviderName, DbFunctionType.Table, funcName, parameters);
            var dataTable = await Database.ExecuteReaderAsync(sql, parameters, cancellationToken: cancellationToken);
            return dataTable.ToList<T>();
        }

        /// <summary>
        /// implement表值函数return List gather
        /// </summary>
        /// <typeparam name="T">return type</typeparam>
        /// <param name="funcName">Function name</param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>Task{List{T}}</returns>
        public virtual async Task<List<T>> SqlFunctionQueryAsync<T>(string funcName, object model,
            CancellationToken cancellationToken = default)
        {
            var sql = DbHelpers.GenerateFunctionSql(Database.ProviderName, DbFunctionType.Table, funcName, model);
            var (dataTable, _) = await Database.ExecuteReaderAsync(sql, model, cancellationToken: cancellationToken);
            return dataTable.ToList<T>();
        }
    }
}