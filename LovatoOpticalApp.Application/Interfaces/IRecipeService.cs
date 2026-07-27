using LovatoOpticalApp.Application.DTOs;

namespace LovatoOpticalApp.Application.Interfaces
{
    public interface IRecipeService
    {
        Task CreateRecipe(RecipeRequestDto recipeRequestDto, Guid customerId, bool isOnlyRecipe=false);
        Task UpdateRecipe(RecipeRequestDto recipeRequestDto);
        Task<RecipeResponseDto> GetLastRecipe(string customerId);
    }
}
