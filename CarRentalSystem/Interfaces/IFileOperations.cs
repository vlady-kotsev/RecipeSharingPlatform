namespace CarRentalSystem.Interfaces
{
    /// <summary>
    /// Interface defining file operations for data persistence
    /// </summary>
    /// <typeparam name="T">Type of data to handle</typeparam>
    public interface IFileOperations<T>
    {
        /// <summary>
        /// Read data from file
        /// </summary>
        /// <param name="filePath">Path to the file to read from</param>
        /// <returns>List of items read from file</returns>
        List<T> ReadFromFile(string filePath);
        
        /// <summary>
        /// Write data to file
        /// </summary>
        /// <param name="filePath">Path to the file to write to</param>
        /// <param name="data">Data to write</param>
        /// <returns>True if write was successful, false otherwise</returns>
        bool WriteToFile(string filePath, List<T> data);
        
        /// <summary>
        /// Check if file exists
        /// </summary>
        /// <param name="filePath">Path to check</param>
        /// <returns>True if file exists, false otherwise</returns>
        bool FileExists(string filePath);
    }
} 