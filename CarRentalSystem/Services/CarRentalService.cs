using CarRentalSystem.Interfaces;
using CarRentalSystem.Models;

namespace CarRentalSystem.Services
{
    /// <summary>
    /// Main service class for car rental operations
    /// Manages the core business logic
    /// </summary>
    public class CarRentalService
    {
        private readonly IFileOperations<Vehicle> _fileService;
        private ISearchable<Vehicle> _searchService;
        private List<Vehicle> _vehicles;
        private List<Rental> _rentals;
        private int _nextVehicleId;
        private int _nextRentalId;

        public CarRentalService(IFileOperations<Vehicle> fileService)
        {
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            _vehicles = new List<Vehicle>();
            _rentals = new List<Rental>();
            _nextVehicleId = 1;
            _nextRentalId = 1;
            _searchService = new SearchService(_vehicles);
        }

        /// <summary>
        /// Load vehicles from file
        /// </summary>
        /// <param name="filePath">Path to the CSV file</param>
        public void LoadVehicles(string filePath)
        {
            try
            {
                _vehicles = _fileService.ReadFromFile(filePath);
                _searchService = new SearchService(_vehicles);
                
                // Update next ID
                if (_vehicles.Any())
                {
                    _nextVehicleId = _vehicles.Max(v => v.Id) + 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading vehicles: {ex.Message}");
                _vehicles = new List<Vehicle>();
            }
        }

        /// <summary>
        /// Save vehicles to file
        /// </summary>
        /// <param name="filePath">Path to the CSV file</param>
        /// <returns>True if save was successful</returns>
        public bool SaveVehicles(string filePath)
        {
            try
            {
                return _fileService.WriteToFile(filePath, _vehicles);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving vehicles: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Add a new vehicle
        /// </summary>
        /// <param name="make">Vehicle make</param>
        /// <param name="model">Vehicle model</param>
        /// <param name="year">Vehicle year</param>
        /// <param name="type">Vehicle type</param>
        /// <returns>True if vehicle was added successfully</returns>
        public bool AddVehicle(string make, string model, int year, string type)
        {
            try
            {
                var vehicle = new Car(_nextVehicleId++, make, model, year, type);
                _vehicles.Add(vehicle);
                _searchService = new SearchService(_vehicles);
                
                Console.WriteLine($"Vehicle added successfully: {vehicle}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding vehicle: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Edit an existing vehicle
        /// </summary>
        /// <param name="id">Vehicle ID</param>
        /// <param name="make">New make</param>
        /// <param name="model">New model</param>
        /// <param name="year">New year</param>
        /// <param name="type">New type</param>
        /// <param name="status">New status</param>
        /// <returns>True if vehicle was edited successfully</returns>
        public bool EditVehicle(int id, string make, string model, int year, string type, string status)
        {
            try
            {
                var vehicle = _searchService.SearchById(id);
                if (vehicle == null)
                {
                    Console.WriteLine($"Vehicle with ID {id} not found.");
                    return false;
                }

                vehicle.Make = make;
                vehicle.Model = model;
                vehicle.Year = year;
                vehicle.Type = type;
                vehicle.Status = status;

                Console.WriteLine($"Vehicle updated successfully: {vehicle}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error editing vehicle: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Remove a vehicle (mark as removed)
        /// </summary>
        /// <param name="id">Vehicle ID</param>
        /// <returns>True if vehicle was removed successfully</returns>
        public bool RemoveVehicle(int id)
        {
            try
            {
                var vehicle = _searchService.SearchById(id);
                if (vehicle == null)
                {
                    Console.WriteLine($"Vehicle with ID {id} not found.");
                    return false;
                }

                if (!vehicle.IsAvailable())
                {
                    Console.WriteLine($"Cannot remove vehicle {id} - it is currently rented.");
                    return false;
                }

                vehicle.Status = "Removed";
                Console.WriteLine($"Vehicle {id} marked as removed.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing vehicle: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// List all vehicles
        /// </summary>
        public void ListVehicles()
        {
            if (!_vehicles.Any())
            {
                Console.WriteLine("No vehicles found.");
                return;
            }

            Console.WriteLine("\n=== VEHICLE LIST ===");
            Console.WriteLine($"{"ID",-5} {"Make",-15} {"Model",-15} {"Year",-6} {"Type",-12} {"Status",-12} {"Renter",-20}");
            Console.WriteLine(new string('-', 85));

            foreach (var vehicle in _vehicles.Where(v => v.Status != "Removed"))
            {
                Console.WriteLine($"{vehicle.Id,-5} {vehicle.Make,-15} {vehicle.Model,-15} {vehicle.Year,-6} {vehicle.Type,-12} {vehicle.Status,-12} {vehicle.CurrentRenter,-20}");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Rent a vehicle
        /// </summary>
        /// <param name="vehicleId">Vehicle ID</param>
        /// <param name="customerName">Customer name</param>
        /// <param name="rentalStartDate">Rental start date</param>
        /// <param name="expectedReturnDate">Expected return date</param>
        /// <returns>True if rental was successful</returns>
        public bool RentVehicle(int vehicleId, string customerName, DateTime rentalStartDate, DateTime expectedReturnDate)
        {
            try
            {
                var vehicle = _searchService.SearchById(vehicleId);
                if (vehicle == null)
                {
                    Console.WriteLine($"Vehicle with ID {vehicleId} not found.");
                    return false;
                }

                if (!vehicle.IsAvailable())
                {
                    Console.WriteLine($"Vehicle {vehicleId} is not available for rental.");
                    return false;
                }

                // Create rental record
                var rental = new Rental(_nextRentalId++, vehicleId, 0, customerName, rentalStartDate, expectedReturnDate, 50.0m);
                _rentals.Add(rental);

                // Rent the vehicle
                if (vehicle.Rent(customerName, rentalStartDate, expectedReturnDate))
                {
                    Console.WriteLine($"Vehicle {vehicleId} rented successfully to {customerName}.");
                    Console.WriteLine($"Rental ID: {rental.Id}, Expected Return: {expectedReturnDate:yyyy-MM-dd}");
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error renting vehicle: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Return a vehicle
        /// </summary>
        /// <param name="vehicleId">Vehicle ID</param>
        /// <returns>True if return was successful</returns>
        public bool ReturnVehicle(int vehicleId)
        {
            try
            {
                var vehicle = _searchService.SearchById(vehicleId);
                if (vehicle == null)
                {
                    Console.WriteLine($"Vehicle with ID {vehicleId} not found.");
                    return false;
                }

                if (vehicle.IsAvailable())
                {
                    Console.WriteLine($"Vehicle {vehicleId} is already available.");
                    return false;
                }

                // Find active rental
                var rental = _rentals.FirstOrDefault(r => r.VehicleId == vehicleId && r.Status == "Active");
                if (rental != null)
                {
                    rental.CompleteRental();
                    Console.WriteLine($"Rental completed. Total cost: ${rental.TotalCost:F2}");
                }

                if (vehicle.Return())
                {
                    Console.WriteLine($"Vehicle {vehicleId} returned successfully.");
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error returning vehicle: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Search vehicles
        /// </summary>
        /// <param name="criteria">Search criteria</param>
        public void SearchVehicles(string criteria)
        {
            var results = _searchService.Search(criteria);
            
            if (!results.Any())
            {
                Console.WriteLine($"No vehicles found matching '{criteria}'.");
                return;
            }

            Console.WriteLine($"\n=== SEARCH RESULTS FOR '{criteria}' ===");
            Console.WriteLine($"{"ID",-5} {"Make",-15} {"Model",-15} {"Year",-6} {"Type",-12} {"Status",-12}");
            Console.WriteLine(new string('-', 65));

            foreach (var vehicle in results)
            {
                Console.WriteLine($"{vehicle.Id,-5} {vehicle.Make,-15} {vehicle.Model,-15} {vehicle.Year,-6} {vehicle.Type,-12} {vehicle.Status,-12}");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Get vehicle statistics
        /// </summary>
        public void ShowStatistics()
        {
            var searchService = _searchService as SearchService;
            if (searchService != null)
            {
                var stats = searchService.GetVehicleStatistics();
                
                Console.WriteLine("\n=== VEHICLE STATISTICS ===");
                Console.WriteLine($"Total Vehicles: {stats["Total"]}");
                Console.WriteLine($"Available: {stats["Available"]}");
                Console.WriteLine($"Rented: {stats["Rented"]}");
                Console.WriteLine();

                // Show type breakdown
                Console.WriteLine("By Type:");
                foreach (var stat in stats.Where(s => s.Key.StartsWith("Type_")))
                {
                    var type = stat.Key.Replace("Type_", "");
                    Console.WriteLine($"  {type}: {stat.Value}");
                }
                Console.WriteLine();

                // Show make breakdown
                Console.WriteLine("By Make:");
                foreach (var stat in stats.Where(s => s.Key.StartsWith("Make_")))
                {
                    var make = stat.Key.Replace("Make_", "");
                    Console.WriteLine($"  {make}: {stat.Value}");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Get all vehicles
        /// </summary>
        /// <returns>List of all vehicles</returns>
        public List<Vehicle> GetAllVehicles()
        {
            return _vehicles.ToList();
        }

        /// <summary>
        /// Get search service
        /// </summary>
        /// <returns>Search service instance</returns>
        public ISearchable<Vehicle> GetSearchService()
        {
            return _searchService;
        }

        /// <summary>
        /// Get search service as SearchService for extended functionality
        /// </summary>
        /// <returns>SearchService instance or null</returns>
        public SearchService? GetSearchServiceExtended()
        {
            return _searchService as SearchService;
        }
    }
} 