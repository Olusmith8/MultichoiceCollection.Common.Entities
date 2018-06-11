using System;
using System.ComponentModel.DataAnnotations;

namespace MultichoiceCollection.Common.Entities.Base
{

    /// <summary>
    /// Used for entities that user need to be stored 
    /// </summary>
    public abstract class AuditableEntity:BaseEntity, IAuditableEntity
    {
        [ScaffoldColumn(false)]
        public DateTime CreatedDate { get; set; }

        [MaxLength(256)]
        [ScaffoldColumn(false)] 
        public string CreatedBy { get; set; }

        [ScaffoldColumn(false)]
        public DateTime UpdatedDate { get; set; }

        [MaxLength(256)]
        [ScaffoldColumn(false)]
        public string UpdatedBy { get; set; }

        [MaxLength(256)]
        [ScaffoldColumn(false)]
        public string IP { get; set; }
    }
}
