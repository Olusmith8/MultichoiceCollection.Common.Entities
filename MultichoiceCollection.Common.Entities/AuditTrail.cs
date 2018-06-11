using System;
using MultichoiceCollection.Common.Entities.Base;

namespace MultichoiceCollection.Common.Entities
{
    public class AuditTrail:BaseEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public string RefUrl { get; set; }
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}