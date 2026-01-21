using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Interfaces;
using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly ILogger<PokemonController> _logger;
        public PokemonController(IPokemonRepository pokemonRepository, ILogger<PokemonController> logger)
        {
            _pokemonRepository = pokemonRepository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public async Task<IActionResult> GetPokemons()
        {
            var pokemons = await _pokemonRepository.GetAllAsync();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemons);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPokemon(int id)
        {
            var pokemon = await _pokemonRepository.GetIdAsync(id);

            if (pokemon == null)
                return NotFound();

            return Ok(pokemon);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Pokemon))]
        public async Task<IActionResult> CreatePokemon([FromBody] Pokemon pokemonToCreate)
        {
            if (pokemonToCreate == null) return BadRequest(ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _pokemonRepository.AddAsync(pokemonToCreate);
            await _pokemonRepository.SaveAsync();
            return CreatedAtAction(nameof(GetPokemon), new { id = pokemonToCreate.Id }, pokemonToCreate);
        }

        [HttpPut("{pokemonId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdatePokemon(int pokemonId, [FromBody] Pokemon updatedPokemon)
        {
            if (updatedPokemon == null) return BadRequest(ModelState);
            if (pokemonId != updatedPokemon.Id) return BadRequest(ModelState);
            if (!await _pokemonRepository.ExistAsync(pokemonId)) return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _pokemonRepository.UpdateAsync(updatedPokemon);

            return NoContent();
        }

        [HttpDelete("{pokemonId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeletePokemon(int pokemonId)
        {
            if (!await _pokemonRepository.ExistAsync(pokemonId)) return NotFound();

            try
            {
                var isDeleted = await _pokemonRepository.DeleteAsync(pokemonId);

                if (!isDeleted)
                {
                    ModelState.AddModelError("", "Удаление не удалось по неизвестной причине.");
                    return StatusCode(500, ModelState);
                }
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", $"Ошибка базы данных: {ex.InnerException?.Message}");
                return StatusCode(500, ModelState);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Непредвиденная ошибка: {ex.Message}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteRangePokemons([FromBody] int[] pokemonIds)
        {
            if (pokemonIds == null || pokemonIds.Length == 0)
            {
                return BadRequest("Список ID не может быть пустым.");
            }

            var isDeleted = await _pokemonRepository.RemoveRange(pokemonIds);

            if (!isDeleted)
            {
                ModelState.AddModelError("", "Удаление не удалось по неизвестной причине.");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}

