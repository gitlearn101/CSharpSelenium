// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
/*
class Program
{
    static void Main(string[] args)
    {
        Debug.WriteLine("Hello World from C#"); // displayed in VS terminal

        Console.WriteLine("WriteLine...");  // Displayed in cmd terminal
    }
}
*/

class Program2
{
    static void Main(string[] args)
    {
        int a = 4;

        Console.WriteLine("The int is :" + a);

        Console.WriteLine($"The int is {a}");       // use of $

        var age = "23";     // var is dynamic datatype and can accept int/string based on tje input provided by the user.

        Console.WriteLine("The age is : " + age);

        dynamic ht = 13.2;  // dynamic is similar to var, the extra advantage is that you can change datatype of dynamic during runtime.
        Console.WriteLine($"The height is {ht}");
        ht = "hello";

        Console.WriteLine($"The height is {ht}");

    }
}
