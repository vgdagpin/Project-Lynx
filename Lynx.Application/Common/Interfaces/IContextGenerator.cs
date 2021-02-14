using Lynx.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

/*
Do not modify this file! This is auto generated!
Any changes to this file will be gone when template gets generated again.
*/

namespace Lynx.Application.Common.Interfaces
{
	public interface ILynxDbContext
	{
		#region Entities
		DbSet<User> Users { get; set; }
        #endregion

		EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
	}
}

