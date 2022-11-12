namespace Silky.EntityFrameworkCore.Values
{
    internal enum DbFunctionType
    {
        /// <summary>
        /// Scalar function
        /// </summary>
        Scalar,

        /// <summary>
        /// table-valued functions
        /// </summary>
        Table
    }
}