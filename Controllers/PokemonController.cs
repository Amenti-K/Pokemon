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
        public IActionResult GetAllPokemons() => Ok(_pokemonService.GetAllPokemons());

        [HttpGet("{id}")]
        public IActionResult GetPokemonById(int id)
        {
            var pokemon = _pokemonService.GetPokemonById(id);
            return pokemon == null ? NotFound("Pokemon not found.") : Ok(pokemon);
        }

        [HttpGet("type/{type}")]
        public IActionResult GetPokemonByType(string type)
        {
            var pokemons = _pokemonService.GetPokemonByType(type);
            return pokemons.Count == 0 ? NotFound($"No Pokémon found with type '{type}'.") : Ok(pokemons);
        }

        [HttpPost]
        public IActionResult AddPokemon([FromBody] Pokemons pokemon)
        {
            if (pokemon == null) return BadRequest("Invalid Pokemon data.");
            var addedPokemon = _pokemonService.AddPokemon(pokemon);
            return Ok(new { message = "Pokemon added successfully.", addedPokemon });
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePokemon(int id, [FromBody] Pokemons updatedPokemon)
        {
            var pokemon = _pokemonService.UpdatePokemon(id, updatedPokemon);
            return pokemon == null ? NotFound("Pokemon not found.") : Ok(new { message = "Pokemon updated successfully.", pokemon });
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePokemon(int id)
        {
            var result = _pokemonService.DeletePokemon(id);
            return !result ? NotFound("Pokemon not found.") : Ok(new { message = "Pokemon deleted successfully." });
        }
    }
}
