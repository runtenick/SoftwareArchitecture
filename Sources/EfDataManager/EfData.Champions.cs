using EF_Champions.Entities;
using EF_Champions.Mapper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfDataManager
{
    public partial class EfData
    {
        public class EfChampionsManager : IChampionsManager
        {
            private readonly EfData parent;

            public EfChampionsManager(EfData parent)
                => this.parent = parent;


            public async Task<Champion?> AddItem(Champion? item)
            {
                // on verifie si le champion passé en paramètre est null/valide
                if (item == null)
                {
                    throw new ArgumentNullException(nameof(item));
                }
                // on essaye de ajouter dans la base de données
                try
                {
                    await parent.ChampDbContext.Champions.AddAsync(item.ChampionToEntity());
                    parent.ChampDbContext.SaveChanges();

                }
                catch (Exception exception)
                {
                    throw new Exception("Error while trying tot add a Champion to the database", exception);
                }

                return item;
            }

            public async Task<bool> DeleteItem(Champion? item)
            {
                try
                {
                    if (item == null)
                    {
                        throw new ArgumentNullException(nameof(item));
                    }

                    ChampionEntity champ = await parent.ChampDbContext.Champions.FindAsync(item.ChampionToEntity());
                    if(champ == null)
                    {
                        return false;
                    }
                    else
                    {
                        parent.ChampDbContext.Champions.Remove(champ);
                        parent.ChampDbContext.SaveChanges();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting Champion from the database", ex);
                }
            }

            public Task<IEnumerable<Champion?>> GetItems(int index, int count, string? orderingPropertyName = null, bool descending = false)
            {
                throw new NotImplementedException();
            }

            public Task<IEnumerable<Champion?>> GetItemsByCharacteristic(string charName, int index, int count, string? orderingPropertyName = null, bool descending = false)
            {
                throw new NotImplementedException();
            }

            public Task<IEnumerable<Champion?>> GetItemsByClass(ChampionClass championClass, int index, int count, string? orderingPropertyName = null, bool descending = false)
            {
                throw new NotImplementedException();
            }

            public Task<IEnumerable<Champion?>> GetItemsByName(string substring, int index, int count, string? orderingPropertyName = null, bool descending = false)
            {
                throw new NotImplementedException();
            }

            public Task<IEnumerable<Champion?>> GetItemsByRunePage(RunePage? runePage, int index, int count, string? orderingPropertyName = null, bool descending = false)
            {
                throw new NotImplementedException();
            }

            public Task<IEnumerable<Champion?>> GetItemsBySkill(Skill? skill, int index, int count, string? orderingPropertyName = null, bool descending = false)
            {
                throw new NotImplementedException();
            }

            public Task<IEnumerable<Champion?>> GetItemsBySkill(string skill, int index, int count, string? orderingPropertyName = null, bool descending = false)
            {
                throw new NotImplementedException();
            }

            public Task<int> GetNbItems()
            {
                throw new NotImplementedException();
            }

            public Task<int> GetNbItemsByCharacteristic(string charName)
            {
                throw new NotImplementedException();
            }

            public Task<int> GetNbItemsByClass(ChampionClass championClass)
            {
                throw new NotImplementedException();
            }

            public Task<int> GetNbItemsByName(string substring)
            {
                throw new NotImplementedException();
            }

            public Task<int> GetNbItemsByRunePage(RunePage? runePage)
            {
                throw new NotImplementedException();
            }

            public Task<int> GetNbItemsBySkill(Skill? skill)
            {
                throw new NotImplementedException();
            }

            public Task<int> GetNbItemsBySkill(string skill)
            {
                throw new NotImplementedException();
            }

            public async Task<Champion?> UpdateItem(Champion? oldItem, Champion? newItem)
            {
                try
                {
                    if (oldItem == null || newItem == null) 
                    {
                        throw new ArgumentNullException("We need a valid old and new item to update");
                    }

                    ChampionEntity champ = await parent.ChampDbContext.Champions.FindAsync(oldItem);
                    if (champ == null)
                    {
                        return null;
                    }
                    champ = newItem.ChampionToEntity();
                    parent.ChampDbContext.SaveChanges();
                }
                catch (Exception exception) 
                {
                    throw new Exception("Error while trying to update a Champion in the database", exception);
                }
                return newItem;
            }
        }
    }
}
