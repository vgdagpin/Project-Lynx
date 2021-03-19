using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Constants;
using Lynx.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lynx.Infrastructure.Persistence.Configurations
{
    public partial class TextTemplate_Configuration
    {
        protected override void KeyBuilder(BaseKeyBuilder<TextTemplate> builder)
        {
            builder.HasKey(a => a.ID);
        }

        protected override void ConfigureProperty(BasePropertyBuilder<TextTemplate> builder)
        {
            builder.Property(a => a.Code)
                .IsRequired()
                .HasMaxLength(StringLengthConstant.Code);

            builder.Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.Content)
                .IsRequired();

            builder.Property(a => a.Version)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(a => a.RecordStatus)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(StringLengthConstant.Enums);
        }

        protected override void ConfigureIndex(BaseIndexBuilder<TextTemplate> builder)
        {
            builder.HasIndex(a => new { a.Code, a.RecordStatus })
                .IsUnique()
                .HasFilter($"[{nameof(RecordStatus)}] = '{RecordStatus.Active}'");
        }

        protected override void SeedData(BaseSeeder<TextTemplate> builder)
        {
            builder.HasData(new TextTemplate
            {
                ID = 1,
                Code = TextTemplateCodeConstants.PayWithLynxTermsOfService,
                Title = "Pay with Lynx Terms of Service",
                Content = "Test",
                RecordStatus = RecordStatus.Active,
                UpdatedOn = new DateTime(2021, 3, 1),
                Version = "1.0"
            });
        }
    }
}