using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using RecipeSharingPlatform.Services.Core.Contracts;
using RecipeSharingPlatform.ViewModels;

namespace RecipeSharingPlatform.Web.Pages.Recipes
{
    public class DeleteModel : PageModel
    {
        private readonly IRecipeService _recipeService;
        private readonly UserManager<IdentityUser> _userManager;

        public DeleteModel(IRecipeService recipeService, UserManager<IdentityUser> userManager)
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var userId = _userManager.GetUserId(User);
            var deleted = await _recipeService.DeleteAsync(id, userId);
            if (!deleted)
                return Forbid();

            return RedirectToPage("Index");
        }
    }
}