using Person.RestApi.Models;
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


namespace Person.RestApi.Controllers
{
    [RoutePrefix("records")]
    public class RecordsController : ApiController
    {
        private PersonRecordContext db = new PersonRecordContext();

        [Route("gender")]
        //GET: records/gender 
        public async Task<List<PersonRecord>> GetPersonsGenderAsync()
        {
            var model = await db.Persons.OrderBy(p=>p.Gender).ThenBy(p=>p.LastName).ToListAsync();
            return model;
        }
        [Route("birthdate")]
        //GET: records/birthdate 
        public async Task<List<PersonRecord>> GetPersonsDOBAsync()
        {
            var model = await db.Persons.OrderBy(p => p.DOB).ToListAsync();
            return model;
        }
        [Route("name"), ActionName("RecordsByName")]
        //GET: records/name  
        public async Task<List<PersonRecord>> GetPersonsLastNameAsync()
        {
            var model = await db.Persons.OrderByDescending(p => p.LastName).ToListAsync();
            return model;
        }

        // POST: api/Records
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

            var response = Request.CreateResponse(HttpStatusCode.Created);

            // Generate a link to the new book and set the Location header in the response
            return CreatedAtRoute("RecordsByName", new { id = person.Id }, person);
        }

       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonExists(int id)
        {
            return db.Persons.Count(e => e.Id == id) > 0;
        }
    }
}