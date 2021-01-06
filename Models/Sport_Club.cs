using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StudentOrganization.Models
{
    public class Sport_Club
    {
        public long id { get; set; }
        public long coach_id { get; set; }
        public Coach Coach { get; set; }
        public long student_id { get; set; }
        public List<Student> players { get; set; }

        [Display(Name = "Type Sport")]
        [Required(ErrorMessage = "Name Required")]
        public string type_sport { get; set; }

        [Display(Name = "Club Name")]
        [Required(ErrorMessage = "Name Required")]
        public string club_name { get; set; }

        [Display(Name = "Players amount")]
        [Required(ErrorMessage = "Number Required")]
        //[Range(1,100)]
        public int players_amount { get; set; }

        [Display(Name = "Founded Year")]
        [Required(ErrorMessage = "Founded Year Required")]
        public int founded_year { get; set; }
    }
}