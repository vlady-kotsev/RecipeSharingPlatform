using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeSharingPlatform.Services.Core.Contracts;
using RecipeSharingPlatform.ViewModels;
using System.Diagnostics;
using System.Security.Claims;

public class RecipeController : Controller
{
    private readonly ILogger<RecipeController> _logger;
    private readonly IRecipeService _recipeService;
    private readonly ICategoryService _categoryService;

    public RecipeController(ILogger<RecipeController> logger, IRecipeService recipeService, ICategoryService categoryService)
    {
        _logger = logger;
        _recipeService = recipeService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var recipes = await _recipeService.GetAllAsync();
        return View(recipes);
    }

    public async Task<IActionResult> Details(int id)
    {
        var recipe = await _recipeService.GetByIdAsync(id);
        return View(recipe);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");
        return View(new RecipeViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(RecipeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name");
            return View(model);
        }
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        await this._recipeService.CreateAsync(model, userId);
        return RedirectToAction("Index");
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}