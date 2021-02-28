using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Constants;
using Lynx.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lynx.Infrastructure.Persistence.Configurations
{
    public partial class ProviderTypeConfigScheduler_Configuration
    {
        protected override void ConfigureProperty(BasePropertyBuilder<ProviderTypeConfigScheduler> builder)
        {
            builder.Property(p => p.ShortDesc)
                .HasMaxLength(StringLengthConstant.ShortDesc);

            builder.Property(p => p.LongDesc)
               .HasMaxLength(StringLengthConstant.LongDesc);

            builder.Property(a => a.Frequency)
                .HasConversion<string>()
                .HasMaxLength(StringLengthConstant.Enums);
        }

        protected override void ConfigureRelationship(BaseRelationshipBuilder<ProviderTypeConfigScheduler> builder)
        {
            builder.HasOne<TrackBill>()
                .WithOne(a => a.N_ProviderTypeConfigScheduler)
                .HasForeignKey<ProviderTypeConfigScheduler>(a => new
                {
                    a.ID,
                    a.UserID
                });

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(a => a.UserID)
                .OnDelete(DeleteBehavior.NoAction);
        }

        protected override void SeedData(BaseSeeder<ProviderTypeConfigScheduler> builder)
        {
            // lancaster
            builder.HasData(new ProviderTypeConfigScheduler
            {
                ID = Guid.Parse(TrackBillIDConstants.Lancaster),
                UserID = Guid.Parse(UserIDConstants.Enteng),
                StartDate = new DateTime(2021, 2, 19),
                EndDate = new DateTime(2021, 5, 19),
                Amount = 21000,
                DayFrequency = 23
            });

            // 588
            builder.HasData(new ProviderTypeConfigScheduler
            {
                ID = Guid.Parse(TrackBillIDConstants.Condo588),
                UserID = Guid.Parse(UserIDConstants.Enteng),
                StartDate = new DateTime(2021, 2, 19),
                EndDate = new DateTime(2021, 5, 19),
                Amount = 14000,
                DayFrequency = 28
            });

            // brv
            builder.HasData(new ProviderTypeConfigScheduler
            {
                ID = Guid.Parse(TrackBillIDConstants.HondaBRV),
                UserID = Guid.Parse(UserIDConstants.Enteng),
                StartDate = new DateTime(2021, 2, 19),
                EndDate = new DateTime(2021, 5, 19),
                Amount = 15000,
                DayFrequency = 13
            });
        }
    }
}
