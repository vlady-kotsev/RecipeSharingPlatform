namespace CarRentalSystem.Interfaces
{
    /// <summary>
    /// Interface defining search functionality for items
    /// </summary>
    /// <typeparam name="T">Type of items to search</typeparam>
    public interface ISearchable<T>
    {
        /// <summary>
        /// Search items by a specific criteria
        /// </summary>
        /// <param name="criteria">Search criteria</param>
        /// <returns>List of items matching the criteria</returns>
        List<T> Search(string criteria);
        
        /// <summary>
        /// Search items by ID
        /// </summary>
        /// <param name="id">ID to search for</param>
        /// <returns>Item with matching ID or null if not found</returns>
        T? SearchById(int id);
        
        /// <summary>
        /// Search items by status
        /// </summary>
        /// <param name="status">Status to search for</param>
        /// <returns>List of items with matching status</returns>
        List<T> SearchByStatus(string status);
    }
} 