# Fibonacci Generator

A C# .NET 8 library that generates Fibonacci sequences with built-in overflow protection and comprehensive validation.

## Project Overview

This project provides a simple, safe, and efficient implementation of the Fibonacci sequence generator. It demonstrates:
- Static utility class design
- Input validation and error handling
- Overflow prevention for integer arithmetic
- Clean API design with clear constraints
- Test-driven development approach

## Features

### Fibonacci Sequence Generation
- Generates the first `n` numbers in the Fibonacci sequence
- Returns results as `IEnumerable<int>` for flexibility
- Handles edge cases (empty sequence for n=0)
- Fast computation using iterative approach

### Safety and Validation
- **Input validation**: Rejects negative values
- **Overflow protection**: Prevents integer overflow by limiting to first 46 numbers
- **Clear error messages**: Informative exceptions with detailed messages
- **Robust bounds checking**: Validates both lower and upper limits

### Mathematical Background

The Fibonacci sequence is defined as:
- F(0) = 0
- F(1) = 1
- F(n) = F(n-1) + F(n-2) for n > 1

Resulting in the sequence: 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89...

## Usage

### Basic Usage

```csharp
using FibonacciGenerator;

// Generate first 10 Fibonacci numbers
var sequence = Fibonacci.Generate(10);
// Returns: [0, 1, 1, 2, 3, 5, 8, 13, 21, 34]

// Display the sequence
foreach (var number in sequence)
{
    Console.WriteLine(number);
}
```

### Edge Cases

```csharp
// Empty sequence
var empty = Fibonacci.Generate(0);
// Returns: []

// Single element
var single = Fibonacci.Generate(1);
// Returns: [0]

// First two elements
var pair = Fibonacci.Generate(2);
// Returns: [0, 1]

// Maximum safe value (46th Fibonacci number)
var max = Fibonacci.Generate(46);
// Returns: [0, 1, 1, ..., 1134903170]
// Last element: 1,134,903,170
```

### Error Handling

```csharp
// Negative input throws ArgumentException
try
{
    var invalid = Fibonacci.Generate(-5);
}
catch (ArgumentException ex)
{
    // Message: "n can not be smaller than 0."
}

// Overflow prevention throws ArgumentException
try
{
    var overflow = Fibonacci.Generate(47);
}
catch (ArgumentException ex)
{
    // Message: "n can not be larger than 46, as it will cause numeric overflow."
}
```

## API Reference

### Fibonacci Class

```csharp
public static class Fibonacci
```

A static utility class for generating Fibonacci sequences.

#### Methods

##### Generate

```csharp
public static IEnumerable<int> Generate(int n)
```

Generates the first `n` numbers in the Fibonacci sequence.

**Parameters:**
- `n` (int): The count of Fibonacci numbers to generate (0 ? n ? 46)

**Returns:**
- `IEnumerable<int>`: The first `n` Fibonacci numbers

**Exceptions:**
- `ArgumentException`: Thrown if `n < 0` or `n > 46`

**Time Complexity:** O(n)  
**Space Complexity:** O(n)

## Technical Constraints

### Why Maximum 46?

The 47th Fibonacci number (1,836,311,903) exceeds `int.MaxValue` (2,147,483,647) when computed, causing integer overflow. The implementation safely limits generation to the first 46 numbers to prevent this:

| Position | Fibonacci Number | Can Store in int? |
|----------|------------------|-------------------|
| 45 | 701,408,733 | Yes |
| 46 | 1,134,903,170 | Yes |
| 47 | 1,836,311,903 | Yes (but sum of 46+47 overflows) |
| 48 | 2,971,215,073 | No (overflow) |

## Implementation Details

### Algorithm

The implementation uses an iterative approach with two variables tracking the previous two numbers:

```csharp
int a = -1;  // F(n-2), starts at -1 for initialization
int b = 1; // F(n-1), starts at 1 for initialization
for (int i = 0; i < n; i++)
{
int sum = a + b;  // F(n) = F(n-1) + F(n-2)
    result.Add(sum);
    a = b;            // Shift: F(n-2) = F(n-1)
    b = sum;      // Shift: F(n-1) = F(n)
}
```

**Why start with a=-1 and b=1?**
- First iteration: -1 + 1 = 0 (F(0))
- Second iteration: 1 + 0 = 1 (F(1))
- Third iteration: 0 + 1 = 1 (F(2))
- And so on...

### Design Decisions

1. **Static Class**: No state to maintain, pure computational utility
2. **IEnumerable<int> Return**: Allows LINQ operations and deferred execution patterns
3. **List<int> Storage**: Pre-allocates collection for known size
4. **Explicit Validation**: Fails fast with clear error messages
5. **Named Constants**: `MaxValidNumber` improves code readability

## Testing

This project is thoroughly tested by the **FibonacciGeneratorTests** project. See the [FibonacciGeneratorTests README](../FibonacciGeneratorTests/README.md) for detailed test coverage information.

### Test Coverage Includes:
- Negative input validation
- Overflow prevention (n > 46)
- Empty sequence generation (n = 0)
- Single and double element sequences
- Valid Fibonacci sequences for various inputs
- Maximum boundary test (n = 46)

## Project Structure

```
FibonacciGenerator/
??? Fibonacci.cs      # Core Fibonacci sequence generator
??? Program.cs        # Console application entry point
??? README.md     # This file
??? FibonacciGenerator.csproj  # Project configuration
```

## Technical Requirements

- .NET 8.0
- C# 12.0

## Related Projects

- **FibonacciGeneratorTests**: Comprehensive test suite using NUnit framework

## Key Learning Objectives

This project demonstrates:

- **Algorithm Implementation**: Classic Fibonacci sequence using iterative approach
- **Input Validation**: Comprehensive parameter checking with meaningful exceptions
- **Overflow Prevention**: Understanding integer limits and safe computation ranges
- **Static Utility Classes**: Stateless computational utilities in C#
- **API Design**: Creating simple, safe, and intuitive public interfaces
- **Documentation**: Clear constraints and usage examples
- **Test-Driven Development**: Designed with testability in mind

## Performance Characteristics

- **Time Complexity**: O(n) - Linear time relative to input size
- **Space Complexity**: O(n) - Stores all n numbers in a list
- **Memory Allocation**: Single list allocation, no recursive stack overhead
- **CPU Efficiency**: Simple arithmetic operations, no heavy computations

## Common Use Cases

1. **Mathematical Education**: Teaching the Fibonacci sequence
2. **Algorithm Practice**: Demonstrating iterative vs. recursive approaches
3. **Pattern Recognition**: Analyzing Fibonacci patterns in nature and art
4. **Test Data Generation**: Creating predictable numeric sequences for testing
5. **Interview Preparation**: Common programming interview question

## Alternatives and Extensions

### For Larger Numbers

If you need Fibonacci numbers beyond the 46th position, consider:

```csharp
// Using long (supports up to ~92nd Fibonacci number)
public static IEnumerable<long> GenerateLong(int n)

// Using BigInteger (unlimited size, slower performance)
public static IEnumerable<BigInteger> GenerateBigInteger(int n)

// Using decimal (up to ~138th Fibonacci number)
public static IEnumerable<decimal> GenerateDecimal(int n)
```

### Lazy Evaluation

For memory-efficient generation:

```csharp
public static IEnumerable<int> GenerateLazy(int n)
{
    if (n <= 0) yield break;
    
    int a = -1, b = 1;
    for (int i = 0; i < n; i++)
    {
        int sum = a + b;
        yield return sum;
        a = b;
      b = sum;
    }
}
```

## Future Enhancements

Potential improvements could include:

- **Generic Implementation**: Support for `long`, `BigInteger`, and `decimal` types
- **Lazy Evaluation**: Use `yield return` for memory efficiency
- **Nth Element**: Method to get just the nth Fibonacci number without generating all previous
- **Memoization**: Cache results for repeated calls
- **Parallel Generation**: Utilize multiple cores for very large sequences
- **Fibonacci Properties**: Methods to check if a number is in the Fibonacci sequence
- **Golden Ratio**: Calculate Fibonacci-related mathematical constants

## Mathematical Properties

Interesting Fibonacci sequence properties:

1. **Golden Ratio**: F(n+1) / F(n) approaches ? (phi) ? 1.618033988749...
2. **Sum Property**: Sum of first n Fibonacci numbers = F(n+2) - 1
3. **GCD Property**: GCD(F(m), F(n)) = F(GCD(m, n))
4. **Even Pattern**: Every 3rd Fibonacci number is even
5. **Divisibility**: F(n) divides F(kn) for any positive integer k

## License

This project is for educational purposes demonstrating algorithm implementation, input validation, and safe arithmetic operations in C#.
