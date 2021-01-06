using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentOrganization.Models
{
    public class Request_SportClub
    {
        public long id { get; set; }
        public Sport_Club club { get; set; }
        public Student student { get; set; }
    }
}