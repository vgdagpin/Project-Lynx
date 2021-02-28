using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Constants;
using Lynx.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lynx.Infrastructure.Persistence.Configurations
{
    public partial class UserSession_Configuration
    {
        protected override void KeyBuilder(BaseKeyBuilder<UserSession> builder)
        {
            builder.HasKey(a => a.SessionID);
        }

        protected override void ConfigureProperty(BasePropertyBuilder<UserSession> builder)
        {
            builder.Property(a => a.Token)
                .IsRequired()
                .HasMaxLength(StringLengthConstant.Token);

            builder.Property(a => a.IsExpired)
              .HasComputedColumnSql("CONVERT(BIT, (IIF(ExpiredOn IS NOT NULL AND GETUTCDATE() >= ExpiredOn, 1, 0)))");

            builder.Property(a => a.Remarks)
                .HasMaxLength(StringLengthConstant.Remarks);

            builder.Property(a => a.Status)
                .HasConversion<string>()
                .HasMaxLength(StringLengthConstant.Enums);
        }

        protected override void ConfigureRelationship(BaseRelationshipBuilder<UserSession> builder)
        {
            builder.HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserID);
        }
    }
}
