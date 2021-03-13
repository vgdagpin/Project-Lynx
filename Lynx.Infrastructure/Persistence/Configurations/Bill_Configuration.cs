using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Constants;
using Lynx.Domain.Entities;

namespace Lynx.Infrastructure.Persistence.Configurations
{
    public partial class Bill_Configuration
    {
        protected override void KeyBuilder(BaseKeyBuilder<Bill> builder)
        {
            builder.HasKey(a => a.ID);
        }

        protected override void ConfigureProperty(BasePropertyBuilder<Bill> builder)
        {
            builder.Property(p => p.Code)
                .IsRequired()
                .HasMaxLength(StringLengthConstant.Code);

            builder.Property(p => p.ShortDesc)
                .IsRequired()
                .HasMaxLength(StringLengthConstant.ShortDesc);

            builder.Property(p => p.LongDesc)
               .IsRequired()
               .HasMaxLength(StringLengthConstant.LongDesc);
        }

        protected override void ConfigureIndex(BaseIndexBuilder<Bill> builder)
        {
            builder.HasIndex(a => a.Code).IsUnique();
        }

        protected override void SeedData(BaseSeeder<Bill> builder)
        {
            builder.HasData(new Bill
            {
                ID = BillIDConstants.Globe,
                Code = "Globe",
                ShortDesc = "Globe",
                LongDesc = "Globe"
            });

            builder.HasData(new Bill
            {
                ID = BillIDConstants.Meralco,
                Code = "Meralco",
                ShortDesc = "Meralco",
                LongDesc = "Meralco"
            });

            builder.HasData(new Bill
            {
                ID = BillIDConstants.HomeLoanAmort,
                Code = "Home Loan Amortization",
                ShortDesc = "Home Loan Amortization",
                LongDesc = "Home Loan Amortization"
            });

            builder.HasData(new Bill
            {
                ID = BillIDConstants.CarLoanAmort,
                Code = "Car Loan Amortization",
                ShortDesc = "Car Loan Amortization",
                LongDesc = "Car Loan Amortization"
            });

            builder.HasData(new Bill
            {
                ID = BillIDConstants.CreditCard,
                Code = "Credit Card",
                ShortDesc = "Credit Card",
                LongDesc = "Credit Card"
            });
        }
    }
}
