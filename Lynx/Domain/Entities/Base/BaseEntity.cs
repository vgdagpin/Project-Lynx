using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid ID { get; set; } = Guid.NewGuid();
    }
}
