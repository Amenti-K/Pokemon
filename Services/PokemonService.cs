using MongoDB.Driver;
using Pokemon.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokemon.Api.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IMongoCollection<Pokemons> _pokemonCollection;

        public PokemonService(IMongoClient client, IConfiguration configuration)
        {
            var database = client.GetDatabase(configuration["MongoDB:DatabaseName"]);
            _pokemonCollection = database.GetCollection<Pokemons>("Pokemons");
        }

        public async Task<List<Pokemons>> GetAllPokemons() =>
            await _pokemonCollection.Find(_ => true).ToListAsync();

        public async Task<Pokemons?> GetPokemonById(int id) =>
            await _pokemonCollection.Find(p => p.Id == id).FirstOrDefaultAsync();

        public async Task<List<Pokemons>> GetPokemonByType(string type) =>
            await _pokemonCollection.Find(p => p.Type.Equals(type, System.StringComparison.OrdinalIgnoreCase)).ToListAsync();

        public async Task<Pokemons> AddPokemon(Pokemons pokemon)
        {
            var maxId = _pokemonCollection.AsQueryable().AsEnumerable().Max(p => (int?)p.Id) ?? 0;
            pokemon.Id = maxId + 1;

            await _pokemonCollection.InsertOneAsync(pokemon);
            return pokemon;
        }

        public async Task<Pokemons?> UpdatePokemon(int id, Pokemons updatedPokemon)
        {
            var result = await _pokemonCollection.ReplaceOneAsync(p => p.Id == id, updatedPokemon);
            return result.IsAcknowledged && result.ModifiedCount > 0 ? updatedPokemon : null;
        }

        public async Task<bool> DeletePokemon(int id)
        {
            var result = await _pokemonCollection.DeleteOneAsync(p => p.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
