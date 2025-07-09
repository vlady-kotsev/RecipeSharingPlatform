using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeSharingPlatform.Services.Core.Contracts;
using RecipeSharingPlatform.ViewModels;

namespace RecipeSharingPlatform.Web.Pages.Recipes
{
    public class DetailsModel : PageModel
    {
        private readonly IRecipeService _recipeService;

        public DetailsModel(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        public RecipeViewModel? Recipe { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Recipe = await _recipeService.GetByIdAsync(id);
            if (Recipe == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}