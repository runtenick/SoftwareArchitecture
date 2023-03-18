using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Champions.Entities
{
    public class JoinRunePageRuneEntity
    {
        public int RuneId { get; set; }

        public int RunePageId { get; set; }

        public RuneCategoryEntity? Category { get; set; }
    }
}
