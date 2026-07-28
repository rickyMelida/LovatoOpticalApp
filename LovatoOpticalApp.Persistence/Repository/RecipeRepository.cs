using LovatoOpticalApp.Core.Entities;
using LovatoOpticalApp.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LovatoOpticalApp.Persistence.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly AppDbContext _context;

        public RecipeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddRecipeToCustomerAsync(Recipe recipe, Guid customerId)
        {
            recipe.CustomerId = customerId;
            await _context.Recipes.AddAsync(recipe);
        }

        public async Task<List<Recipe>> GetRecipesByCustomerAsync(Guid customerId)
        {
            return await _context.Recipes
                .Where(r => r.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<Recipe?> GetByIdAsync(Guid id) =>
            await _context.Recipes.FindAsync(id);

        public async Task UpdateAsync(Recipe recipe)
        {
            _context.Recipes.Update(recipe);
        }

        public async Task DeleteAsync(Guid id)
        {
            var recipe = await GetByIdAsync(id);
            if (recipe is not null)
            {
                _context.Recipes.Remove(recipe);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Recipe> GetLastRecipe(Guid customerId)
        {
            var recipeFound =  await _context.Recipes
                .Where(r => r.CustomerId == customerId)
                .OrderByDescending(r => r.PrescriptionIssueDate)
                .FirstOrDefaultAsync();

            if (recipeFound is null)
                return null;

            return recipeFound;
        }

		public async Task DeleteCustomerRecipe(Guid customerId)
		{
			var recipes = await GetRecipesByCustomerAsync(customerId);

			if(recipes.Any())
				_context.Recipes.RemoveRange(recipes);
		}
	}
}