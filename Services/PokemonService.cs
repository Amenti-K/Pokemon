using Pokemon.Models;
using System.Collections.Generic;
using System.Linq;

namespace Pokemon.Api.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly List<Pokemons> _pokemons;

        public PokemonService()
        {
            // Initialize the list with some default data
            _pokemons = new List<Pokemons>
            {
                new Pokemons { Id = 2, Name = "Charmander", Type = "Fire", Ability = "Blaze", Level = 18 },
                new Pokemons { Id = 1, Name = "Pikachu", Type = "Electric", Ability = "Static", Level = 25 },
                new Pokemons { Id = 3, Name = "Squirtle", Type = "Water", Ability = "Torrent", Level = 16 }
            };
        }

        public List<Pokemons> GetAllPokemons() => _pokemons;

        public Pokemons GetPokemonById(int id) => _pokemons.FirstOrDefault(p => p.Id == id);

        public List<Pokemons> GetPokemonByType(string type) =>
            _pokemons.Where(p => p.Type.Equals(type, System.StringComparison.OrdinalIgnoreCase)).ToList();

        public Pokemons AddPokemon(Pokemons pokemon)
        {
            pokemon.Id = _pokemons.Count > 0 ? _pokemons.Max(p => p.Id) + 1 : 1;
            _pokemons.Add(pokemon);
            return pokemon;
        }

        public Pokemons UpdatePokemon(int id, Pokemons updatedPokemon)
        {
            var pokemon = _pokemons.FirstOrDefault(p => p.Id == id);
            if (pokemon == null) return null;

            pokemon.Name = updatedPokemon.Name;
            pokemon.Type = updatedPokemon.Type;
            pokemon.Ability = updatedPokemon.Ability;
            pokemon.Level = updatedPokemon.Level;

            return pokemon;
        }

        public bool DeletePokemon(int id)
        {
            var pokemon = _pokemons.FirstOrDefault(p => p.Id == id);
            if (pokemon == null) return false;

            _pokemons.Remove(pokemon);
            return true;
        }
    }
}
