namespace CarRentalSystem.Models
{
    /// <summary>
    /// Car class that inherits from Vehicle
    /// Demonstrates inheritance and polymorphism
    /// </summary>
    public class Car : Vehicle
    {
        // Additional car-specific properties
        public string FuelType { get; set; }
        public int Mileage { get; set; }
        public string Transmission { get; set; }

        // Constructor
        public Car(int id, string make, string model, int year, string type) 
            : base(id, make, model, year, type)
        {
            FuelType = "Gasoline";
            Mileage = 0;
            Transmission = "Automatic";
        }

        // Override abstract method from base class
        public override string GetVehicleInfo()
        {
            return $"Car: {Make} {Model} ({Year}) - {Type} - Fuel: {FuelType}, Transmission: {Transmission}, Mileage: {Mileage}";
        }

        // Override ToString to include car-specific information
        public override string ToString()
        {
            return base.ToString() + $", Fuel: {FuelType}, Transmission: {Transmission}, Mileage: {Mileage}";
        }

        // Override ToCsv to include car-specific fields
        public override string ToCsv()
        {
            return $"{base.ToCsv()},{FuelType},{Mileage},{Transmission}";
        }

        // Method to update mileage (car-specific functionality)
        public void UpdateMileage(int newMileage)
        {
            if (newMileage >= 0)
            {
                Mileage = newMileage;
            }
            else
            {
                throw new ArgumentException("Mileage cannot be negative");
            }
        }

        // Method to check if car needs maintenance (car-specific business logic)
        public bool NeedsMaintenance()
        {
            return Mileage > 50000; // Example threshold
        }
    }
} 