using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFundas
{
    class ConstructorDemo
    {

        String name;
        string firstName;
        string lastName;

        ConstructorDemo(string name) // Constructor // CTT arg scope is only for CTT block
        {
            this.name = name; // we are assigning CTT 'name' to global 'name' by using this operator 
        }

        public ConstructorDemo(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public void getName()
        {
            Debug.WriteLine("My name is " + this.name);
        }


        static void Main(String[] args)
        {

            ConstructorDemo demo = new ConstructorDemo("Amol");

            ConstructorDemo demoFullName = new ConstructorDemo("Papa", "Shango");
            
            demo.getName();
            demoFullName.getName();


        }

    }
}
