using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Champions.Entities
{
    public class SkillEntity
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public SkillType SkillType { get; set; }

        public ICollection<ChampionEntity>? Champions { get; set; } = new List<ChampionEntity>();
    }
}
