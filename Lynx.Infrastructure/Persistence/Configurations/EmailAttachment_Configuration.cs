using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.Entities;

namespace Lynx.Infrastructure.Persistence.Configurations
{
    public partial class EmailAttachment_Configuration
    {
        protected override void KeyBuilder(BaseKeyBuilder<EmailAttachment> builder)
        {
            builder.HasKey(a => a.ID);
        }

        protected override void ConfigureProperty(BasePropertyBuilder<EmailAttachment> builder)
        {
            builder.Property(a => a.ContentType)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.FileName)
                .HasMaxLength(255)
                .IsRequired();
        }

        protected override void ConfigureRelationship(BaseRelationshipBuilder<EmailAttachment> builder)
        {
            builder.HasOne<Email>()
                .WithMany(a => a.N_Attachments)
                .HasForeignKey(a => a.EmailID);
        }
    }
}
