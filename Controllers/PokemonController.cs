using Microsoft.AspNetCore.Mvc;
using Pokemon.Models;
using System.Collections.Generic;
using System.Linq;

namespace Pokemon.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private const string NotFoundMessage = "Pokemon not found.";
        private const string InvalidDataMessage = "Invalid Pokemon data.";

        private static List<Pokemons> _pokemons = new List<Pokemons>
        {
            new Pokemons { Id = 2, Name = "Charmander", Type = "Fire", Ability = "Blaze", Level = 18 },
            new Pokemons { Id = 1, Name = "Pikachu", Type = "Electric", Ability = "Static", Level = 25 },
            new Pokemons { Id = 3, Name = "Squirtle", Type = "Water", Ability = "Torrent", Level = 16 }
        };

        [HttpGet]
        public IActionResult GetAllPokemons() => Ok(_pokemons);

        [HttpGet("{id}")]
        public IActionResult GetPokemonById(int id)
        {
            var pokemon = _pokemons.FirstOrDefault(p => p.Id == id);
            return pokemon == null ? NotFound(NotFoundMessage) : Ok(pokemon);
        }

        [HttpGet("type/{type}")]
        public IActionResult GetPokemonByType(string type)
        {
            var pokemons = _pokemons.Where(p => p.Type.Equals(type, System.StringComparison.OrdinalIgnoreCase)).ToList();
            return !pokemons.Any() ? NotFound($"No Pokémon found with type '{type}'.") : Ok(pokemons);
        }

        [HttpPost]
        public IActionResult AddPokemon([FromBody] Pokemons pokemon)
        {
            if (pokemon == null) return BadRequest(InvalidDataMessage);

            pokemon.Id = _pokemons.Count > 0 ? _pokemons.Max(p => p.Id) + 1 : 1;
            _pokemons.Add(pokemon);
            return Ok(new { message = "Pokemon added successfully.", pokemon });
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePokemon(int id, [FromBody] Pokemons updatedPokemon)
        {
            var pokemon = _pokemons.FirstOrDefault(p => p.Id == id);
            if (pokemon == null) return NotFound(NotFoundMessage);

            pokemon.Name = updatedPokemon.Name;
            pokemon.Type = updatedPokemon.Type;
            pokemon.Ability = updatedPokemon.Ability;
            pokemon.Level = updatedPokemon.Level;

            return Ok(new { message = "Pokemon updated successfully.", pokemon });
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePokemon(int id)
        {
            var pokemon = _pokemons.FirstOrDefault(p => p.Id == id);
            if (pokemon == null) return NotFound(NotFoundMessage);

            _pokemons.Remove(pokemon);
            return Ok(new { message = "Pokemon deleted successfully." });
        }
    }
}
