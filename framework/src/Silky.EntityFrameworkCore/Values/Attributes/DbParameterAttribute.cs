using System.Data;

namespace System.ComponentModel.DataAnnotations.Schema
{
    /// <summary>
    /// DbParameter Configuration Features
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DbParameterAttribute : Attribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DbParameterAttribute()
        {
            Direction = ParameterDirection.Input;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="direction">parameter direction</param>
        public DbParameterAttribute(ParameterDirection direction)
        {
            Direction = direction;
        }

        /// <summary>
        /// Parameter output direction
        /// </summary>
        public ParameterDirection Direction { get; set; }

        /// <summary>
        /// database type
        /// </summary>
        public object DbType { get; set; }

        /// <summary>
        /// size
        /// </summary>
        /// <remarks>Nvarchar/varchartype to be specified</remarks>
        public int Size { get; set; }
    }
}