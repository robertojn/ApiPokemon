namespace PokemonApi.Domain.DTOs
{
    public class PokemonDto
    {
        public string Name { get; set; }
        public List<string> Types { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
    }
}
