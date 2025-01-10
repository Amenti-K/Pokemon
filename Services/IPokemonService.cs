using Pokemon.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokemon.Api.Services
{
    public interface IPokemonService
    {
        Task<List<Pokemons>> GetAllPokemons();
        Task<Pokemons> GetPokemonById(int id);
        Task<List<Pokemons>> GetPokemonByType(string type);
        Task<Pokemons> AddPokemon(Pokemons pokemon);
        Task<Pokemons> UpdatePokemon(int id, Pokemons updatedPokemon);
        Task<bool> DeletePokemon(int id);
    }
}
