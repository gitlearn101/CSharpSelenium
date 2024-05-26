// ArrayList 
using System.Collections;
using System.Diagnostics;

ArrayList al = new ArrayList();
al.Add(9);
al.Add(3);

// enhanced for loop

foreach(int s in al)
{
    Debug.WriteLine(s);
}


// Contains
if(al.Contains(9)){
    Debug.WriteLine("match of 9 found in the arraylist");
}

// Sorting
al.Sort();

Debug.WriteLine("After sort");
foreach (int s in al)
{
    Debug.WriteLine(s);
}
