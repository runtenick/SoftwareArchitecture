using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EF_Champions.Entities
{
    public class RuneCategoryEntity
    {
        public int Id { get; set; }

        public RuneEntity? Rune { get; set; }

        public Model.RunePage.Category Category { get; set; }
    }
}
