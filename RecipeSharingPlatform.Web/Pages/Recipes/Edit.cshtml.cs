using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using RecipeSharingPlatform.Services.Core.Contracts;
using RecipeSharingPlatform.ViewModels;

namespace RecipeSharingPlatform.Web.Pages.Recipes
{
    public class EditModel : PageModel
    {
        private readonly IRecipeService _recipeService;
        private readonly UserManager<IdentityUser> _userManager;

        public EditModel(IRecipeService recipeService, UserManager<IdentityUser> userManager)
        {
            _recipeService = recipeService;
            _userManager = userManager;
        }

        [BindProperty]
        public RecipeViewModel Recipe { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Recipe = await _recipeService.GetByIdAsync(id);
            if (Recipe == null)
                return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var userId = _userManager.GetUserId(User);
            var updated = await _recipeService.UpdateAsync(Recipe, userId);
            if (!updated)
                return Forbid();

            return RedirectToPage("Index");
        }
    }
}