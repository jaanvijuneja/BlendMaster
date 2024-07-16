using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
