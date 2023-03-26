using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Champions.Entities
{
    public class ChampionEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ChampionClass Class { get; set; }

        public string? Icon { get; set; }

        public string? Bio { get; set; }

        public string? Image { get; set; }

        public ICollection<SkinEntity>? Skins { get; set; } = new List<SkinEntity>();

        public ICollection<SkillEntity>? Skills { get; set; } = new List<SkillEntity>();

        public ICollection<RunePageEntity>? PagesRune { get; set; } = new List<RunePageEntity>();
    }
}
