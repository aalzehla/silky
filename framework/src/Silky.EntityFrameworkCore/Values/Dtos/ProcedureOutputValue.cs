namespace Silky.EntityFrameworkCore.Values
{
    /// <summary>
    /// Stored procedure output value model
    /// </summary>
    public sealed class ProcedureOutputValue
    {
        /// <summary>
        /// output parameter name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// output parameter value
        /// </summary>
        public object Value { get; set; }
    }
}