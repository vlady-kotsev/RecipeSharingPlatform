using System.ComponentModel.DataAnnotations;
using RecipeSharingPlatform.GCommon;

namespace RecipeSharingPlatform.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(ValidationConstants.CategoryNameMaxLength, MinimumLength = ValidationConstants.CategoryNameMinLength)]
        public string Name { get; set; } = null!;

        public ICollection<Recipe> Recipes { get; set; } = new HashSet<Recipe>();
    }
}

