using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IPokemonRepository : IRepository<Pokemon>
    {
        Task<IEnumerable<Pokemon>> GetPokemonsByOwnerAsync(int ownerId);
    }
}
