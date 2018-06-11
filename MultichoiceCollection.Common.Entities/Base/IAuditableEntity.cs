using System;

namespace MultichoiceCollection.Common.Entities.Base
{

    public interface IAuditableEntity
    {
        DateTime CreatedDate { get; set; }

        string CreatedBy { get; set; }

        DateTime UpdatedDate { get; set; }

        string UpdatedBy { get; set; }

        string IP { get; set; }
    }
}
