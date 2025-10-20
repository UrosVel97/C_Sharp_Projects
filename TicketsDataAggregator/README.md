# Tickets Data Aggregator

A C# .NET 8 console application that extracts ticket information from PDF files and aggregates them into a single text file with culture-aware date and time formatting.

## Project Overview

This application demonstrates:
- PDF document parsing using PdfPig library
- Culture-specific date and time parsing
- Text extraction and pattern-based data splitting
- File I/O operations with clean abstractions
- String formatting and aggregation
- Dependency injection principles
- Extension methods for domain-specific logic

## Features

### PDF Ticket Processing
- Reads all PDF files from a designated tickets folder
- Extracts text content from the first page of each PDF document
- Parses ticket information using delimiter-based splitting
- Handles multiple tickets per PDF file

### Culture-Aware Date/Time Parsing
The application supports tickets from multiple regions with automatic culture detection:

| Domain | Culture | Date/Time Format |
|--------|---------|------------------|
| `.com` | `en-US` | US English format |
| `.fr` | `fr-FR` | French format |
| `.jp` | `ja-JP` | Japanese format |

The domain is extracted from the "Visit us:" web address in each PDF, and the corresponding culture is used to parse dates and times correctly.

### Data Aggregation
- Processes all tickets from multiple PDF files
- Converts dates and times to invariant culture format for consistency
- Generates a formatted output with aligned columns
- Saves aggregated results to `aggregatedTickets.txt`

## Architecture

The application follows clean architecture principles with clear separation of concerns:

### **File Access Layer**
- **`IDocumentsReader`**: Interface for reading documents from a directory
- **`DocumentsFromPdfReader`**: PDF-specific implementation using PdfPig
- **`IFileWriter`**: Interface for writing content to files
- **`FileWriter`**: File system writer implementation

### **Ticket Aggregation Layer**
- **`TicketsAggregator`**: Main orchestrator that processes PDF tickets and generates output
  - Reads PDF documents from the tickets folder
  - Extracts and parses ticket data with culture-specific formatting
  - Aggregates all tickets into a single formatted text file

### **Extensions**
- **`WebAddressExtensions`**: Provides `ExtractDomain()` method to parse domain from URLs

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- Visual Studio 2022 (recommended)

### Required Folder Structure

Create a `Tickets` folder in the application output directory and place your PDF ticket files inside:

```
TicketsDataAggregator/
??? bin/
?   ??? Debug/
?       ??? net8.0/
?           ??? TicketsDataAggregator.exe
?           ??? Tickets/              # Create this folder
?               ??? Tickets1.pdf
?               ??? Tickets2.pdf
?               ??? ...
```

### PDF Format Requirements

Each PDF ticket file should contain text in the following format:

```
Title: Concert Name
Date: 12/25/2024
Time: 7:30 PM
Title: Event Name
Date: 01/15/2024
Time: 2:00 PM
Visit us: www.example.com
```

The application expects:
- Multiple tickets separated by `Title:`, `Date:`, `Time:` delimiters
- A `Visit us:` section at the end with a web address containing the domain

### Running the Application

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd TicketsDataAggregator
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Build and run**
   ```bash
   dotnet build
   dotnet run
   ```

4. **Check the output**
   - The application will process all PDF files in the `.\Tickets` folder
   - Results will be saved to `.\Tickets\aggregatedTickets.txt`

### Sample Output

```
Concert Night                           | 12/25/2024| 19:30:00
Theater Performance                     | 01/15/2024| 14:00:00
Sports Event                            | 03/10/2024| 18:00:00
```

Each line contains:
- **Title**: Left-aligned, 40 characters wide
- **Date**: Invariant culture format (MM/dd/yyyy)
- **Time**: Invariant culture format (HH:mm:ss)

## Technical Highlights

### **PDF Processing**
- Uses **PdfPig** library for reliable PDF text extraction
- Reads the first page of each PDF document
- Handles multiple PDF files in a single batch

### **Culture-Specific Parsing**
- Automatically detects the source culture from the domain extension
- Parses dates and times using the correct `CultureInfo`
- Converts all outputs to `InvariantCulture` for consistency

### **Modern C# Features**
- **Nullable reference types**: Enhanced null safety throughout
- **String interpolation**: Clean formatting with interpolated strings
- **yield return**: Efficient document processing with deferred execution
- **Extension methods**: Domain-specific functionality (`ExtractDomain`)
- **StringBuilder**: Efficient string concatenation for large outputs
- **Pattern matching**: Clean null checking with `is null` patterns

### **Design Patterns**
- **Dependency Injection**: Constructor injection of file access dependencies
- **Interface Segregation**: Focused interfaces (`IDocumentsReader`, `IFileWriter`)
- **Single Responsibility**: Each class has one clear purpose
- **Strategy Pattern**: Pluggable document readers and file writers

## Error Handling

The application includes robust error handling:
- **Exception Catching**: Top-level try-catch in `Program.cs`
- **User-Friendly Messages**: Clear error messages displayed to console
- **Graceful Exit**: Application waits for user input before closing

Common errors handled:
- Missing or inaccessible PDF files
- Invalid PDF format or corrupted files
- Incorrect date/time formats in PDF content
- Missing or invalid domain information
- File system write errors

## Project Structure

```
TicketsDataAggregator/
??? Extensions/
?   ??? WebAddressExtensions.cs   # Domain extraction extension method
??? FileAccess/
?   ??? IDocumentsReader.cs       # Document reader interface
?   ??? DocumentsFromPdfReader.cs # PDF reader implementation
?   ??? IFileWriter.cs            # File writer interface
?   ??? FileWriter.cs             # File writer implementation
??? TicketsAggregation/
?   ??? TicketsAggregator.cs      # Main aggregation logic
??? Program.cs                     # Application entry point
??? README.md                      # This file
```

## Dependencies

- **.NET 8.0**: Latest LTS version of .NET
- **PdfPig (v0.1.11)**: Open-source library for PDF text extraction

## How It Works

1. **Initialization**: Application creates `TicketsAggregator` with file access dependencies
2. **PDF Reading**: `DocumentsFromPdfReader` reads all `.pdf` files from the Tickets folder
3. **Text Extraction**: PdfPig extracts text content from the first page of each PDF
4. **Data Parsing**: Text is split by delimiters (`Title:`, `Date:`, `Time:`, `Visit us:`)
5. **Domain Detection**: Web address is parsed to extract domain (e.g., `.com`, `.fr`, `.jp`)
6. **Culture Mapping**: Domain is mapped to appropriate `CultureInfo` for parsing
7. **Date/Time Conversion**: 
   - Dates parsed using culture-specific format
   - Times parsed using culture-specific format
   - Both converted to invariant culture for output
8. **Formatting**: Each ticket formatted as: `Title (40 chars) | Date | Time`
9. **Aggregation**: All tickets concatenated with line breaks
10. **File Writing**: Results saved to `aggregatedTickets.txt` in the Tickets folder

## Key Learning Objectives

This project demonstrates:

- **PDF Processing**: Working with PDF documents in .NET using third-party libraries
- **Globalization**: Culture-aware date and time parsing with `CultureInfo`
- **Text Processing**: String splitting, parsing, and formatting techniques
- **Clean Architecture**: Separation of concerns with clear layer boundaries
- **Dependency Management**: Using interfaces for testability and flexibility
- **File I/O**: Reading from and writing to the file system safely
- **Extension Methods**: Creating domain-specific utility methods
- **Error Handling**: Comprehensive exception handling for production readiness

## Customization

### Adding New Cultures

To support additional regions, update the `_domainCultureMapping` dictionary in `TicketsAggregator.cs`:

```csharp
private readonly Dictionary<string, CultureInfo> _domainCultureMapping = new()
{
    [".com"] = new CultureInfo("en-US"),
    [".fr"] = new CultureInfo("fr-FR"),
    [".jp"] = new CultureInfo("ja-JP"),
    [".de"] = new CultureInfo("de-DE"),  // Add German support
    [".es"] = new CultureInfo("es-ES"),  // Add Spanish support
};
```

### Changing Output Format

Modify the `BuildTicketData` method in `TicketsAggregator.cs` to customize the output format:

```csharp
private static string BuildTicketData(string[] split, int i, CultureInfo ticketCulture)
{
    // Customize column widths, delimiters, or date/time formats here
    var ticketData = $"{title,-40}| {dateAsStringInvariant}| {timeAsStringInvariant}";
    return ticketData;
}
```

### Changing Tickets Folder Location

Update the `TicketsFolder` constant in `Program.cs`:

```csharp
const string TicketsFolder = @"C:\MyTickets";  // Absolute path
// or
const string TicketsFolder = @".\MyTickets";   // Relative path
```

## Future Enhancements

Potential improvements could include:
- **Multi-Page Support**: Process all pages in PDF files, not just the first page
- **CSV Export**: Output to CSV format for Excel compatibility
- **Error Logging**: Detailed logging of parsing errors per file
- **Validation**: Verify ticket data integrity before aggregation
- **Sorting**: Sort tickets by date, time, or title
- **Filtering**: Include/exclude tickets based on criteria
- **Database Storage**: Store tickets in a database for querying
- **Configuration File**: External configuration for folder paths and cultures
- **Unit Tests**: Comprehensive test coverage for all components
- **Batch Processing**: Progress indication for large batches

## Troubleshooting

### "No PDF files found"
Ensure the `Tickets` folder exists in the application directory and contains `.pdf` files.

### "Invalid date/time format"
Check that dates and times in the PDF match the expected culture format for the domain.

### "Domain not supported"
The web address domain must be `.com`, `.fr`, or `.jp`. Add new cultures to support other domains.

### "Access denied" errors
Ensure the application has write permissions to the Tickets folder.

## License

This project is for educational purposes demonstrating PDF processing, culture-aware parsing, and clean architecture principles in C#.
