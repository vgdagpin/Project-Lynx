using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.Entities;

namespace Lynx.Infrastructure.Persistence.Configurations
{
    public partial class Email_Configuration
    {
        protected override void KeyBuilder(BaseKeyBuilder<Email> builder)
        {
            builder.HasKey(a => a.ID);
        }

        protected override void ConfigureProperty(BasePropertyBuilder<Email> builder)
        {
            builder.Property(a => a.From)
                .HasMaxLength(255);

            builder.Property(a => a.Remarks)
                .HasMaxLength(500);
        }
    }
}
