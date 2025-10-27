# Dice Roll Game (To Be Tested)

A C# .NET 8 console game where players try to guess a dice roll result within three attempts. This project demonstrates clean architecture, dependency injection, and testable design patterns.

## Project Overview

This is an interactive console game designed with testability in mind. It demonstrates:
- Dependency injection for decoupled components
- Interface-based design for easy mocking in tests
- Separation of concerns between game logic and user interaction
- Resource file management for localized messages
- Clean architecture principles
- Test-driven development support

## Game Description

### How to Play

1. The program rolls a virtual six-sided dice (values 1-6)
2. The player has **3 attempts** to guess the number
3. After each wrong guess, the game informs the player and continues
4. Win condition: Guess the correct number within 3 tries
5. Lose condition: Fail to guess the number after 3 attempts

### Sample Gameplay

```
Dice rolled. Guess what number it shows in 3 tries.
Enter a number:
> 3
Wrong number.
Enter a number:
> 5
Wrong number.
Enter a number:
> 2
You win!
```

## Architecture

The application follows clean architecture principles with clear separation of concerns:

### **Game Layer**
Core game logic and models.

#### GuessingGame
The main game orchestrator that manages the game flow.

**Constructor:**
```csharp
public GuessingGame(IDice dice, IUserCommunication userCommunication)
```

**Key Methods:**
- `Play()` - Executes the main game loop and returns the result
- `PrintResult(GameResult)` - Displays the final game outcome

**Game Flow:**
1. Roll the dice using `IDice`
2. Show welcome message with number of tries
3. Loop for 3 attempts:
   - Read player's guess via `IUserCommunication`
   - Check if guess matches dice result
   - If correct: return `Victory`
- If wrong: show "Wrong number" and decrement tries
4. If all tries exhausted: return `Loss`

#### Dice
Implementation of dice rolling using `Random`.

**Constructor:**
```csharp
public Dice(Random random)
```

**Methods:**
- `Roll()` - Returns a random integer between 1 and 6 (inclusive)

#### IDice Interface
Abstraction for dice rolling, enabling test mocking.

```csharp
public interface IDice
{
    int Roll();
}
```

#### GameResult Enum
Represents the outcome of the game.

```csharp
public enum GameResult
{
    Victory,
    Loss
}
```

### **User Communication Layer**
Handles all user input/output operations.

#### IUserCommunication Interface
Abstraction for user interaction, enabling test mocking.

```csharp
public interface IUserCommunication
{
    int ReadInteger(string prompt);
    void ShowMessage(string message);
}
```

#### ConsoleUserCommunication
Console-based implementation of user communication.

**Methods:**
- `ReadInteger(string prompt)` - Displays prompt and reads integer input (with validation loop)
- `ShowMessage(string message)` - Displays a message to the console

**Input Validation:**
- Continuously prompts until valid integer is entered
- Uses `int.TryParse()` for safe parsing
- No exception throwing for invalid input

### **Resources**
Externalized string resources for maintainability and potential localization.

**Resource.resx contains:**
- `DiceRolledMessage`: "Dice rolled. Guess what number it shows in {0} tries."
- `EnterNumberMessage`: "Enter a number:"

## Design Patterns

### Dependency Injection
All dependencies are injected via constructor:

```csharp
var random = new Random();
var dice = new Dice(random);
var userCommunication = new ConsoleUserCommunication();
var guessingGame = new GuessingGame(dice, userCommunication);
```

**Benefits:**
- Loose coupling between components
- Easy to swap implementations
- Enables comprehensive unit testing with mocks
- Follows SOLID principles (Dependency Inversion)

### Interface Segregation
Focused interfaces with single responsibilities:
- `IDice` - Only dice rolling concern
- `IUserCommunication` - Only user interaction concern

### Strategy Pattern
Different implementations can be provided:
- `Dice` for real random rolling
- `ConsoleUserCommunication` for console interaction
- Mock implementations for testing

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- Visual Studio 2022 or Visual Studio Code (recommended)

### Running the Game

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd DiceRollGameToBeTested
   ```

2. **Build and run**
   ```bash
   dotnet build
   dotnet run
   ```

3. **Play the game**
   - Follow the on-screen prompts
   - Enter numbers between 1 and 6
   - Try to guess the dice result within 3 attempts

### Project Structure

```
DiceRollGameToBeTested/
??? Game/
?   ??? GuessingGame.cs    # Main game logic
?   ??? Dice.cs              # Dice rolling implementation
?   ??? IDice.cs             # Dice interface
?   ??? GameResult.cs   # Game outcome enum
??? UserCommunication/
?   ??? IUserCommunication.cs        # User interaction interface
?   ??? ConsoleUserCommunication.cs  # Console implementation
??? Resource.resx     # Localized string resources
??? Resource.Designer.cs     # Auto-generated resource accessor
??? Program.cs     # Application entry point
??? README.md          # This file
```

## Technical Requirements

- .NET 8.0
- C# 12.0

## Testing

This project is designed for comprehensive testing. See the **DiceRollGameTests** project for:
- Unit tests using NUnit framework
- Mocking with Moq library
- Complete test coverage of game logic
- Verification of user interactions
- Edge case testing

[View DiceRollGameTests README](../DiceRollGameTests/README.md)

## Key Design Decisions

### 1. Interface-Based Dependencies
Using `IDice` and `IUserCommunication` interfaces enables:
- Easy mocking for unit tests
- Future alternative implementations (GUI, network, etc.)
- Adherence to Dependency Inversion Principle

### 2. Constant for Tries
```csharp
private const int InitialTries = 3;
```
Centralized configuration makes it easy to adjust game difficulty.

### 3. Resource Files
Externalized messages support:
- Easy text changes without code modification
- Future internationalization/localization
- Centralized message management

### 4. Enum for Results
`GameResult` enum provides type-safe outcome representation versus magic strings or booleans.

### 5. Testability First
Every dependency is injectable and mockable, making the code:
- 100% unit testable
- Easy to verify behavior
- Decoupled and maintainable

## Game Logic Flow

```
START
  ?
Roll Dice (1-6)
  ?
Show Welcome Message
  ?
Loop (3 times max)
  ?
  Read Player Guess
  ?
  Is Guess Correct?
  ?? Yes ? Return VICTORY
  ?? No ? Show "Wrong number"
         ?
     Decrement Tries
   ?
         Tries Left?
         ?? Yes ? Continue Loop
         ?? No ? Return LOSS
 ?
           END
```

## Extension Points

The design allows easy extensions:

### Different Dice Types
```csharp
public class TwentySidedDice : IDice
{
    public int Roll() => random.Next(1, 21);
}
```

### GUI Implementation
```csharp
public class WpfUserCommunication : IUserCommunication
{
    public int ReadInteger(string prompt) { /* WPF dialog */ }
    public void ShowMessage(string message) { /* WPF MessageBox */ }
}
```

### Configurable Difficulty
```csharp
public GuessingGame(IDice dice, IUserCommunication userCommunication, int maxTries)
```

### Multiplayer Support
```csharp
public class MultiplayerGuessingGame
{
    private readonly List<IUserCommunication> _players;
}
```

## Learning Objectives

This project demonstrates:

- **Clean Architecture**: Separation of game logic, I/O, and data
- **Dependency Injection**: Constructor injection for loose coupling
- **Interface Design**: Creating testable abstractions
- **SOLID Principles**: Especially Dependency Inversion and Single Responsibility
- **Resource Management**: Using .resx files for externalized strings
- **Test-Driven Development**: Design optimized for unit testing
- **Console I/O**: Reading and validating user input
- **Random Number Generation**: Working with `Random` class safely

## Common Patterns Demonstrated

### 1. Try Counter Pattern
```csharp
var triesLeft = InitialTries;
while (triesLeft > 0)
{
    // Game logic
    --triesLeft;
}
```

### 2. Validated Input Loop
```csharp
do
{
    Console.WriteLine(prompt);
} 
while(!int.TryParse(Console.ReadLine(), out result));
```

### 3. Dependency Injection via Constructor
```csharp
public GuessingGame(IDice dice, IUserCommunication userCommunication)
{
    _dice = dice;
    _userCommunication = userCommunication;
}
```

## Relationship to DiceRollGameTests

This project is thoroughly tested by **DiceRollGameTests**:

```
DiceRollGameToBeTested (Production Code)
    ? (tested by)
DiceRollGameTests (Unit Tests with Moq)
```

The test project uses:
- **NUnit** for test framework
- **Moq** for mocking `IDice` and `IUserCommunication`
- **Arrange-Act-Assert** pattern
- **Setup/Teardown** with `[SetUp]` attribute

See [DiceRollGameTests README](../DiceRollGameTests/README.md) for detailed test coverage.

## Future Enhancements

Potential improvements:

- **Difficulty Levels**: Easy (5 tries), Medium (3 tries), Hard (1 try)
- **Score Tracking**: Track wins/losses across multiple games
- **Hints System**: "Higher" or "Lower" feedback after wrong guesses
- **Multi-Dice**: Guess the sum of multiple dice
- **Time Limits**: Add time pressure for each guess
- **Leaderboard**: Store best scores in a database
- **Network Play**: Remote multiplayer via TCP/UDP
- **Custom Dice**: Player-configurable number of sides
- **Sound Effects**: Audio feedback for wins/losses
- **Statistics**: Track guess patterns and analytics

## Best Practices Demonstrated

1. **Separation of Concerns**: Game logic separate from I/O
2. **Dependency Injection**: All dependencies injected, not instantiated internally
3. **Interface-Based Design**: Abstractions enable flexibility and testing
4. **Resource Externalization**: Strings in resource files for maintainability
5. **Meaningful Names**: Clear, descriptive class and method names
6. **Single Responsibility**: Each class has one clear purpose
7. **Testability**: Design enables 100% unit test coverage
8. **No Magic Numbers**: Constants for configuration values
9. **Type Safety**: Enums instead of magic strings
10. **Input Validation**: Robust parsing with error handling

## Troubleshooting

### Common Issues

**Issue**: Game doesn't accept input
- **Solution**: Ensure console input is redirected properly

**Issue**: Same number always rolled
- **Solution**: Check `Random` instance is properly seeded

**Issue**: Wrong number of tries allowed
- **Solution**: Verify `InitialTries` constant is set to 3

## License

This project is for educational purposes demonstrating clean architecture, dependency injection, and testable game design in C#.
