# Chuck Norris Jokes

A .NET 8 console application that fetches and displays Chuck Norris jokes from the [Chuck Norris API](https://api.chucknorris.io/).

## Overview

This project demonstrates asynchronous programming, HTTP client usage, and clean architecture patterns in C#. It allows users to fetch multiple Chuck Norris jokes by category with parallel HTTP requests for improved performance.

## Features

- **Category Selection**: Browse and select from available joke categories
- **Batch Fetching**: Retrieve multiple jokes at once with configurable pagination
- **Parallel Processing**: Concurrent API requests for faster data retrieval
- **Loading Indicator**: Visual feedback while fetching jokes
- **JSON Parsing**: Extracts joke content from API responses
- **Clean Architecture**: Separated concerns with interfaces for data access and user interaction

## Project Structure

```
ChuckNorisJokes/
??? DataAccess/
?   ??? IJokesApiDataReader.cs      # Interface for API data access
?   ??? JokesApiDataReader.cs       # HTTP client implementation
??? UserInteraction/
?   ??? IUserInteractor.cs          # Interface for user interaction
?   ??? ConsoleUserInteractor.cs    # Console-based user interface
??? Models/
?   ??? Categories.cs               # Category models
?   ??? Root.cs                     # API response models
??? Program.cs                      # Application entry point
```

## Technologies Used

- **.NET 8**: Target framework
- **C# 12.0**: Programming language
- **HttpClient**: For REST API calls
- **System.Text.Json**: For JSON parsing
- **Async/Await**: For asynchronous operations
- **LINQ**: For data manipulation

## How It Works

1. **Fetch Categories**: Retrieves available joke categories from the API
2. **User Input**: Prompts user to select a category and specify number of pages and jokes per page
3. **Parallel Requests**: Makes concurrent HTTP GET requests to fetch jokes
4. **Process Responses**: Extracts joke text from JSON responses
5. **Display Results**: Shows all fetched jokes numbered and formatted

## Usage

Run the application and follow the prompts:

```
Enter category of joke:
1. animal
2. career
3. celebrity
...

How many pages do you want to read?
> 2

How many jokes per page?
> 5

Fetching data...
Loading |
```

The application will display all fetched jokes:

```
All 10 fetched:
Chuck Norris joke number 1:
[Joke text here]

Chuck Norris joke number 2:
[Joke text here]
...
```

## API Endpoint

This project uses the free Chuck Norris API:
- **Base URL**: https://api.chucknorris.io
- **Categories Endpoint**: `/jokes/categories`
- **Random Joke Endpoint**: `/jokes/random?category={category}`

## Performance Features

- **Concurrent HTTP Requests**: Multiple jokes are fetched simultaneously using `Task.WhenAll()`
- **Efficient String Building**: Uses `StringBuilder` for result compilation
- **Resource Management**: Implements `IDisposable` for proper cleanup

## Requirements

- .NET 8 SDK
- Internet connection (to access Chuck Norris API)

## Building and Running

```bash
# Clone the repository
cd ChuckNorisJokes

# Build the project
dotnet build

# Run the application
dotnet run
```

## License

This project is part of a C# learning repository and is available for educational purposes.
