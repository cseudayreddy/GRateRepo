using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;

namespace Person.RestApi.Models
{
    public static class PersonService
    {
        //Convert to DateTime
        public static DateTime ToDate(this string date)
        {
            DateTime value;
            try
            {
                value = Convert.ToDateTime(date);
            }
            catch
            {
                value = DateTime.Now;
            }

            return value;
        }

        //split the string by delimeter and create person object
        public static PersonRecord CreatePersonFromString(string value)
        {
            PersonRecord per;
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("input string canot be empty");
            var elems = value.Split(new Char[] { ',', '|', ' ' },
                                       StringSplitOptions.RemoveEmptyEntries);
            return per = new PersonRecord()
            {
                LastName = elems[0],
                FirstName = elems[1],
                Gender = elems[2],
                FavoriteColor = elems[3],
                DOB = elems[4].ToDate(),
            };

        }
        //Add the person async
        public async static Task AddPersonAsync(string value)
        {
            var per = CreatePersonFromString(value);
            using (var db = new PersonRecordContext())
            {
                db.PersonRecords.Add(per);
                await db.SaveChangesAsync();
            }
        }

        //get the person async
        public async static Task<List<PersonRecord>> GetPersonsAsync()
        {
            using (var db = new PersonRecordContext())
            {
                return await db.PersonRecords.ToListAsync();
            }
        }



    }
}
