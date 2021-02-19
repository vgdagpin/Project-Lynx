using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.Entities;
using Lynx.Infrastructure.Common.Constants;
using Microsoft.EntityFrameworkCore;

namespace Lynx.Infrastructure.Persistence.Configurations
{
    public partial class UserBill_Configuration
    {
        protected override void ConfigureProperty(BasePropertyBuilder<UserBill> builder)
        {
            builder.Property(a => a.Status)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(StringLengthConstant.Enums);
        }

        protected override void ConfigureRelationship(BaseRelationshipBuilder<UserBill> builder)
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

        protected override void SeedData(BaseSeeder<UserBill> builder)
        {
            builder.HasData(new UserBill
            {
                ID = Guid.Empty.Increment(1),
                TrackBillID = Guid.Parse(TrackBillIDConstants.Globe),
                UserID = Guid.Parse(UserIDConstants.Enteng),
                Status = BillPaymentStatus.Active,
                Amount = 2100,
                DueDate = new DateTime(2021, 2, 21)
            });

            builder.HasData(new UserBill
            {
                ID = Guid.Empty.Increment(2),
                TrackBillID = Guid.Parse(TrackBillIDConstants.Lancaster),
                UserID = Guid.Parse(UserIDConstants.Enteng),
                Status = BillPaymentStatus.Active,
                Amount = 2100,
                DueDate = new DateTime(2021, 2, 23)
            });

            builder.HasData(new UserBill
            {
                ID = Guid.Empty.Increment(3),
                TrackBillID = Guid.Parse(TrackBillIDConstants.Condo588),
                UserID = Guid.Parse(UserIDConstants.Enteng),
                Status = BillPaymentStatus.Active,
                Amount = 14000,
                DueDate = new DateTime(2021, 2, 28)
            });

            builder.HasData(new UserBill
            {
                ID = Guid.Empty.Increment(4),
                TrackBillID = Guid.Parse(TrackBillIDConstants.HondaBRV),
                UserID = Guid.Parse(UserIDConstants.Enteng),
                Status = BillPaymentStatus.Active,
                Amount = 15000,
                DueDate = new DateTime(2021, 2, 13)
            });
        }
    }
}
