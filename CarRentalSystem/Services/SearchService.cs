using CarRentalSystem.Interfaces;
using CarRentalSystem.Models;

namespace CarRentalSystem.Services
{
    /// <summary>
    /// Service class for searching vehicles
    /// Implements ISearchable interface
    /// </summary>
    public class SearchService : ISearchable<Vehicle>
    {
        private readonly List<Vehicle> _vehicles;

        public SearchService(List<Vehicle> vehicles)
        {
            _vehicles = vehicles ?? new List<Vehicle>();
        }

        /// <summary>
        /// Search vehicles by various criteria
        /// </summary>
        /// <param name="criteria">Search criteria (model, make, type)</param>
        /// <returns>List of matching vehicles</returns>
        public List<Vehicle> Search(string criteria)
        {
            if (string.IsNullOrWhiteSpace(criteria))
            {
                return new List<Vehicle>();
            }

            criteria = criteria.ToLower();
            
            return _vehicles.Where(v => 
                v.Make.ToLower().Contains(criteria) ||
                v.Model.ToLower().Contains(criteria) ||
                v.Type.ToLower().Contains(criteria) ||
                v.Status.ToLower().Contains(criteria)
            ).ToList();
        }

        /// <summary>
        /// Search vehicle by ID
        /// </summary>
        /// <param name="id">Vehicle ID to search for</param>
        /// <returns>Vehicle with matching ID or null</returns>
        public Vehicle? SearchById(int id)
        {
            return _vehicles.FirstOrDefault(v => v.Id == id);
        }

        /// <summary>
        /// Search vehicles by status
        /// </summary>
        /// <param name="status">Status to search for</param>
        /// <returns>List of vehicles with matching status</returns>
        public List<Vehicle> SearchByStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                return new List<Vehicle>();
            }

            return _vehicles.Where(v => 
                v.Status.Equals(status, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }

        /// <summary>
        /// Search vehicles by make
        /// </summary>
        /// <param name="make">Make to search for</param>
        /// <returns>List of vehicles with matching make</returns>
        public List<Vehicle> SearchByMake(string make)
        {
            if (string.IsNullOrWhiteSpace(make))
            {
                return new List<Vehicle>();
            }

            return _vehicles.Where(v => 
                v.Make.Equals(make, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }

        /// <summary>
        /// Search vehicles by model
        /// </summary>
        /// <param name="model">Model to search for</param>
        /// <returns>List of vehicles with matching model</returns>
        public List<Vehicle> SearchByModel(string model)
        {
            if (string.IsNullOrWhiteSpace(model))
            {
                return new List<Vehicle>();
            }

            return _vehicles.Where(v => 
                v.Model.ToLower().Contains(model.ToLower())
            ).ToList();
        }

        /// <summary>
        /// Search vehicles by year range
        /// </summary>
        /// <param name="startYear">Start year</param>
        /// <param name="endYear">End year</param>
        /// <returns>List of vehicles within year range</returns>
        public List<Vehicle> SearchByYearRange(int startYear, int endYear)
        {
            return _vehicles.Where(v => v.Year >= startYear && v.Year <= endYear).ToList();
        }

        /// <summary>
        /// Get all available vehicles
        /// </summary>
        /// <returns>List of available vehicles</returns>
        public List<Vehicle> GetAvailableVehicles()
        {
            return _vehicles.Where(v => v.IsAvailable()).ToList();
        }

        /// <summary>
        /// Get all rented vehicles
        /// </summary>
        /// <returns>List of rented vehicles</returns>
        public List<Vehicle> GetRentedVehicles()
        {
            return _vehicles.Where(v => !v.IsAvailable()).ToList();
        }

        /// <summary>
        /// Get statistics about vehicles
        /// </summary>
        /// <returns>Dictionary with vehicle statistics</returns>
        public Dictionary<string, int> GetVehicleStatistics()
        {
            var stats = new Dictionary<string, int>
            {
                ["Total"] = _vehicles.Count,
                ["Available"] = _vehicles.Count(v => v.IsAvailable()),
                ["Rented"] = _vehicles.Count(v => !v.IsAvailable())
            };

            // Group by type
            var typeGroups = _vehicles.GroupBy(v => v.Type);
            foreach (var group in typeGroups)
            {
                stats[$"Type_{group.Key}"] = group.Count();
            }

            // Group by make
            var makeGroups = _vehicles.GroupBy(v => v.Make);
            foreach (var group in makeGroups)
            {
                stats[$"Make_{group.Key}"] = group.Count();
            }

            return stats;
        }
    }
} 