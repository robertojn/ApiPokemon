using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonApi.Infrastructure.Repositories
{
    public class PokeApiResponse
    {
        public string Name { get; set; } = string.Empty;
        public int Height { get; set; }
        public int Weight { get; set; }
        public List<TypeSlot> Types { get; set; } = new();

        public class TypeSlot
        {
            public TypeInfo Type { get; set; } = new();

        }

        public class TypeInfo
        {
            public string Name { get; set; } = string.Empty;
        }   
    }

}
     