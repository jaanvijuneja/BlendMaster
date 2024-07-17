using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Entities
{
    public enum RecipeStatusType
    {
        Testing, 
        RolledOut,
        Rejected,
        Archived
    }

    public class Recipe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RecipeId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public List<string>? Ingredients { get; set; }

        public List<string>? Instructions { get; set; }

        public List<string>? Tags { get; set; }

        public RecipeStatusType Status { get; set; }
    }
}
