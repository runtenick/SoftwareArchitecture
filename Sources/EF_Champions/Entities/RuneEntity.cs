using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Champions.Entities
{
    public class RuneEntity
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public RuneFamily RuneFamily { get; set; }

        public string? Icon { get; set; }  
        
        public string? Image { get; set; }

        public ICollection<RunePageEntity>? Pages { get; set; } = new List<RunePageEntity>();
    }
}
    