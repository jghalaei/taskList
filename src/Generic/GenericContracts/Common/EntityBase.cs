using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericContracts.Common;

public class EntityBase
{
    public Guid Id { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
}