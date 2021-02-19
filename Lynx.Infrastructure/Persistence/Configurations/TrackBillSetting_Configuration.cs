using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.Entities;

namespace Lynx.Infrastructure.Persistence.Configurations
{
    public partial class TrackBillSetting_Configuration
    {
        protected override void KeyBuilder(BaseKeyBuilder<TrackBillSetting> builder)
        {
            builder.HasKey(a => new
            {
                a.TrackBillID,
                a.UserID,
                a.Code
            });
        }

        protected override void ConfigureProperty(BasePropertyBuilder<TrackBillSetting> builder)
        {
            builder.Property(a => a.Code)
                .IsRequired()
                .HasMaxLength(StringLengthConstant.Code);

            builder.Property(a => a.Value)
                .IsRequired()
                .HasMaxLength(500);
        }

        protected override void ConfigureRelationship(BaseRelationshipBuilder<TrackBillSetting> builder)
        {
            builder.HasOne<TrackBill>()
                .WithMany(a => a.N_TrackBillSettings)
                .HasForeignKey(a => new
                {
                    a.TrackBillID,
                    a.UserID
                });
        }
    }
}
