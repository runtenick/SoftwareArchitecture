using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Champions.Entities
{
    public class ChampionEntity
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public ChampClassEntity Class { get; set; }

        public string? Icon { get; set; }

        public string? Bio { get; set; }

        public string? Image { get; set; }

        public ICollection<SkinEntity>? Skins { get; set; } = new List<SkinEntity>();
    }
}
