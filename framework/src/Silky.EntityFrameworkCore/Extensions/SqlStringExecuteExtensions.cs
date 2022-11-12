using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Silky.EntityFrameworkCore.Locators;
using Silky.EntityFrameworkCore.Values;

namespace Silky.EntityFrameworkCore.Extensions
{
    /// <summary>
    /// Sql String execution extension class
    /// </summary>
    public static class SqlStringExecuteExtensions
    {
        /// <summary>
        /// switch database
        /// </summary>
        /// <typeparam name="TDbContextLocator"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static SqlStringExecutePart Change<TDbContextLocator>(this string sql)
            where TDbContextLocator : class, IDbContextLocator
        {
            return new SqlStringExecutePart().SetSqlString(sql).Change<TDbContextLocator>();
        }

        /// <summary>
        /// switch database
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public static SqlStringExecutePart Change(this string sql, Type dbContextLocator)
        {
            return new SqlStringExecutePart().SetSqlString(sql).Change(dbContextLocator);
        }

        /// <summary>
        /// set up ADO.NET overtime time
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="timeout">unit second</param>
        /// <returns></returns>
        public static SqlStringExecutePart SetCommandTimeout(this string sql, int timeout)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SetCommandTimeout(timeout);
        }

        /// <summary>
        /// Sql query returns DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>DataTable</returns>
        public static DataTable SqlQuery(this string sql, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQuery(parameters);
        }

        /// <summary>
        /// Sql query returns DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="model">parametric model</param>
        /// <returns>DataTable</returns>
        public static DataTable SqlQuery(this string sql, object model)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQuery(model);
        }

        /// <summary>
        /// Sql query returns DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>Task{DataTable}</returns>
        public static Task<DataTable> SqlQueryAsync(this string sql, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueryAsync(parameters);
        }

        /// <summary>
        /// Sql query returns DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>Task{DataTable}</returns>
        public static Task<DataTable> SqlQueryAsync(this string sql, DbParameter[] parameters,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueryAsync(parameters, cancellationToken);
        }

        /// <summary>
        /// Sql query returns DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>Task{DataTable}</returns>
        public static Task<DataTable> SqlQueryAsync(this string sql, object model,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueryAsync(model, cancellationToken);
        }

        /// <summary>
        /// Sql query returns List gather
        /// </summary>
        /// <param name="sql"></param>
        /// <typeparam name="T">return type</typeparam>
        /// <param name="parameters">Command parameters</param>
        /// <returns>List{T}</returns>
        public static List<T> SqlQuery<T>(this string sql, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQuery<T>(parameters);
        }

        /// <summary>
        /// Sql query returns List gather
        /// </summary>
        /// <param name="sql"></param>
        /// <typeparam name="T">return type</typeparam>
        /// <param name="model">parametric model</param>
        /// <returns>List{T}</returns>
        public static List<T> SqlQuery<T>(this string sql, object model)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQuery<T>(model);
        }

        /// <summary>
        /// Sql query returns List gather
        /// </summary>
        /// <typeparam name="T">return type</typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>Task{List{T}}</returns>
        public static Task<List<T>> SqlQueryAsync<T>(this string sql, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueryAsync<T>(parameters);
        }

        /// <summary>
        /// Sql query returns List gather
        /// </summary>
        /// <typeparam name="T">return type</typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>Task{List{T}}</returns>
        public static Task<List<T>> SqlQueryAsync<T>(this string sql, DbParameter[] parameters,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueryAsync<T>(parameters, cancellationToken);
        }

        /// <summary>
        /// Sql query returns List gather
        /// </summary>
        /// <typeparam name="T">return type</typeparam>
        /// <param name="sql"></param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>Task{List{T}}</returns>
        public static Task<List<T>> SqlQueryAsync<T>(this string sql, object model,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueryAsync<T>(model, cancellationToken);
        }

        /// <summary>
        ///  Sql query returns DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>DataSet</returns>
        public static DataSet SqlQueries(this string sql, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueries(parameters);
        }

        /// <summary>
        ///  Sql query returns DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="model">parametric model</param>
        /// <returns>DataSet</returns>
        public static DataSet SqlQueries(this string sql, object model)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueries(model);
        }

        /// <summary>
        ///  Sql query returns DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>Task{DataSet}</returns>
        public static Task<DataSet> SqlQueriesAsync(this string sql, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueriesAsync(parameters);
        }

        /// <summary>
        ///  Sql query returns DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>Task{DataSet}</returns>
        public static Task<DataSet> SqlQueriesAsync(this string sql, DbParameter[] parameters,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueriesAsync(parameters, cancellationToken);
        }

        /// <summary>
        ///  Sql query returns DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>Task{DataSet}</returns>
        public static Task<DataSet> SqlQueriesAsync(this string sql, object model,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueriesAsync(model, cancellationToken);
        }

        /// <summary>
        ///  Sql query returns List gather
        /// </summary>
        /// <typeparam name="T1">return type</typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>List{T1}</returns>
        public static List<T1> SqlQueries<T1>(this string sql, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueries<T1>(parameters);
        }

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        public static (List<T1> list1, List<T2> list2) SqlQueries<T1, T2>(this string sql,
            params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueries<T1, T2>(parameters);
        }

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3) SqlQueries<T1, T2, T3>(this string sql,
            params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueries<T1, T2, T3>(parameters);
        }

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4) SqlQueries<T1, T2, T3, T4>(
            this string sql, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueries<T1, T2, T3, T4>(parameters);
        }

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)
            SqlQueries<T1, T2, T3, T4, T5>(this string sql, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueries<T1, T2, T3, T4, T5>(parameters);
        }

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)
            SqlQueries<T1, T2, T3, T4, T5, T6>(this string sql, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueries<T1, T2, T3, T4, T5, T6>(parameters);
        }

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
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6,
            List<T7> list7) SqlQueries<T1, T2, T3, T4, T5, T6, T7>(this string sql, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueries<T1, T2, T3, T4, T5, T6, T7>(parameters);
        }

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
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6,
            List<T7> list7, List<T8> list8) SqlQueries<T1, T2, T3, T4, T5, T6, T7, T8>(this string sql,
                params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueries<T1, T2, T3, T4, T5, T6, T7, T8>(parameters);
        }

        /// <summary>
        ///  Sql query returns List gather
        /// </summary>
        /// <typeparam name="T1">return type</typeparam>
        /// <param name="sql"></param>
        /// <param name="model">parametric model</param>
        /// <returns>List{T1}</returns>
        public static List<T1> SqlQueries<T1>(this string sql, object model)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueries<T1>(model);
        }

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="model">parametric model</param>
        /// <returns>tuple类型</returns>
        public static (List<T1> list1, List<T2> list2) SqlQueries<T1, T2>(this string sql, object model)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueries<T1, T2>(model);
        }

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="model">parametric model</param>
        /// <returns>tuple类型</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3) SqlQueries<T1, T2, T3>(this string sql,
            object model)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueries<T1, T2, T3>(model);
        }

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="model">parametric model</param>
        /// <returns>tuple类型</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4) SqlQueries<T1, T2, T3, T4>(
            this string sql, object model)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueries<T1, T2, T3, T4>(model);
        }

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="model">parametric model</param>
        /// <returns>tuple类型</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5) SqlQueries<T1,
            T2, T3, T4, T5>(this string sql, object model)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueries<T1, T2, T3, T4, T5>(model);
        }

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="model">parametric model</param>
        /// <returns>tuple类型</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)
            SqlQueries<T1, T2, T3, T4, T5, T6>(this string sql, object model)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueries<T1, T2, T3, T4, T5, T6>(model);
        }

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
        /// <param name="sql"></param>
        /// <param name="model">parametric model</param>
        /// <returns>tuple类型</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6,
            List<T7> list7) SqlQueries<T1, T2, T3, T4, T5, T6, T7>(this string sql, object model)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueries<T1, T2, T3, T4, T5, T6, T7>(model);
        }

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
        /// <param name="sql"></param>
        /// <param name="model">parametric model</param>
        /// <returns>tuple类型</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6,
            List<T7> list7, List<T8> list8) SqlQueries<T1, T2, T3, T4, T5, T6, T7, T8>(this string sql, object model)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueries<T1, T2, T3, T4, T5, T6, T7, T8>(model);
        }

        /// <summary>
        ///  Sql query returns List gather
        /// </summary>
        /// <typeparam name="T1">return type</typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>Task{List{T1}}</returns>
        public static Task<List<T1>> SqlQueriesAsync<T1>(this string sql, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueriesAsync<T1>(parameters);
        }

        /// <summary>
        ///  Sql query returns List gather
        /// </summary>
        /// <typeparam name="T1">return type</typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>Task{List{T1}}</returns>
        public static Task<List<T1>> SqlQueriesAsync<T1>(this string sql, DbParameter[] parameters,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueriesAsync<T1>(parameters, cancellationToken);
        }

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        public static Task<(List<T1> list1, List<T2> list2)> SqlQueriesAsync<T1, T2>(this string sql,
            params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueriesAsync<T1, T2>(parameters);
        }

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        public static Task<(List<T1> list1, List<T2> list2)> SqlQueriesAsync<T1, T2>(this string sql,
            DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueriesAsync<T1, T2>(parameters, cancellationToken);
        }

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        public static Task<(List<T1> list1, List<T2> list2, List<T3> list3)> SqlQueriesAsync<T1, T2, T3>(
            this string sql, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueriesAsync<T1, T2, T3>(parameters);
        }

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        public static Task<(List<T1> list1, List<T2> list2, List<T3> list3)> SqlQueriesAsync<T1, T2, T3>(
            this string sql, DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(sql)
                .SqlQueriesAsync<T1, T2, T3>(parameters, cancellationToken);
        }

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        public static Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4)>
            SqlQueriesAsync<T1, T2, T3, T4>(this string sql, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueriesAsync<T1, T2, T3, T4>(parameters);
        }

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        public static Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4)>
            SqlQueriesAsync<T1, T2, T3, T4>(this string sql, DbParameter[] parameters,
                CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(sql)
                .SqlQueriesAsync<T1, T2, T3, T4>(parameters, cancellationToken);
        }

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        public static Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)>
            SqlQueriesAsync<T1, T2, T3, T4, T5>(this string sql, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueriesAsync<T1, T2, T3, T4, T5>(parameters);
        }

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        public static Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)>
            SqlQueriesAsync<T1, T2, T3, T4, T5>(this string sql, DbParameter[] parameters,
                CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(sql)
                .SqlQueriesAsync<T1, T2, T3, T4, T5>(parameters, cancellationToken);
        }

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        public static
            Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)>
            SqlQueriesAsync<T1, T2, T3, T4, T5, T6>(this string sql, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueriesAsync<T1, T2, T3, T4, T5, T6>(parameters);
        }

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        public static
            Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)>
            SqlQueriesAsync<T1, T2, T3, T4, T5, T6>(this string sql, DbParameter[] parameters,
                CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(sql)
                .SqlQueriesAsync<T1, T2, T3, T4, T5, T6>(parameters, cancellationToken);
        }

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
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        public static
            Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6,
                List<T7> list7)> SqlQueriesAsync<T1, T2, T3, T4, T5, T6, T7>(this string sql,
                params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueriesAsync<T1, T2, T3, T4, T5, T6, T7>(parameters);
        }

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
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        public static
            Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6,
                List<T7> list7)> SqlQueriesAsync<T1, T2, T3, T4, T5, T6, T7>(this string sql, DbParameter[] parameters,
                CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(sql)
                .SqlQueriesAsync<T1, T2, T3, T4, T5, T6, T7>(parameters, cancellationToken);
        }

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
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        public static
            Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6,
                List<T7> list7, List<T8> list8)> SqlQueriesAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this string sql,
                params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(sql)
                .SqlQueriesAsync<T1, T2, T3, T4, T5, T6, T7, T8>(parameters);
        }

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
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        public static
            Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6,
                List<T7> list7, List<T8> list8)> SqlQueriesAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this string sql,
                DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(sql)
                .SqlQueriesAsync<T1, T2, T3, T4, T5, T6, T7, T8>(parameters, cancellationToken);
        }

        /// <summary>
        ///  Sql query returns List gather
        /// </summary>
        /// <typeparam name="T1">return type</typeparam>
        /// <param name="sql"></param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>List{T1}</returns>
        public static Task<List<T1>> SqlQueriesAsync<T1>(this string sql, object model,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueriesAsync<T1>(model, cancellationToken);
        }

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        public static Task<(List<T1> list1, List<T2> list2)> SqlQueriesAsync<T1, T2>(this string sql, object model,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueriesAsync<T1, T2>(model, cancellationToken);
        }

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        public static Task<(List<T1> list1, List<T2> list2, List<T3> list3)> SqlQueriesAsync<T1, T2, T3>(
            this string sql, object model, CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlQueriesAsync<T1, T2, T3>(model, cancellationToken);
        }

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        public static Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4)>
            SqlQueriesAsync<T1, T2, T3, T4>(this string sql, object model,
                CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(sql)
                .SqlQueriesAsync<T1, T2, T3, T4>(model, cancellationToken);
        }

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        public static Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)>
            SqlQueriesAsync<T1, T2, T3, T4, T5>(this string sql, object model,
                CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(sql)
                .SqlQueriesAsync<T1, T2, T3, T4, T5>(model, cancellationToken);
        }

        /// <summary>
        /// Sql query returns tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <param name="sql"></param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        public static
            Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)>
            SqlQueriesAsync<T1, T2, T3, T4, T5, T6>(this string sql, object model,
                CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(sql)
                .SqlQueriesAsync<T1, T2, T3, T4, T5, T6>(model, cancellationToken);
        }

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
        /// <param name="sql"></param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        public static
            Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6,
                List<T7> list7)> SqlQueriesAsync<T1, T2, T3, T4, T5, T6, T7>(this string sql, object model,
                CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(sql)
                .SqlQueriesAsync<T1, T2, T3, T4, T5, T6, T7>(model, cancellationToken);
        }

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
        /// <param name="sql"></param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        public static
            Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6,
                List<T7> list7, List<T8> list8)> SqlQueriesAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this string sql,
                object model, CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(sql)
                .SqlQueriesAsync<T1, T2, T3, T4, T5, T6, T7, T8>(model, cancellationToken);
        }

        /// <summary>
        /// implement Sql no data returned
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>int</returns>
        public static int SqlNonQuery(this string sql, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlNonQuery(parameters);
        }

        /// <summary>
        /// implement Sql no data returned
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="model">parametric model</param>
        /// <returns>int</returns>
        public static int SqlNonQuery(this string sql, object model)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlNonQuery(model);
        }

        /// <summary>
        /// implement Sql no data returned
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>int</returns>
        public static Task<int> SqlNonQueryAsync(this string sql, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlNonQueryAsync(parameters);
        }

        /// <summary>
        /// implement Sql no data returned
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>int</returns>
        public static Task<int> SqlNonQueryAsync(this string sql, DbParameter[] parameters,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlNonQueryAsync(parameters, cancellationToken);
        }

        /// <summary>
        /// implement Sql no data returned
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>int</returns>
        public static Task<int> SqlNonQueryAsync(this string sql, object model,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlNonQueryAsync(model, cancellationToken);
        }

        /// <summary>
        /// implement Sql return single row single column
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>object</returns>
        public static object SqlScalar(this string sql, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlScalar(parameters);
        }

        /// <summary>
        /// implement Sql return single row single column
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="model">parametric model</param>
        /// <returns>object</returns>
        public static object SqlScalar(this string sql, object model)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlScalar(model);
        }

        /// <summary>
        /// implement Sql return single row single column
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>object</returns>
        public static Task<object> SqlScalarAsync(this string sql, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlScalarAsync(parameters);
        }

        /// <summary>
        /// implement Sql return single row single column
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>object</returns>
        public static Task<object> SqlScalarAsync(this string sql, DbParameter[] parameters,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlScalarAsync(parameters, cancellationToken);
        }

        /// <summary>
        /// implement Sql return single row single column
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>object</returns>
        public static Task<object> SqlScalarAsync(this string sql, object model,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlScalarAsync(model, cancellationToken);
        }

        /// <summary>
        /// implement Sql return single row single column
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>TResult</returns>
        public static TResult SqlScalar<TResult>(this string sql, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlScalar<TResult>(parameters);
        }

        /// <summary>
        /// implement Sql return single row single column
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="model">parametric model</param>
        /// <returns>TResult</returns>
        public static TResult SqlScalar<TResult>(this string sql, object model)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlScalar<TResult>(model);
        }

        /// <summary>
        /// implement Sql return single row single column
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>TResult</returns>
        public static Task<TResult> SqlScalarAsync<TResult>(this string sql, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlScalarAsync<TResult>(parameters);
        }

        /// <summary>
        /// implement Sql return single row single column
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>TResult</returns>
        public static Task<TResult> SqlScalarAsync<TResult>(this string sql, DbParameter[] parameters,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlScalarAsync<TResult>(parameters, cancellationToken);
        }

        /// <summary>
        /// implement Sql return single row single column
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>TResult</returns>
        public static Task<TResult> SqlScalarAsync<TResult>(this string sql, object model,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(sql).SqlScalarAsync<TResult>(model, cancellationToken);
        }

        /// <summary>
        /// implement存储过程return DataTable
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>DataTable</returns>
        public static DataTable SqlProcedureQuery(this string procName, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureQuery(parameters);
        }

        /// <summary>
        /// implement存储过程return DataTable
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="model">parametric model</param>
        /// <returns>DataTable</returns>
        public static DataTable SqlProcedureQuery(this string procName, object model)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureQuery(model);
        }

        /// <summary>
        /// implement存储过程return DataTable
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>DataTable</returns>
        public static Task<DataTable> SqlProcedureQueryAsync(this string procName, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureQueryAsync(parameters);
        }

        /// <summary>
        /// implement存储过程return DataTable
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>DataTable</returns>
        public static Task<DataTable> SqlProcedureQueryAsync(this string procName, DbParameter[] parameters,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureQueryAsync(parameters, cancellationToken);
        }

        /// <summary>
        /// implement存储过程return DataTable
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>DataTable</returns>
        public static Task<DataTable> SqlProcedureQueryAsync(this string procName, object model,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureQueryAsync(model, cancellationToken);
        }

        /// <summary>
        /// implement存储过程return List gather
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>List{T}</returns>
        public static List<T> SqlProcedureQuery<T>(this string procName, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureQuery<T>(parameters);
        }

        /// <summary>
        /// implement存储过程return List gather
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="model">parametric model</param>
        /// <returns>List{T}</returns>
        public static List<T> SqlProcedureQuery<T>(this string procName, object model)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureQuery<T>(model);
        }

        /// <summary>
        /// implement存储过程return List gather
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>List{T}</returns>
        public static Task<List<T>> SqlProcedureQueryAsync<T>(this string procName, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureQueryAsync<T>(parameters);
        }

        /// <summary>
        /// implement存储过程return List gather
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>List{T}</returns>
        public static Task<List<T>> SqlProcedureQueryAsync<T>(this string procName, DbParameter[] parameters,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureQueryAsync<T>(parameters, cancellationToken);
        }

        /// <summary>
        /// implement存储过程return List gather
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>List{T}</returns>
        public static Task<List<T>> SqlProcedureQueryAsync<T>(this string procName, object model,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureQueryAsync<T>(model, cancellationToken);
        }

        /// <summary>
        /// implement存储过程return DataSet
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>DataSet</returns>
        public static DataSet SqlProcedureQueries(this string procName, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureQueries(parameters);
        }

        /// <summary>
        /// implement存储过程return DataSet
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="model">parametric model</param>
        /// <returns>DataSet</returns>
        public static DataSet SqlProcedureQueries(this string procName, object model)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureQueries(model);
        }

        /// <summary>
        /// implement存储过程return DataSet
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>DataSet</returns>
        public static Task<DataSet> SqlProcedureQueriesAsync(this string procName, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureQueriesAsync(parameters);
        }

        /// <summary>
        /// implement存储过程return DataSet
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>DataSet</returns>
        public static Task<DataSet> SqlProcedureQueriesAsync(this string procName, DbParameter[] parameters,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureQueriesAsync(parameters, cancellationToken);
        }

        /// <summary>
        /// implement存储过程return DataSet
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>DataSet</returns>
        public static Task<DataSet> SqlProcedureQueriesAsync(this string procName, object model,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureQueriesAsync(model, cancellationToken);
        }

        /// <summary>
        ///  implement存储过程return List gather
        /// </summary>
        /// <typeparam name="T1">return type</typeparam>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>List{T1}</returns>
        public static List<T1> SqlProcedureQueries<T1>(this string procName, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureQueries<T1>(parameters);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        public static (List<T1> list1, List<T2> list2) SqlProcedureQueries<T1, T2>(this string procName,
            params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureQueries<T1, T2>(parameters);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3) SqlProcedureQueries<T1, T2, T3>(
            this string procName, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureQueries<T1, T2, T3>(parameters);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4)
            SqlProcedureQueries<T1, T2, T3, T4>(this string procName, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureQueries<T1, T2, T3, T4>(parameters);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)
            SqlProcedureQueries<T1, T2, T3, T4, T5>(this string procName, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureQueries<T1, T2, T3, T4, T5>(parameters);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)
            SqlProcedureQueries<T1, T2, T3, T4, T5, T6>(this string procName, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureQueries<T1, T2, T3, T4, T5, T6>(parameters);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <typeparam name="T7">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6,
            List<T7> list7) SqlProcedureQueries<T1, T2, T3, T4, T5, T6, T7>(this string procName,
                params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureQueries<T1, T2, T3, T4, T5, T6, T7>(parameters);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <typeparam name="T7">tuple元素类型</typeparam>
        /// <typeparam name="T8">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6,
            List<T7> list7, List<T8> list8) SqlProcedureQueries<T1, T2, T3, T4, T5, T6, T7, T8>(this string procName,
                params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureQueries<T1, T2, T3, T4, T5, T6, T7, T8>(parameters);
        }

        /// <summary>
        ///  implement存储过程return List gather
        /// </summary>
        /// <typeparam name="T1">return type</typeparam>
        /// <param name="procName"></param>
        /// <param name="model">parametric model</param>
        /// <returns>List{T1}</returns>
        public static List<T1> SqlProcedureQueries<T1>(this string procName, object model)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureQueries<T1>(model);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="model">parametric model</param>
        /// <returns>tuple类型</returns>
        public static (List<T1> list1, List<T2> list2) SqlProcedureQueries<T1, T2>(this string procName, object model)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureQueries<T1, T2>(model);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="model">parametric model</param>
        /// <returns>tuple类型</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3) SqlProcedureQueries<T1, T2, T3>(
            this string procName, object model)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureQueries<T1, T2, T3>(model);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="model">parametric model</param>
        /// <returns>tuple类型</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4) SqlProcedureQueries<T1, T2, T3,
            T4>(this string procName, object model)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureQueries<T1, T2, T3, T4>(model);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="model">parametric model</param>
        /// <returns>tuple类型</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)
            SqlProcedureQueries<T1, T2, T3, T4, T5>(this string procName, object model)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureQueries<T1, T2, T3, T4, T5>(model);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="model">parametric model</param>
        /// <returns>tuple类型</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)
            SqlProcedureQueries<T1, T2, T3, T4, T5, T6>(this string procName, object model)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureQueries<T1, T2, T3, T4, T5, T6>(model);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <typeparam name="T7">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="model">parametric model</param>
        /// <returns>tuple类型</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6,
            List<T7> list7) SqlProcedureQueries<T1, T2, T3, T4, T5, T6, T7>(this string procName, object model)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureQueries<T1, T2, T3, T4, T5, T6, T7>(model);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <typeparam name="T7">tuple元素类型</typeparam>
        /// <typeparam name="T8">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="model">parametric model</param>
        /// <returns>tuple类型</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6,
            List<T7> list7, List<T8> list8) SqlProcedureQueries<T1, T2, T3, T4, T5, T6, T7, T8>(this string procName,
                object model)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureQueries<T1, T2, T3, T4, T5, T6, T7, T8>(model);
        }

        /// <summary>
        ///  implement存储过程return List gather
        /// </summary>
        /// <typeparam name="T1">return type</typeparam>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>Task{List{T1}}</returns>
        public static Task<List<T1>> SqlProcedureQueriesAsync<T1>(this string procName, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureQueriesAsync<T1>(parameters);
        }

        /// <summary>
        ///  implement存储过程return List gather
        /// </summary>
        /// <typeparam name="T1">return type</typeparam>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>Task{List{T1}}</returns>
        public static Task<List<T1>> SqlProcedureQueriesAsync<T1>(this string procName, DbParameter[] parameters,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureQueriesAsync<T1>(parameters, cancellationToken);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        public static Task<(List<T1> list1, List<T2> list2)> SqlProcedureQueriesAsync<T1, T2>(this string procName,
            params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureQueriesAsync<T1, T2>(parameters);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        public static Task<(List<T1> list1, List<T2> list2)> SqlProcedureQueriesAsync<T1, T2>(this string procName,
            DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureQueriesAsync<T1, T2>(parameters, cancellationToken);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        public static Task<(List<T1> list1, List<T2> list2, List<T3> list3)> SqlProcedureQueriesAsync<T1, T2, T3>(
            this string procName, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureQueriesAsync<T1, T2, T3>(parameters);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        public static Task<(List<T1> list1, List<T2> list2, List<T3> list3)> SqlProcedureQueriesAsync<T1, T2, T3>(
            this string procName, DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureQueriesAsync<T1, T2, T3>(parameters, cancellationToken);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        public static Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4)>
            SqlProcedureQueriesAsync<T1, T2, T3, T4>(this string procName, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureQueriesAsync<T1, T2, T3, T4>(parameters);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        public static Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4)>
            SqlProcedureQueriesAsync<T1, T2, T3, T4>(this string procName, DbParameter[] parameters,
                CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureQueriesAsync<T1, T2, T3, T4>(parameters, cancellationToken);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        public static Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)>
            SqlProcedureQueriesAsync<T1, T2, T3, T4, T5>(this string procName, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureQueriesAsync<T1, T2, T3, T4, T5>(parameters);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        public static Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)>
            SqlProcedureQueriesAsync<T1, T2, T3, T4, T5>(this string procName, DbParameter[] parameters,
                CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureQueriesAsync<T1, T2, T3, T4, T5>(parameters, cancellationToken);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        public static
            Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)>
            SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6>(this string procName, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6>(parameters);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        public static
            Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)>
            SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6>(this string procName, DbParameter[] parameters,
                CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6>(parameters, cancellationToken);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <typeparam name="T7">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        public static
            Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6,
                List<T7> list7)> SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6, T7>(this string procName,
                params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6, T7>(parameters);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <typeparam name="T7">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        public static
            Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6,
                List<T7> list7)> SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6, T7>(this string procName,
                DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6, T7>(parameters, cancellationToken);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <typeparam name="T7">tuple元素类型</typeparam>
        /// <typeparam name="T8">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>tuple类型</returns>
        public static
            Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6,
                List<T7> list7, List<T8> list8)> SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6, T7, T8>(
                this string procName, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6, T7, T8>(parameters);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <typeparam name="T7">tuple元素类型</typeparam>
        /// <typeparam name="T8">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        public static
            Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6,
                List<T7> list7, List<T8> list8)> SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6, T7, T8>(
                this string procName, DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6, T7, T8>(parameters, cancellationToken);
        }

        /// <summary>
        ///  implement存储过程return List gather
        /// </summary>
        /// <typeparam name="T1">return type</typeparam>
        /// <param name="procName"></param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>List{T1}</returns>
        public static Task<List<T1>> SqlProcedureQueriesAsync<T1>(this string procName, object model,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureQueriesAsync<T1>(model, cancellationToken);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        public static Task<(List<T1> list1, List<T2> list2)> SqlProcedureQueriesAsync<T1, T2>(this string procName,
            object model, CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureQueriesAsync<T1, T2>(model, cancellationToken);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        public static Task<(List<T1> list1, List<T2> list2, List<T3> list3)> SqlProcedureQueriesAsync<T1, T2, T3>(
            this string procName, object model, CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureQueriesAsync<T1, T2, T3>(model, cancellationToken);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        public static Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4)>
            SqlProcedureQueriesAsync<T1, T2, T3, T4>(this string procName, object model,
                CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureQueriesAsync<T1, T2, T3, T4>(model, cancellationToken);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        public static Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5)>
            SqlProcedureQueriesAsync<T1, T2, T3, T4, T5>(this string procName, object model,
                CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureQueriesAsync<T1, T2, T3, T4, T5>(model, cancellationToken);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        public static
            Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)>
            SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6>(this string procName, object model,
                CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6>(model, cancellationToken);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <typeparam name="T7">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        public static
            Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6,
                List<T7> list7)> SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6, T7>(this string procName,
                object model, CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6, T7>(model, cancellationToken);
        }

        /// <summary>
        /// implement存储过程return tuple gather
        /// </summary>
        /// <typeparam name="T1">tuple元素类型</typeparam>
        /// <typeparam name="T2">tuple元素类型</typeparam>
        /// <typeparam name="T3">tuple元素类型</typeparam>
        /// <typeparam name="T4">tuple元素类型</typeparam>
        /// <typeparam name="T5">tuple元素类型</typeparam>
        /// <typeparam name="T6">tuple元素类型</typeparam>
        /// <typeparam name="T7">tuple元素类型</typeparam>
        /// <typeparam name="T8">tuple元素类型</typeparam>
        /// <param name="procName"></param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>tuple类型</returns>
        public static
            Task<(List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6,
                List<T7> list7, List<T8> list8)> SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6, T7, T8>(
                this string procName, object model, CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureQueriesAsync<T1, T2, T3, T4, T5, T6, T7, T8>(model, cancellationToken);
        }

        /// <summary>
        /// implement存储过程return single row single column
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>object</returns>
        public static object SqlProcedureScalar(this string procName, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureScalar(parameters);
        }

        /// <summary>
        /// implement存储过程return single row single column
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="model">parametric model</param>
        /// <returns>object</returns>
        public static object SqlProcedureScalar(this string procName, object model)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureScalar(model);
        }

        /// <summary>
        /// implement存储过程return single row single column
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>object</returns>
        public static Task<object> SqlProcedureScalarAsync(this string procName, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureScalarAsync(parameters);
        }

        /// <summary>
        /// implement存储过程return single row single column
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>object</returns>
        public static Task<object> SqlProcedureScalarAsync(this string procName, DbParameter[] parameters,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureScalarAsync(parameters, cancellationToken);
        }

        /// <summary>
        /// implement存储过程return single row single column
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>object</returns>
        public static Task<object> SqlProcedureScalarAsync(this string procName, object model,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureScalarAsync(model, cancellationToken);
        }

        /// <summary>
        /// implement存储过程return single row single column
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>TResult</returns>
        public static TResult SqlProcedureScalar<TResult>(this string procName, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureScalar<TResult>(parameters);
        }

        /// <summary>
        /// implement存储过程return single row single column
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="model">parametric model</param>
        /// <returns>TResult</returns>
        public static TResult SqlProcedureScalar<TResult>(this string procName, object model)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureScalar<TResult>(model);
        }

        /// <summary>
        /// implement存储过程return single row single column
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>TResult</returns>
        public static Task<TResult> SqlProcedureScalarAsync<TResult>(this string procName,
            params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureScalarAsync<TResult>(parameters);
        }

        /// <summary>
        /// implement存储过程return single row single column
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>TResult</returns>
        public static Task<TResult> SqlProcedureScalarAsync<TResult>(this string procName, DbParameter[] parameters,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureScalarAsync<TResult>(parameters, cancellationToken);
        }

        /// <summary>
        /// implement存储过程return single row single column
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>TResult</returns>
        public static Task<TResult> SqlProcedureScalarAsync<TResult>(this string procName, object model,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureScalarAsync<TResult>(model, cancellationToken);
        }

        /// <summary>
        /// implement存储过程no data returned
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>int</returns>
        public static int SqlProcedureNonQuery(this string procName, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureNonQuery(parameters);
        }

        /// <summary>
        /// implement存储过程no data returned
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="model">parametric model</param>
        /// <returns>int</returns>
        public static int SqlProcedureNonQuery(this string procName, object model)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureNonQuery(model);
        }

        /// <summary>
        /// implement存储过程no data returned
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>int</returns>
        public static Task<int> SqlProcedureNonQueryAsync(this string procName, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureNonQueryAsync(parameters);
        }

        /// <summary>
        /// implement存储过程no data returned
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>int</returns>
        public static Task<int> SqlProcedureNonQueryAsync(this string procName, DbParameter[] parameters,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureNonQueryAsync(parameters, cancellationToken);
        }

        /// <summary>
        /// implement存储过程no data returned
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>int</returns>
        public static Task<int> SqlProcedureNonQueryAsync(this string procName, object model,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureNonQueryAsync(model, cancellationToken);
        }

        /// <summary>
        /// implement存储过程returnOUPUT、RETURN、result set
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>ProcedureOutput</returns>
        public static ProcedureOutputResult SqlProcedureOutput(this string procName, DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureOutput(parameters);
        }

        /// <summary>
        /// implement存储过程returnOUPUT、RETURN、result set
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>ProcedureOutput</returns>
        public static Task<ProcedureOutputResult> SqlProcedureOutputAsync(this string procName,
            DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureOutputAsync(parameters, cancellationToken);
        }

        /// <summary>
        /// implement存储过程returnOUPUT、RETURN、result set
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="model">command model</param>
        /// <returns>ProcedureOutput</returns>
        public static ProcedureOutputResult SqlProcedureOutput(this string procName, object model)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureOutput(model);
        }

        /// <summary>
        /// implement存储过程returnOUPUT、RETURN、result set
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="model">command model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>ProcedureOutput</returns>
        public static Task<ProcedureOutputResult> SqlProcedureOutputAsync(this string procName, object model,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureOutputAsync(model, cancellationToken);
        }

        /// <summary>
        /// implement存储过程returnOUPUT、RETURN、result set
        /// </summary>
        /// <typeparam name="TResult">dataset results</typeparam>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>ProcedureOutput</returns>
        public static ProcedureOutputResult<TResult> SqlProcedureOutput<TResult>(this string procName,
            DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureOutput<TResult>(parameters);
        }

        /// <summary>
        /// implement存储过程returnOUPUT、RETURN、result set
        /// </summary>
        /// <typeparam name="TResult">dataset results</typeparam>
        /// <param name="procName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>ProcedureOutput</returns>
        public static Task<ProcedureOutputResult<TResult>> SqlProcedureOutputAsync<TResult>(this string procName,
            DbParameter[] parameters, CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureOutputAsync<TResult>(parameters, cancellationToken);
        }

        /// <summary>
        /// implement存储过程returnOUPUT、RETURN、result set
        /// </summary>
        /// <typeparam name="TResult">dataset results</typeparam>
        /// <param name="procName"></param>
        /// <param name="model">command model</param>
        /// <returns>ProcedureOutput</returns>
        public static ProcedureOutputResult<TResult> SqlProcedureOutput<TResult>(this string procName, object model)
        {
            return new SqlStringExecutePart().SetSqlString(procName).SqlProcedureOutput<TResult>(model);
        }

        /// <summary>
        /// implement存储过程returnOUPUT、RETURN、result set
        /// </summary>
        /// <typeparam name="TResult">dataset results</typeparam>
        /// <param name="procName"></param>
        /// <param name="model">command model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>ProcedureOutput</returns>
        public static Task<ProcedureOutputResult<TResult>> SqlProcedureOutputAsync<TResult>(this string procName,
            object model, CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(procName)
                .SqlProcedureOutputAsync<TResult>(model, cancellationToken);
        }

        /// <summary>
        /// implement标量函数return single row single column
        /// </summary>
        /// <param name="funcName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>object</returns>
        public static object SqlFunctionScalar(this string funcName, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(funcName).SqlFunctionScalar(parameters);
        }

        /// <summary>
        /// implement标量函数return single row single column
        /// </summary>
        /// <param name="funcName"></param>
        /// <param name="model"></param>
        /// <returns>object</returns>
        public static object SqlFunctionScalar(this string funcName, object model)
        {
            return new SqlStringExecutePart().SetSqlString(funcName).SqlFunctionScalar(model);
        }

        /// <summary>
        /// implement标量函数return single row single column
        /// </summary>
        /// <param name="funcName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>object</returns>
        public static Task<object> SqlFunctionScalarAsync(this string funcName, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(funcName).SqlFunctionScalarAsync(parameters);
        }

        /// <summary>
        /// implement标量函数return single row single column
        /// </summary>
        /// <param name="funcName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>object</returns>
        public static Task<object> SqlFunctionScalarAsync(this string funcName, DbParameter[] parameters,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(funcName)
                .SqlFunctionScalarAsync(parameters, cancellationToken);
        }

        /// <summary>
        /// implement标量函数return single row single column
        /// </summary>
        /// <param name="funcName"></param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>object</returns>
        public static Task<object> SqlFunctionScalarAsync(this string funcName, object model,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(funcName).SqlFunctionScalarAsync(model, cancellationToken);
        }

        /// <summary>
        /// implement标量函数return single row single column
        /// </summary>
        /// <typeparam name="TResult">return值类型</typeparam>
        /// <param name="funcName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>TResult</returns>
        public static TResult SqlFunctionScalar<TResult>(this string funcName, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(funcName).SqlFunctionScalar<TResult>(parameters);
        }

        /// <summary>
        /// implement标量函数return single row single column
        /// </summary>
        /// <typeparam name="TResult">return值类型</typeparam>
        /// <param name="funcName"></param>
        /// <param name="model">parametric model</param>
        /// <returns>TResult</returns>
        public static TResult SqlFunctionScalar<TResult>(this string funcName, object model)
        {
            return new SqlStringExecutePart().SetSqlString(funcName).SqlFunctionScalar<TResult>(model);
        }

        /// <summary>
        /// implement标量函数return single row single column
        /// </summary>
        /// <typeparam name="TResult">return值类型</typeparam>
        /// <param name="funcName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>TResult</returns>
        public static Task<TResult> SqlFunctionScalarAsync<TResult>(this string funcName,
            params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(funcName).SqlFunctionScalarAsync<TResult>(parameters);
        }

        /// <summary>
        /// implement标量函数return single row single column
        /// </summary>
        /// <typeparam name="TResult">return值类型</typeparam>
        /// <param name="funcName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>TResult</returns>
        public static Task<TResult> SqlFunctionScalarAsync<TResult>(this string funcName, DbParameter[] parameters,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(funcName)
                .SqlFunctionScalarAsync<TResult>(parameters, cancellationToken);
        }

        /// <summary>
        /// implement标量函数return single row single column
        /// </summary>
        /// <typeparam name="TResult">return值类型</typeparam>
        /// <param name="funcName"></param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>object</returns>
        public static Task<TResult> SqlFunctionScalarAsync<TResult>(this string funcName, object model,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(funcName)
                .SqlFunctionScalarAsync<TResult>(model, cancellationToken);
        }

        /// <summary>
        /// implement表值函数return DataTable
        /// </summary>
        /// <param name="funcName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>DataTable</returns>
        public static DataTable SqlFunctionQuery(this string funcName, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(funcName).SqlFunctionQuery(parameters);
        }

        /// <summary>
        /// implement表值函数return DataTable
        /// </summary>
        /// <param name="funcName"></param>
        /// <param name="model">parametric model</param>
        /// <returns>DataTable</returns>
        public static DataTable SqlFunctionQuery(this string funcName, object model)
        {
            return new SqlStringExecutePart().SetSqlString(funcName).SqlFunctionQuery(model);
        }

        /// <summary>
        /// implement表值函数return DataTable
        /// </summary>
        /// <param name="funcName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>Task{DataTable}</returns>
        public static Task<DataTable> SqlFunctionQueryAsync(this string funcName, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(funcName).SqlFunctionQueryAsync(parameters);
        }

        /// <summary>
        /// implement表值函数return DataTable
        /// </summary>
        /// <param name="funcName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>Task{DataTable}</returns>
        public static Task<DataTable> SqlFunctionQueryAsync(this string funcName, DbParameter[] parameters,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(funcName)
                .SqlFunctionQueryAsync(parameters, cancellationToken);
        }

        /// <summary>
        /// implement表值函数return DataTable
        /// </summary>
        /// <param name="funcName"></param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>Task{DataTable}</returns>
        public static Task<DataTable> SqlFunctionQueryAsync(this string funcName, object model,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(funcName).SqlFunctionQueryAsync(model, cancellationToken);
        }

        /// <summary>
        /// implement表值函数return List gather
        /// </summary>
        /// <typeparam name="T">return type</typeparam>
        /// <param name="funcName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>List{T}</returns>
        public static List<T> SqlFunctionQuery<T>(this string funcName, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(funcName).SqlFunctionQuery<T>(parameters);
        }

        /// <summary>
        /// implement表值函数return List gather
        /// </summary>
        /// <typeparam name="T">return type</typeparam>
        /// <param name="funcName"></param>
        /// <param name="model">parametric model</param>
        /// <returns>List{T}</returns>
        public static List<T> SqlFunctionQuery<T>(this string funcName, object model)
        {
            return new SqlStringExecutePart().SetSqlString(funcName).SqlFunctionQuery<T>(model);
        }

        /// <summary>
        /// implement表值函数return List gather
        /// </summary>
        /// <typeparam name="T">return type</typeparam>
        /// <param name="funcName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <returns>Task{List{T}}</returns>
        public static Task<List<T>> SqlFunctionQueryAsync<T>(this string funcName, params DbParameter[] parameters)
        {
            return new SqlStringExecutePart().SetSqlString(funcName).SqlFunctionQueryAsync<T>(parameters);
        }

        /// <summary>
        /// implement表值函数return List gather
        /// </summary>
        /// <typeparam name="T">return type</typeparam>
        /// <param name="funcName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>Task{List{T}}</returns>
        public static Task<List<T>> SqlFunctionQueryAsync<T>(this string funcName, DbParameter[] parameters,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(funcName)
                .SqlFunctionQueryAsync<T>(parameters, cancellationToken);
        }

        /// <summary>
        /// implement表值函数return List gather
        /// </summary>
        /// <typeparam name="T">return type</typeparam>
        /// <param name="funcName"></param>
        /// <param name="model">parametric model</param>
        /// <param name="cancellationToken">Asynchronous cancellation token</param>
        /// <returns>Task{List{T}}</returns>
        public static Task<List<T>> SqlFunctionQueryAsync<T>(this string funcName, object model,
            CancellationToken cancellationToken = default)
        {
            return new SqlStringExecutePart().SetSqlString(funcName).SqlFunctionQueryAsync<T>(model, cancellationToken);
        }
    }
}