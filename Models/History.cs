
using System;

namespace Dilo.Models
{
    public class History
    {
        public Guid ID { get; set; }
        public Guid WorkItemId { get; set; }
        public DateTime Date { get; set; }
        public string ChangeLog { get; set; }
    }
}