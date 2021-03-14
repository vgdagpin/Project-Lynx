using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.Entities;

namespace Lynx.Infrastructure.Persistence.Configurations
{
    public partial class EmailExtract_Configuration
    {
        protected override void KeyBuilder(BaseKeyBuilder<EmailExtract> builder)
        {
            builder.HasKey(a => new
            {
                a.EmailID,
                a.EmailWorkerID,
                a.Key
            });
        }

        protected override void ConfigureProperty(BasePropertyBuilder<EmailExtract> builder)
        {
            builder.Property(a => a.Key)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Value)
                .HasMaxLength(255);
        }

        protected override void ConfigureRelationship(BaseRelationshipBuilder<EmailExtract> builder)
        {
            builder.HasOne<Email>()
                .WithMany(a => a.N_Extracts)
                .HasForeignKey(a => a.EmailID);

            builder.HasOne<EmailWorker>()
                .WithMany()
                .HasForeignKey(a => a.EmailWorkerID);
        }
    }
}
