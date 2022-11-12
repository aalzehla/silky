using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Silky.Core.Extensions;

namespace Silky.EntityFrameworkCore.Extensions.DatabaseFacade
{
    /// <summary>
    /// Database data conversion extension
    /// </summary>
    public static class DbDataConvertExtensions
    {
        /// <summary>
        /// Will DataTable change List gather
        /// </summary>
        /// <typeparam name="T">return value type</typeparam>
        /// <param name="dataTable">DataTable</param>
        /// <returns>List{T}</returns>
        public static List<T> ToList<T>(this DataTable dataTable)
        {
            return dataTable.ToList(typeof(List<T>)) as List<T>;
        }

        /// <summary>
        /// Will DataTable change List gather
        /// </summary>
        /// <typeparam name="T">return value type</typeparam>
        /// <param name="dataTable">DataTable</param>
        /// <returns>List{T}</returns>
        public static async Task<List<T>> ToListAsync<T>(this DataTable dataTable)
        {
            var list = await dataTable.ToListAsync(typeof(List<T>));
            return list as List<T>;
        }

        /// <summary>
        /// Will DataSet change tuple
        /// </summary>
        /// <typeparam name="T1">tuple元素type</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns>tupletype</returns>
        public static List<T1> ToList<T1>(this DataSet dataSet)
        {
            var tuple = dataSet.ToList(typeof(List<T1>));
            return tuple[0] as List<T1>;
        }

        /// <summary>
        /// Will DataSet change tuple
        /// </summary>
        /// <typeparam name="T1">tuple元素type</typeparam>
        /// <typeparam name="T2">tuple元素type</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns>tupletype</returns>
        public static (List<T1> list1, List<T2> list2) ToList<T1, T2>(this DataSet dataSet)
        {
            var tuple = dataSet.ToList(typeof(List<T1>), typeof(List<T2>));
            return (tuple[0] as List<T1>, tuple[1] as List<T2>);
        }

        /// <summary>
        /// Will DataSet change tuple
        /// </summary>
        /// <typeparam name="T1">tuple元素type</typeparam>
        /// <typeparam name="T2">tuple元素type</typeparam>
        /// <typeparam name="T3">tuple元素type</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns>tupletype</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3) ToList<T1, T2, T3>(this DataSet dataSet)
        {
            var tuple = dataSet.ToList(typeof(List<T1>), typeof(List<T2>), typeof(List<T3>));
            return (tuple[0] as List<T1>, tuple[1] as List<T2>, tuple[2] as List<T3>);
        }

        /// <summary>
        /// Will DataSet change tuple
        /// </summary>
        /// <typeparam name="T1">tuple元素type</typeparam>
        /// <typeparam name="T2">tuple元素type</typeparam>
        /// <typeparam name="T3">tuple元素type</typeparam>
        /// <typeparam name="T4">tuple元素type</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns>tupletype</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4) ToList<T1, T2, T3, T4>(
            this DataSet dataSet)
        {
            var tuple = dataSet.ToList(typeof(List<T1>), typeof(List<T2>), typeof(List<T3>), typeof(List<T4>));
            return (tuple[0] as List<T1>, tuple[1] as List<T2>, tuple[2] as List<T3>, tuple[3] as List<T4>);
        }

        /// <summary>
        /// Will DataSet change tuple
        /// </summary>
        /// <typeparam name="T1">tuple元素type</typeparam>
        /// <typeparam name="T2">tuple元素type</typeparam>
        /// <typeparam name="T3">tuple元素type</typeparam>
        /// <typeparam name="T4">tuple元素type</typeparam>
        /// <typeparam name="T5">tuple元素type</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns>tupletype</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5) ToList<T1, T2,
            T3, T4, T5>(this DataSet dataSet)
        {
            var tuple = dataSet.ToList(typeof(List<T1>), typeof(List<T2>), typeof(List<T3>), typeof(List<T4>),
                typeof(List<T5>));
            return (tuple[0] as List<T1>, tuple[1] as List<T2>, tuple[2] as List<T3>, tuple[3] as List<T4>,
                tuple[4] as List<T5>);
        }

        /// <summary>
        /// Will DataSet change tuple
        /// </summary>
        /// <typeparam name="T1">tuple元素type</typeparam>
        /// <typeparam name="T2">tuple元素type</typeparam>
        /// <typeparam name="T3">tuple元素type</typeparam>
        /// <typeparam name="T4">tuple元素type</typeparam>
        /// <typeparam name="T5">tuple元素type</typeparam>
        /// <typeparam name="T6">tuple元素type</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns>tupletype</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6)
            ToList<T1, T2, T3, T4, T5, T6>(this DataSet dataSet)
        {
            var tuple = dataSet.ToList(typeof(List<T1>), typeof(List<T2>), typeof(List<T3>), typeof(List<T4>),
                typeof(List<T5>), typeof(List<T6>));
            return (tuple[0] as List<T1>, tuple[1] as List<T2>, tuple[2] as List<T3>, tuple[3] as List<T4>,
                tuple[4] as List<T5>, tuple[5] as List<T6>);
        }

        /// <summary>
        /// Will DataSet change tuple
        /// </summary>
        /// <typeparam name="T1">tuple元素type</typeparam>
        /// <typeparam name="T2">tuple元素type</typeparam>
        /// <typeparam name="T3">tuple元素type</typeparam>
        /// <typeparam name="T4">tuple元素type</typeparam>
        /// <typeparam name="T5">tuple元素type</typeparam>
        /// <typeparam name="T6">tuple元素type</typeparam>
        /// <typeparam name="T7">tuple元素type</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns>tupletype</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6,
            List<T7> list7) ToList<T1, T2, T3, T4, T5, T6, T7>(this DataSet dataSet)
        {
            var tuple = dataSet.ToList(typeof(List<T1>), typeof(List<T2>), typeof(List<T3>), typeof(List<T4>),
                typeof(List<T5>), typeof(List<T6>), typeof(List<T7>));
            return (tuple[0] as List<T1>, tuple[1] as List<T2>, tuple[2] as List<T3>, tuple[3] as List<T4>,
                tuple[4] as List<T5>, tuple[5] as List<T6>, tuple[6] as List<T7>);
        }

        /// <summary>
        /// Will DataSet change tuple
        /// </summary>
        /// <typeparam name="T1">tuple元素type</typeparam>
        /// <typeparam name="T2">tuple元素type</typeparam>
        /// <typeparam name="T3">tuple元素type</typeparam>
        /// <typeparam name="T4">tuple元素type</typeparam>
        /// <typeparam name="T5">tuple元素type</typeparam>
        /// <typeparam name="T6">tuple元素type</typeparam>
        /// <typeparam name="T7">tuple元素type</typeparam>
        /// <typeparam name="T8">tuple元素type</typeparam>
        /// <param name="dataSet">DataSet</param>
        /// <returns>tupletype</returns>
        public static (List<T1> list1, List<T2> list2, List<T3> list3, List<T4> list4, List<T5> list5, List<T6> list6,
            List<T7> list7, List<T8> list8) ToList<T1, T2, T3, T4, T5, T6, T7, T8>(this DataSet dataSet)
        {
            var tuple = dataSet.ToList(typeof(List<T1>), typeof(List<T2>), typeof(List<T3>), typeof(List<T4>),
                typeof(List<T5>), typeof(List<T6>), typeof(List<T7>), typeof(List<T8>));
            return (tuple[0] as List<T1>, tuple[1] as List<T2>, tuple[2] as List<T3>, tuple[3] as List<T4>,
                tuple[4] as List<T5>, tuple[5] as List<T6>, tuple[6] as List<T7>, tuple[7] as List<T8>);
        }

        /// <summary>
        /// Will DataSet change specific type
        /// </summary>
        /// <param name="dataSet">DataSet</param>
        /// <param name="returnTypes">specific typegather</param>
        /// <returns>List{object}</returns>
        public static List<object> ToList(this DataSet dataSet, params Type[] returnTypes)
        {
            // get all DataTable
            var dataTables = dataSet.Tables;

            // deal withtupletype
            if (returnTypes.Length == 1 && returnTypes[0].IsValueType)
            {
                returnTypes = returnTypes[0].GenericTypeArguments;
            }

            // handle not incoming returnTypes type
            if (returnTypes == null || returnTypes.Length == 0)
            {
                returnTypes = Enumerable.Range(0, dataTables.Count).Select(u => typeof(List<object>)).ToArray();
            }

            // deal with 8 result sets
            if (returnTypes.Length >= 8)
            {
                return new List<object>
                {
                    dataTables[0].ToList(returnTypes[0]),
                    dataTables[1].ToList(returnTypes[1]),
                    dataTables[2].ToList(returnTypes[2]),
                    dataTables[3].ToList(returnTypes[3]),
                    dataTables[4].ToList(returnTypes[4]),
                    dataTables[5].ToList(returnTypes[5]),
                    dataTables[6].ToList(returnTypes[6]),
                    dataTables[7].ToList(returnTypes[7])
                };
            }
            // deal with 7 result sets
            else if (returnTypes.Length == 7)
            {
                return new List<object>
                {
                    dataTables[0].ToList(returnTypes[0]),
                    dataTables[1].ToList(returnTypes[1]),
                    dataTables[2].ToList(returnTypes[2]),
                    dataTables[3].ToList(returnTypes[3]),
                    dataTables[4].ToList(returnTypes[4]),
                    dataTables[5].ToList(returnTypes[5]),
                    dataTables[6].ToList(returnTypes[6])
                };
            }
            // deal with 6 result sets
            else if (returnTypes.Length == 6)
            {
                return new List<object>
                {
                    dataTables[0].ToList(returnTypes[0]),
                    dataTables[1].ToList(returnTypes[1]),
                    dataTables[2].ToList(returnTypes[2]),
                    dataTables[3].ToList(returnTypes[3]),
                    dataTables[4].ToList(returnTypes[4]),
                    dataTables[5].ToList(returnTypes[5])
                };
            }
            // deal with 5 result sets
            else if (returnTypes.Length == 5)
            {
                return new List<object>
                {
                    dataTables[0].ToList(returnTypes[0]),
                    dataTables[1].ToList(returnTypes[1]),
                    dataTables[2].ToList(returnTypes[2]),
                    dataTables[3].ToList(returnTypes[3]),
                    dataTables[4].ToList(returnTypes[4])
                };
            }
            // deal with 4 result sets
            else if (returnTypes.Length == 4)
            {
                return new List<object>
                {
                    dataTables[0].ToList(returnTypes[0]),
                    dataTables[1].ToList(returnTypes[1]),
                    dataTables[2].ToList(returnTypes[2]),
                    dataTables[3].ToList(returnTypes[3])
                };
            }
            // deal with 3 result sets
            else if (returnTypes.Length == 3)
            {
                return new List<object>
                {
                    dataTables[0].ToList(returnTypes[0]),
                    dataTables[1].ToList(returnTypes[1]),
                    dataTables[2].ToList(returnTypes[2])
                };
            }
            // deal with 2 result sets
            else if (returnTypes.Length == 2)
            {
                return new List<object>
                {
                    dataTables[0].ToList(returnTypes[0]),
                    dataTables[1].ToList(returnTypes[1])
                };
            }
            // deal with 1 result sets
            else
            {
                return new List<object>
                {
                    dataTables[0].ToList(returnTypes[0])
                };
            }
        }

        /// <summary>
        /// Will DataSet change specific type
        /// </summary>
        /// <param name="dataSet">DataSet</param>
        /// <param name="returnTypes">specific typegather</param>
        /// <returns>object</returns>
        public static Task<List<object>> ToListAsync(this DataSet dataSet, params Type[] returnTypes)
        {
            return Task.FromResult(dataSet.ToList(returnTypes));
        }

        /// <summary>
        /// Will DataTable change specific type
        /// </summary>
        /// <param name="dataTable">DataTable</param>
        /// <param name="returnType">return value type</param>
        /// <returns>object</returns>
        public static object ToList(this DataTable dataTable, Type returnType)
        {
            var isGenericType = returnType.IsGenericType;
            // 获取type真实返回type
            var underlyingType = isGenericType ? returnType.GenericTypeArguments.First() : returnType;

            var resultType = typeof(List<>).MakeGenericType(underlyingType);
            var list = Activator.CreateInstance(resultType);
            var addMethod = resultType.GetMethod("Add");

            // Will DataTable change为行gather
            var dataRows = dataTable.AsEnumerable();

            // if是基元type
            if (underlyingType.IsRichPrimitive())
            {
                // iterate over all rows
                foreach (var dataRow in dataRows)
                {
                    // Only take the first column of data
                    var firstColumnValue = dataRow[0];
                    // change换成目标type数据
                    var destValue = firstColumnValue?.ChangeType(underlyingType);
                    // 添加到gather中
                    _ = addMethod.Invoke(list, new[] { destValue });
                }
            }
            // deal withObjecttype
            else if (underlyingType == typeof(object))
            {
                // get all column names
                var columns = dataTable.Columns;

                // iterate over all rows
                foreach (var dataRow in dataRows)
                {
                    var dic = new Dictionary<string, object>();
                    foreach (DataColumn column in columns)
                    {
                        dic.Add(column.ColumnName, dataRow[column]);
                    }

                    _ = addMethod.Invoke(list, new[] { dic });
                }
            }
            else
            {
                // get all数据列和类公开实例属性
                var dataColumns = dataTable.Columns;
                var properties = underlyingType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                //.Where(p => !p.IsDefined(typeof(NotMappedAttribute), true));  // sql 数据change换无需判断 [NotMapperd] characteristic

                // iterate over all rows
                foreach (var dataRow in dataRows)
                {
                    var model = Activator.CreateInstance(underlyingType);

                    // Iterate over all properties and assign values ​​one by one
                    foreach (var property in properties)
                    {
                        // Get the real column name corresponding to the attribute
                        var columnName = property.Name;
                        if (property.IsDefined(typeof(ColumnAttribute), true))
                        {
                            var columnAttribute = property.GetCustomAttribute<ColumnAttribute>(true);
                            if (!string.IsNullOrWhiteSpace(columnAttribute.Name)) columnName = columnAttribute.Name;
                        }

                        // if DataTable does not contain the column name，skip
                        if (!dataColumns.Contains(columnName)) continue;

                        // get column value
                        var columnValue = dataRow[columnName];
                        // if列值未空，skip
                        if (columnValue == DBNull.Value) continue;

                        // change换成目标type数据
                        var destValue = columnValue?.ChangeType(property.PropertyType);
                        property.SetValue(model, destValue);
                    }

                    // 添加到gather中
                    _ = addMethod.Invoke(list, new[] { model });
                }
            }

            return list;
        }

        /// <summary>
        /// Will DataTable change specific type
        /// </summary>
        /// <param name="dataTable">DataTable</param>
        /// <param name="returnType">return value type</param>
        /// <returns>object</returns>
        public static Task<object> ToListAsync(this DataTable dataTable, Type returnType)
        {
            return Task.FromResult(dataTable.ToList(returnType));
        }

        /// <summary>
        /// Will DbDataReader change DataTable
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(this DbDataReader dataReader)
        {
            var dataTable = new DataTable();

            // Create column
            for (var i = 0; i < dataReader.FieldCount; i++)
            {
                var dataClumn = new DataColumn
                {
                    DataType = dataReader.GetFieldType(i),
                    ColumnName = dataReader.GetName(i)
                };

                dataTable.Columns.Add(dataClumn);
            }

            // cyclic read
            while (dataReader.Read())
            {
                // create row
                var dataRow = dataTable.NewRow();
                for (var i = 0; i < dataReader.FieldCount; i++)
                {
                    dataRow[i] = dataReader[i];
                }

                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }

        /// <summary>
        /// Will DbDataReader change DataSet
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public static DataSet ToDataSet(this DbDataReader dataReader)
        {
            var dataSet = new DataSet();

            do
            {
                // get metadata
                var schemaTable = dataReader.GetSchemaTable();
                var dataTable = new DataTable();

                if (schemaTable != null)
                {
                    for (var i = 0; i < schemaTable.Rows.Count; i++)
                    {
                        var dataRow = schemaTable.Rows[i];

                        var columnName = (string)dataRow["ColumnName"];
                        var column = new DataColumn(columnName, (Type)dataRow["DataType"]);
                        dataTable.Columns.Add(column);
                    }

                    dataSet.Tables.Add(dataTable);

                    // cyclic read
                    while (dataReader.Read())
                    {
                        var dataRow = dataTable.NewRow();

                        for (var i = 0; i < dataReader.FieldCount; i++)
                        {
                            dataRow[i] = dataReader.GetValue(i);
                        }

                        dataTable.Rows.Add(dataRow);
                    }
                }
                else
                {
                    var column = new DataColumn("RecordsAffected");
                    dataTable.Columns.Add(column);
                    dataSet.Tables.Add(dataTable);

                    var dataRow = dataTable.NewRow();
                    dataRow[0] = dataReader.RecordsAffected;
                    dataTable.Rows.Add(dataRow);
                }
            }

            // read next result
            while (dataReader.NextResult());

            return dataSet;
        }

        /// <summary>
        /// deal withtupletype返回值
        /// </summary>
        /// <param name="dataSet">data set</param>
        /// <param name="tupleType">return value type</param>
        /// <returns></returns>
        internal static object ToValueTuple(this DataSet dataSet, Type tupleType)
        {
            // 获取tuple最底层type
            var underlyingTypes = tupleType.GetGenericArguments()
                .Select(u => u.IsGenericType ? u.GetGenericArguments().First() : u);

            var toListMethod = typeof(DbDataConvertExtensions)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .First(u => u.Name == "ToList" && u.IsGenericMethod &&
                            u.GetGenericArguments().Length == tupleType.GetGenericArguments().Length)
                .MakeGenericMethod(underlyingTypes.ToArray());

            return toListMethod.Invoke(null, new[] { dataSet });
        }
    }
}