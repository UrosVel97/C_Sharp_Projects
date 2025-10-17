namespace Linked_List;

internal class Program
{
    static void Main(string[] args)
    {

        var list = new SinglyLinkedList<string>();

        list.AddToFront("First");
        list.AddToFront("Second");
        list.AddToFront("Third");
        list.AddToFront("Forth");
        list.Add("Fifth");
        list.Add("Sixth");

        Console.WriteLine("Contains 'Third': " + list.Contains("Third"));
        Console.WriteLine("Contains 'Firsst; " + list.Contains("Firsst"));

        list.Remove("Third");
        list.Remove("First");

        var arr = new string[7];
        list.CopyTo(arr, 2);

        foreach (var item in list)
        {
            Console.WriteLine(item);
        }

    }
}




