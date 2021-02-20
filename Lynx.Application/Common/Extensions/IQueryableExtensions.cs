using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Lynx.Application
{
    public static class IQueryableExtensions
    {
        public static DbSet<T> AsDbSet<T>(this IQueryable<T> queryable) where T : class
        {
            return (DbSet<T>)queryable;
        }
    }
}
