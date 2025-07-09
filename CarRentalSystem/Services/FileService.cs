using CarRentalSystem.Interfaces;
using CarRentalSystem.Models;

namespace CarRentalSystem.Services
{
    /// <summary>
    /// Service class for handling file operations (CSV reading/writing)
    /// Implements IFileOperations interface
    /// </summary>
    public class FileService : IFileOperations<Vehicle>
    {
        /// <summary>
        /// Read vehicles from CSV file
        /// </summary>
        /// <param name="filePath">Path to the CSV file</param>
        /// <returns>List of vehicles read from file</returns>
        public List<Vehicle> ReadFromFile(string filePath)
        {
            var vehicles = new List<Vehicle>();

            try
            {
                if (!FileExists(filePath))
                {
                    Console.WriteLine($"File {filePath} does not exist. Creating new file.");
                    return vehicles;
                }

                var lines = File.ReadAllLines(filePath);
                
                // Skip header line if it exists
                var dataLines = lines.Length > 0 && lines[0].Contains("Id,Make,Model") 
                    ? lines.Skip(1).ToArray() 
                    : lines;

                foreach (var line in dataLines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        try
                        {
                            var vehicle = Vehicle.FromCsv(line);
                            vehicles.Add(vehicle);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error parsing line: {line}. Error: {ex.Message}");
                        }
                    }
                }

                Console.WriteLine($"Successfully loaded {vehicles.Count} vehicles from {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file {filePath}: {ex.Message}");
                throw;
            }

            return vehicles;
        }

        /// <summary>
        /// Write vehicles to CSV file
        /// </summary>
        /// <param name="filePath">Path to the CSV file</param>
        /// <param name="data">List of vehicles to write</param>
        /// <returns>True if write was successful</returns>
        public bool WriteToFile(string filePath, List<Vehicle> data)
        {
            try
            {
                // Create directory if it doesn't exist
                var directory = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var lines = new List<string>();
                
                // Add header
                lines.Add("Id,Make,Model,Year,Type,Status,CurrentRenter,FuelType,Mileage,Transmission");

                // Add data lines
                foreach (var vehicle in data)
                {
                    lines.Add(vehicle.ToCsv());
                }

                File.WriteAllLines(filePath, lines);
                Console.WriteLine($"Successfully saved {data.Count} vehicles to {filePath}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to file {filePath}: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Check if file exists
        /// </summary>
        /// <param name="filePath">Path to check</param>
        /// <returns>True if file exists</returns>
        public bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// Create a backup of the current file
        /// </summary>
        /// <param name="filePath">Path to the file to backup</param>
        /// <returns>True if backup was successful</returns>
        public bool CreateBackup(string filePath)
        {
            try
            {
                if (!FileExists(filePath))
                {
                    return false;
                }

                var backupPath = $"{filePath}.backup_{DateTime.Now:yyyyMMdd_HHmmss}";
                File.Copy(filePath, backupPath);
                Console.WriteLine($"Backup created: {backupPath}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating backup: {ex.Message}");
                return false;
            }
        }
    }
} 