namespace CarRentalSystem.Utils
{
    /// <summary>
    /// Helper class for console operations and user interface
    /// </summary>
    public static class ConsoleHelper
    {
        /// <summary>
        /// Display the main menu
        /// </summary>
        public static void DisplayMenu()
        {
            Console.WriteLine("\n=== CAR RENTAL SYSTEM ===");
            Console.WriteLine("1. Add Car");
            Console.WriteLine("2. Edit Car");
            Console.WriteLine("3. Remove Car");
            Console.WriteLine("4. List Cars");
            Console.WriteLine("5. Rent Car");
            Console.WriteLine("6. Return Car");
            Console.WriteLine("7. Search Cars");
            Console.WriteLine("8. Show Statistics");
            Console.WriteLine("9. Save & Exit");
            Console.WriteLine("0. Exit without saving");
            Console.Write("Enter your choice: ");
        }

        /// <summary>
        /// Get user input with validation
        /// </summary>
        /// <param name="prompt">Prompt to display</param>
        /// <returns>User input string</returns>
        public static string GetUserInput(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine() ?? "";
        }

        /// <summary>
        /// Get integer input with validation
        /// </summary>
        /// <param name="prompt">Prompt to display</param>
        /// <param name="minValue">Minimum allowed value</param>
        /// <param name="maxValue">Maximum allowed value</param>
        /// <returns>Validated integer value</returns>
        public static int GetIntInput(string prompt, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value))
                {
                    if (value >= minValue && value <= maxValue)
                    {
                        return value;
                    }
                    Console.WriteLine($"Please enter a value between {minValue} and {maxValue}.");
                }
                else
                {
                    Console.WriteLine("Please enter a valid number.");
                }
            }
        }

        /// <summary>
        /// Get date input with validation
        /// </summary>
        /// <param name="prompt">Prompt to display</param>
        /// <param name="minDate">Minimum allowed date</param>
        /// <returns>Validated date</returns>
        public static DateTime GetDateInput(string prompt, DateTime? minDate = null)
        {
            while (true)
            {
                Console.Write(prompt);
                if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                {
                    if (minDate == null || date >= minDate.Value)
                    {
                        return date;
                    }
                    Console.WriteLine($"Please enter a date on or after {minDate.Value:yyyy-MM-dd}.");
                }
                else
                {
                    Console.WriteLine("Please enter a valid date (YYYY-MM-DD format).");
                }
            }
        }

        /// <summary>
        /// Display a formatted table header
        /// </summary>
        /// <param name="title">Table title</param>
        public static void DisplayTableHeader(string title)
        {
            Console.WriteLine($"\n=== {title.ToUpper()} ===");
        }

        /// <summary>
        /// Display a separator line
        /// </summary>
        /// <param name="length">Length of the separator</param>
        public static void DisplaySeparator(int length = 50)
        {
            Console.WriteLine(new string('-', length));
        }

        /// <summary>
        /// Display success message
        /// </summary>
        /// <param name="message">Success message</param>
        public static void DisplaySuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✓ {message}");
            Console.ResetColor();
        }

        /// <summary>
        /// Display error message
        /// </summary>
        /// <param name="message">Error message</param>
        public static void DisplayError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"✗ {message}");
            Console.ResetColor();
        }

        /// <summary>
        /// Display warning message
        /// </summary>
        /// <param name="message">Warning message</param>
        public static void DisplayWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"⚠ {message}");
            Console.ResetColor();
        }

        /// <summary>
        /// Display info message
        /// </summary>
        /// <param name="message">Info message</param>
        public static void DisplayInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"ℹ {message}");
            Console.ResetColor();
        }

        /// <summary>
        /// Clear the console screen
        /// </summary>
        public static void ClearScreen()
        {
            Console.Clear();
        }

        /// <summary>
        /// Wait for user to press a key
        /// </summary>
        /// <param name="message">Message to display</param>
        public static void WaitForKey(string message = "Press any key to continue...")
        {
            Console.WriteLine(message);
            Console.ReadKey();
        }

        /// <summary>
        /// Display a confirmation prompt
        /// </summary>
        /// <param name="message">Confirmation message</param>
        /// <returns>True if user confirms</returns>
        public static bool GetConfirmation(string message)
        {
            Console.Write($"{message} (y/n): ");
            var response = Console.ReadLine()?.ToLower();
            return response == "y" || response == "yes";
        }

        /// <summary>
        /// Display help information
        /// </summary>
        public static void DisplayHelp()
        {
            Console.WriteLine("\n=== HELP ===");
            Console.WriteLine("Commands:");
            Console.WriteLine("  Add Car: Enter car details (make, model, year, type)");
            Console.WriteLine("  Edit Car: Modify existing car details");
            Console.WriteLine("  Remove Car: Mark a car as removed (cannot be rented)");
            Console.WriteLine("  List Cars: Show all cars in the system");
            Console.WriteLine("  Rent Car: Rent a car to a customer");
            Console.WriteLine("  Return Car: Return a rented car");
            Console.WriteLine("  Search Cars: Search by make, model, or type");
            Console.WriteLine("  Show Statistics: Display vehicle statistics");
            Console.WriteLine("  Save & Exit: Save data and exit");
            Console.WriteLine("  Exit without saving: Exit without saving changes");
        }
    }
} 