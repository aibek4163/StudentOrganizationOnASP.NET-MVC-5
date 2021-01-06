using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentOrganization.Models
{
    public class Student
    {
        public long id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public long group_id { get; set; }
        public Group group { get; set; }
        public List<Organization> organizations { get; set; }
        public long club_id { get; set; }
        public Sport_Club club { get; set; }
    }
}