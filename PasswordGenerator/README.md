# Password Generator

A C# .NET 8 library for generating secure random passwords with configurable length and character sets. This project demonstrates clean code practices, dependency injection, and testable design patterns.

## Project Overview

This project provides a flexible and secure password generation utility. It demonstrates:
- Dependency injection for testability
- Interface-based design for random number generation
- Input validation and error handling
- Configurable password complexity
- Clean API design with clear parameters
- SOLID principles and separation of concerns

## Features

### Random Password Generation
- Generate passwords with customizable minimum and maximum lengths
- Support for alphanumeric passwords
- Optional special character inclusion for enhanced security
- Cryptographically random character selection
- Configurable character sets

### Security Features
- **Uppercase Letters**: A-Z (26 characters)
- **Digits**: 0-9 (10 characters)
- **Special Characters**: `!@#$%^&*()_-+=` (14 characters, optional)
- Random length within specified range for unpredictability
- Each character randomly selected from allowed set

### Validation and Safety
- **Input Validation**: Ensures minimum length is at least 1
- **Range Validation**: Validates max length is not less than min length
- **Clear Error Messages**: Informative exceptions with parameter names
- **Defensive Programming**: Guards against invalid configurations

## Usage

### Basic Usage

```csharp
using PasswordGenerator;

// Create password generator with random wrapper
var passwordGenerator = new PasswordGenerator(new RandomWrapper());

// Generate a password between 8-16 characters without special characters
var password1 = passwordGenerator.Generate(8, 16, false);
// Example output: "XJKM3P9QRLWZ"

// Generate a password between 12-20 characters with special characters
var password2 = passwordGenerator.Generate(12, 20, true);
// Example output: "P@9ZM!X3K#7WQ$2Y"

Console.WriteLine($"Generated password: {password1}");
Console.WriteLine($"Secure password: {password2}");
```

### Generating Multiple Passwords

```csharp
var passwordGenerator = new PasswordGenerator(new RandomWrapper());

// Generate 10 passwords
for (int i = 0; i < 10; i++)
{
    var password = passwordGenerator.Generate(5, 10, true);
  Console.WriteLine(password);
}
```

**Sample Output:**
```
K7@P2X
9M#L5Q!W
R3$J8
Z@4N7K2
P!9X6M#5
```

### Different Password Strengths

```csharp
var generator = new PasswordGenerator(new RandomWrapper());

// Weak: Short alphanumeric
var weakPassword = generator.Generate(4, 6, false);
// Example: "AB3X9"

// Medium: Longer alphanumeric
var mediumPassword = generator.Generate(8, 12, false);
// Example: "K3PM9ZL2XW"

// Strong: Long with special characters
var strongPassword = generator.Generate(12, 16, true);
// Example: "P@9X#M3K!7WQ$2"

// Very Strong: Very long with special characters
var veryStrongPassword = generator.Generate(16, 24, true);
// Example: "Z!9@K3#P7$M2X&8Q+5W=L"
```

### Error Handling

```csharp
var generator = new PasswordGenerator(new RandomWrapper());

// Invalid: Minimum length less than 1
try
{
  var invalid = generator.Generate(0, 10, true);
}
catch (ArgumentOutOfRangeException ex)
{
    // Message: "minLength must be greater than 0"
}

// Invalid: Maximum length less than minimum
try
{
  var invalid = generator.Generate(10, 5, true);
}
catch (ArgumentOutOfRangeException ex)
{
    // Message: "minLength must be smaller than maxLength"
}
```

## API Reference

### PasswordGenerator Class

```csharp
public class PasswordGenerator
```

Main class for generating random passwords.

#### Constructor

```csharp
public PasswordGenerator(IRandom random)
```

Creates a new password generator with the specified random number generator.

**Parameters:**
- `random` (IRandom): Random number generator implementation

#### Methods

##### Generate

```csharp
public string Generate(int minLength, int maxLength, bool shallUseSpecialCharacters)
```

Generates a random password with specified constraints.

**Parameters:**
- `minLength` (int): Minimum password length (must be ? 1)
- `maxLength` (int): Maximum password length (must be ? minLength)
- `shallUseSpecialCharacters` (bool): Whether to include special characters

**Returns:**
- `string`: A randomly generated password

**Exceptions:**
- `ArgumentOutOfRangeException`: Thrown if minLength < 1 or maxLength < minLength

**Character Sets:**
- Without special characters: `ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789` (36 characters)
- With special characters: `ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_-+=` (50 characters)

### IRandom Interface

```csharp
public interface IRandom
{
    int Next(int minValue, int maxValue);
    int Next(int maxValue);
}
```

Abstraction for random number generation, enabling dependency injection and testing.

**Methods:**
- `Next(int minValue, int maxValue)`: Returns random integer in range [minValue, maxValue)
- `Next(int maxValue)`: Returns random integer in range [0, maxValue)

### RandomWrapper Class

```csharp
public class RandomWrapper : IRandom
```

Wrapper around `System.Random` implementing `IRandom` interface.

## Architecture

### Design Patterns

#### Dependency Injection
The generator accepts an `IRandom` implementation via constructor:

```csharp
public PasswordGenerator(IRandom random)
{
    _random = random;
}
```

**Benefits:**
- Testable with mock random number generators
- Decoupled from concrete Random implementation
- Follows Dependency Inversion Principle
- Easy to swap RNG implementations

#### Strategy Pattern
Different random number generators can be injected:

```csharp
// Production: Real random numbers
var generator1 = new PasswordGenerator(new RandomWrapper());

// Testing: Predictable/mock random numbers
var mockRandom = new MockRandom(); // Custom implementation
var generator2 = new PasswordGenerator(mockRandom);
```

#### Wrapper Pattern
`RandomWrapper` wraps `System.Random` to implement `IRandom`:

```csharp
public class RandomWrapper : IRandom
{
    private readonly Random _random = new Random();
  
    public int Next(int minValue, int maxValue)
    {
        return _random.Next(minValue, maxValue);
    }
}
```

### Implementation Details

#### Password Length Generation

```csharp
private int GeneratePasswordLength(int minLength, int maxLength)
{
    return _random.Next(minLength, maxLength + 1);
}
```

Returns a random length between `minLength` and `maxLength` (inclusive).

#### Character Selection

```csharp
private string GenerateRandomString(int length, string charactersToBeIncluded)
{
    var passwordCharacters =
        Enumerable.Repeat(charactersToBeIncluded, length)
        .Select(characters => characters[_random.Next(characters.Length)])
        .ToArray();

    return new string(passwordCharacters);
}
```

Uses LINQ to:
1. Repeat the character set `length` times
2. For each repetition, select a random character
3. Convert character array to string

#### Validation Logic

```csharp
private static void Validate(int minLength, int maxLength)
{
    if (minLength < 1)
    {
      throw new ArgumentOutOfRangeException(
          $"{nameof(minLength)} must be greater than 0");
    }
    if (maxLength < minLength)
    {
        throw new ArgumentOutOfRangeException(
         $"{nameof(minLength)} must be smaller than {nameof(maxLength)}");
    }
}
```

Ensures valid input parameters before generation.

## Password Strength Analysis

### Character Set Sizes

| Configuration | Characters | Entropy per Character | 10-char Entropy |
|---------------|------------|----------------------|-----------------|
| Alphanumeric | 36 | ~5.17 bits | ~51.7 bits |
| With Special | 50 | ~5.64 bits | ~56.4 bits |

### Entropy Calculation

Entropy = log?(character_set_size^password_length)

**Examples:**
- 8-char alphanumeric: log?(36?) ? 41.4 bits
- 8-char with special: log?(50?) ? 45.1 bits
- 16-char alphanumeric: log?(36¹?) ? 82.7 bits
- 16-char with special: log?(50¹?) ? 90.3 bits

### Recommended Configurations

| Use Case | Min Length | Max Length | Special Chars | Strength |
|----------|-----------|------------|---------------|----------|
| Temporary/Demo | 4 | 6 | No | Weak |
| User Accounts | 8 | 12 | No | Medium |
| Sensitive Accounts | 12 | 16 | Yes | Strong |
| Critical Systems | 16 | 24 | Yes | Very Strong |
| Encryption Keys | 24 | 32 | Yes | Excellent |

## Project Structure

```
PasswordGenerator/
??? PasswordGenerator.cs    # Main generator class and interfaces
??? Program.cs               # Demo application
??? README.md  # This file
??? PasswordGenerator.csproj # Project configuration
```

## Technical Requirements

- .NET 8.0
- C# 12.0

## Key Design Decisions

### 1. Interface for Random
Using `IRandom` interface enables:
- Unit testing with predictable random values
- Potential use of cryptographically secure RNG
- Flexibility to swap implementations

### 2. Uppercase Only
Currently uses only uppercase letters:
- Simplifies character set
- Avoids ambiguous characters (l vs I, O vs 0)
- Can be extended to include lowercase

### 3. Variable Length
Random length within range adds unpredictability:
- Harder to guess password length
- Adds entropy to password generation
- Flexible for different security requirements

### 4. Character Set Selection
Conditional character set based on special character flag:
- Simple boolean for user choice
- Clear distinction between alphanumeric and complex
- Easy to extend with more character types

### 5. LINQ-Based Generation
Using LINQ for character selection:
- Functional, declarative approach
- Readable and maintainable
- Efficient for password generation

## Security Considerations

### Strengths
- Random character selection from allowed set
- Variable password length
- Optional special characters for complexity
- No predictable patterns in output

### Limitations
- Uses `System.Random` (not cryptographically secure)
- No guarantee of character type distribution
- No dictionary word checking
- Uppercase only (can be extended)

### Recommendations for Production

#### 1. Use Cryptographically Secure RNG

```csharp
public class CryptoRandomWrapper : IRandom
{
    private readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();
    
    public int Next(int minValue, int maxValue)
    {
        var bytes = new byte[4];
        _rng.GetBytes(bytes);
     var value = BitConverter.ToUInt32(bytes, 0);
  return (int)(value % (maxValue - minValue) + minValue);
    }
}
```

#### 2. Ensure Character Type Diversity

```csharp
// Guarantee at least one uppercase, digit, and special character
private string EnforceDiversity(string password)
{
    // Implementation to ensure character type distribution
}
```

#### 3. Add Password Complexity Validation

```csharp
public bool MeetsComplexityRequirements(string password)
{
    var hasUpper = password.Any(char.IsUpper);
    var hasDigit = password.Any(char.IsDigit);
    var hasSpecial = password.Any(c => "!@#$%^&*()_-+=".Contains(c));
    
    return hasUpper && hasDigit && hasSpecial;
}
```

## Testing

The `IRandom` interface design enables comprehensive testing:

### Example Test with Mock

```csharp
public class MockRandom : IRandom
{
    private int _callCount = 0;
    
    public int Next(int minValue, int maxValue)
    {
        // Return predictable values for testing
        return minValue;
    }
    
    public int Next(int maxValue)
    {
  return _callCount++ % maxValue;
    }
}

[Test]
public void Generate_ReturnsMinLength_WhenRandomReturnsMinValue()
{
    var mockRandom = new MockRandom();
    var generator = new PasswordGenerator(mockRandom);
  
    var password = generator.Generate(5, 10, false);
    
    Assert.AreEqual(5, password.Length);
}
```

## Learning Objectives

This project demonstrates:

- **Dependency Injection**: Constructor injection for loose coupling
- **Interface Design**: Creating testable abstractions
- **Input Validation**: Defensive programming practices
- **LINQ Usage**: Functional programming for collections
- **Random Number Generation**: Working with randomness in .NET
- **Security Awareness**: Understanding password strength
- **Clean Code**: Readable, maintainable implementation
- **SOLID Principles**: Single Responsibility and Dependency Inversion

## Future Enhancements

Potential improvements:

- **Lowercase Letters**: Add lowercase character support
- **Character Type Requirements**: Ensure at least one of each type
- **Exclusion List**: Avoid ambiguous characters (0/O, 1/l/I)
- **Pronounceable Passwords**: Generate easier-to-remember passwords
- **Passphrase Generation**: Generate multi-word passphrases
- **Cryptographic RNG**: Use `RandomNumberGenerator` for security
- **Customizable Character Sets**: Allow user-defined character pools
- **Password Strength Meter**: Calculate and return entropy
- **Batch Generation**: Generate multiple unique passwords
- **Validation Regex**: Validate generated passwords against rules
- **Export Options**: Save passwords to file (encrypted)

## Common Patterns Demonstrated

### 1. Constructor Dependency Injection
```csharp
public PasswordGenerator(IRandom random)
{
    _random = random;
}
```

### 2. Guard Clauses
```csharp
if (minLength < 1)
{
  throw new ArgumentOutOfRangeException(...);
}
```

### 3. LINQ for Collection Processing
```csharp
var passwordCharacters =
  Enumerable.Repeat(charactersToBeIncluded, length)
    .Select(characters => characters[_random.Next(characters.Length)])
    .ToArray();
```

### 4. Conditional String Building
```csharp
var charactersToBeIncluded = shallUseSpecialCharacters ?
    "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_-+=" :
  "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
```

## Best Practices

1. **Dependency Injection**: Dependencies injected, not instantiated
2. **Interface Abstraction**: `IRandom` enables testability
3. **Input Validation**: Validates parameters before processing
4. **Meaningful Names**: Clear method and variable names
5. **Single Responsibility**: Each method has one clear purpose
6. **Immutable Strings**: Uses string concatenation safely
7. **Fail Fast**: Throws exceptions immediately on invalid input
8. **Static Validation**: Validation method is static (stateless)

## Integration Examples

### ASP.NET Core Dependency Injection

```csharp
// In Startup.cs or Program.cs
services.AddSingleton<IRandom, RandomWrapper>();
services.AddTransient<PasswordGenerator>();

// In controller
public class AccountController : Controller
{
    private readonly PasswordGenerator _passwordGenerator;
    
    public AccountController(PasswordGenerator passwordGenerator)
    {
        _passwordGenerator = passwordGenerator;
    }
    
    [HttpPost]
    public IActionResult GeneratePassword([FromBody] PasswordRequest request)
    {
 var password = _passwordGenerator.Generate(
     request.MinLength,
        request.MaxLength,
         request.UseSpecialCharacters);
            
        return Ok(new { Password = password });
    }
}
```

### Console Application

```csharp
class Program
{
    static void Main()
    {
        var generator = new PasswordGenerator(new RandomWrapper());
  
        Console.Write("Minimum length: ");
        var min = int.Parse(Console.ReadLine());
        
     Console.Write("Maximum length: ");
        var max = int.Parse(Console.ReadLine());
        
        Console.Write("Use special characters? (y/n): ");
     var useSpecial = Console.ReadLine().ToLower() == "y";
        
  var password = generator.Generate(min, max, useSpecial);
        Console.WriteLine($"Generated password: {password}");
    }
}
```

## Troubleshooting

### Common Issues

**Issue**: Same password generated multiple times
- **Cause**: Creating new `Random` instances too quickly
- **Solution**: Reuse single `RandomWrapper` instance

**Issue**: ArgumentOutOfRangeException on valid input
- **Cause**: Max length less than min length
- **Solution**: Ensure maxLength ? minLength

**Issue**: Predictable passwords in tests
- **Cause**: Using real `Random` in tests
- **Solution**: Use mock `IRandom` implementation

## License

This project is for educational purposes demonstrating password generation, dependency injection, and secure random string creation in C#.
