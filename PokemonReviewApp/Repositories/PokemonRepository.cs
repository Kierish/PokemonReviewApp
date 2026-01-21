using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repositories
{
    public class PokemonRepository : Repository<Pokemon>, IPokemonRepository
    {
        public PokemonRepository(DataContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<Pokemon>> GetPokemonsByOwnerAsync(int ownerId)
        {
            return await _context.Pokemons
                .Where(p => p.OwnerId == ownerId)
                .ToListAsync();
        }
    }
}
