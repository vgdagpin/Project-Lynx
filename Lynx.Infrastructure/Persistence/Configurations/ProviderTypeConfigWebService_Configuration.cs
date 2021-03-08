using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lynx.Infrastructure.Persistence.Configurations
{
    public partial class ProviderTypeConfigWebService_Configuration
    {
        protected override void ConfigureRelationship(BaseRelationshipBuilder<ProviderTypeConfigWebService> builder)
        {
            builder.HasOne<TrackBill>()
                .WithOne(a => a.N_ProviderTypeConfigWebService)
                .HasForeignKey<ProviderTypeConfigWebService>(a => new
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
