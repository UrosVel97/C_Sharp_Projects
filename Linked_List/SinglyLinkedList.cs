using System.Collections;
using Linked_List.Interface;

namespace Linked_List;

public class SinglyLinkedList<T> : ILinkedList<T?>
{
    private Node? _head;
    private int _count; // keep track of the number of elements



    public int Count => _count;

    public bool IsReadOnly => false;

    public void Add(T? item)
    {
        AddToEnd(item);
    }

    public void AddToEnd(T? item)
    {
        var newNode = new Node(item);
        if (_head is null)
        {
            _head = newNode;
        }
        else
        {
            var tail = GetNodes().Last();

            tail.Next = newNode;
        }

        _count++;
    }

    public void AddToFront(T? item)
    {
        var newHead = new Node(item)
        {
            Next = _head
        };
        _head = newHead;
        _count++;
    }

    public void Clear()
    {
        Node? current = _head;

        while (current is not null)
        {
            var temporary = current.Next;
            current.Next = null;
            current = temporary;
        }

        _head = null;
        _count = 0;
    }

    public bool Contains(T? item)
    {
        if(item is null)
        {
            return GetNodes().Any(node => node.Value is null);
        }

        return GetNodes().Any(node => item.Equals(node.Value));
    }

    public void CopyTo(T?[] array, int arrayIndex)
    {
        if(array is null)
        {
            throw new ArgumentNullException(nameof(array));
        }
        if(arrayIndex<0 || arrayIndex >= array.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(array));
        }
        if(array.Length < _count + arrayIndex)
        {
            throw new ArgumentException("Array is not long enough");
        }

        foreach(var node in GetNodes())
        {
            array[arrayIndex] = node.Value;
            ++arrayIndex;
        }

    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<T?> GetEnumerator()
    {
        foreach (var node in GetNodes())
        {
            yield return node.Value;
        }
    }

    public bool Remove(T? item)
    {
        Node? predecesor = null;
        foreach (var node in GetNodes())
        {

            if ((node.Value is null && item is null) ||
                (node.Value is not null && node.Value.Equals(item)))
            {
                if (predecesor is null)
                {
                    _head = node.Next;

                }
                else
                {
                    predecesor.Next = node.Next;

                }

                _count--;
                return true;
            }
            predecesor = node;
        }
        return false;
    }



    private IEnumerable<Node> GetNodes()
    {


        Node? current = _head;

        while (current is not null)
        {
            yield return current;
            current = current.Next;
        }


    }

    private class Node
    {
        public T? Value { get; }

        public Node? Next { get; set; }

        public Node(T? value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return $"Value: {Value}, " +
                $"Next: {(Next is null ? "null" : Next.Value)}";

        }
    }

}




