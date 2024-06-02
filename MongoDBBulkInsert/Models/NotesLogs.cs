using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBBulkInsert.Models
{
    public class NotesLog
    {
       public Guid? ID { get; set; }
        public string? PortalName { get; set; }
        public string? ClientName { get; set; }
        public string? AccountNumber { get; set; }
        public string? AgentId { get; set; }
        public string? Description { get; set; }
        public string? CreatedDate { get; set; }
    }
}
