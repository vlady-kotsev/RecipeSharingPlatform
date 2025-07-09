namespace CarRentalSystem.Utils
{
    /// <summary>
    /// Helper class for input validation
    /// </summary>
    public static class ValidationHelper
    {
        /// <summary>
        /// Validate vehicle make
        /// </summary>
        /// <param name="make">Make to validate</param>
        /// <returns>True if valid</returns>
        public static bool IsValidMake(string make)
        {
            return !string.IsNullOrWhiteSpace(make) && make.Length <= 50;
        }

        /// <summary>
        /// Validate vehicle model
        /// </summary>
        /// <param name="model">Model to validate</param>
        /// <returns>True if valid</returns>
        public static bool IsValidModel(string model)
        {
            return !string.IsNullOrWhiteSpace(model) && model.Length <= 50;
        }

        /// <summary>
        /// Validate vehicle year
        /// </summary>
        /// <param name="year">Year to validate</param>
        /// <returns>True if valid</returns>
        public static bool IsValidYear(int year)
        {
            return year >= 1900 && year <= DateTime.Now.Year + 1;
        }

        /// <summary>
        /// Validate vehicle type
        /// </summary>
        /// <param name="type">Type to validate</param>
        /// <returns>True if valid</returns>
        public static bool IsValidType(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                return false;

            var validTypes = new[] { "Sedan", "SUV", "Hatchback", "Coupe", "Convertible", "Truck", "Van", "Wagon" };
            return validTypes.Contains(type, StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Validate customer name
        /// </summary>
        /// <param name="name">Name to validate</param>
        /// <returns>True if valid</returns>
        public static bool IsValidCustomerName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && name.Length <= 100;
        }

        /// <summary>
        /// Validate email address
        /// </summary>
        /// <param name="email">Email to validate</param>
        /// <returns>True if valid</returns>
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Validate phone number
        /// </summary>
        /// <param name="phone">Phone to validate</param>
        /// <returns>True if valid</returns>
        public static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            // Remove all non-digit characters
            var digits = new string(phone.Where(char.IsDigit).ToArray());
            return digits.Length >= 10 && digits.Length <= 15;
        }

        /// <summary>
        /// Validate date range
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>True if valid</returns>
        public static bool IsValidDateRange(DateTime startDate, DateTime endDate)
        {
            return startDate < endDate && startDate >= DateTime.Today;
        }

        /// <summary>
        /// Validate vehicle ID
        /// </summary>
        /// <param name="id">ID to validate</param>
        /// <returns>True if valid</returns>
        public static bool IsValidVehicleId(int id)
        {
            return id > 0;
        }

        /// <summary>
        /// Get validation error message for make
        /// </summary>
        /// <param name="make">Make that failed validation</param>
        /// <returns>Error message</returns>
        public static string GetMakeValidationError(string make)
        {
            if (string.IsNullOrWhiteSpace(make))
                return "Make cannot be empty.";
            if (make.Length > 50)
                return "Make cannot exceed 50 characters.";
            return "Invalid make.";
        }

        /// <summary>
        /// Get validation error message for model
        /// </summary>
        /// <param name="model">Model that failed validation</param>
        /// <returns>Error message</returns>
        public static string GetModelValidationError(string model)
        {
            if (string.IsNullOrWhiteSpace(model))
                return "Model cannot be empty.";
            if (model.Length > 50)
                return "Model cannot exceed 50 characters.";
            return "Invalid model.";
        }

        /// <summary>
        /// Get validation error message for year
        /// </summary>
        /// <param name="year">Year that failed validation</param>
        /// <returns>Error message</returns>
        public static string GetYearValidationError(int year)
        {
            if (year < 1900)
                return "Year cannot be before 1900.";
            if (year > DateTime.Now.Year + 1)
                return $"Year cannot be after {DateTime.Now.Year + 1}.";
            return "Invalid year.";
        }

        /// <summary>
        /// Get validation error message for type
        /// </summary>
        /// <param name="type">Type that failed validation</param>
        /// <returns>Error message</returns>
        public static string GetTypeValidationError(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                return "Type cannot be empty.";
            
            var validTypes = new[] { "Sedan", "SUV", "Hatchback", "Coupe", "Convertible", "Truck", "Van", "Wagon" };
            return $"Type must be one of: {string.Join(", ", validTypes)}.";
        }

        /// <summary>
        /// Get validation error message for customer name
        /// </summary>
        /// <param name="name">Name that failed validation</param>
        /// <returns>Error message</returns>
        public static string GetCustomerNameValidationError(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return "Customer name cannot be empty.";
            if (name.Length > 100)
                return "Customer name cannot exceed 100 characters.";
            return "Invalid customer name.";
        }

        /// <summary>
        /// Get validation error message for email
        /// </summary>
        /// <param name="email">Email that failed validation</param>
        /// <returns>Error message</returns>
        public static string GetEmailValidationError(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return "Email cannot be empty.";
            return "Invalid email format.";
        }

        /// <summary>
        /// Get validation error message for phone
        /// </summary>
        /// <param name="phone">Phone that failed validation</param>
        /// <returns>Error message</returns>
        public static string GetPhoneValidationError(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return "Phone number cannot be empty.";
            return "Invalid phone number format.";
        }

        /// <summary>
        /// Get validation error message for date range
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>Error message</returns>
        public static string GetDateRangeValidationError(DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate)
                return "Start date must be before end date.";
            if (startDate < DateTime.Today)
                return "Start date cannot be in the past.";
            return "Invalid date range.";
        }

        /// <summary>
        /// Sanitize input string
        /// </summary>
        /// <param name="input">Input to sanitize</param>
        /// <returns>Sanitized string</returns>
        public static string SanitizeInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "";

            // Remove potentially dangerous characters
            return input.Trim()
                       .Replace("<", "")
                       .Replace(">", "")
                       .Replace("\"", "")
                       .Replace("'", "")
                       .Replace("&", "and");
        }

        /// <summary>
        /// Format phone number
        /// </summary>
        /// <param name="phone">Phone number to format</param>
        /// <returns>Formatted phone number</returns>
        public static string FormatPhoneNumber(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return "";

            var digits = new string(phone.Where(char.IsDigit).ToArray());
            
            if (digits.Length == 10)
                return $"({digits.Substring(0, 3)}) {digits.Substring(3, 3)}-{digits.Substring(6, 4)}";
            if (digits.Length == 11 && digits[0] == '1')
                return $"+1 ({digits.Substring(1, 3)}) {digits.Substring(4, 3)}-{digits.Substring(7, 4)}";
            
            return phone; // Return original if can't format
        }
    }
} 