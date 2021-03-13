using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lynx.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lynx.Application.Common.Extensions
{
    public static class ILynxDbContextExtensions
    {
        public static int SaveChanges(this ILynxDbContext dbContext)
        {
            var baseDbContext = (DbContext)dbContext;

            return baseDbContext.SaveChanges();
        }

        public static Task<int> SaveChangesAsync(this ILynxDbContext dbContext, CancellationToken cancellationToken = default)
        {
            var baseDbContext = (DbContext)dbContext;
            
            return baseDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
