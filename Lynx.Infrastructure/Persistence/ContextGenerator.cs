using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

using Lynx.Interfaces;
using Lynx.Domain.Entities;

/*
Do not modify this file! This is auto generated!
Any changes to this file will be gone when template gets generated again.
*/

namespace Lynx.Infrastructure.Persistence
{
	public partial class LynxDbContext : DbContext, ILynxDbContext
	{
		#region Entities
		private DbSet<User> db_Users { get; set; }
		public IQueryable<User> Users 
		{ 
			get { return db_Users; }
			set { db_Users = (DbSet<User>)value; }
		}
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
