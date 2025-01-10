using Microsoft.AspNetCore.Mvc;
using Pokemon.Api.Services;
using Pokemon.Models;

namespace Pokemon.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPokemons() =>
            Ok(await _pokemonService.GetAllPokemons());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPokemonById(int id)
        {
            var pokemon = await _pokemonService.GetPokemonById(id);
            return pokemon == null ? NotFound("Pokemon not found.") : Ok(pokemon);
        }

        [HttpGet("type/{type}")]
        public async Task<IActionResult> GetPokemonByType(string type)
        {
            var pokemons = await _pokemonService.GetPokemonByType(type);
            return pokemons.Count == 0 ? NotFound($"No Pokémon found with type '{type}'.") : Ok(pokemons);
        }

        [HttpPost]
        public async Task<IActionResult> AddPokemon([FromBody] Pokemons pokemon)
        {
            if (pokemon == null) return BadRequest("Invalid Pokemon data.");
            var addedPokemon = await _pokemonService.AddPokemon(pokemon);
            return Ok(new { message = "Pokemon added successfully.", addedPokemon });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePokemon(int id, [FromBody] Pokemons updatedPokemon)
        {
            var pokemon = await _pokemonService.UpdatePokemon(id, updatedPokemon);
            return pokemon == null ? NotFound("Pokemon not found.") : Ok(new { message = "Pokemon updated successfully.", pokemon });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePokemon(int id)
        {
            var result = await _pokemonService.DeletePokemon(id);
            return !result ? NotFound("Pokemon not found.") : Ok(new { message = "Pokemon deleted successfully." });
        }
    }
}
