using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentOrganization.Models
{
    public class News
    {
        public long id { get; set; }
        [Display(Name = "Title Name")]
        [Required(ErrorMessage = "Name Required")]
        [Remote("Unique","News",ErrorMessage ="Title should be unique")]
        public string title { get; set; }

        [Display(Name = "Short Description")]
        [Required(ErrorMessage = "Short Description Required")]
        public string short_description { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = " Description Required")]
        public string description { get; set; }

        [Display(Name = "Picture URL")]
        [Required(ErrorMessage = "Picture URL Required")]
        public string picture_url { get; set; }
    }
}