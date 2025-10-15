# Star Wars API Planet Statistics Analyzer

A C# .NET 8 console application that fetches planet data from the Star Wars API (SWAPI) and provides interactive statistical analysis with formatted table display.

## Project Overview

This application demonstrates modern C# development practices including:
- API consumption with HTTP client and JSON deserialization
- Async/await programming patterns
- Dependency injection and SOLID principles
- Error handling and fallback mechanisms
- Interactive console user interface
- Custom table formatting utilities

## Features

### Planet Data Retrieval
- Fetches planet information from [SWAPI (Star Wars API)](https://swapi.dev/)
- Automatic fallback to mock data when API is unavailable
- Robust error handling for network issues
- JSON deserialization using System.Text.Json

### Interactive Statistics Analysis
Users can analyze planet statistics for:
- **Population**: Total inhabitants of each planet
- **Diameter**: Planet size in kilometers  
- **Surface Water**: Percentage of planet surface covered by water

For each selected property, the application shows:
- Planet with **maximum** value
- Planet with **minimum** value

### Formatted Data Display
- Beautiful table formatting using custom `TablePrinter` utility
- Consistent column alignment and spacing
- Clean, readable console output
- Generic table printer that works with any object type

## Architecture

The application follows Clean Architecture principles with clear separation of concerns:

### **App Layer**
- **`StarWarsPlanetStatsApp`**: Main application orchestrator that coordinates the workflow
- **`PlanetsStatisticsAnalyzer`**: Handles statistical calculations and analysis logic
- **`IPlanetsStatisticsAnalyzer`**: Interface defining statistics analysis contract

### **Data Access Layer**
- **`PlanetsFromApiReader`**: Primary API data reader with automatic fallback support
- **`IPlanetsReader`**: Interface for planet data reading operations
- **`ApiDataReader`**: HTTP client wrapper for making SWAPI calls
- **`MockStarWarsApiDataReader`**: Fallback mock data provider with sample planet data
- **`IApiDataReader`**: Interface for API data reading abstraction

### **User Interaction Layer**
- **`PlanetsStatsUserInteractor`**: Handles user choice selection for statistics
- **`ConsoleUserInteractor`**: Console input/output operations
- **`IPlanetsStatsUserInteractor`**: Interface for statistics user interactions
- **`IUserIteractor`**: Base interface for user interaction operations

### **Utilities**
- **`TablePrinter`**: Generic table formatter for console output using reflection
- **`StringExtensions`**: Extension methods for string parsing and type conversion

### **Model & DTOs**
- **`Planet`**: Immutable record struct representing processed planet data
- **`Root`**: DTO for SWAPI API response root object
- **`Result`**: DTO for individual planet data from SWAPI

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- Visual Studio 2022 or VS Code (recommended)
- Internet connection (for live API access)

### Running the Application

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd StarWars_API
   ```

2. **Build and run**
   ```bash
   dotnet build
   dotnet run
   ```

3. **Follow the interactive prompts**
   - View the formatted planet data table
   - Select a statistic to analyze (population, diameter, or surface water)
   - See the results showing min/max values

### Sample Output

```
Name           |Diameter       |SurfaceWater   |Population     |
------------------------------------------------------------

Tatooine       |10465          |1              |200000         |
Alderaan       |12500          |40             |2000000000     |
Yavin IV       |10200          |8              |1000           |
Hoth           |7200           |100            |               |
Dagobah        |8900           |8              |               |
Bespin         |118000         |0              |6000000        |
Endor          |4900           |8              |30000000       |
Naboo          |12120          |12             |4500000000     |
Coruscant      |12240          |               |1000000000000  |
Kamino         |19720          |100            |1000000000     |

Choose the statistic to be shown:
- population
- diameter  
- surface water
Please type your choice: population

Max population is: 1000000000000 (planet: Coruscant)
Min population is: 1000 (planet: Yavin IV)
```

## Technical Highlights

### **Design Patterns**
- **Dependency Injection**: Loose coupling between components through constructor injection
- **Strategy Pattern**: Pluggable API readers (live API + mock fallback)
- **Generic Programming**: Reusable `TablePrinter<T>` utility using reflection

### **Error Handling**
- Graceful API failure recovery with automatic mock data fallback
- Comprehensive exception handling with user-friendly messages
- Input validation for user selections
- Null-safe string parsing with extension methods

### **Modern C# Features**
- **Record structs**: Immutable `Planet` model with value semantics
- **Nullable reference types**: Enhanced null safety throughout the codebase
- **Pattern matching**: Clean type conversions and validations
- **Extension methods**: Custom string parsing utilities (`ToIntOrNull`, `ToLongOrNull`)
- **Top-level programs**: Simplified Program.cs structure
- **Global using statements**: Cleaner code organization

### **SOLID Principles**
- **Single Responsibility**: Each class has one clear, well-defined purpose
- **Open/Closed**: Extensible through interfaces without modifying existing code
- **Liskov Substitution**: Interchangeable API readers and user interactors
- **Interface Segregation**: Focused, minimal interfaces for each concern
- **Dependency Inversion**: Depends on abstractions, not concrete implementations

## Data Model

### Planet Record
The `Planet` record represents the core domain model:

```csharp
public readonly record struct Planet
{
    public string Name { get; }           // Planet name
    public long? Population { get; }      // Number of inhabitants (nullable)
    public int? SurfaceWater { get; }     // Water coverage percentage (nullable)
    public int Diameter { get; }          // Size in kilometers (required)
}
```

### API Response DTOs
- **`Root`**: Contains pagination info and results array
- **`Result`**: Complete planet data from SWAPI with all available fields

## Project Structure

```
StarWars_API/
??? ApiDataAccess/                # HTTP client layer
?   ??? IApiDataReader.cs         # API reader interface
?   ??? ApiDataReader.cs          # Live HTTP client implementation
?   ??? MockStarWarsApiDataReader.cs # Mock data fallback
??? App/                          # Application layer
?   ??? StarWarsPlanetStatsApp.cs # Main app orchestrator
?   ??? PlanetsStatisticsAnalyzer.cs # Statistical analysis logic
?   ??? IPlanetsStatisticsAnalyzer.cs # Analysis interface
??? DataAccess/                   # Data access abstraction
?   ??? PlanetsFromApiReader.cs   # API data reader with fallback
?   ??? IPlanetsReader.cs         # Reader interface
??? DTOs/                         # Data transfer objects
?   ??? Root.cs                   # SWAPI response root
?   ??? Result.cs                 # Individual planet DTO
??? Model/                        # Domain models
?   ??? Planet.cs                 # Core planet data model
??? UserInteraction/              # User interface layer
?   ??? IPlanetsStatsUserInteractor.cs # Statistics UI interface
?   ??? PlanetsStatsUserInteractor.cs # Statistics UI implementation
?   ??? IUserIteractor.cs         # Base UI interface
?   ??? ConsoleUserInteractor.cs  # Console I/O implementation
??? Utilities/                    # Helper utilities
?   ??? TablePrinter.cs          # Generic table formatting
?   ??? StringExtensions.cs      # String parsing extensions
??? Program.cs                    # Application entry point
```

## Key Learning Objectives

This project demonstrates:

- **API Integration**: Consuming REST APIs with proper error handling and fallbacks
- **Async Programming**: Modern async/await patterns for I/O operations
- **Clean Architecture**: Separation of concerns and dependency management
- **User Experience**: Creating intuitive interactive console applications
- **Resilience**: Graceful degradation when external services fail
- **Generic Programming**: Building reusable utility components
- **Modern C#**: Latest language features and best practices (.NET 8)
- **JSON Processing**: System.Text.Json for high-performance deserialization
- **Type Safety**: Leveraging nullable reference types and record structs

## Error Handling & Resilience

The application implements multiple layers of error handling:

1. **Network Failures**: Automatic fallback from live API to mock data
2. **Parse Failures**: Safe string-to-number conversion with null returns  
3. **Invalid Input**: User input validation with clear error messages
4. **API Unavailability**: Complete mock dataset ensures application always works

## Mock Data

When SWAPI is unavailable, the application uses a comprehensive mock dataset including:
- **10 iconic Star Wars planets**: Tatooine, Alderaan, Yavin IV, Hoth, Dagobah, Bespin, Endor, Naboo, Coruscant, Kamino
- **Complete planet data**: All fields populated with canonical Star Wars information
- **Realistic statistics**: Proper population, diameter, and surface water values for meaningful analysis

## Future Enhancements

Potential improvements could include:
- **Caching**: Store API responses to reduce network calls and improve performance
- **Configuration**: Configurable API endpoints, timeouts, and retry policies
- **Logging**: Structured logging with Serilog for debugging and monitoring
- **Testing**: Comprehensive unit and integration tests
- **Data Export**: Save results to CSV, JSON, or Excel files
- **Web Interface**: ASP.NET Core web application version
- **Database Storage**: Persist planet data locally
- **More Statistics**: Additional analysis like averages, medians, standard deviation
- **Sorting & Filtering**: Interactive data exploration features

## Dependencies

- **.NET 8.0**: Latest LTS version of .NET
- **System.Text.Json**: High-performance JSON serialization
- **HttpClient**: Built-in HTTP client for API calls

## Contributing

This project follows standard C# coding conventions and SOLID principles. When contributing:
1. Maintain the existing architecture patterns
2. Add appropriate error handling
3. Include XML documentation for public APIs
4. Follow async/await best practices
5. Ensure null safety with nullable reference types

## License

This project is for educational purposes demonstrating modern C# development techniques, API consumption patterns, and clean architecture principles.