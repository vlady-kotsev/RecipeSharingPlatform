using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecipeSharingPlatform.Data;
using RecipeSharingPlatform.Data.Models;
using RecipeSharingPlatform.Services.Core.Contracts;
using RecipeSharingPlatform.ViewModels;

namespace RecipeSharingPlatform.Services.Core
{
    public class RecipeService : IRecipeService
    {
        public Task<int> CreateAsync(RecipeViewModel model, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RecipeViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RecipeViewModel?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(RecipeViewModel model, string userId)
        {
            throw new NotImplementedException();
        }
    }
}

