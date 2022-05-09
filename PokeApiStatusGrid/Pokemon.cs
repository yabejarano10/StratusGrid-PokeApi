using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeApiStatusGrid
{
    internal class Pokemon
    {
        public string name { get; set; }
        public int height { get; set; }
        public int weight { get; set; }

        public List<TypeList> types { get; set; }
    }
}
