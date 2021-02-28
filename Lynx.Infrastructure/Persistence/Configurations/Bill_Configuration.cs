using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Commands.BillCmds;
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

            builder.Property(p => p.AssemblyName)
                .IsRequired()
                .HasMaxLength(StringLengthConstant.AssemblyName);

            builder.Property(p => p.TypeName)
                .IsRequired()
                .HasMaxLength(StringLengthConstant.TypeName);
        }

        protected override void ConfigureIndex(BaseIndexBuilder<Bill> builder)
        {
            builder.HasIndex(a => a.Code).IsUnique();
        }

        protected override void SeedData(BaseSeeder<Bill> builder)
        {
            builder.HasData(new Bill
            {
                ID = 1,
                Code = "Globe",
                ShortDesc = "Globe",
                LongDesc = "Globe",
                AssemblyName = typeof(GlobeBillCmd).Assembly.FullName,
                TypeName = typeof(GlobeBillCmd).FullName
            });

            builder.HasData(new Bill
            {
                ID = 2,
                Code = "Meralco",
                ShortDesc = "Meralco",
                LongDesc = "Meralco",
                AssemblyName = typeof(MeralcoBillCmd).Assembly.FullName,
                TypeName = typeof(MeralcoBillCmd).FullName
            });

            builder.HasData(new Bill
            {
                ID = 3,
                Code = "House Loan Amortization",
                ShortDesc = "House Loan Amortization",
                LongDesc = "House Loan Amortization",
                AssemblyName = typeof(HouseLoanAmortizationCmd).Assembly.FullName,
                TypeName = typeof(HouseLoanAmortizationCmd).FullName
            });

            builder.HasData(new Bill
            {
                ID = 4,
                Code = "Car Loan Amortization",
                ShortDesc = "Car Loan Amortization",
                LongDesc = "Car Loan Amortization",
                AssemblyName = typeof(CarLoanAmortizationCmd).Assembly.FullName,
                TypeName = typeof(CarLoanAmortizationCmd).FullName
            });
        }
    }
}
