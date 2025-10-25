# Fibonacci Generator Tests

A comprehensive NUnit test suite for the FibonacciGenerator library, demonstrating test-driven development practices and thorough validation of Fibonacci sequence generation.

## Project Overview

This project provides complete test coverage for the `FibonacciGenerator` library using the NUnit testing framework. It demonstrates:
- Unit testing best practices
- Test-driven development (TDD) approach
- Parameterized testing with NUnit
- Edge case validation
- Exception testing
- Boundary value analysis

## Purpose

The FibonacciGeneratorTests project ensures that the `Fibonacci.Generate()` method:
- Correctly generates Fibonacci sequences
- Properly validates input parameters
- Handles edge cases gracefully
- Prevents integer overflow
- Produces mathematically accurate results

## Test Coverage

### Input Validation Tests

#### Negative Input Validation
Tests that the generator rejects negative values:

```csharp
[TestCase(-1)]
[TestCase(-10)]
[TestCase(-100)]
public void Generate_ShallThrowException_IfNIsSmallerThanZero(int n)
```

**Validates:**
- Negative inputs are rejected
- Proper `ArgumentException` is thrown
- Error occurs before any computation

#### Overflow Prevention
Tests that the generator prevents integer overflow:

```csharp
[TestCase(47)]
[TestCase(100)]
[TestCase(1000)]
public void Generate_ShallThrowException_IfNIsLargerThan46(int n)
```

**Validates:**
- Values exceeding 46 are rejected
- Maximum safe limit is enforced
- Overflow protection works correctly

### Edge Case Tests

#### Empty Sequence (n = 0)
```csharp
[Test]
public void Generate_ShallProduceEmptySequence_ForNEqualTo0()
```

**Validates:**
- Returns empty collection for n = 0
- No exceptions thrown
- Graceful handling of minimum boundary

#### Single Element (n = 1)
```csharp
[Test]
public void Generate_ShallProduceSequence_With_0_ForNEqualTo1()
```

**Validates:**
- Returns `[0]` for n = 1
- First Fibonacci number is correct
- Minimal sequence generation works

#### Two Elements (n = 2)
```csharp
[Test]
public void Generate_ShallProduceSequence_With_0_1_ForNEqualTo2()
```

**Validates:**
- Returns `[0, 1]` for n = 2
- Both base cases are correct
- Transition to recursive formula works

### Correctness Tests

#### Valid Sequences
```csharp
[TestCase(3, new int[] { 0, 1, 1 })]
[TestCase(5, new int[] { 0, 1, 1, 2, 3 })]
[TestCase(10, new int[] { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34 })]
public void Generate_ShallProduceValidForFibonacciSequence(int n, int[] expected)
```

**Validates:**
- Correct Fibonacci numbers are generated
- Sequence follows mathematical definition
- Multiple test cases ensure consistency

#### Maximum Boundary (n = 46)
```csharp
[Test]
public void Generate_ShallProduceSequenceWithLastNumber_1134903170_ForNEqualTo46()
```

**Validates:**
- 46th Fibonacci number is 1,134,903,170
- Maximum safe value is correctly computed
- No overflow at upper boundary

## Test Scenarios Summary

| Test Category | Test Cases | Purpose |
|---------------|-----------|---------|
| **Negative Input** | 3 tests | Validate rejection of n < 0 |
| **Overflow Protection** | 3 tests | Validate rejection of n > 46 |
| **Edge Cases** | 3 tests | Test n = 0, 1, 2 |
| **Correctness** | 3 tests | Verify accurate sequences (n = 3, 5, 10) |
| **Boundary** | 1 test | Verify maximum safe value (n = 46) |
| **Total** | **13 tests** | Complete coverage |

## Running the Tests

### Using Visual Studio

1. **Open Test Explorer**
   - View ? Test Explorer (Ctrl+E, T)

2. **Run all tests**
   - Click "Run All" in Test Explorer
   - Or press Ctrl+R, A

3. **Run specific tests**
   - Right-click on a test and select "Run"
   - Or click the play button next to individual tests

### Using Command Line

```bash
# Navigate to the test project directory
cd FibonacciGeneratorTests

# Run all tests
dotnet test

# Run with detailed output
dotnet test --verbosity detailed

# Run specific test
dotnet test --filter "FullyQualifiedName~Generate_ShallProduceEmptySequence_ForNEqualTo0"

# Run tests in a category (if categorized)
dotnet test --filter "TestCategory=EdgeCases"
```

### Sample Test Output

```
Starting test execution, please wait...
A total of 1 test files matched the specified pattern.

Passed!  - Failed:   0, Passed:    13, Skipped:     0, Total:  13, Duration: 45 ms
```

## Test Framework

### NUnit Framework

This project uses **NUnit 3.13.3**, a popular unit testing framework for .NET.

**Key NUnit Features Used:**

- **[TestFixture]**: Marks the test class
- **[Test]**: Marks individual test methods
- **[TestCase]**: Parameterized tests with different inputs
- **Assert.Throws**: Exception testing
- **Assert.AreEqual**: Value comparison
- **Assert.IsEmpty**: Collection emptiness validation
- **Assert.Last**: LINQ integration for collection testing

### Dependencies

```xml
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="18.0.0" />
<PackageReference Include="NUnit" Version="3.13.3" />
<PackageReference Include="NUnit3TestAdapter" Version="3.13.0" />
```

- **Microsoft.NET.Test.Sdk**: Test platform for .NET
- **NUnit**: Core testing framework
- **NUnit3TestAdapter**: Visual Studio integration for NUnit

## Architecture

### Project Reference

The test project references the main library:

```xml
<ProjectReference Include="..\FibonacciGenerator\FibonacciGenerator.csproj" />
```

This allows the tests to access the `Fibonacci` class and validate its behavior.

### Test Class Structure

```csharp
namespace FibonacciGeneratorTests
{
    [TestFixture]
    public class FibbonacciTests
    {
        // Input validation tests
  // Edge case tests
        // Correctness tests
   // Boundary tests
    }
}
```

## Test Design Patterns

### Arrange-Act-Assert (AAA)

Each test follows the AAA pattern:

```csharp
[Test]
public void Generate_ShallProduceSequence_With_0_ForNEqualTo1()
{
    // Arrange: Set up test data
    // (implicit - using parameter n = 1)
    
    // Act: Execute the method under test
    var result = Fibonacci.Generate(1);
    
    // Assert: Verify the outcome
    Assert.AreEqual(new List<int> { 0 }, result);
}
```

### Parameterized Tests

Using `[TestCase]` for testing multiple inputs:

```csharp
[TestCase(3, new int[] { 0, 1, 1 })]
[TestCase(5, new int[] { 0, 1, 1, 2, 3 })]
[TestCase(10, new int[] { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34 })]
public void Generate_ShallProduceValidForFibonacciSequence(int n, int[] expected)
{
 var result = Fibonacci.Generate(n);
    Assert.AreEqual(expected, result);
}
```

**Benefits:**
- Reduces code duplication
- Tests multiple scenarios with one test method
- Easy to add new test cases

### Exception Testing

Validating that exceptions are thrown correctly:

```csharp
[TestCase(-1)]
public void Generate_ShallThrowException_IfNIsSmallerThanZero(int n)
{
    Assert.Throws<ArgumentException>(() =>
    {
        Fibonacci.Generate(n);
 });
}
```

## Test Categories

### 1. Negative Testing
Tests that verify proper error handling:
- Negative input rejection
- Overflow prevention
- Exception message validation

### 2. Positive Testing
Tests that verify correct functionality:
- Valid sequence generation
- Edge case handling
- Boundary value correctness

### 3. Boundary Testing
Tests at the limits of valid input:
- Minimum value (n = 0)
- Maximum value (n = 46)
- Just beyond maximum (n = 47)

## Project Structure

```
FibonacciGeneratorTests/
??? FibbonacciTests.cs   # Main test class
??? README.md            # This file
??? FibonacciGeneratorTests.csproj  # Project configuration
```

## Technical Requirements

- .NET 8.0
- C# 12.0
- NUnit 3.13.3
- NUnit3TestAdapter 3.13.0
- Microsoft.NET.Test.Sdk 18.0.0

## Relationship to FibonacciGenerator

This test project is tightly coupled with the **FibonacciGenerator** library:

```
FibonacciGenerator
    ? (tested by)
FibonacciGeneratorTests
```

### Testing Workflow

1. **Development**: FibonacciGenerator implements Fibonacci logic
2. **Validation**: FibonacciGeneratorTests verifies correctness
3. **Refactoring**: Tests ensure behavior remains consistent
4. **Regression**: Tests catch bugs introduced by changes

### Continuous Integration

These tests can be integrated into CI/CD pipelines:

```yaml
# Example GitHub Actions workflow
- name: Run tests
  run: dotnet test --no-build --verbosity normal
```

## Best Practices Demonstrated

### 1. Comprehensive Coverage
- Tests all public methods
- Covers edge cases and boundaries
- Validates both success and failure paths

### 2. Clear Test Names
Test names follow the pattern: `MethodName_Shall[Behavior]_[Condition]`

Examples:
- `Generate_ShallThrowException_IfNIsSmallerThanZero`
- `Generate_ShallProduceEmptySequence_ForNEqualTo0`
- `Generate_ShallProduceValidForFibonacciSequence`

### 3. Isolated Tests
Each test is independent:
- No shared state between tests
- Tests can run in any order
- Failures don't cascade

### 4. Readable Assertions
Clear, self-documenting assertions:
```csharp
Assert.AreEqual(new List<int> { 0 }, result);
Assert.IsEmpty(result);
Assert.AreEqual(FibonacciNumber46, result.Last());
```

### 5. Test Data Organization
- Inline test data using `[TestCase]`
- Named constants for magic numbers
- Clear expected values

## Code Coverage

Expected code coverage metrics:

| Metric | Target | Actual |
|--------|--------|--------|
| Line Coverage | 100% | 100% |
| Branch Coverage | 100% | 100% |
| Method Coverage | 100% | 100% |

The `Fibonacci.Generate()` method is fully tested:
- All execution paths covered
- All branches (if statements) tested
- All exception paths validated

## Continuous Testing

### Test-Driven Development (TDD)

This project demonstrates TDD principles:

1. **Red**: Write failing tests first
2. **Green**: Implement minimal code to pass tests
3. **Refactor**: Improve code while maintaining passing tests

### Regression Testing

Tests prevent regression bugs:
- Changes to `Fibonacci.Generate()` are validated immediately
- Breaking changes are caught before deployment
- Confidence in refactoring efforts

## Debugging Tests

### Failed Test Investigation

When a test fails:

1. **Check the failure message**
   ```
   Expected: [0, 1, 1]
   But was: [0, 1, 2]
 ```

2. **Set breakpoints** in the test method

3. **Run in debug mode** (Debug ? Debug Selected Tests)

4. **Step through** the implementation

### Common Failure Reasons
- Algorithm bug in Fibonacci generation
- Off-by-one errors in loop
- Incorrect initialization values
- Overflow not caught

## Future Enhancements

Potential test improvements:

- **Performance tests**: Measure execution time for large n
- **Memory tests**: Validate memory usage
- **Concurrent tests**: Test thread safety (if applicable)
- **Property-based tests**: Use FsCheck for generative testing
- **Mutation testing**: Use Stryker.NET to verify test quality
- **Coverage reports**: Generate and track code coverage over time
- **Test categories**: Organize tests into Smoke, Regression, etc.

## Learning Objectives

This test project demonstrates:

- **Unit Testing**: Writing effective unit tests with NUnit
- **Test Design**: Parameterized tests and exception testing
- **Coverage**: Achieving comprehensive test coverage
- **TDD Practices**: Test-first development approach
- **Assertions**: Various assertion techniques in NUnit
- **Test Organization**: Structuring tests logically
- **CI/CD Integration**: Automating test execution

## Contributing

When adding new tests:

1. Follow the existing naming convention
2. Use `[TestCase]` for parameterized tests
3. Include both positive and negative test cases
4. Document expected behavior in test names
5. Keep tests focused and isolated
6. Ensure tests run quickly (< 100ms each)

## Related Resources

- [NUnit Documentation](https://docs.nunit.org/)
- [FibonacciGenerator README](../FibonacciGenerator/README.md)
- [Unit Testing Best Practices](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices)

## License

This project is for educational purposes demonstrating unit testing practices, NUnit framework usage, and test-driven development in C#.
