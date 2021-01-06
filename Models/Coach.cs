using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentOrganization.Models
{
    public class Coach
    {
        [HiddenInput(DisplayValue=false)]
        public long id { get; set; }

        [Display(Name="Name")]
        [Required(ErrorMessage ="Enter Name")]
        public string name { get; set; }

        [Display(Name = "Surname")]
        [Required(ErrorMessage = "Enter Surname")]
        public string surname { get; set; }

        [Display(Name = "Create Login")]
        [Required(ErrorMessage = "Login Required")]
        public string login { get; set; }

        [Display(Name = "Create Password")]
        //[DataType(DataType.Password)]
        [Required(ErrorMessage = "Password Required")]
        public string password { get; set; }

        [Display(Name = "Sport type")]
        [Required(ErrorMessage = "Sport name Required")]
        public string name_of_sport { get; set; }
        public Sport_Club Sport_Club { get; set; }
    }
}