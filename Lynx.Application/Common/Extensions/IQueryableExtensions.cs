using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Lynx.Application.Common.Extensions
{
    public static class IQueryableExtensions
    {
        private static DbSet<T> AsDbSet<T>(this IQueryable<T> collection) where T : class
        {
            DbSet<T> dbSet = (DbSet<T>)collection;

            if (dbSet == null)
            {
                throw new LynxException("Collection is not dbset");
            }

            return dbSet;
        }

        public static EntityEntry<T> Add<T>(this IQueryable<T> collection, T entry) where T : class => collection.AsDbSet().Add(entry);

        public static EntityEntry<T> Remove<T>(this IQueryable<T> collection, T entry) where T : class => collection.AsDbSet().Remove(entry);

        public static ValueTask<T> FindAsync<T>(this IQueryable<T> collection, params object[] keyValues) where T : class => collection.AsDbSet().FindAsync(keyValues);
    }
}
