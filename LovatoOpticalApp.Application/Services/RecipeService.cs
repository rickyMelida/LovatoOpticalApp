
using AutoMapper;
using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.Interfaces;
using LovatoOpticalApp.Core.Entities;
using LovatoOpticalApp.Persistence.Interfaces;

namespace LovatoOpticalApp.Application.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RecipeService(IRecipeRepository recipeRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateRecipe(RecipeRequestDto recipeRequestDto, Guid customerId, bool isOnlyRecipe = false)
        {
            var recipe = _mapper.Map<Recipe>(recipeRequestDto);

            await _recipeRepository.AddRecipeToCustomerAsync(recipe, customerId);

            if (isOnlyRecipe)
                await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateRecipe(RecipeRequestDto recipeRequestDto)
        {
            var recipe = _mapper.Map<Recipe>(recipeRequestDto);
            await _recipeRepository.UpdateAsync(recipe);
        }
    }
}
