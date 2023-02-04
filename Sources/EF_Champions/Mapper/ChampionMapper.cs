using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EF_Champions.Mapper
{
    public static class ChampionMapper
    {
        public static ChampionEntity ChampionToEntity(this Champion champion) 
        {
            return new ChampionEntity { Name = champion.Name, Class = (ChampClassEntity)champion.Class };
        }
    }
}
