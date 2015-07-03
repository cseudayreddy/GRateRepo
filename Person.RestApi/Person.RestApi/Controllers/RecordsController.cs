using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Person.RestApi.Models;

namespace Person.RestApi.Controllers
{
     [RoutePrefix("records")]
    public class RecordsController : ApiController
    {
        private PersonRecordContext db = new PersonRecordContext();
        [Route("", Name = "GetIndividualRecord")]
        public async Task<PersonRecord> GetPersonAsync(int id)
        {
            var model = await db.Persons.FirstOrDefaultAsync(p => p.Id == id);
            return model;
        }


        [Route("gender")]
        //GET: records/gender 
        public async Task<List<Record>> GetPersonsGenderAsync()
        {
            var model = await db.Persons.OrderBy(p => p.Gender).ThenBy(p => p.LastName).ToListAsync();
            return model.Select(p => new Record { LastName = p.LastName, FirstName = p.FirstName, FavoriteColor = p.FavoriteColor, Gender = p.Gender, DateOfBirth = p.DOB.ToString("MM/dd/yyyy") }).ToList();

        }
        [Route("birthdate")]
        //GET: records/birthdate 
        public async Task<List<Record>> GetPersonsDOBAsync()
        {
            var model = await db.Persons.OrderBy(p => p.DOB).ToListAsync();
            return model.Select(p => new Record { LastName = p.LastName, FirstName = p.FirstName, FavoriteColor = p.FavoriteColor, Gender = p.Gender, DateOfBirth = p.DOB.ToString("MM/dd/yyyy") }).ToList();
        }
        [Route("name")]
        //GET: records/name  
        public async Task<List<Record>> GetPersonsLastNameAsync()
        {
            var model = await db.Persons.OrderByDescending(p => p.LastName).ToListAsync();
            return model.Select(p =>
                new Record
                {
                    LastName = p.LastName,
                    FirstName = p.FirstName,
                    FavoriteColor = p.FavoriteColor,
                    Gender = p.Gender,
                    DateOfBirth = p.DOB.ToString("MM/dd/yyyy")
                }).ToList();
        }

        // POST: Records
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(PersonRecord))]
        public async Task<IHttpActionResult> PostPerson([FromBody]string value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var person = PersonService.CreatePersonFromString(value);
            db.Persons.Add(person);
            await db.SaveChangesAsync();



            // Generate a link to the new person record
            return CreatedAtRoute("GetIndividualRecord", new { id = person.Id }, person);
        }

        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonRecordExists(int id)
        {
            return db.Persons.Count(e => e.Id == id) > 0;
        }
    }
}