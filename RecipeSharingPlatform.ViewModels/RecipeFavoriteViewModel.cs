namespace RecipeSharingPlatform.ViewModels
{
    public class RecipeFavoriteViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string Category { get; set; } = string.Empty;
    }
}