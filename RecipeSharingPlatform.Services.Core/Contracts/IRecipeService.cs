using RecipeSharingPlatform.ViewModels;

namespace RecipeSharingPlatform.Services.Core.Contracts
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipeViewModel>> GetAllAsync();
        Task<RecipeViewModel?> GetByIdAsync(int id);
        Task<int> CreateAsync(RecipeViewModel model, string userId);
        Task<bool> UpdateAsync(RecipeViewModel model, string userId);
        Task<bool> DeleteAsync(int id, string userId);
    }
}
