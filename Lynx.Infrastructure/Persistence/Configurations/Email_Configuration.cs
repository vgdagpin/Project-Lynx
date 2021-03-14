using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Constants;
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
                .HasMaxLength(500);

            builder.Property(a => a.To)
                .HasMaxLength(500);

            builder.Property(a => a.CC)
                .HasMaxLength(500);

            builder.Property(a => a.Subject)
                .HasMaxLength(200);

            builder.Property(a => a.Remarks)
                .HasMaxLength(500);

            builder.Property(a => a.Status)
                .HasConversion<string>()
                .HasMaxLength(StringLengthConstant.Enums);
        }
    }
}
