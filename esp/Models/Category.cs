using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace esp.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Category Name")]
        public string CategoryName { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string CategoryDescription { get; set; }

        public ICollection<Job> Jobs { get; set; }

    }
}