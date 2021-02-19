using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.Entities;
using Lynx.Infrastructure.Common.Constants;

namespace Lynx.Infrastructure.Persistence.Configurations
{
    public partial class ProviderType_Configuration
    {
        protected override void KeyBuilder(BaseKeyBuilder<ProviderType> builder)
        {
            builder.HasKey(a => a.ID);
        }

        protected override void ConfigureProperty(BasePropertyBuilder<ProviderType> builder)
        {
            builder.Property(a => a.ID).ValueGeneratedNever();

            builder.Property(p => p.ShortDesc)
                .IsRequired()
                .HasMaxLength(StringLengthConstant.ShortDesc);

            builder.Property(p => p.LongDesc)
               .IsRequired()
               .HasMaxLength(StringLengthConstant.LongDesc);
        }

        protected override void SeedData(BaseSeeder<ProviderType> builder)
        {
            builder.HasData(new ProviderType
            {
                ID = ProviderTypeConstants.Scheduled,
                ShortDesc = "Scheduled",
                LongDesc = "Scheduled"
            });

            builder.HasData(new ProviderType
            {
                ID = ProviderTypeConstants.API,
                ShortDesc = "API",
                LongDesc = "API"
            });

            builder.HasData(new ProviderType
            {
                ID = ProviderTypeConstants.Email,
                ShortDesc = "Email",
                LongDesc = "Email"
            });
        }
    }
}