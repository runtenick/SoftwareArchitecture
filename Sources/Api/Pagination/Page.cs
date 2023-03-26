using DTO;

namespace Api.Pagination
{
    public class Page
    {
        public IEnumerable<ChampionDto>? MyChampions { get; set; }
        public int Index { get; set; }
        public int Count { get; set; }
        public int TotalCount { get; set; }
    }
}
