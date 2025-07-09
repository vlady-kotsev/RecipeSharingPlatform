namespace RecipeSharingPlatform.ViewModels
{
    public class RecipeViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Instructions { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string Author { get; set; } = string.Empty;
        public string AuthorId { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public string Category { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int SavedCount { get; set; }
        public bool IsAuthor { get; set; }
        public bool IsSaved { get; set; }
    }
}