using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.Entities;
using Lynx.Infrastructure.Common.Constants;
using Microsoft.EntityFrameworkCore;

namespace Lynx.Infrastructure.Persistence.Configurations
{
    public partial class TrackBillScheduler_Configuration
    {
        protected override void ConfigureProperty(BasePropertyBuilder<TrackBillScheduler> builder)
        {
            builder.Property(p => p.ShortDesc)
                .HasMaxLength(StringLengthConstant.ShortDesc);

            builder.Property(p => p.LongDesc)
               .HasMaxLength(StringLengthConstant.LongDesc);

            builder.Property(a => a.Frequency)
                .HasConversion<string>()
                .HasMaxLength(StringLengthConstant.Enums);
        }

        protected override void ConfigureRelationship(BaseRelationshipBuilder<TrackBillScheduler> builder)
        {
            builder.HasOne(a => a.N_TrackBill)
                .WithMany()
                .HasForeignKey(a => new
                {
                    a.TrackBillID,
                    a.UserID
                });

            builder.HasOne(a => a.N_User)
                .WithMany()
                .HasForeignKey(a => a.UserID)
                .OnDelete(DeleteBehavior.NoAction);
        }

        protected override void SeedData(BaseSeeder<TrackBillScheduler> builder)
        {
            // lancaster
            builder.HasData(new TrackBillScheduler
            {
                ID = Guid.Parse(TrackBillIDConstants.Lancaster),
                TrackBillID = Guid.Parse(TrackBillIDConstants.Lancaster),
                UserID = Guid.Parse(UserIDConstants.Enteng),
                StartDate = new DateTime(2021, 2, 19),
                EndDate = new DateTime(2021, 5, 19),
                Amount = 21000,
                DayFrequency = 23
            });

            // 588
            builder.HasData(new TrackBillScheduler
            {
                ID = Guid.Parse(TrackBillIDConstants.Condo588),
                TrackBillID = Guid.Parse(TrackBillIDConstants.Condo588),
                UserID = Guid.Parse(UserIDConstants.Enteng),
                StartDate = new DateTime(2021, 2, 19),
                EndDate = new DateTime(2021, 5, 19),
                Amount = 14000,
                DayFrequency = 28
            });

            // brv
            builder.HasData(new TrackBillScheduler
            {
                ID = Guid.Parse(TrackBillIDConstants.HondaBRV),
                TrackBillID = Guid.Parse(TrackBillIDConstants.HondaBRV),
                UserID = Guid.Parse(UserIDConstants.Enteng),
                StartDate = new DateTime(2021, 2, 19),
                EndDate = new DateTime(2021, 5, 19),
                Amount = 15000,
                DayFrequency = 13
            });
        }
    }
}
