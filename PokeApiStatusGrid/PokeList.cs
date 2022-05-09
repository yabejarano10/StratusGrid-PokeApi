using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeApiStatusGrid
{
    internal class PokeList
    {
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public List<UrlsList> results { get; set; }
    }
}
