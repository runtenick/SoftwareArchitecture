using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    static class IdManager
    {
        static int id = 0; 

        public static int GiveId()
        {
            return id++;   
        }
    }
}
