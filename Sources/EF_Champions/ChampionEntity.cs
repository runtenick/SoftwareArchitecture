using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Champions
{
    public class ChampionEntity
    {
        public long Id { get; set; }
        public string? Name { get; set; }

        public ChampClassEntity Class { get; set; }
    }
}
