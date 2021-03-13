using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Lynx.Application.Common.Extensions
{
    public static class IQueryableExtensions
    {
        public static DbSet<T> AsDbSet<T>(this IQueryable<T> queryable) where T : class
        {
            return (DbSet<T>)queryable;
        }

        public static EntityEntry<T> Add<T>(this IQueryable<T> collection, T entry) where T : class
        {
            DbSet<T> dbSet = (DbSet<T>)collection;

            if (dbSet == null)
            {
                throw new LynxException("Collection is not dbset");
            }

            return dbSet.Add(entry);
        }
    }
}
