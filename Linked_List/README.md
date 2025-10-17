# Linked List

A C# implementation of a generic singly linked list data structure that demonstrates fundamental concepts of linked data structures and collection interfaces.

## Overview

This project implements a custom `SinglyLinkedList<T>` class that provides a type-safe, generic linked list with full support for .NET collection interfaces. The implementation showcases modern C# features including nullable reference types, pattern matching, and yield return statements.

## Features

- Generic type support for any data type
- Implementation of `ILinkedList<T>` and `ICollection<T>` interfaces
- Full enumeration support via `IEnumerable<T>`
- Null-safe operations with nullable reference types
- Efficient node traversal using yield return

## Implementation Details

### Core Components

#### SinglyLinkedList<T>
The main data structure class that manages a collection of nodes linked in a single direction.

**Key Methods:**
- `Add(T item)` - Adds an item to the end of the list
- `AddToFront(T item)` - Adds an item to the beginning of the list
- `AddToEnd(T item)` - Adds an item to the end of the list
- `Remove(T item)` - Removes the first occurrence of an item
- `Contains(T item)` - Checks if an item exists in the list
- `Clear()` - Removes all items from the list
- `CopyTo(T[] array, int arrayIndex)` - Copies list elements to an array
- `GetEnumerator()` - Provides enumeration support

**Properties:**
- `Count` - Returns the number of elements in the list
- `IsReadOnly` - Returns false (collection is modifiable)

#### ILinkedList<T>
A custom interface that extends `ICollection<T>` with linked list-specific operations.

### Internal Structure

The implementation uses a private nested `Node` class that contains:
- `Value` - The data stored in the node
- `Next` - Reference to the next node in the sequence

The list maintains:
- `_head` - Reference to the first node
- `_count` - Tracks the number of elements for O(1) count operations

## Usage Example

```csharp
var list = new SinglyLinkedList<string>();

// Adding elements
list.AddToFront("First");
list.AddToFront("Second");
list.Add("Third");

// Checking for elements
bool exists = list.Contains("Second"); // true

// Removing elements
list.Remove("First");

// Iterating through the list
foreach (var item in list)
{
    Console.WriteLine(item);
}

// Copying to an array
var arr = new string[10];
list.CopyTo(arr, 0);
```

## Technical Requirements

- .NET 8.0
- C# 12.0

## Project Structure

```
Linked_List/
??? Interface/
?   ??? ILinkedList.cs       # Custom linked list interface
??? SinglyLinkedList.cs      # Main implementation
??? Program.cs               # Demo application
??? README.md                # This file
```

## Key Design Decisions

1. **Generic Implementation**: Uses C# generics to support any data type
2. **Interface Compliance**: Implements standard .NET collection interfaces for compatibility
3. **Nullable Support**: Properly handles null values in the collection
4. **Private Node Class**: Encapsulates internal structure details
5. **Lazy Enumeration**: Uses yield return for memory-efficient iteration
6. **Validation**: Includes proper argument validation with descriptive exceptions

## Time Complexity

- AddToFront: O(1)
- AddToEnd: O(n)
- Remove: O(n)
- Contains: O(n)
- Clear: O(n)
- GetEnumerator: O(1) initial, O(n) full enumeration

## Space Complexity

O(n) where n is the number of elements in the list.

## Exception Handling

The implementation includes proper exception handling for:
- `ArgumentNullException` - When a null array is passed to CopyTo
- `ArgumentOutOfRangeException` - When an invalid array index is provided
- `ArgumentException` - When the destination array is too small

## License

This is an educational project demonstrating data structure implementation in C#.
