namespace System.Collections.Generic
{
    /// <summary>
    /// Paginated Generic Collections
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class PagedList<TEntity>
        where TEntity : new()
    {
        /// <summary>
        /// total number
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// total pages
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// current page collection
        /// </summary>
        public IEnumerable<TEntity> Items { get; set; }

        /// <summary>
        /// Is there a previous page
        /// </summary>
        public bool HasPrevPages { get; set; }

        /// <summary>
        /// Is there a next page
        /// </summary>
        public bool HasNextPages { get; set; }
    }

    /// <summary>
    /// Paginated collection
    /// </summary>
    public class PagedList : PagedList<object>
    {
    }
}