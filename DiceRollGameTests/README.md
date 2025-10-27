# Dice Roll Game Tests

A comprehensive unit test suite for the DiceRollGame project using NUnit and Moq frameworks. This project demonstrates advanced unit testing techniques including mocking, test isolation, and behavior verification.

## Project Overview

This test suite provides complete coverage for the `GuessingGame` class from the **DiceRollGameToBeTested** project. It demonstrates:
- Unit testing with NUnit framework
- Mocking dependencies with Moq library
- Test setup and teardown patterns
- Parameterized testing
- Behavior verification (not just state verification)
- Test-driven development (TDD) practices
- Comprehensive edge case coverage

## Purpose

The DiceRollGameTests project ensures that the `GuessingGame` class:
- Correctly implements game logic for wins and losses
- Properly handles user input through the communication interface
- Displays appropriate messages at the right times
- Respects the 3-try limit
- Interacts correctly with dependencies

## Test Coverage

### Victory Scenarios

#### First Try Victory
```csharp
[Test]
public void Play_ShallReturnVictory_IfTheUserGuessesTheNumberOnFirstTry()
```

**Validates:**
- Player wins when guessing correctly on first attempt
- Game returns `GameResult.Victory`
- Game logic short-circuits after correct guess

**Mock Setup:**
- Dice rolls a specific number (e.g., 3)
- User always guesses that same number

#### Third Try Victory
```csharp
[Test]
public void Play_ShallReturnVictory_IfTheUserGuessesTheNumberOnThirdTry()
```

**Validates:**
- Player wins when guessing correctly on third attempt
- Game allows all three tries
- Previous wrong guesses don't prevent victory

**Mock Setup:**
- Dice rolls a specific number (e.g., 3)
- User guesses wrong twice, then correct: 1 ? 2 ? 3

### Loss Scenarios

#### Never Guesses Correctly
```csharp
[Test]
public void Play_ShallReturnLoss_IfTheUserNeverGuessesTheNumber()
```

**Validates:**
- Player loses after 3 wrong guesses
- Game returns `GameResult.Loss`
- Game respects try limit

**Mock Setup:**
- Dice rolls a specific number (e.g., 3)
- User always guesses wrong number (e.g., 1)

#### Fourth Try Would Win (But Not Allowed)
```csharp
[Test]
public void Play_ShallReturnLoss_IfTheUserGuessesTheNumberOnFourthTry()
```

**Validates:**
- Player loses even if fourth guess would be correct
- Game strictly enforces 3-try limit
- No additional attempts after limit

**Mock Setup:**
- Dice rolls a specific number (e.g., 3)
- User guesses: 1 ? 2 ? 5 ? 3 (last guess never used)

### Message Display Tests

#### Result Messages
```csharp
[TestCase(GameResult.Victory, "You win!")]
[TestCase(GameResult.Loss, "You lose :(")]
public void PrintResult_ShallShowProperMessageForGameResult(GameResult gameResult, string expectedMessage)
```

**Validates:**
- Correct message displayed for victory
- Correct message displayed for loss
- `IUserCommunication.ShowMessage()` called with exact text

#### Welcome Message
```csharp
[Test]
public void Play_ShallShowWelcomeMessage()
```

**Validates:**
- Welcome message displayed at game start
- Message includes number of tries available
- Message shown exactly once

**Expected Message:**
```
"Dice rolled. Guess what number it shows in 3 tries."
```

#### Wrong Number Messages
```csharp
[Test]
public void Play_ShallShowWrongNumberTwice_IfTheUserGuessesTheNumberOnThirdTry()
```

**Validates:**
- "Wrong number." shown after each incorrect guess
- Message shown correct number of times
- Message not shown after correct guess or when tries exhausted

### Interaction Count Tests

#### Read Integer Calls
```csharp
[Test]
public void Play_ShallAskForNumberThreeTimes_IfTheUserGuessesTheNumberOnThirdTry()
```

**Validates:**
- `ReadInteger()` called exactly 3 times when guessing on third try
- Each attempt prompts for user input
- Correct prompt message passed

## Test Scenarios Summary

| Test Category | Test Cases | Purpose |
|---------------|-----------|---------|
| **Victory Conditions** | 2 tests | Validate winning on 1st and 3rd tries |
| **Loss Conditions** | 2 tests | Validate losing scenarios (never guess, too late) |
| **Message Display** | 3 tests | Verify correct messages shown |
| **Interaction Counts** | 2 tests | Validate method call frequencies |
| **Total** | **9 tests** | Complete behavior coverage |

## Mocking Strategy

### Mock Objects

The test suite creates mocks for both dependencies:

```csharp
private Mock<IDice> _diceMock;
private Mock<IUserCommunication> _userCommunicationMock;
private GuessingGame _cut;  // Class Under Test
```

### Setup Method

Using NUnit's `[SetUp]` attribute for test initialization:

```csharp
[SetUp]
public void Setup()
{
    _diceMock = new Mock<IDice>();
    _userCommunicationMock = new Mock<IUserCommunication>();
    _cut = new GuessingGame(_diceMock.Object, _userCommunicationMock.Object);
}
```

**Benefits:**
- Fresh mocks for each test (test isolation)
- No shared state between tests
- Consistent test initialization

### Mock Configurations

#### Simple Return Value
```csharp
_diceMock.Setup(mock => mock.Roll()).Returns(3);
```

#### Any String Parameter
```csharp
_userCommunicationMock
    .Setup(mock => mock.ReadInteger(It.IsAny<string>()))
    .Returns(3);
```

#### Sequential Returns
```csharp
_userCommunicationMock
    .SetupSequence(mock => mock.ReadInteger(It.IsAny<string>()))
    .Returns(1)
    .Returns(2)
    .Returns(3);
```

**Explanation:**
- First call returns 1
- Second call returns 2
- Third call returns 3
- Simulates player guessing different numbers

## Moq Verification Techniques

### Exact Match Verification
```csharp
_userCommunicationMock.Verify(
    mock => mock.ShowMessage("You win!"));
```

Verifies method was called with exact parameter.

### Call Count Verification
```csharp
_userCommunicationMock.Verify(
    mock => mock.ShowMessage("Dice rolled. Guess what number it shows in 3 tries."),
    Times.Once());
```

Verifies method called exactly once.

### Frequency Verification
```csharp
_userCommunicationMock.Verify(
    mock => mock.ReadInteger(Resource.EnterNumberMessage),
    Times.Exactly(3));
```

Verifies method called exact number of times.

### Times Options
- `Times.Never()` - Not called
- `Times.Once()` - Called exactly once
- `Times.Exactly(n)` - Called exactly n times
- `Times.AtLeastOnce()` - Called one or more times
- `Times.AtMost(n)` - Called at most n times

## Running the Tests

### Using Visual Studio

1. **Open Test Explorer**
   - View ? Test Explorer (Ctrl+E, T)

2. **Run all tests**
   - Click "Run All" in Test Explorer
   - Or press Ctrl+R, A

3. **Run specific tests**
   - Right-click on a test and select "Run"

4. **Debug tests**
   - Right-click and select "Debug"

### Using Command Line

```bash
# Navigate to test project directory
cd DiceRollGameTests

# Run all tests
dotnet test

# Run with detailed output
dotnet test --verbosity detailed

# Run specific test
dotnet test --filter "FullyQualifiedName~Play_ShallReturnVictory_IfTheUserGuessesTheNumberOnFirstTry"

# Generate code coverage
dotnet test --collect:"XPlat Code Coverage"
```

### Sample Test Output

```
Starting test execution, please wait...
A total of 1 test files matched the specified pattern.

Passed!  - Failed:     0, Passed: 9, Skipped:0, Total:     9, Duration: 52 ms
```

## Test Framework and Dependencies

### NUnit 3.13.3
Popular unit testing framework for .NET.

**Key Features Used:**
- `[TestFixture]` - Marks the test class
- `[Test]` - Marks individual test methods
- `[TestCase]` - Parameterized tests
- `[SetUp]` - Method run before each test
- `Assert.AreEqual()` - Value comparison

### Moq 4.20.72
Modern mocking library for .NET.

**Key Features Used:**
- `Mock<T>` - Create mock objects
- `Setup()` - Configure mock behavior
- `SetupSequence()` - Configure sequential returns
- `Returns()` - Specify return values
- `Verify()` - Assert method was called
- `Times` - Specify call count expectations
- `It.IsAny<T>()` - Parameter matching

### Microsoft.NET.Test.Sdk 18.0.0
Test platform adapter for running tests.

## Project Dependencies

```xml
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="18.0.0" />
<PackageReference Include="Moq" Version="4.20.72" />
<PackageReference Include="NUnit" Version="3.13.3" />
<PackageReference Include="NUnit3TestAdapter" Version="3.13.0" />

<ProjectReference Include="..\DiceRollGameToBeTested\DiceRollGameToBeTested.csproj" />
```

## Test Design Patterns

### Arrange-Act-Assert (AAA)

All tests follow the AAA pattern:

```csharp
[Test]
public void Play_ShallReturnVictory_IfTheUserGuessesTheNumberOnFirstTry()
{
    // Arrange: Set up mocks and expectations
    const int NumberOnDie = 3;
    _diceMock.Setup(mock => mock.Roll()).Returns(NumberOnDie);
    _userCommunicationMock
        .Setup(mock => mock.ReadInteger(It.IsAny<string>()))
        .Returns(NumberOnDie);

    // Act: Execute the method under test
    var gameResult = _cut.Play();

    // Assert: Verify the outcome
    Assert.AreEqual(GameResult.Victory, gameResult);
}
```

### Test Isolation

Each test is completely isolated:
- Fresh mocks created in `[SetUp]`
- No shared state between tests
- Tests can run in any order
- Parallel execution safe

### Helper Methods

Extracted common setup logic:

```csharp
private void SetupUserGuessingTheNumberOnTheThirdTry()
{
    const int NumberOnDie = 3;
    _diceMock.Setup(mock => mock.Roll()).Returns(NumberOnDie);
    _userCommunicationMock
        .SetupSequence(mock => mock.ReadInteger(It.IsAny<string>()))
     .Returns(1)
.Returns(2)
        .Returns(NumberOnDie);
}
```

**Benefits:**
- Reduces code duplication
- Improves test readability
- Centralizes complex setup

### Meaningful Test Names

Test names follow the pattern:
```
MethodName_ShallBehavior_IfCondition
```

Examples:
- `Play_ShallReturnVictory_IfTheUserGuessesTheNumberOnFirstTry`
- `Play_ShallReturnLoss_IfTheUserNeverGuessesTheNumber`
- `PrintResult_ShallShowProperMessageForGameResult`

**Benefits:**
- Tests are self-documenting
- Clear intent from name alone
- Easy to understand failures

## Architecture

### Project Structure

```
DiceRollGameTests/
??? GuessingGameTests.cs   # Main test class
??? README.md  # This file
??? Program.cs  # Test runner entry point
??? DiceRollGameTests.csproj  # Project configuration
```

### Test Class Organization

```csharp
[TestFixture]
public class GuessingGameTests
{
    // Mock fields
    private Mock<IDice> _diceMock;
    private Mock<IUserCommunication> _userCommunicationMock;
    private GuessingGame _cut;

    // Setup
    [SetUp]
    public void Setup() { }

    // Victory tests
    [Test] public void Play_ShallReturnVictory_IfTheUserGuessesTheNumberOnFirstTry() { }
    [Test] public void Play_ShallReturnVictory_IfTheUserGuessesTheNumberOnThirdTry() { }

    // Loss tests
    [Test] public void Play_ShallReturnLoss_IfTheUserNeverGuessesTheNumber() { }
    [Test] public void Play_ShallReturnLoss_IfTheUserGuessesTheNumberOnFourthTry() { }

    // Message tests
    [TestCase] public void PrintResult_ShallShowProperMessageForGameResult() { }
    [Test] public void Play_ShallShowWelcomeMessage() { }
    [Test] public void Play_ShallShowWrongNumberTwice_IfTheUserGuessesTheNumberOnThirdTry() { }

    // Interaction tests
    [Test] public void Play_ShallAskForNumberThreeTimes_IfTheUserGuessesTheNumberOnThirdTry() { }

    // Helper methods
    private void SetupUserGuessingTheNumberOnTheThirdTry() { }
}
```

## Relationship to DiceRollGameToBeTested

This test project validates the production code:

```
DiceRollGameToBeTested (Production)
    ? (references)
DiceRollGameTests (Tests)
```

### Testing Workflow

1. **Production Code**: `DiceRollGameToBeTested` implements game logic
2. **Test Code**: `DiceRollGameTests` validates behavior
3. **Mocking**: Moq creates fake `IDice` and `IUserCommunication`
4. **Verification**: Tests ensure correct interactions and results

### Benefits of This Relationship

- **Decoupling**: Tests don't need real dice or console I/O
- **Speed**: No actual user interaction, tests run instantly
- **Determinism**: Controlled inputs produce predictable outputs
- **Isolation**: Tests focus only on `GuessingGame` logic
- **Regression Prevention**: Changes to game logic caught immediately

## Best Practices Demonstrated

### 1. Mock Isolation
Every test gets fresh mocks:
```csharp
[SetUp]
public void Setup()
{
    _diceMock = new Mock<IDice>();
    _userCommunicationMock = new Mock<IUserCommunication>();
    _cut = new GuessingGame(_diceMock.Object, _userCommunicationMock.Object);
}
```

### 2. Behavior Verification
Tests verify interactions, not just return values:
```csharp
_userCommunicationMock.Verify(
    mock => mock.ShowMessage("Wrong number."),
    Times.Exactly(2));
```

### 3. Parameterized Tests
Reduce duplication with `[TestCase]`:
```csharp
[TestCase(GameResult.Victory, "You win!")]
[TestCase(GameResult.Loss, "You lose :(")]
```

### 4. Meaningful Constants
Use named constants instead of magic numbers:
```csharp
const int NumberOnDie = 3;
const int UserNumber = 1;
```

### 5. Helper Methods
Extract complex setups:
```csharp
private void SetupUserGuessingTheNumberOnTheThirdTry()
```

## Code Coverage

Expected coverage metrics for `GuessingGame`:

| Metric | Target | Actual |
|--------|--------|--------|
| Line Coverage | 100% | 100% |
| Branch Coverage | 100% | 100% |
| Method Coverage | 100% | 100% |

All paths through the `GuessingGame` class are tested:
- Victory on first try (short-circuit)
- Victory on later tries (loop iterations)
- Loss after all tries (loop exhaustion)
- All message display paths
- All user input paths

## Advanced Moq Techniques

### Sequential Setups
```csharp
_userCommunicationMock
    .SetupSequence(mock => mock.ReadInteger(It.IsAny<string>()))
    .Returns(1)
    .Returns(2)
    .Returns(3);
```

Simulates different responses on each call.

### Argument Matching
```csharp
mock.Setup(m => m.ReadInteger(It.IsAny<string>()))
```

Matches any string argument.

### Verification with Arguments
```csharp
mock.Verify(m => m.ShowMessage("You win!"));
```

Verifies exact argument value.

### Frequency Verification
```csharp
mock.Verify(m => m.ReadInteger(Resource.EnterNumberMessage), Times.Exactly(3));
```

Verifies specific call count.

## Technical Requirements

- .NET 8.0
- C# 12.0
- NUnit 3.13.3
- Moq 4.20.72
- NUnit3TestAdapter 3.13.0
- Microsoft.NET.Test.Sdk 18.0.0

## Continuous Integration

These tests integrate into CI/CD pipelines:

```yaml
# Example GitHub Actions
- name: Run tests
  run: dotnet test --no-build --verbosity normal

- name: Generate coverage report
  run: dotnet test --collect:"XPlat Code Coverage"
```

## Debugging Tests

### When a Test Fails

1. **Read the failure message**
   ```
   Expected: Victory
   But was: Loss
 ```

2. **Check mock setups**
   - Verify dice return value
   - Verify user input sequence

3. **Set breakpoints**
   - In test method
   - In production code

4. **Debug the test**
   - Right-click ? Debug Test
   - Step through execution

### Common Failure Reasons

- Mock not configured correctly
- Wrong expected value in assertion
- Incorrect Times specification
- Off-by-one error in iteration count

## Future Enhancements

Potential test improvements:

- **Performance Tests**: Measure execution time
- **Stress Tests**: Rapid successive game plays
- **Property-Based Tests**: Use FsCheck for generative testing
- **Mutation Testing**: Use Stryker.NET to verify test quality
- **Integration Tests**: Test with real `ConsoleUserCommunication`
- **Coverage Reports**: HTML coverage reports with ReportGenerator
- **Test Categories**: Organize tests (Unit, Integration, etc.)
- **Custom Assertions**: Create domain-specific assertion helpers

## Learning Objectives

This test project demonstrates:

- **Unit Testing**: Writing effective tests with NUnit
- **Mocking**: Creating and configuring mocks with Moq
- **Test Isolation**: Fresh test state for each test
- **Behavior Verification**: Testing interactions, not just state
- **TDD**: Test-first development approach
- **AAA Pattern**: Structured test organization
- **Parameterized Tests**: Data-driven testing
- **Code Coverage**: Achieving comprehensive coverage

## Contributing

When adding new tests:

1. Follow the `MethodName_ShallBehavior_IfCondition` naming convention
2. Use `[SetUp]` for test initialization
3. Follow the AAA pattern in test methods
4. Extract common setups into helper methods
5. Verify both return values and interactions
6. Ensure tests are isolated and independent
7. Keep tests fast (< 50ms each)

## Related Resources

- [DiceRollGameToBeTested README](../DiceRollGameToBeTested/README.md)
- [NUnit Documentation](https://docs.nunit.org/)
- [Moq Documentation](https://github.com/moq/moq4)
- [Unit Testing Best Practices](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices)

## License

This project is for educational purposes demonstrating unit testing, mocking techniques, and behavior verification in C# with NUnit and Moq.
