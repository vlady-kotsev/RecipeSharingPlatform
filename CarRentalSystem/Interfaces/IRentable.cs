namespace CarRentalSystem.Interfaces
{
    /// <summary>
    /// Interface defining the contract for rentable items
    /// </summary>
    public interface IRentable
    {
        /// <summary>
        /// Unique identifier for the rentable item
        /// </summary>
        int Id { get; set; }
        
        /// <summary>
        /// Current availability status of the item
        /// </summary>
        string Status { get; set; }
        
        /// <summary>
        /// Method to rent the item
        /// </summary>
        /// <param name="customerName">Name of the customer renting the item</param>
        /// <param name="rentalStartDate">Start date of the rental</param>
        /// <param name="expectedReturnDate">Expected return date</param>
        /// <returns>True if rental was successful, false otherwise</returns>
        bool Rent(string customerName, DateTime rentalStartDate, DateTime expectedReturnDate);
        
        /// <summary>
        /// Method to return the item
        /// </summary>
        /// <returns>True if return was successful, false otherwise</returns>
        bool Return();
        
        /// <summary>
        /// Method to check if the item is available for rental
        /// </summary>
        /// <returns>True if available, false otherwise</returns>
        bool IsAvailable();
    }
} 