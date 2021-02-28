using System;
using System.Collections.Generic;
using System.Text;

namespace Lynx.Domain.Entities
{
    public class ProviderTypeConfigWebService : BaseEntity
    {
        public Guid UserID { get; set; }

        public string Indentity { get; set; }
    }
}
