using EF_Champions.Entities;
using Model;

namespace EF_Champions.Mapper
{
    public static class ChampionMapper
    {
        public static ChampionEntity ChampionToEntity(this Champion champion) 
        {
            return new ChampionEntity 
            { 
                Name = champion.Name, 
                Class = champion.Class,
                Bio = champion.Bio,
                Icon = champion.Icon,
                Image = champion.Image.Base64
            };
        }
    }
}
