using CarRentalSystem.Managers;
using CarRentalSystem.Services;
using CarRentalSystem.Utils;

namespace CarRentalSystem
{
    /// <summary>
    /// Main program class for the Car Rental System
    /// Demonstrates the complete application flow and OOP principles
    /// </summary>
    class Program
    {
        private const string DATA_FILE_PATH = "Data/cars.csv";

        static void Main(string[] args)
        {
            try
            {
                // Initialize the application
                InitializeApplication();

                // Create services and manager
                var fileService = new FileService();
                var carRentalService = new CarRentalService(fileService);
                var rentalManager = new RentalManager(carRentalService, DATA_FILE_PATH);

                // Load existing data
                ConsoleHelper.DisplayInfo("Loading vehicle data...");
                carRentalService.LoadVehicles(DATA_FILE_PATH);

                // Display welcome message
                DisplayWelcomeMessage();

                // Main application loop
                RunApplication(rentalManager);
            }
            catch (Exception ex)
            {
                ConsoleHelper.DisplayError($"Application error: {ex.Message}");
                ConsoleHelper.DisplayError("Please check the error and try again.");
                ConsoleHelper.WaitForKey("Press any key to exit...");
            }
        }

        /// <summary>
        /// Initialize the application
        /// </summary>
        private static void InitializeApplication()
        {
            // Set console title
            Console.Title = "Car Rental System - OOP Console Application";
            
            // Set console colors
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            
            // Clear console
            Console.Clear();
        }

        /// <summary>
        /// Display welcome message
        /// </summary>
        private static void DisplayWelcomeMessage()
        {
            ConsoleHelper.DisplayInfo("Welcome to the Car Rental System!");
            ConsoleHelper.DisplayInfo("This application demonstrates Object-Oriented Programming principles:");
            Console.WriteLine("  • Encapsulation: Private fields with public properties");
            Console.WriteLine("  • Inheritance: Car inherits from Vehicle");
            Console.WriteLine("  • Polymorphism: Different implementations of abstract methods");
            Console.WriteLine("  • Abstraction: Abstract Vehicle class and interfaces");
            Console.WriteLine();
            ConsoleHelper.DisplayInfo("Type 'help' for available commands or use the menu below.");
        }

        /// <summary>
        /// Run the main application loop
        /// </summary>
        /// <param name="rentalManager">Rental manager instance</param>
        private static void RunApplication(RentalManager rentalManager)
        {
            bool isRunning = true;

            while (isRunning)
            {
                try
                {
                    // Display menu
                    ConsoleHelper.DisplayMenu();

                    // Get user input
                    var command = Console.ReadLine() ?? "";

                    // Execute command
                    isRunning = rentalManager.ExecuteCommand(command);

                    // Add some spacing for better readability
                    if (isRunning)
                    {
                        Console.WriteLine();
                    }
                }
                catch (Exception ex)
                {
                    ConsoleHelper.DisplayError($"Error in main loop: {ex.Message}");
                    ConsoleHelper.WaitForKey("Press any key to continue...");
                }
            }
        }
    }
}
