using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFundas
{
    class ChildInheritance : ParentInheritance
    {

        public void childMtd()
        {
            Debug.WriteLine("I am inside childMtd() of ChildInheritance class");
        }


        static void Main(string[] args)
        {


            ChildInheritance child = new ChildInheritance();

            child.childMtd();

            child.parentMtd();
        }



    }
}
