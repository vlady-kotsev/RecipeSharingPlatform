using CarRentalSystem.Services;
using CarRentalSystem.Utils;

namespace CarRentalSystem.Managers
{
    /// <summary>
    /// Manager class that handles user commands and orchestrates the application
    /// </summary>
    public class RentalManager
    {
        private readonly CarRentalService _service;
        private readonly string _dataFilePath;

        public RentalManager(CarRentalService service, string dataFilePath)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _dataFilePath = dataFilePath ?? throw new ArgumentNullException(nameof(dataFilePath));
        }

        /// <summary>
        /// Execute a user command
        /// </summary>
        /// <param name="command">Command to execute</param>
        /// <returns>True if application should continue running</returns>
        public bool ExecuteCommand(string command)
        {
            try
            {
                var parts = command.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 0)
                {
                    ConsoleHelper.DisplayError("Please enter a valid command.");
                    return true;
                }

                var action = parts[0].ToLower();
                var args = parts.Skip(1).ToArray();

                return action switch
                {
                    "1" or "add" => HandleAddCar(args),
                    "2" or "edit" => HandleEditCar(args),
                    "3" or "remove" => HandleRemoveCar(args),
                    "4" or "list" => HandleListCars(),
                    "5" or "rent" => HandleRentCar(args),
                    "6" or "return" => HandleReturnCar(args),
                    "7" or "search" => HandleSearchCars(args),
                    "8" or "stats" => HandleShowStatistics(),
                    "9" or "save" => HandleSaveAndExit(),
                    "0" or "exit" => HandleExit(),
                    "help" => HandleHelp(),
                    _ => HandleUnknownCommand(command)
                };
            }
            catch (Exception ex)
            {
                ConsoleHelper.DisplayError($"Error executing command: {ex.Message}");
                return true;
            }
        }

        /// <summary>
        /// Handle adding a new car
        /// </summary>
        /// <param name="args">Command arguments</param>
        /// <returns>True to continue</returns>
        private bool HandleAddCar(string[] args)
        {
            ConsoleHelper.DisplayInfo("=== ADD NEW CAR ===");
            
            var make = ConsoleHelper.GetUserInput("Enter car make: ");
            if (!ValidationHelper.IsValidMake(make))
            {
                ConsoleHelper.DisplayError(ValidationHelper.GetMakeValidationError(make));
                return true;
            }

            var model = ConsoleHelper.GetUserInput("Enter car model: ");
            if (!ValidationHelper.IsValidModel(model))
            {
                ConsoleHelper.DisplayError(ValidationHelper.GetModelValidationError(model));
                return true;
            }

            var year = ConsoleHelper.GetIntInput("Enter car year: ", 1900, DateTime.Now.Year + 1);
            if (!ValidationHelper.IsValidYear(year))
            {
                ConsoleHelper.DisplayError(ValidationHelper.GetYearValidationError(year));
                return true;
            }

            Console.WriteLine("Available types: Sedan, SUV, Hatchback, Coupe, Convertible, Truck, Van, Wagon");
            var type = ConsoleHelper.GetUserInput("Enter car type: ");
            if (!ValidationHelper.IsValidType(type))
            {
                ConsoleHelper.DisplayError(ValidationHelper.GetTypeValidationError(type));
                return true;
            }

            // Sanitize inputs
            make = ValidationHelper.SanitizeInput(make);
            model = ValidationHelper.SanitizeInput(model);
            type = ValidationHelper.SanitizeInput(type);

            if (_service.AddVehicle(make, model, year, type))
            {
                ConsoleHelper.DisplaySuccess("Car added successfully!");
            }

            return true;
        }

        /// <summary>
        /// Handle editing an existing car
        /// </summary>
        /// <param name="args">Command arguments</param>
        /// <returns>True to continue</returns>
        private bool HandleEditCar(string[] args)
        {
            ConsoleHelper.DisplayInfo("=== EDIT CAR ===");
            
            var id = ConsoleHelper.GetIntInput("Enter car ID to edit: ", 1);
            if (!ValidationHelper.IsValidVehicleId(id))
            {
                ConsoleHelper.DisplayError("Invalid vehicle ID.");
                return true;
            }

            var make = ConsoleHelper.GetUserInput("Enter new make (or press Enter to keep current): ");
            var model = ConsoleHelper.GetUserInput("Enter new model (or press Enter to keep current): ");
            var year = ConsoleHelper.GetIntInput("Enter new year (or 0 to keep current): ", 1900, DateTime.Now.Year + 1);
            Console.WriteLine("Available types: Sedan, SUV, Hatchback, Coupe, Convertible, Truck, Van, Wagon");
            var type = ConsoleHelper.GetUserInput("Enter new type (or press Enter to keep current): ");
            var status = ConsoleHelper.GetUserInput("Enter new status (Available/Rented/Removed): ");

            // Use current values if new values are not provided
            var vehicle = _service.GetSearchService().SearchById(id);
            if (vehicle != null)
            {
                make = string.IsNullOrWhiteSpace(make) ? vehicle.Make : ValidationHelper.SanitizeInput(make);
                model = string.IsNullOrWhiteSpace(model) ? vehicle.Model : ValidationHelper.SanitizeInput(model);
                year = year == 0 ? vehicle.Year : year;
                type = string.IsNullOrWhiteSpace(type) ? vehicle.Type : ValidationHelper.SanitizeInput(type);
                status = string.IsNullOrWhiteSpace(status) ? vehicle.Status : ValidationHelper.SanitizeInput(status);

                if (_service.EditVehicle(id, make, model, year, type, status))
                {
                    ConsoleHelper.DisplaySuccess("Car updated successfully!");
                }
            }
            else
            {
                ConsoleHelper.DisplayError($"Car with ID {id} not found.");
            }

            return true;
        }

        /// <summary>
        /// Handle removing a car
        /// </summary>
        /// <param name="args">Command arguments</param>
        /// <returns>True to continue</returns>
        private bool HandleRemoveCar(string[] args)
        {
            ConsoleHelper.DisplayInfo("=== REMOVE CAR ===");
            
            var id = ConsoleHelper.GetIntInput("Enter car ID to remove: ", 1);
            if (!ValidationHelper.IsValidVehicleId(id))
            {
                ConsoleHelper.DisplayError("Invalid vehicle ID.");
                return true;
            }

            if (ConsoleHelper.GetConfirmation($"Are you sure you want to remove car {id}?"))
            {
                if (_service.RemoveVehicle(id))
                {
                    ConsoleHelper.DisplaySuccess("Car removed successfully!");
                }
            }
            else
            {
                ConsoleHelper.DisplayInfo("Operation cancelled.");
            }

            return true;
        }

        /// <summary>
        /// Handle listing all cars
        /// </summary>
        /// <returns>True to continue</returns>
        private bool HandleListCars()
        {
            _service.ListVehicles();
            return true;
        }

        /// <summary>
        /// Handle renting a car
        /// </summary>
        /// <param name="args">Command arguments</param>
        /// <returns>True to continue</returns>
        private bool HandleRentCar(string[] args)
        {
            ConsoleHelper.DisplayInfo("=== RENT CAR ===");
            
            var vehicleId = ConsoleHelper.GetIntInput("Enter car ID to rent: ", 1);
            if (!ValidationHelper.IsValidVehicleId(vehicleId))
            {
                ConsoleHelper.DisplayError("Invalid vehicle ID.");
                return true;
            }

            var customerName = ConsoleHelper.GetUserInput("Enter customer name: ");
            if (!ValidationHelper.IsValidCustomerName(customerName))
            {
                ConsoleHelper.DisplayError(ValidationHelper.GetCustomerNameValidationError(customerName));
                return true;
            }

            var rentalStartDate = ConsoleHelper.GetDateInput("Enter rental start date (YYYY-MM-DD): ", DateTime.Today);
            var expectedReturnDate = ConsoleHelper.GetDateInput("Enter expected return date (YYYY-MM-DD): ", rentalStartDate);

            if (!ValidationHelper.IsValidDateRange(rentalStartDate, expectedReturnDate))
            {
                ConsoleHelper.DisplayError(ValidationHelper.GetDateRangeValidationError(rentalStartDate, expectedReturnDate));
                return true;
            }

            // Sanitize customer name
            customerName = ValidationHelper.SanitizeInput(customerName);

            if (_service.RentVehicle(vehicleId, customerName, rentalStartDate, expectedReturnDate))
            {
                ConsoleHelper.DisplaySuccess("Car rented successfully!");
            }

            return true;
        }

        /// <summary>
        /// Handle returning a car
        /// </summary>
        /// <param name="args">Command arguments</param>
        /// <returns>True to continue</returns>
        private bool HandleReturnCar(string[] args)
        {
            ConsoleHelper.DisplayInfo("=== RETURN CAR ===");
            
            var vehicleId = ConsoleHelper.GetIntInput("Enter car ID to return: ", 1);
            if (!ValidationHelper.IsValidVehicleId(vehicleId))
            {
                ConsoleHelper.DisplayError("Invalid vehicle ID.");
                return true;
            }

            if (_service.ReturnVehicle(vehicleId))
            {
                ConsoleHelper.DisplaySuccess("Car returned successfully!");
            }

            return true;
        }

        /// <summary>
        /// Handle searching cars
        /// </summary>
        /// <param name="args">Command arguments</param>
        /// <returns>True to continue</returns>
        private bool HandleSearchCars(string[] args)
        {
            ConsoleHelper.DisplayInfo("=== SEARCH CARS ===");
            
            var criteria = ConsoleHelper.GetUserInput("Enter search criteria (make, model, or type): ");
            if (string.IsNullOrWhiteSpace(criteria))
            {
                ConsoleHelper.DisplayError("Search criteria cannot be empty.");
                return true;
            }

            _service.SearchVehicles(criteria);
            return true;
        }

        /// <summary>
        /// Handle showing statistics
        /// </summary>
        /// <returns>True to continue</returns>
        private bool HandleShowStatistics()
        {
            _service.ShowStatistics();
            return true;
        }

        /// <summary>
        /// Handle saving and exiting
        /// </summary>
        /// <returns>False to exit</returns>
        private bool HandleSaveAndExit()
        {
            ConsoleHelper.DisplayInfo("Saving data...");
            if (_service.SaveVehicles(_dataFilePath))
            {
                ConsoleHelper.DisplaySuccess("Data saved successfully!");
            }
            else
            {
                ConsoleHelper.DisplayError("Failed to save data.");
            }
            
            ConsoleHelper.DisplayInfo("Thank you for using the Car Rental System!");
            return false;
        }

        /// <summary>
        /// Handle exiting without saving
        /// </summary>
        /// <returns>False to exit</returns>
        private bool HandleExit()
        {
            if (ConsoleHelper.GetConfirmation("Are you sure you want to exit without saving?"))
            {
                ConsoleHelper.DisplayInfo("Thank you for using the Car Rental System!");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Handle help command
        /// </summary>
        /// <returns>True to continue</returns>
        private bool HandleHelp()
        {
            ConsoleHelper.DisplayHelp();
            return true;
        }

        /// <summary>
        /// Handle unknown command
        /// </summary>
        /// <param name="command">Unknown command</param>
        /// <returns>True to continue</returns>
        private bool HandleUnknownCommand(string command)
        {
            ConsoleHelper.DisplayError($"Unknown command: {command}");
            ConsoleHelper.DisplayInfo("Type 'help' for available commands.");
            return true;
        }
    }
} 