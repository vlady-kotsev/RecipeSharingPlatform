# Car Rental System - OOP Console Application

## Project Overview

This is a comprehensive console-based Car Rental System that demonstrates Object-Oriented Programming (OOP) principles in C#. The application manages vehicle inventory and rental transactions for a car rental agency.

## Key Features

- **Vehicle Management**: Add, edit, remove, and list vehicles
- **Rental Operations**: Rent and return vehicles with customer tracking
- **Search Functionality**: Search vehicles by make, model, type, or status
- **Data Persistence**: CSV file-based data storage (no external libraries)
- **Statistics**: View comprehensive vehicle and rental statistics
- **User-Friendly Interface**: Intuitive console-based menu system

## OOP Principles Demonstrated

### 1. Encapsulation
- Private fields with public properties in `Vehicle` class
- Data validation and controlled access to object state
- Information hiding through proper access modifiers

### 2. Inheritance
- `Car` class inherits from abstract `Vehicle` class
- Code reuse and hierarchical relationship modeling
- Extension of base class functionality

### 3. Polymorphism
- Abstract `GetVehicleInfo()` method implemented differently in derived classes
- Interface implementations (`IRentable`, `ISearchable`, `IFileOperations`)
- Method overriding for different behaviors

### 4. Abstraction
- Abstract `Vehicle` class defining common interface
- Interface contracts for different functionalities
- Simplified complex operations through abstraction layers

## Project Structure

```
CarRentalSystem/
├── Models/                    # Data models
│   ├── Vehicle.cs            # Abstract base class
│   ├── Car.cs                # Concrete vehicle implementation
│   ├── Customer.cs           # Customer entity
│   └── Rental.cs             # Rental transaction entity
├── Interfaces/               # Interface definitions
│   ├── IRentable.cs          # Rentable items contract
│   ├── ISearchable.cs        # Search functionality contract
│   └── IFileOperations.cs    # File operations contract
├── Services/                 # Business logic services
│   ├── CarRentalService.cs   # Main business logic
│   ├── FileService.cs        # CSV file operations
│   └── SearchService.cs      # Search functionality
├── Managers/                 # Application managers
│   └── RentalManager.cs      # Command handling and orchestration
├── Utils/                    # Utility classes
│   ├── ConsoleHelper.cs      # Console UI utilities
│   └── ValidationHelper.cs   # Input validation utilities
├── Data/                     # Data files
│   └── cars.csv              # Sample vehicle data
├── Program.cs                # Application entry point
└── README.md                 # This file
```

## Data Structures Used

- **Lists**: Primary collection for vehicles and rentals
- **Dictionaries**: Statistics and grouping operations
- **Arrays**: Command argument processing
- **Generic Collections**: Type-safe data management

## File Handling

The application implements custom CSV file operations without external libraries:

- **Reading**: Parses CSV files with header detection
- **Writing**: Generates properly formatted CSV output
- **Error Handling**: Robust exception management for file operations
- **Backup**: Automatic backup creation before file modifications

## Exception Handling

Comprehensive error management throughout the application:

- **Input Validation**: User input sanitization and validation
- **File Operations**: Graceful handling of file I/O errors
- **Business Logic**: Proper error messages for business rule violations
- **System Errors**: Application-level exception handling

## How to Run

### Prerequisites
- .NET 8.0 SDK or later
- Windows, macOS, or Linux

### Running the Application

1. **Navigate to the project directory:**
   ```bash
   cd CarRentalSystem
   ```

2. **Build the application:**
   ```bash
   dotnet build
   ```

3. **Run the application:**
   ```bash
   dotnet run
   ```

### Sample Usage

```
=== CAR RENTAL SYSTEM ===
1. Add Car
2. Edit Car
3. Remove Car
4. List Cars
5. Rent Car
6. Return Car
7. Search Cars
8. Show Statistics
9. Save & Exit
0. Exit without saving
Enter your choice: 4

=== VEHICLE LIST ===
ID    Make            Model           Year   Type         Status       Renter
-------------------------------------------------------------------------------------
1     Toyota          Corolla         2019   Sedan        Available    
2     Ford            Focus           2020   Hatchback    Rented       Jane Smith
3     Audi            A4              2022   Sedan        Available    
...
```

## Available Commands

| Command | Description | Example |
|---------|-------------|---------|
| `1` or `add` | Add a new car | Interactive prompts for car details |
| `2` or `edit` | Edit existing car | Interactive prompts for modifications |
| `3` or `remove` | Remove a car | `3` then enter car ID |
| `4` or `list` | List all cars | `4` |
| `5` or `rent` | Rent a car | Interactive prompts for rental details |
| `6` or `return` | Return a car | `6` then enter car ID |
| `7` or `search` | Search cars | `7` then enter search criteria |
| `8` or `stats` | Show statistics | `8` |
| `9` or `save` | Save and exit | `9` |
| `0` or `exit` | Exit without saving | `0` |
| `help` | Show help | `help` |

## CSV File Format

The application uses a CSV file with the following structure:

```csv
Id,Make,Model,Year,Type,Status,CurrentRenter,FuelType,Mileage,Transmission
1,Toyota,Corolla,2019,Sedan,Available,,Gasoline,25000,Automatic
2,Ford,Focus,2020,Hatchback,Rented,Jane Smith,Gasoline,18000,Automatic
```

## Validation Rules

### Vehicle Validation
- **Make/Model**: Non-empty, max 50 characters
- **Year**: Between 1900 and current year + 1
- **Type**: Must be one of: Sedan, SUV, Hatchback, Coupe, Convertible, Truck, Van, Wagon
- **ID**: Positive integer

### Customer Validation
- **Name**: Non-empty, max 100 characters
- **Email**: Valid email format
- **Phone**: 10-15 digits

### Rental Validation
- **Date Range**: Start date must be before end date
- **Start Date**: Cannot be in the past
- **Vehicle**: Must be available for rental

## Error Handling Examples

The application provides clear error messages for various scenarios:

- Invalid input formats
- Business rule violations
- File operation failures
- Data validation errors

## Extensibility

The application is designed for easy extension:

- **New Vehicle Types**: Add new classes inheriting from `Vehicle`
- **Additional Services**: Implement new interfaces
- **Different Storage**: Replace `FileService` with database implementation
- **Enhanced UI**: Extend `ConsoleHelper` for better user experience

## Testing the Application

1. **Add a new car** (Option 1)
2. **List all cars** (Option 4) to verify addition
3. **Search for cars** (Option 7) by make or model
4. **Rent a car** (Option 5) to a customer
5. **View statistics** (Option 8) to see rental status
6. **Return a car** (Option 6) to complete the rental cycle
7. **Save and exit** (Option 9) to persist changes

## Git Usage

The project demonstrates proper Git practices:

- **Frequent Commits**: Small, logical changes
- **Descriptive Messages**: Clear commit descriptions
- **Feature Branches**: Separate development for new features
- **Clean History**: Well-organized commit structure

## Performance Considerations

- **Efficient Collections**: Appropriate use of Lists and Dictionaries
- **Lazy Loading**: Data loaded only when needed
- **Memory Management**: Proper disposal of resources
- **Scalability**: Design supports larger datasets

## Security Features

- **Input Sanitization**: Removes potentially dangerous characters
- **Validation**: Comprehensive input validation
- **Error Handling**: No sensitive information in error messages
- **File Security**: Safe file operations with proper permissions

## Future Enhancements

Potential improvements for the application:

- **Database Integration**: Replace CSV with SQL database
- **Web Interface**: Add web-based UI
- **User Authentication**: Add login system
- **Reporting**: Advanced reporting and analytics
- **Multi-language Support**: Internationalization
- **API Integration**: REST API for external access

## Conclusion

This Car Rental System demonstrates a complete, production-ready application that showcases OOP principles, proper architecture, and best practices in C# development. The code is well-documented, maintainable, and extensible for future enhancements. 