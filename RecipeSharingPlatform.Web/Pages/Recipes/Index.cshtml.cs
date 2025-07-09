using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeSharingPlatform.Services.Core.Contracts;
using RecipeSharingPlatform.ViewModels;

namespace RecipeSharingPlatform.Web.Pages.Recipes
{
    public class IndexModel : PageModel
    {
        private readonly IRecipeService _recipeService;

        public IndexModel(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        public IList<RecipeViewModel> Recipes { get; set; } = new List<RecipeViewModel>();

        public async Task OnGetAsync()
        {
            Recipes = (await _recipeService.GetAllAsync()).ToList();
        }
    }
}