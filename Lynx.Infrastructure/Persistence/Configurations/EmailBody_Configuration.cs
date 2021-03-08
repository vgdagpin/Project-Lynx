using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.Entities;

namespace Lynx.Infrastructure.Persistence.Configurations
{
    public partial class EmailBody_Configuration
    {
        protected override void KeyBuilder(BaseKeyBuilder<EmailBody> builder)
        {
            builder.HasKey(a => a.ID);
        }

        protected override void ConfigureRelationship(BaseRelationshipBuilder<EmailBody> builder)
        {
            builder.HasOne(a => a.N_Email)
                .WithOne(a => a.N_Body)
                .HasForeignKey<EmailBody>(a => a.ID);
        }
    }
}