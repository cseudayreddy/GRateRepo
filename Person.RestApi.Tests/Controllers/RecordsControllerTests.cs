using Microsoft.VisualStudio.TestTools.UnitTesting;
using Person.RestApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person.RestApi.Tests.Controllers
{
    [TestClass]
    public class RecordsControllerTests
    {
        private RecordsController ctrl;
        public RecordsControllerTests()
        {
            ctrl = new RecordsController();
        }
        [TestMethod]
        public async Task records_contrller_returns_gender_in_ascending_order()
        {

            var results = await ctrl.GetPersonsGenderAsync();
            string prevGen = string.Empty;
            string prevName = string.Empty;
            var list = results.ToList();
            foreach (var person in list)
            {
                Assert.IsTrue(prevGen.CompareTo(person.Gender) <= 0);
                if (prevGen == person.Gender)
                    Assert.IsTrue(prevName.CompareTo(person.LastName) <= 0);
                prevGen = person.Gender;
                prevName = person.LastName;

            }

        }

        [TestMethod]
        public async Task records_contrller_returns_name_in_ascending_order()
        {
            
            var results = await ctrl.GetPersonsGenderAsync();
            
            string prevName = string.Empty;
            var list = results.ToList();
            foreach (var person in list)
            {
                Assert.IsTrue(prevName.CompareTo(person.LastName) <= 0);
                prevName = person.LastName;

            }

        }
    }
}
