
using System;

namespace Dilo.Models
{
    public class Comment
    {
        public Guid ID { get; set;}
        public Guid WorkItemId { get; set; }
        public string CommentedBy { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}