using DTO;
using Model;

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
    }
}
/*
    creer controleur
    recuperer un DTO et le convertir en champion
 */
