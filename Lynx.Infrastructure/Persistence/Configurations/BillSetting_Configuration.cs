using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Constants;
using Lynx.Domain.Entities;

namespace Lynx.Infrastructure.Persistence.Configurations
{
    public partial class BillSetting_Configuration
    {
        protected override void KeyBuilder(BaseKeyBuilder<BillSetting> builder)
        {
            builder.HasKey(a => new
            {
                a.BillID,
                a.Code
            });
        }

        protected override void ConfigureProperty(BasePropertyBuilder<BillSetting> builder)
        {
            builder.Property(a => a.Code)
                .IsRequired()
                .HasMaxLength(StringLengthConstant.Code);

            builder.Property(a => a.Value)
                .IsRequired()
                .HasMaxLength(500);
        }

        protected override void ConfigureRelationship(BaseRelationshipBuilder<BillSetting> builder)
        {
            builder.HasOne<Bill>()
                .WithMany(a => a.N_BillSettings)
                .HasForeignKey(a => a.BillID);
        }
    }
}
