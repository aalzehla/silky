using System.Collections.Generic;
using System.Data;

namespace Silky.EntityFrameworkCore.Values
{
    /// <summary>
    /// Stored procedure output return value
    /// </summary>
    public sealed class ProcedureOutputResult : ProcedureOutputResult<DataSet>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ProcedureOutputResult() : base()
        {
        }
    }

    /// <summary>
    /// Stored procedure output return value
    /// </summary>
    /// <typeparam name="TResult">generic version</typeparam>
    public class ProcedureOutputResult<TResult>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ProcedureOutputResult()
        {
            OutputValues = new List<ProcedureOutputValue>();
        }

        /// <summary>
        /// output value
        /// </summary>
        public IEnumerable<ProcedureOutputValue> OutputValues { get; set; }

        /// <summary>
        /// return value
        /// </summary>
        public object ReturnValue { get; set; }

        /// <summary>
        /// result set
        /// </summary>
        public TResult Result { get; set; }
    }
}