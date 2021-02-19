using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.Entities;
using Lynx.Infrastructure.Common.Constants;

namespace Lynx.Infrastructure.Persistence.Configurations
{
    public partial class BillProvider_Configuration
    {
        protected override void KeyBuilder(BaseKeyBuilder<BillProvider> builder)
        {
            builder.HasKey(a => a.ID);
        }

        protected override void ConfigureProperty(BasePropertyBuilder<BillProvider> builder)
        {
            builder.Property(p => p.ShortDesc)
               .HasMaxLength(StringLengthConstant.ShortDesc);

            builder.Property(p => p.LongDesc)
               .HasMaxLength(StringLengthConstant.LongDesc);
        }

        protected override void ConfigureRelationship(BaseRelationshipBuilder<BillProvider> builder)
        {
            builder.HasOne<Bill>()
                .WithMany()
                .HasForeignKey(a => a.BillID);

            builder.HasOne<ProviderType>()
                .WithMany()
                .HasForeignKey(a => a.ProviderTypeID);
        }

        protected override void SeedData(BaseSeeder<BillProvider> builder)
        { 
            builder.HasData(new BillProvider
            {
                ID = 1,
                BillID = 1,
                ProviderTypeID = ProviderTypeConstants.Email
            });

            builder.HasData(new BillProvider
            {
                ID = 2,
                BillID = 2,
                ProviderTypeID = ProviderTypeConstants.Email
            });

            builder.HasData(new BillProvider
            {
                ID = 3,
                BillID = 3,
                ProviderTypeID = ProviderTypeConstants.Scheduled
            });

            builder.HasData(new BillProvider
            {
                ID = 4,
                BillID = 4,
                ProviderTypeID = ProviderTypeConstants.Scheduled
            });
        }
    }
}
