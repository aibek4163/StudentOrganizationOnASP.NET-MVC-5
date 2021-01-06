using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentOrganization.Models
{
    public class RequestOrganization
    {
        public long id { get; set; }
        public long orgID { get; set; }
        public Organization org { get; set; }
        public string studentID { get; set; }
        public ApplicationUser student { get; set; }
    }
}