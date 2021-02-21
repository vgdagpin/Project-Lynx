using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Lynx.Domain.Entities;

namespace Lynx
{
    public static class BaseEntityExtensions
    {
        public static TSummary GetSummary<TBaseEntity, TSummary>(this TBaseEntity baseEntity, IMapper mapper)
            where TBaseEntity : BaseEntity
            where TSummary : class
        {
            return mapper.Map<TSummary>(baseEntity);
        }

        public static IEnumerable<TSummary> GetSummary<TBaseEntity, TSummary>(this IEnumerable<TBaseEntity> baseEntities, IMapper mapper)
            where TBaseEntity : BaseEntity
            where TSummary : class
        {
            return baseEntities.Select(a => a.GetSummary<TBaseEntity, TSummary>(mapper));
        }
    }
}
