using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentOrganization.Models
{
    public class Event
    {
        public long id { get; set; }

        public Organization org_id { get; set; }
        [Display(Name ="Event Name")]
        [Required(ErrorMessage = "Name Required")]
        public string name { get; set; }

        [Required(ErrorMessage = "Choose date")]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime date { get; set; }

        [Required(ErrorMessage = "Write description")]
        [Display(Name = "Description")]
        public string description { get; set; }

        [Display(Name = "Picture URL")]
        public string picture_url { get; set; }
    }
}