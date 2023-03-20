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

        public static Champion EntityToModel(this ChampionEntity championEntity)
        {
            Champion modelChampion = new Champion(championEntity.Name, championEntity.Class, championEntity.Icon, championEntity.Image, championEntity.Bio);
            foreach(var skill in championEntity.Skills)
            {
                modelChampion.AddSkill(skill.EntityToModel());
            }
            return modelChampion;
           
        }
    }
}
