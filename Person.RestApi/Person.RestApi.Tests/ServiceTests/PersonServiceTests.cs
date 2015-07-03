using Microsoft.VisualStudio.TestTools.UnitTesting;
using Person.RestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person.RestApi.Tests.ServiceTests
{
    [TestClass]
    public class PersonServiceTests
    {
        [TestMethod, TestCategory("Functional Positive- PersonService")]
        public async Task add_new_person_using_personservice()
        {
            
            var records = await PersonService.GetPersonsAsync();
            int count =  records.Count();
            await PersonService.AddPersonAsync("mands|uday|M|red|05/16/1989");
            var recs = await PersonService.GetPersonsAsync();
            int count1 = recs.Count();
            Assert.AreEqual(count + 1, count1);
        }

        [TestMethod, TestCategory("Functional Positive - PersonService")]
        public void split_string_on_delimiter_and_create_person_objects()
        {
            var input = "manda|uday|M|red|05/16/1989";
            var record = PersonService.CreatePersonFromString(input);
            Assert.AreEqual(record.FirstName, "uday");
            Assert.AreEqual(record.LastName, "manda");
            Assert.AreEqual(record.Gender, "M");
            Assert.AreEqual(record.FavoriteColor, "red");
            Assert.AreEqual(record.DOB.ToString("MM/dd/yyyy"), "05/16/1989");
        }

        [TestMethod, TestCategory("Functional Positive - PersonService")]
        public void convert_string_to_dataformat()
        {
            var input = "05/13/1988";
            var date = PersonService.ToDate(input);
            Assert.AreEqual(date.ToString("MM/dd/yyyy"),input);
        }

        [TestMethod, TestCategory("Functaional - Negative - PersonService")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void personservice_throws_argumentexception_if_argument_is_nullorempty()
        {
            PersonService.CreatePersonFromString(string.Empty);
        }
    }
}
