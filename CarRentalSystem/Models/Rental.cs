namespace CarRentalSystem.Models
{
    /// <summary>
    /// Rental class representing rental transactions
    /// </summary>
    public class Rental
    {
        // Properties
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime RentalStartDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public decimal DailyRate { get; set; }
        public decimal TotalCost { get; set; }
        public string Status { get; set; } // "Active", "Completed", "Overdue"

        // Constructor
        public Rental(int id, int vehicleId, int customerId, string customerName, 
                     DateTime rentalStartDate, DateTime expectedReturnDate, decimal dailyRate)
        {
            Id = id;
            VehicleId = vehicleId;
            CustomerId = customerId;
            CustomerName = customerName ?? throw new ArgumentNullException(nameof(customerName));
            RentalStartDate = rentalStartDate;
            ExpectedReturnDate = expectedReturnDate;
            DailyRate = dailyRate;
            Status = "Active";
            CalculateTotalCost();
        }

        // Method to calculate total cost
        private void CalculateTotalCost()
        {
            var days = (ExpectedReturnDate - RentalStartDate).Days;
            TotalCost = days * DailyRate;
        }

        // Method to complete rental
        public void CompleteRental()
        {
            ActualReturnDate = DateTime.Now;
            Status = "Completed";
            
            // Recalculate cost based on actual return date
            if (ActualReturnDate.HasValue)
            {
                var actualDays = (ActualReturnDate.Value - RentalStartDate).Days;
                TotalCost = actualDays * DailyRate;
            }
        }

        // Method to check if rental is overdue
        public bool IsOverdue()
        {
            return Status == "Active" && DateTime.Now > ExpectedReturnDate;
        }

        // Method to get rental duration in days
        public int GetRentalDuration()
        {
            var endDate = ActualReturnDate ?? DateTime.Now;
            return (endDate - RentalStartDate).Days;
        }

        // Method to get overdue days
        public int GetOverdueDays()
        {
            if (!IsOverdue())
                return 0;
            
            return (DateTime.Now - ExpectedReturnDate).Days;
        }

        // Override ToString
        public override string ToString()
        {
            return $"Rental ID: {Id}, Vehicle: {VehicleId}, Customer: {CustomerName}, " +
                   $"Start: {RentalStartDate:yyyy-MM-dd}, Expected Return: {ExpectedReturnDate:yyyy-MM-dd}, " +
                   $"Status: {Status}, Total Cost: ${TotalCost:F2}";
        }
    }
} 