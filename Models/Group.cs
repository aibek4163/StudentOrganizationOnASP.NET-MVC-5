using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentOrganization.Models
{
    //IValidatableObject
    public class Group
    {
        public long id { get; set; }

        [Display(Name ="Group Name")]
        [Required(ErrorMessage = "Group Name Required")]
        //[StringLength(255, MinimumLength = 9)] //CSSE-1801-SWD
        public string group_name { get; set; }
        public List<Student> students { get; set; }
        public long courseId { get; set; }
        public Course course { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    var res = new List<ValidationResult>();
        //    if (string.IsNullOrWhiteSpace(this.group_name))
        //    {
        //        res.Add(new ValidationResult("Group Name Not Enterred"));
        //    }
        //    if (!this.group_name.Contains("CSSE") || !this.group_name.Contains("DA")
        //        || !this.group_name.Contains("NSA") || !this.group_name.Contains("ROB"))
        //    {
        //        res.Add(new ValidationResult("Group Name Incorrect!"));
        //    }
        //    return res;
        //}
    }
}