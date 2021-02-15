using System;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Lynx.Domain.Entities;

/*
Do not modify this file! This is auto generated!
Any changes to this file will be gone when template gets generated again.
*/

namespace Lynx.Interfaces
{
	public interface ILynxDbContext
	{
		#region Entities
		IQueryable<User> Users { get; set; }
        #endregion
	}
}

