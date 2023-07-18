using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokeAPi.Models
{
    public class PokemonApiResponse
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public List<Results> Results { get; set; }
    }
}