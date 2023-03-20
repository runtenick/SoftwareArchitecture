using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EfDataManager
{
    public static class DbSetExtensions
    {
         // Expression<Func<TEntity, bool>>: this syntax is used to represent a lambda expression that takes a TEntity and returns a bool
        public static IEnumerable<TEntity> GetItemsWithFilterAndOrdering<TEntity>(this DbSet<TEntity> dbSet, Func<TEntity, bool> filter, int index, 
            int count, string? orderingPropertyName = null, bool descending = false)
        where TEntity : class
        {
            var query = dbSet.Where(filter);

            if (orderingPropertyName != null)
            {
                if (descending)
                {
                    query = query.OrderByDescending(e => e.GetType()
                        .GetProperty(orderingPropertyName)
                        .GetValue(e, null));
                }
                else
                {
                    query = query.OrderBy(e => e.GetType()
                        .GetProperty(orderingPropertyName)
                        .GetValue(e, null));
                }
            }

            return query.Skip(index * count).Take(count).ToList();
        }

    }
}
