namespace RecipeSharingPlatform.ViewModels
{
    public class RecipeDeleteViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string AuthorId { get; set; } = string.Empty;
    }
}