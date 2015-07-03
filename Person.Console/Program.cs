using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person.WinConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true) // Loop indefinitely
            {
                Console.WriteLine("Please enter file path: "); // Prompt
                string path = Console.ReadLine(); // Get path from user
                Tools.AddPerson(path);

                Tools.PrintPersonAsync();

            }
        }
    }
}
