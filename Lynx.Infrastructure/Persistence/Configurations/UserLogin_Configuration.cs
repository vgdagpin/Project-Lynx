using System;
using System.Collections.Generic;
using System.Text;
using Lynx.Domain.Entities;
using Lynx.Infrastructure.Common;

namespace Lynx.Infrastructure.Persistence.Configurations
{
    public partial class UserLogin_Configuration
    {
        protected override void ConfigureProperty(BasePropertyBuilder<UserLogin> builder)
        {
            builder.Property(a => a.Username)
                .IsRequired()
                .HasMaxLength(StringLengthConstant.Name);

            builder.Property(a => a.TemporaryPassword)
                .IsRequired()
                .HasMaxLength(StringLengthConstant.Password);
        }

        protected override void ConfigureIndex(BaseIndexBuilder<UserLogin> builder)
        {
            builder.HasIndex(a => a.Username).IsUnique();
        }

        protected override void ConfigureRelationship(BaseRelationshipBuilder<UserLogin> builder)
        {
            builder.HasOne(a => a.User)
                .WithOne(a => a.UserLogin)
                .HasForeignKey<UserLogin>(a => a.ID);
        }

        protected override void SeedData(BaseSeeder<UserLogin> builder)
        {
            var hasher = new PasswordHasher();

            builder.HasData(new UserLogin
            {
                ID = Guid.Empty.Increment(1),
                Salt = Encoding.ASCII.GetBytes(Guid.Empty.Increment(1).ToString()),
                Password = hasher.HashPassword(Encoding.ASCII.GetBytes(Guid.Empty.Increment(1).ToString()), "k4m0t3"),
                Username = "admin",
                IsTemporaryPassword = true,
                TemporaryPassword = "k4m0t3"
            });

            builder.HasData(new UserLogin
            {
                ID = Guid.Empty.Increment(2),
                Salt = Encoding.ASCII.GetBytes(Guid.Empty.Increment(2).ToString()),
                Password = hasher.HashPassword(Encoding.ASCII.GetBytes(Guid.Empty.Increment(2).ToString()), "k4m0t3"),
                Username = "vgdagpin",
                IsTemporaryPassword = true,
                TemporaryPassword = "k4m0t3"
            });
        }
    }
}