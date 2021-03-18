using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Constants;
using Lynx.Domain.Entities;

namespace Lynx.Infrastructure.Persistence.Configurations
{
    public partial class BillPaymentStepsTemplate_Configuration
    {
        protected override void KeyBuilder(BaseKeyBuilder<BillPaymentStepsTemplate> builder)
        {
            builder.HasKey(a => a.ID);
        }

        protected override void ConfigureProperty(BasePropertyBuilder<BillPaymentStepsTemplate> builder)
        {
            builder.Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(a => a.ShortDesc)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(a => a.LongDesc)
                .IsRequired();

            builder.Property(a => a.Keywords)
                .HasMaxLength(255);
        }

        protected override void ConfigureRelationship(BaseRelationshipBuilder<BillPaymentStepsTemplate> builder)
        {
            builder.HasOne<Bill>()
                .WithMany(a => a.N_PaymentStepsTemplates)
                .HasForeignKey(a => a.BillID);
        }

        protected override void SeedData(BaseSeeder<BillPaymentStepsTemplate> builder)
        {
            builder.HasData(new BillPaymentStepsTemplate
            {
                ID = 1,
                BillID = BillIDConstants.HomeLoanAmort,
                Title = "7/11",
                ShortDesc = "Go to nearest 7/11, ask cashier for instructions lol",
                LongDesc = "Just kidding, see below very long instructions",
                Keywords = "7/11,711"
            });

            builder.HasData(new BillPaymentStepsTemplate
            {
                ID = 2,
                BillID = BillIDConstants.HomeLoanAmort,
                Title = "GCash",
                ShortDesc = "Payment using GCash, follow this instruction",
                LongDesc = "See below very long instructions",
                Keywords = "e-wallet,ewallet,wallet,gcash"
            });

            builder.HasData(new BillPaymentStepsTemplate
            {
                ID = 3,
                BillID = BillIDConstants.HomeLoanAmort,
                Title = "Paymaya",
                ShortDesc = "Payment using Paymaya, follow this instruction",
                LongDesc = "See below very long instructions",
                Keywords = "e-wallet,ewallet,wallet,paymaya"
            });
        }
    }
}
