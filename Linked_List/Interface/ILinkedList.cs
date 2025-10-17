using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linked_List.Interface
{
    public interface ILinkedList<T> : ICollection<T>
    {
        void AddToFront(T item);

        void AddToEnd(T item);

    }
}
