using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentOrganization.Models
{
    public class NewsTest
    {
        public long id { get; set; }
        public string title { get; set; }
        public string short_description { get; set; }
        public string description { get; set; }
        public string picture_url { get; set; }
    }
}