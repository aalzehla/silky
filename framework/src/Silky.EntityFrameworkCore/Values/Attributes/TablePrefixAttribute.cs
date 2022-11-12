namespace System.ComponentModel.DataAnnotations.Schema
{
    /// <summary>
    /// Configuration table name prefix
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TablePrefixAttribute : Attribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="prefix"></param>
        public TablePrefixAttribute(string prefix)
        {
            Prefix = prefix;
        }

        /// <summary>
        /// prefix
        /// </summary>
        public string Prefix { get; set; }
    }
}