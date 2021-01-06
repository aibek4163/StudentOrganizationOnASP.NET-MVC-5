using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentOrganization.Models
{
    public class Organization
    {
        public long id { get; set; }
        public string leader_id { get; set; }
        public ApplicationUser student_lead { get; set; }        

        [Display(Name = "Organization Name")]
        [Required(ErrorMessage = "Name Required")]
        public string name { get; set; }

        [Display(Name = "Organization Description")]
        [Required(ErrorMessage = "Description Required")]
        public string description { get; set; }

        [Display(Name = "Picture URL")]
        [Required(ErrorMessage = "Picture URL Required")]
        public string picture_url { get; set; }
        public List<ApplicationUser> students { get; set; }

        public Organization()
        {
            students = new List<ApplicationUser>();
        }
    }
}