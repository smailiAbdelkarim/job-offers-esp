using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace esp.Models
{
    public class Job
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Job Name")]
        public string JobName { get; set; }
        [Required]
        [DisplayName("Description")]
        public string JobDescription { get; set; }
        [Required]
        [DisplayName("workplace")]
        public string JobPlace { get; set; }
        [Required]
        [DisplayName("Image")]
        public string JobImage { get; set; }
        [DisplayName("Job category")]
        public int CategoryId { get; set; } // each job has category id
        public Category Category { get; set; }
    }
}