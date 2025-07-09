using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using RecipeSharingPlatform.Services.Core.Contracts;
using RecipeSharingPlatform.ViewModels;

namespace RecipeSharingPlatform.Web.Pages.Recipes
{
    public class CreateModel : PageModel
    {
        private readonly IRecipeService _recipeService;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(IRecipeService recipeService, UserManager<IdentityUser> userManager)
        {
            _recipeService = recipeService;
            _userManager = userManager;
        }

        [BindProperty]
        public RecipeViewModel Recipe { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var userId = _userManager.GetUserId(User);
            await _recipeService.CreateAsync(Recipe, userId);
            return RedirectToPage("Index");
        }
    }
}