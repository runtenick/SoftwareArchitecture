﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Champions.Entities
{
    public class SkinEntity
    {
        public long Id { get; set; }

        public string? Name { get; set; }

        public float? Price { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        public string? Icon { get; set; }
    }
}
