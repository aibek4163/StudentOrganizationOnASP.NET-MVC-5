using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentOrganization.Models
{
    //:IValidatableObjects
    public class Course 
    {
        public long id { get; set; }

        [Display(Name = "Course")]
       // [Required(ErrorMessage = "Course number must be numeric")]
        public int number_course { get; set; }
        public List<Group> groups { get; set; }



        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    var res = new List<ValidationResult>();
        //    if (this.number_course < 0)
        //    {
        //        res.Add(new ValidationResult("Course Not Enterred"));
        //    }
        //    if (this.number_course > 0 && this.number_course < 5)
        //    {
        //        res.Add(new ValidationResult("This course does not exist in our elite University"));
        //    }
        //    return res;
        //}
    }
}