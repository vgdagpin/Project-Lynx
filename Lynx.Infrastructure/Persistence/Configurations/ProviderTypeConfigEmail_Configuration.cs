using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Constants;
using Lynx.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lynx.Infrastructure.Persistence.Configurations
{
    public partial class ProviderTypeConfigEmail_Configuration
    {
        protected override void ConfigureProperty(BasePropertyBuilder<ProviderTypeConfigEmail> builder)
        {
            builder.Property(a => a.ClientEmailAddress)
                .IsRequired()
                .HasMaxLength(StringLengthConstant.Email);

            builder.Property(a => a.ReceiverEmailAddress)
                .IsRequired()
                .HasMaxLength(StringLengthConstant.Email);
        }

        protected override void ConfigureRelationship(BaseRelationshipBuilder<ProviderTypeConfigEmail> builder)
        {
            builder.HasOne<TrackBill>()
                 .WithOne(a => a.N_ProviderTypeConfigEmail)
                 .HasForeignKey<ProviderTypeConfigEmail>(a => new
                 {
                     a.ID,
                     a.UserID
                 });

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(a => a.UserID)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
