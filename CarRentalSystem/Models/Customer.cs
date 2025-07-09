namespace CarRentalSystem.Models
{
    /// <summary>
    /// Customer class representing rental customers
    /// </summary>
    public class Customer
    {
        // Properties with validation
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime RegistrationDate { get; set; }
        public List<Rental> RentalHistory { get; set; }

        // Constructor
        public Customer(int id, string name, string email, string phone)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Phone = phone ?? throw new ArgumentNullException(nameof(phone));
            RegistrationDate = DateTime.Now;
            RentalHistory = new List<Rental>();
        }

        // Method to add rental to history
        public void AddRental(Rental rental)
        {
            if (rental != null)
            {
                RentalHistory.Add(rental);
            }
        }

        // Method to get total rentals
        public int GetTotalRentals()
        {
            return RentalHistory.Count;
        }

        // Method to check if customer is active (has rented in last 30 days)
        public bool IsActive()
        {
            return RentalHistory.Any(r => r.RentalStartDate > DateTime.Now.AddDays(-30));
        }

        // Override ToString
        public override string ToString()
        {
            return $"Customer ID: {Id}, Name: {Name}, Email: {Email}, Phone: {Phone}, Total Rentals: {GetTotalRentals()}";
        }
    }
} 