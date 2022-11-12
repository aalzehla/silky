using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Silky.Core.Extensions;
using Silky.EntityFrameworkCore.Extensions.DatabaseFacade;
using Silky.EntityFrameworkCore.Values;
using System.ComponentModel.DataAnnotations.Schema;

namespace Silky.EntityFrameworkCore.Helpers
{
    internal static class DbHelpers
    {
        /// <summary>
        /// Convert the model to DbParameter gather
        /// </summary>
        /// <param name="model">parametric model</param>
        /// <param name="dbCommand">database command object</param>
        /// <returns></returns>
        internal static DbParameter[] ConvertToDbParameters(object model, DbCommand dbCommand)
        {
            var modelType = model?.GetType();

            // Handling dictionary type parameters
            if (modelType == typeof(Dictionary<string, object>))
                return ConvertToDbParameters((Dictionary<string, object>)model, dbCommand);

            var dbParameters = new List<DbParameter>();
            if (model == null || !modelType.IsClass) return dbParameters.ToArray();

            // Get all public instance properties
            var properties = modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            if (properties.Length == 0) return dbParameters.ToArray();

            // iterate over all properties
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(model) ?? DBNull.Value;

                // Create command parameters
                var dbParameter = dbCommand.CreateParameter();

                // Determine whether the attribute is pasted [DbParameter] characteristic
                if (property.IsDefined(typeof(DbParameterAttribute), true))
                {
                    var dbParameterAttribute = property.GetCustomAttribute<DbParameterAttribute>(true);
                    dbParameters.Add(ConfigureDbParameter(property.Name, propertyValue, dbParameterAttribute,
                        dbParameter));
                    continue;
                }

                dbParameter.ParameterName = property.Name;
                dbParameter.Value = propertyValue.ChangeType(propertyValue.GetActualType()); // solve object/json type value
                dbParameters.Add(dbParameter);
            }

            return dbParameters.ToArray();
        }

        /// <summary>
        /// Convert dictionary to command parameter
        /// </summary>
        /// <param name="keyValues">dictionary</param>
        /// <param name="dbCommand">database command object</param>
        /// <returns></returns>
        internal static DbParameter[] ConvertToDbParameters(Dictionary<string, object> keyValues, DbCommand dbCommand)
        {
            var dbParameters = new List<DbParameter>();
            if (keyValues == null || keyValues.Count == 0) return dbParameters.ToArray();

            foreach (var key in keyValues.Keys)
            {
                var value = keyValues[key] ?? DBNull.Value;

                // Create command parameters
                var dbParameter = dbCommand.CreateParameter();
                dbParameter.ParameterName = key;
                dbParameter.Value = value;
                dbParameters.Add(dbParameter);
            }

            return dbParameters.ToArray();
        }

        /// <summary>
        /// Configure database command parameters
        /// </summary>
        /// <param name="name">parameter name</param>
        /// <param name="value">parameter value</param>
        /// <param name="dbParameterAttribute">parametercharacteristic</param>
        /// <param name="dbParameter">Database command parameters</param>
        /// <returns>DbParameter</returns>
        internal static DbParameter ConfigureDbParameter(string name, object value,
            DbParameterAttribute dbParameterAttribute, DbParameter dbParameter)
        {
            // Set parameter direction
            dbParameter.ParameterName = name;
            dbParameter.Value = value;
            dbParameter.Direction = dbParameterAttribute.Direction;

            // Set parameter database type
            if (dbParameterAttribute.DbType != null)
            {
                var type = dbParameterAttribute.DbType.GetType();
                if (type.IsEnum)
                {
                    // handle generic DbType type
                    if (typeof(DbType).IsAssignableFrom(type)) dbParameter.DbType = (DbType)dbParameterAttribute.DbType;

                    // solve Oracle 数据库游标typeparameter
                    if (type.FullName.Equals("Oracle.ManagedDataAccess.Client.OracleDbType",
                        StringComparison.OrdinalIgnoreCase))
                    {
                        dbParameter.GetType().GetProperty("OracleDbType")
                            ?.SetValue(dbParameter, dbParameterAttribute.DbType);
                    }
                }
            }

            // set size，solveNVarchar，Varchar question
            if (dbParameterAttribute.Size > 0)
            {
                dbParameter.Size = dbParameterAttribute.Size;
            }

            return dbParameter;
        }

        /// <summary>
        /// Generate function execution sql statement
        /// </summary>
        /// <param name="providerName">ADO.NET database object</param>
        /// <param name="dbFunctionType">函数type</param>
        /// <param name="funcName">function noun</param>
        /// <param name="parameters">function parameter</param>
        /// <returns>sql statement</returns>
        internal static string GenerateFunctionSql(string providerName, DbFunctionType dbFunctionType, string funcName,
            params DbParameter[] parameters)
        {
            // Check if a function is supported
            DbProvider.CheckFunctionSupported(providerName, dbFunctionType);

            parameters ??= Array.Empty<DbParameter>();

            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"SELECT{(dbFunctionType == DbFunctionType.Table ? " * FROM" : "")} {funcName}(");

            // 生成function parameter
            for (var i = 0; i < parameters.Length; i++)
            {
                var sqlParameter = parameters[i];

                // Handling placeholders for different databases
                stringBuilder.Append(FixSqlParameterPlaceholder(providerName, sqlParameter.ParameterName));

                // handle last argument comma
                if (i != parameters.Length - 1)
                {
                    stringBuilder.Append(", ");
                }
            }

            stringBuilder.Append("); ");

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Generate function execution sql statement
        /// </summary>
        ///<param name="providerName">ADO.NET database object</param>
        /// <param name="dbFunctionType">函数type</param>
        /// <param name="funcName">function noun</param>
        /// <param name="model">parametric model</param>
        /// <returns>(string sql, DbParameter[] parameters)</returns>
        internal static string GenerateFunctionSql(string providerName, DbFunctionType dbFunctionType, string funcName,
            object model)
        {
            // Check if a function is supported
            DbProvider.CheckFunctionSupported(providerName, dbFunctionType);

            var modelType = model?.GetType();
            // Handling dictionary type parameters
            if (modelType == typeof(Dictionary<string, object>))
                return GenerateFunctionSql(providerName, dbFunctionType, funcName, (Dictionary<string, object>)model);

            // Get all the exposed properties of the model
            var properities = model == null
                ? Array.Empty<PropertyInfo>()
                : modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"SELECT{(dbFunctionType == DbFunctionType.Table ? " * FROM" : "")} {funcName}(");

            for (var i = 0; i < properities.Length; i++)
            {
                var property = properities[i];

                stringBuilder.Append(FixSqlParameterPlaceholder(providerName, property.Name));

                // handle last argument comma
                if (i != properities.Length - 1)
                {
                    stringBuilder.Append(", ");
                }
            }

            stringBuilder.Append("); ");

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Generate function execution sql statement
        /// </summary>
        ///<param name="providerName">ADO.NET database object</param>
        /// <param name="dbFunctionType">函数type</param>
        /// <param name="funcName">function noun</param>
        /// <param name="keyValues">dictionarytypeparameter</param>
        /// <returns></returns>
        internal static string GenerateFunctionSql(string providerName, DbFunctionType dbFunctionType, string funcName,
            Dictionary<string, object> keyValues)
        {
            // Check if a function is supported
            DbProvider.CheckFunctionSupported(providerName, dbFunctionType);

            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"SELECT{(dbFunctionType == DbFunctionType.Table ? " * FROM" : "")} {funcName}(");

            if (keyValues != null && keyValues.Count > 0)
            {
                var i = 0;
                foreach (var key in keyValues.Keys)
                {
                    stringBuilder.Append(FixSqlParameterPlaceholder(providerName, key));

                    // handle last argument comma
                    if (i != keyValues.Count - 1)
                    {
                        stringBuilder.Append(", ");
                    }

                    i++;
                }
            }

            stringBuilder.Append("); ");

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Wrapped stored procedure returns result set
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="dataSet">data set</param>
        /// <returns>ProcedureOutput</returns>
        internal static ProcedureOutputResult WrapperProcedureOutput(string providerName, DbParameter[] parameters,
            DataSet dataSet)
        {
            // read output return value
            ReadOuputValue(providerName, parameters, out var outputValues, out var returnValue);

            return new ProcedureOutputResult
            {
                Result = dataSet,
                OutputValues = outputValues,
                ReturnValue = returnValue
            };
        }

        /// <summary>
        /// Wrapped stored procedure returns result set
        /// </summary>
        /// <typeparam name="TResult">data set结果</typeparam>
        /// <param name="providerName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="dataSet">data set</param>
        /// <returns>ProcedureOutput</returns>
        internal static ProcedureOutputResult<TResult> WrapperProcedureOutput<TResult>(string providerName,
            DbParameter[] parameters, DataSet dataSet)
        {
            // read output return value
            ReadOuputValue(providerName, parameters, out var outputValues, out var returnValue);

            return new ProcedureOutputResult<TResult>
            {
                Result = (TResult)dataSet.ToValueTuple(typeof(TResult)),
                OutputValues = outputValues,
                ReturnValue = returnValue
            };
        }

        /// <summary>
        /// Wrapped stored procedure returns result set
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="parameters">Command parameters</param>
        /// <param name="dataSet">data set</param>
        /// <param name="type">返回type</param>
        /// <returns>ProcedureOutput</returns>
        internal static object WrapperProcedureOutput(string providerName, DbParameter[] parameters, DataSet dataSet,
            Type type)
        {
            var wrapperProcedureOutputMethod = typeof(DbHelpers)
                .GetMethods(BindingFlags.Static | BindingFlags.NonPublic)
                .First(u => u.Name == "WrapperProcedureOutput" && u.IsGenericMethod)
                .MakeGenericMethod(type);

            return wrapperProcedureOutputMethod.Invoke(null, new object[] { providerName, parameters, dataSet });
        }

        /// <summary>
        /// data not found exception
        /// </summary>
        /// <returns></returns>
        internal static InvalidOperationException DataNotFoundException()
        {
            return new InvalidOperationException("Sequence contains no elements.");
        }

        /// <summary>
        /// 修正不同Database command parameters前缀不一致question
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="parameterName"></param>
        /// <param name="isFixed"></param>
        /// <returns></returns>
        internal static string FixSqlParameterPlaceholder(string providerName, string parameterName,
            bool isFixed = true)
        {
            var placeholder = !DbProvider.IsDatabaseFor(providerName, DbProvider.Oracle) ? "@" : ":";
            if (parameterName.StartsWith("@") || parameterName.StartsWith(":"))
            {
                parameterName = parameterName[1..];
            }

            return isFixed ? placeholder + parameterName : parameterName;
        }

        /// <summary>
        /// read output return value
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="parameters">parameter</param>
        /// <param name="outputValues">输出parameter</param>
        /// <param name="returnValue">return value</param>
        private static void ReadOuputValue(string providerName, DbParameter[] parameters,
            out IEnumerable<ProcedureOutputValue> outputValues, out object returnValue)
        {
            // query allOUTPUTvalue
            outputValues = parameters
                .Where(u => u.Direction == ParameterDirection.Output)
                .Select(u => new ProcedureOutputValue
                {
                    Name = FixSqlParameterPlaceholder(providerName, u.ParameterName, false),
                    Value = u.Value
                });

            // 查询return value
            returnValue = parameters.FirstOrDefault(u => u.Direction == ParameterDirection.ReturnValue)?.Value;
        }
    }
}