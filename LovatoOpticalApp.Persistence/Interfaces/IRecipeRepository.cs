using LovatoOpticalApp.Core.Entities;

namespace LovatoOpticalApp.Persistence.Interfaces
{
    public interface IRecipeRepository
    {
        Task AddRecipeToCustomerAsync(Recipe recipe, Guid customerId);
        Task<List<Recipe>> GetRecipesByCustomerAsync(Guid customerId);
        Task<Recipe> GetLastRecipe(Guid customerId);
        Task<Recipe?> GetByIdAsync(Guid id);
        Task UpdateAsync(Recipe recipe);
        Task DeleteAsync(Guid id);
    }
}