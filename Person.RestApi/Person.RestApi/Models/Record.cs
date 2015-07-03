using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Person.RestApi.Models
{
    public class Record
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public string FavoriteColor { get; set; }
        public string DateOfBirth { get; set; }
    }
}