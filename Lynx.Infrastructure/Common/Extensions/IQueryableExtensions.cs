using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Lynx.Infrastructure.Common.Extensions
{
    public static class IQueryableExtensions
    {
        public static DbSet<T> AsDbSet<T>(this IQueryable<T> set) where T : class
        {
            return (DbSet<T>)set;
        }
    }
}
