using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empresa_X_API.Repository.Models
{
    [Table("Directions")]
    public class Directions
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int clientId { get; set; }
        [Required]
        public string direction { get; set; }
        
        //public Clients clients { get; set; }
    }
}
