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
       
        //Parse the list and prints diffrent output.
        public async static void PrintPersonAsync()
        {
            var persons = await PersonService.GetPersonsAsync();
            if (persons.Any())
            {
                Console.WriteLine("Orderb By Gender and last name");
                //prints order by gender and last name
                foreach (var pg in persons.OrderBy(p => p.Gender).ThenBy(p => p.LastName))
                {
                   
                    PrintToConsole(pg);
                }
                Console.WriteLine("Orderb By Date of birth");
                //prints order by date of bith
                foreach (var pg in persons.OrderBy(p => p.DOB))
                {

                    PrintToConsole(pg);
                }
                Console.WriteLine("Orderb By last name descending");
                //prints order by the last name
                foreach (var pg in persons.OrderByDescending(p => p.LastName))
                {

                    PrintToConsole(pg);
                }
            }
        }

        //Print the records of person to console
        private static void PrintToConsole(PersonRecord person)
        {
            
            Console.WriteLine("LastName:- {0} FirstName:- {1} Gender:- {2} FavoriteColor:- {3} Date Of Birthday:- {4}",person.LastName,person.FirstName,person.Gender,person.FavoriteColor,person.DOB.ToString("mm/dd/yyyy"));
        }

        //Add Person from file path
        public static void AddPerson(string path)
        {

            if (File.Exists(path))
            {
                try
                {
                    //read the file
                    using (var sr = new StreamReader(path))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            //add person to db asynchroniously
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
