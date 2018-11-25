
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dilo.Models
{
    public class WorkItem
    {
        public Guid ID { get; set; }

        [Required]
        [Display(Name="Project")]
        public Guid? ProjectId { get; set; }
        public Project Project { get; set; }

        [Required]
        public string Tracker { get; set; }

        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Category { get; set; }
        
        [Required]
        public string Priority { get; set; }
        
        [Required]
        [Display(Name="Assignee")]
        public Guid? AssigneeId { get; set; }
        public Team Assignee { get; set; }
        public string Description { get; set; }

        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
     }
}