using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Champions.Entities
{
    public class LargeImageEntity
    {
        public string? Base64 { get; set; }

        public LargeImageEntity(string? base64)
        {
            Base64 = base64;
        }
    }
}
