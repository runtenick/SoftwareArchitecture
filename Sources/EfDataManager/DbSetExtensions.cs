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
        internal static IEnumerable<T?> GetItemsWithFilterAndOrdering<T>(this IEnumerable<T> collection,
            Func<T, bool> filter, int index, int count, string? orderingPropertyName = null, bool descending = false)
        {
            IEnumerable<T> temp = collection;
            temp = temp.Where(item => filter(item));
            if (orderingPropertyName != null)
            {
                var prop = typeof(T).GetProperty(orderingPropertyName!);
                if (prop != null)
                {
                    temp = descending ? temp.OrderByDescending(item => prop.GetValue(item))
                                        : temp.OrderBy(item => prop.GetValue(item));
                }
            }
            return temp.Skip(index * count).Take(count);
        }
    }
}
