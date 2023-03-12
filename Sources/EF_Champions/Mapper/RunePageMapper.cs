using EF_Champions.Entities;
using Model;


namespace EF_Champions.Mapper
{
    public static class RunePageMapper
    {
        public static RunePageEntity RunePageToEntity(this RunePage runePage)
        {
            RunePageEntity entity = new RunePageEntity { Name = runePage.Name };

            foreach (var category in runePage.Runes.Keys)
            {
                RuneEntity runeEntity = runePage.Runes[category].RuneToEntity();
                RunePage.Category runeCategory = category;
                runeEntity.Pages?.Add(entity);
                entity.Runes?.Add(runeEntity);
            }
            return entity;
        }
    }
}
