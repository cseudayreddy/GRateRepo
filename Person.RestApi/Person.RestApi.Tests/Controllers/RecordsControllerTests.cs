using Microsoft.VisualStudio.TestTools.UnitTesting;
using Person.RestApi.Controllers;
using Person.RestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Script.Serialization;

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
        public async Task records_contrller_returns_name_in_descending_order()
        {

            var results = await ctrl.GetPersonsLastNameAsync();

            string prevName = results[0].LastName;
            var list = results.ToList();
            foreach (var person in list)
            {
                Assert.IsTrue(prevName.CompareTo(person.LastName) >= 0);
                prevName = person.LastName;

            }

        }

        [TestMethod]
        public async Task records_contrller_returns_dob_in_ascending_order()
        {

            var results = await ctrl.GetPersonsDOBAsync();

            DateTime dob = results[0].DateOfBirth.ToDate();
            var list = results.ToList();
            foreach (var person in list)
            {
                Assert.IsTrue(dob.CompareTo(person.DateOfBirth.ToDate()) <= 0);
                dob = person.DateOfBirth.ToDate();

            }

        }
    }
}
