using Lynx.Domain.Entities;
using Lynx.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq;

/*
Do not modify this file! This is auto generated!
Any changes to this file will be gone when template gets generated again.
*/

namespace Lynx.Infrastructure.Persistence
{
	public partial class LynxDbContext : DbContext, ILynxDbContext
	{
		#region Entities
		public DbSet<User> Users { get; set; }
        #endregion

		public LynxDbContext(DbContextOptions<LynxDbContext> dbContextOpt) : base(dbContextOpt)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
	}
}

namespace Lynx.Infrastructure.Persistence.Configurations
{
	public partial class User_Configuration : BaseConfiguration<User> { }
}
