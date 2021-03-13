using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Constants;
using Lynx.Domain.Entities;

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
                .WithMany(a => a.N_BillProviders)
                .HasForeignKey(a => a.BillID);

            builder.HasOne(a => a.N_ProviderType)
                .WithMany()
                .HasForeignKey(a => a.ProviderTypeID);
        }

        protected override void SeedData(BaseSeeder<BillProvider> builder)
        {
            builder.HasData(new BillProvider
            {
                ID = 1,
                BillID = BillIDConstants.Globe,
                ProviderTypeID = ProviderTypeConstants.Email
            });

            builder.HasData(new BillProvider
            {
                ID = 2,
                BillID = BillIDConstants.Meralco,
                ProviderTypeID = ProviderTypeConstants.Email
            });

            builder.HasData(new BillProvider
            {
                ID = 3,
                BillID = BillIDConstants.HomeLoanAmort,
                ProviderTypeID = ProviderTypeConstants.Scheduled
            });

            builder.HasData(new BillProvider
            {
                ID = 4,
                BillID = BillIDConstants.CarLoanAmort,
                ProviderTypeID = ProviderTypeConstants.Scheduled
            });

            builder.HasData(new BillProvider
            {
                ID = 5,
                ShortDesc = "BDO",
                LongDesc = "BDO",
                BillID = BillIDConstants.CreditCard,
                ProviderTypeID = ProviderTypeConstants.Email
            });

            builder.HasData(new BillProvider
            {
                ID = 6,
                ShortDesc = "Metrobank",
                LongDesc = "Metrobank",
                BillID = BillIDConstants.CreditCard,
                ProviderTypeID = ProviderTypeConstants.Email
            });
        }
    }
}
