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
                Icon = champion.Icon,
                Bio = champion.Bio,
                Image = champion.Image.Base64
            };
        }
        public static Champion ToModel(this ChampionDto championDto)
        {
            return new Champion(championDto.Name)
            {
               Icon = championDto.Icon,
               Bio = championDto.Bio,
               Image = new LargeImage(championDto.Image)
            };
        }
    }
 }
