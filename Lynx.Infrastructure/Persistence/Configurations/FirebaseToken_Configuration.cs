using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.Entities;

namespace Lynx.Infrastructure.Persistence.Configurations
{
    public partial class FirebaseToken_Configuration
    {
        protected override void KeyBuilder(BaseKeyBuilder<FirebaseToken> builder)
        {
            builder.HasKey(a => a.ID);
        }

        protected override void ConfigureProperty(BasePropertyBuilder<FirebaseToken> builder)
        {
            builder.Property(a => a.Token)
                .IsRequired()
                .HasMaxLength(300);
        }

        protected override void ConfigureIndex(BaseIndexBuilder<FirebaseToken> builder)
        {
            builder.HasIndex(a => a.Token)
                .IsUnique();
        }

        protected override void ConfigureRelationship(BaseRelationshipBuilder<FirebaseToken> builder)
        {
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(a => a.UserID);
        }
    }
}
