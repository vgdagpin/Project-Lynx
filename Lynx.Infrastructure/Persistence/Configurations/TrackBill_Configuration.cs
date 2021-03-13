using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Constants;
using Lynx.Domain.Entities;

namespace Lynx.Infrastructure.Persistence.Configurations
{
    public partial class TrackBill_Configuration
    {
        protected override void KeyBuilder(BaseKeyBuilder<TrackBill> builder)
        {
            builder.HasKey(a => new
            {
                a.ID,
                a.UserID
            });
        }

        protected override void ConfigureProperty(BasePropertyBuilder<TrackBill> builder)
        {
            builder.Property(a => a.ID).ValueGeneratedOnAdd();

            builder.Property(p => p.ShortDesc)
                .HasMaxLength(StringLengthConstant.ShortDesc);

            builder.Property(p => p.LongDesc)
               .HasMaxLength(StringLengthConstant.LongDesc);

            builder.Property(p => p.AccountNumber)
              .IsRequired()
              .HasMaxLength(100);
        }

        protected override void ConfigureRelationship(BaseRelationshipBuilder<TrackBill> builder)
        {
            builder.HasOne(a => a.N_Bill)
                .WithMany()
                .HasForeignKey(a => a.BillID);

            builder.HasOne(a => a.N_ProviderType)
                .WithMany()
                .HasForeignKey(a => a.ProviderTypeID);

            builder.HasOne(a => a.N_User)
                .WithMany()
                .HasForeignKey(a => a.UserID);
        }

        //protected override void SeedData(BaseSeeder<TrackBill> builder)
        //{
        //    builder.HasData(new TrackBill
        //    {
        //        ID = Guid.Parse(TrackBillIDConstants.Globe),
        //        UserID = Guid.Parse(UserIDConstants.Enteng),
        //        BillID = 1,
        //        ProviderTypeID = ProviderTypeConstants.Email,
        //        AccountNumber = "GLOBE-ACCT-NUM"
        //    });

        //    builder.HasData(new TrackBill
        //    {
        //        ID = Guid.Parse(TrackBillIDConstants.Meralco),
        //        UserID = Guid.Parse(UserIDConstants.Enteng),
        //        BillID = 2,
        //        ProviderTypeID = ProviderTypeConstants.Email,
        //        AccountNumber = "MERALCO-ACCT-NUM"
        //    });

        //    builder.HasData(new TrackBill
        //    {
        //        ID = Guid.Parse(TrackBillIDConstants.Lancaster),
        //        UserID = Guid.Parse(UserIDConstants.Enteng),
        //        BillID = 3,
        //        ProviderTypeID = ProviderTypeConstants.Scheduled,
        //        AccountNumber = "LANCASTER-ACCT-NUM"
        //    });

        //    builder.HasData(new TrackBill
        //    {
        //        ID = Guid.Parse(TrackBillIDConstants.Condo588),
        //        UserID = Guid.Parse(UserIDConstants.Enteng),
        //        BillID = 3,
        //        ProviderTypeID = ProviderTypeConstants.Scheduled,
        //        AccountNumber = "588-ACCT-NUM"
        //    });

        //    builder.HasData(new TrackBill
        //    {
        //        ID = Guid.Parse(TrackBillIDConstants.HondaBRV),
        //        UserID = Guid.Parse(UserIDConstants.Enteng),
        //        BillID = 4,
        //        ProviderTypeID = ProviderTypeConstants.Scheduled,
        //        AccountNumber = "BRV-ACCT-NUM"
        //    });
        //}
    }
}
