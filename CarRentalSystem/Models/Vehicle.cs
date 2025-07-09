using CarRentalSystem.Interfaces;

namespace CarRentalSystem.Models
{
    /// <summary>
    /// Abstract base class for all vehicles in the rental system
    /// Demonstrates abstraction and inheritance principles
    /// </summary>
    public abstract class Vehicle : IRentable
    {
        // Private fields - encapsulation
        private int _id;
        private string _make = string.Empty;
        private string _model = string.Empty;
        private int _year;
        private string _type = string.Empty;
        private string _status = string.Empty;
        private string _currentRenter = string.Empty;
        private DateTime? _rentalStartDate;
        private DateTime? _expectedReturnDate;

        // Properties with validation - encapsulation
        public int Id 
        { 
            get => _id;
            set => _id = value > 0 ? value : throw new ArgumentException("ID must be positive");
        }
        
        public string Make 
        { 
            get => _make;
            set => _make = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("Make cannot be empty");
        }
        
        public string Model 
        { 
            get => _model;
            set => _model = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("Model cannot be empty");
        }
        
        public int Year 
        { 
            get => _year;
            set => _year = value >= 1900 && value <= DateTime.Now.Year + 1 ? value : throw new ArgumentException("Invalid year");
        }
        
        public string Type 
        { 
            get => _type;
            set => _type = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("Type cannot be empty");
        }
        
        public string Status 
        { 
            get => _status;
            set => _status = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("Status cannot be empty");
        }
        
        public string CurrentRenter 
        { 
            get => _currentRenter;
            set => _currentRenter = value;
        }
        
        public DateTime? RentalStartDate 
        { 
            get => _rentalStartDate;
            set => _rentalStartDate = value;
        }
        
        public DateTime? ExpectedReturnDate 
        { 
            get => _expectedReturnDate;
            set => _expectedReturnDate = value;
        }

        // Constructor
        protected Vehicle(int id, string make, string model, int year, string type)
        {
            Id = id;
            Make = make;
            Model = model;
            Year = year;
            Type = type;
            Status = "Available";
            CurrentRenter = "";
        }

        // Abstract method - must be implemented by derived classes
        public abstract string GetVehicleInfo();

        // Virtual method - can be overridden by derived classes
        public virtual bool Rent(string customerName, DateTime rentalStartDate, DateTime expectedReturnDate)
        {
            if (!IsAvailable())
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(customerName))
            {
                throw new ArgumentException("Customer name cannot be empty");
            }

            if (rentalStartDate >= expectedReturnDate)
            {
                throw new ArgumentException("Rental start date must be before expected return date");
            }

            Status = "Rented";
            CurrentRenter = customerName;
            RentalStartDate = rentalStartDate;
            ExpectedReturnDate = expectedReturnDate;
            
            return true;
        }

        public virtual bool Return()
        {
            if (IsAvailable())
            {
                return false; // Already available
            }

            Status = "Available";
            CurrentRenter = "";
            RentalStartDate = null;
            ExpectedReturnDate = null;
            
            return true;
        }

        public virtual bool IsAvailable()
        {
            return Status.Equals("Available", StringComparison.OrdinalIgnoreCase);
        }

        // Override ToString for easy display
        public override string ToString()
        {
            return $"ID: {Id}, {Make} {Model} ({Year}), Type: {Type}, Status: {Status}" +
                   (IsAvailable() ? "" : $", Rented by: {CurrentRenter}");
        }

        // Method to convert to CSV format
        public virtual string ToCsv()
        {
            return $"{Id},{Make},{Model},{Year},{Type},{Status},{CurrentRenter}";
        }

        // Static method to create from CSV line
        public static Vehicle FromCsv(string csvLine)
        {
            var parts = csvLine.Split(',');
            if (parts.Length < 6)
            {
                throw new ArgumentException("Invalid CSV format");
            }

            var id = int.Parse(parts[0]);
            var make = parts[1];
            var model = parts[2];
            var year = int.Parse(parts[3]);
            var type = parts[4];
            var status = parts[5];
            var currentRenter = parts.Length > 6 ? parts[6] : "";

            var vehicle = new Car(id, make, model, year, type);
            vehicle.Status = status;
            vehicle.CurrentRenter = currentRenter;

            return vehicle;
        }
    }
} 