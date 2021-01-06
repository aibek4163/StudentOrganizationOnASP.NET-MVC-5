using StudentOrganization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentOrganization.ViewModels
{
    public class ViewGroupModel
    {
        public Group group { get; set; }
        public IEnumerable<Course> courses { get; set; }
    }
}