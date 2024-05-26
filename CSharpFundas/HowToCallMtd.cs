using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class HowToCallMtd 

{ 


    public void getData()
    {

        Debug.WriteLine("I am in getData() Method !!!");

    }


static void Main(string[] args)
    {
        HowToCallMtd howToCallMtd = new HowToCallMtd();

        //howToCallMtd.getData();

        Debug.WriteLine("We are in Main ");


        howToCallMtd.getData();


    }
}
