using EF_Champions.Entities;
using Model;


namespace EF_Champions.Mapper
{
    public static class RunePageMapper
    {
        public static RunePageEntity RunePageToEntity(this RunePage runePage)
        {
            RunePageEntity pageEntity = new RunePageEntity { Name = runePage.Name };

            foreach (var category in runePage.Runes.Keys)
            {
                RuneEntity runeEntity = runePage.Runes[category].RuneToEntity();
                runeEntity.Pages?.Add(pageEntity);
                pageEntity.Runes?.Add(runeEntity);
            }
            return pageEntity;
        }
    }
}
