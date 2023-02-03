using DTO;
using Model;
using System.Runtime.CompilerServices;

namespace Api.Mapper
{
    public static class ChampionMapper
    {
        public static ChampionDto ToDto(this Champion champion)
        {
            return new ChampionDto()
            {
                Name = champion.Name,
            };
        }
        public static Champion ToModel(this ChampionDto championDto)
        {
            /* recuperer les autres attributs a apartir du stub */
            return new Champion(championDto.Name);
        }
    }
 }
