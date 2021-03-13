using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Application.Handlers.Commands.EmailWorkerCmds;
using Lynx.Constants;
using Lynx.Domain.Entities;

namespace Lynx.Infrastructure.Persistence.Configurations
{
    public partial class EmailWorker_Configuration
    {
        protected override void KeyBuilder(BaseKeyBuilder<EmailWorker> builder)
        {
            builder.HasKey(a => a.ID);
        }

        protected override void ConfigureProperty(BasePropertyBuilder<EmailWorker> builder)
        {
            builder.Property(p => p.AssemblyName)
                .IsRequired()
                .HasMaxLength(StringLengthConstant.AssemblyName);

            builder.Property(p => p.TypeName)
                .IsRequired()
                .HasMaxLength(StringLengthConstant.TypeName);
        }

        protected override void ConfigureRelationship(BaseRelationshipBuilder<EmailWorker> builder)
        {
            builder.HasOne<BillProvider>()
                .WithOne(a => a.N_EmailWorker)
                .HasForeignKey<EmailWorker>(a => a.ID);
        }

        protected override void SeedData(BaseSeeder<EmailWorker> builder)
        {
            builder.HasData(new EmailWorker
            {
                ID = 1,
                AssemblyName = typeof(ReadUserBillFromGlobeEmailCmdHandler).Assembly.FullName,
                TypeName = typeof(ReadUserBillFromGlobeEmailCmdHandler).FullName
            });

            builder.HasData(new EmailWorker
            {
                ID = 5,
                AssemblyName = typeof(ReadUserBillFromBDOCmdHandler).Assembly.FullName,
                TypeName = typeof(ReadUserBillFromBDOCmdHandler).FullName
            });

            builder.HasData(new EmailWorker
            {
                ID = 6,
                AssemblyName = typeof(ReadUserBillFromMetrobankCmdHandler).Assembly.FullName,
                TypeName = typeof(ReadUserBillFromMetrobankCmdHandler).FullName
            });
        }
    }
}
