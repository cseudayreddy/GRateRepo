using Person.RestApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Person.WinConsole
{
    public static class Tools
    {
       

        public async static void PrintPersonAsync()
        {
            var persons = await PersonService.GetPersonsAsync();
            if (persons.Any())
            {
                Console.WriteLine("Orderb By Gender and last name");
                foreach (var pg in persons.OrderBy(p => p.Gender).ThenBy(p => p.LastName))
                {
                   
                    PrintToConsole(pg);
                }
                Console.WriteLine("Orderb By Date of birth");
                foreach (var pg in persons.OrderBy(p => p.DOB))
                {

                    PrintToConsole(pg);
                }
                Console.WriteLine("Orderb By last name descending");
                foreach (var pg in persons.OrderByDescending(p => p.LastName))
                {

                    PrintToConsole(pg);
                }
            }
        }

        private static void PrintToConsole(PersonRecord person)
        {
            
            Console.WriteLine("LastName:- {0} FirstName:- {1} Gender:- {2} FavoriteColor:- {3} Date Of Birthday:- {4}",person.LastName,person.FirstName,person.Gender,person.FavoriteColor,person.DOB.ToString("mm/dd/yyyy"));
        }

        public static void AddPerson(string path)
        {

            if (File.Exists(path))
            {
                try
                {
                    using (var sr = new StreamReader(path))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {

                            PersonService.AddPersonAsync(line);
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("File could not be readed");
                }
            }
            else { Console.WriteLine("File Doesnt exist at specified path"); }


        }
    }
}
