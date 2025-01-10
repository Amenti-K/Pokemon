using Pokemon.Models;
using System.Collections.Generic;

namespace Pokemon.Api.Services
{
    public interface IPokemonService
    {
        List<Pokemons> GetAllPokemons();
        Pokemons GetPokemonById(int id);
        List<Pokemons> GetPokemonByType(string type);
        Pokemons AddPokemon(Pokemons pokemon);
        Pokemons UpdatePokemon(int id, Pokemons updatedPokemon);
        bool DeletePokemon(int id);
    }
}
