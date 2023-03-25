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
        public static IEnumerable<TEntity> GetItemsWithFilterAndOrdering<TEntity>(this DbSet<TEntity> dbSet, Func<TEntity, bool> filter, int index, int count, string? orderingPropertyName = null, bool descending = false)
        where TEntity : class // to ensure that TEntity ia a class and not a value type (int, long etc.)
        {
            var filteredSet = dbSet.Where(filter);

            if (orderingPropertyName != null)
            {
                if (descending)
                {
                    filteredSet = filteredSet.OrderByDescending(e => e.GetType()
                        .GetProperty(orderingPropertyName)
                        .GetValue(e, null));
                }
                else
                {
                    filteredSet = filteredSet.OrderBy(e => e.GetType()
                        .GetProperty(orderingPropertyName)
                        .GetValue(e, null));
                }
            }

            return filteredSet.Skip(index * count).Take(count).ToList();
        }
    }
}
