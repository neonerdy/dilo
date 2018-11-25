
using System;
using System.ComponentModel.DataAnnotations;

namespace Dilo.Models
{
    public class Team
    {
        public Guid ID { get; set; }
    
        [Required]
        [Display(Name="Full Name")]
        public string FullName { get; set; }

        [Required]
        public string Role { get; set; }
        
        [Required]
        public string Address { get; set; }
        
        [Required]
        public string Phone { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        
    }
}