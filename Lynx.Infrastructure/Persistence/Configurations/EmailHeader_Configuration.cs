using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.Entities;

namespace Lynx.Infrastructure.Persistence.Configurations
{
    public partial class EmailHeader_Configuration
    {
        protected override void KeyBuilder(BaseKeyBuilder<EmailHeader> builder)
        {
            builder.HasKey(a => a.ID);
        }

        protected override void ConfigureProperty(BasePropertyBuilder<EmailHeader> builder)
        {
            builder.Property(a => a.Name)
                .HasMaxLength(100)
                .IsRequired();
        }

        protected override void ConfigureRelationship(BaseRelationshipBuilder<EmailHeader> builder)
        {
            builder.HasOne<Email>()
                .WithMany(a => a.N_Headers)
                .HasForeignKey(a => a.EmailID);
        }
    }
}