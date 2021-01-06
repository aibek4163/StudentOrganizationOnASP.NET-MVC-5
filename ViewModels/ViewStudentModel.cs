using StudentOrganization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentOrganization.ViewModels
{
    public class ViewStudentModel
    {
        public Student student { get; set; }
        public Group gr { get; set; }
        public Course cr { get; set; }
        public IEnumerable<Group> group { get; set; }
        public IEnumerable<Course> courses { get; set; }


        public long Course { get; set; }
        public long Group { get; set; }
        public long organizationID { get; set; }
        //public long groupID { get; set; }
        //public long courseID { get; set; }
    }
}