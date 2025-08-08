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
        private readonly ApplicationDbContext _context;

        public RecipeService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> CreateAsync(RecipeViewModel model, string userId)
        {
            var entity = new Recipe
            {
                Title = model.Title,
                Instructions = model.Instructions,
                ImageUrl = model.ImageUrl,
                CategoryId = model.CategoryId,   // link to Category
                AuthorId = userId,               // link to current user
                CreatedOn = DateTime.UtcNow
            };

            await _context.Recipes.AddAsync(entity); // or _context.Add(entity)
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public Task<bool> DeleteAsync(int id, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RecipeViewModel>> GetAllAsync()
        {
            return await this._context.Recipes.AsNoTracking().Select(r => new RecipeViewModel
            {
                Id = r.Id,
                Title = r.Title,
                Instructions = r.Instructions,
                ImageUrl = r.ImageUrl,
                AuthorId = r.AuthorId,
                CategoryId = r.CategoryId,
                Category = r.Category.Name,
                CreatedOn = r.CreatedOn
            }).ToListAsync();
        }

        public async Task<RecipeViewModel?> GetByIdAsync(int id)
        {
            return await this._context.Recipes.AsNoTracking().Select(r => new RecipeViewModel
            {
                Id = r.Id,
                Title = r.Title,
                Instructions = r.Instructions,
                ImageUrl = r.ImageUrl,
                AuthorId = r.AuthorId,
                Author = r.Author.UserName!,
                CategoryId = r.CategoryId,
                Category = r.Category.Name,
                CreatedOn = r.CreatedOn
            })

            .FirstOrDefaultAsync(r => r.Id == id);
        }

        public Task<bool> UpdateAsync(RecipeViewModel model, string userId)
        {
            throw new NotImplementedException();
        }
    }
}

