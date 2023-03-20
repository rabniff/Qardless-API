using System.ComponentModel.DataAnnotations;

namespace QardlessAPI.Data.Models
{
    public class EndUser
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
