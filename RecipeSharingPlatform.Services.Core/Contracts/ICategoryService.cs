using RecipeSharingPlatform.ViewModels;

namespace RecipeSharingPlatform.Services.Core.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllAsync();
        Task<CategoryViewModel?> GetByIdAsync(int id);
    }
}