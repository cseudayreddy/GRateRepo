using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Person.RestApi.Models
{
    public class PersonRecordContext : DbContext
    {
        public PersonRecordContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer<PersonRecordContext>(new CreateDatabaseIfNotExists<PersonRecordContext>());
        }
        public DbSet<PersonRecord> Persons { get; set; }
    }
}
