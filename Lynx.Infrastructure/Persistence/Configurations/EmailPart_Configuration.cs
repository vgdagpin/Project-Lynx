using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Constants;
using Lynx.Domain.Entities;

namespace Lynx.Infrastructure.Persistence.Configurations
{
    public partial class EmailPart_Configuration
    {
        protected override void KeyBuilder(BaseKeyBuilder<EmailPart> builder)
        {
            builder.HasKey(a => a.ID);
        }

        protected override void ConfigureProperty(BasePropertyBuilder<EmailPart> builder)
        {
            builder.Property(a => a.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.PartType)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(StringLengthConstant.Enums);
        }

        protected override void ConfigureRelationship(BaseRelationshipBuilder<EmailPart> builder)
        {
            builder.HasOne<Email>()
                .WithMany(a => a.N_Headers)
                .HasForeignKey(a => a.EmailID);
        }
    }
}