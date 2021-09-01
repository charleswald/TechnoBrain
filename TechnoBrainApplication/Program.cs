using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TechnoBrainApplication
{
    class Program
    {

        
        static void Main(string[] args)
        {
            var path = "testEmployee.csv";



            Employees e = new Employees(File.ReadAllLines(path));

            //e.ProcessEmployees(path);
        }

    }
}
