//Approach 1 : declare and initialize in parallel
using System.Diagnostics;

String[] arr = { "hello", "world"};

// Approach 2 : declare first and then initialize array
String[] arrNew = new string[4];
arrNew[0] = "hello";
arrNew[1] = "world";
arrNew[2] = "bye";

// Display
Debug.WriteLine(arrNew[1]);

// loop through array
for(int i = 0; i < arrNew.Length; i++)
{
    Debug.WriteLine(arrNew[i]);

    if (arrNew[i] == "byerr")
    {
        Debug.WriteLine("Match found");
        break;
    }
    else
    {
        Debug.WriteLine("no match found");
    }


}