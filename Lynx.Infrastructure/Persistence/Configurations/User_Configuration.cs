using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.Entities;

namespace Lynx.Infrastructure.Persistence.Configurations
{
    public partial class User_Configuration
    {
        protected override void ConfigureProperty(BasePropertyBuilder<User> builder)
        {
            builder.Property(a => a.FirstName)
                .IsRequired()
                .HasMaxLength(StringLengthConstant.Name);

            builder.Property(a => a.LastName)
                .IsRequired()
                .HasMaxLength(StringLengthConstant.Name);
        }

        protected override void SeedData(BaseSeeder<User> builder)
        {
            builder.HasData(new User
            {
                ID = Guid.Empty.Increment(1),
                FirstName = "Admin",
                LastName = "Admin"
            });

            builder.HasData(new User
            {
                ID = Guid.Empty.Increment(2),
                FirstName = "Vincent",
                LastName = "Dagpin"
            });
        }
    }
}