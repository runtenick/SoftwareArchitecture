using EF_Champions.Mapper;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDataManager
{
    public partial class EfData
    {
        public class EfSkinsManager : ISkinsManager
        {
            private readonly EfData parent;

            public EfSkinsManager(EfData parent)
                => this.parent = parent;

            public Task<Skin?> AddItem(Skin? item)
            {
                throw new NotImplementedException();
            }

            public Task<bool> DeleteItem(Skin? item)
            {
                throw new NotImplementedException();
            }

            public Task<IEnumerable<Skin?>> GetItems(int index, int count, string? orderingPropertyName = null, bool descending = false)
            {
                throw new NotImplementedException();
            }

            private static Func<Skin, Champion?, bool> filterByChampion = (skin, champion) => champion != null && skin.Champion.Equals(champion!);

            public async Task<IEnumerable<Skin?>> GetItemsByChampion(Champion? champion, int index, int count, string? orderingPropertyName = null, bool descending = false)
            {
                return parent.ChampDbContext.Skins.Include("Champion").GetItemsWithFilterAndOrdering(
                    s => s.Champion.Name.Equals(champion?.Name),
                    index, count,
                    orderingPropertyName, descending).Select(skin => skin?.EntityToModel());
            }

            public Task<IEnumerable<Skin?>> GetItemsByName(string substring, int index, int count, string? orderingPropertyName = null, bool descending = false)
            {
                throw new NotImplementedException();
            }

            public async Task<int> GetNbItems()
                => await parent.ChampDbContext.Skins.CountAsync();

            public Task<int> GetNbItemsByChampion(Champion? champion)
            {
                throw new NotImplementedException();
            }

            public Task<int> GetNbItemsByName(string substring)
            {
                throw new NotImplementedException();
            }

            public Task<Skin?> UpdateItem(Skin? oldItem, Skin? newItem)
            {
                throw new NotImplementedException();
            }
        }
    }
}
