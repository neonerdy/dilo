
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dilo.Models
{
    public class Project
    {
        public Guid ID { get; set; }

        [Required]
        [Display(Name="Project Name")]
        public string ProjectName { get; set; }
        
        [Required]
        public string Initial { get; set; }
        
        [Required]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        
        public string Status { get; set; }
        public List<WorkItem> WorkItems { get; set; }
    }
}