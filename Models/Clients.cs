using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empresa_X_API.Repository.Models
{
    [Table("Clients")]
    public class Clients
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string fName { get; set; }
        [Required]
        public string lName { get; set; }
        [Required]
        public int cedula { get; set; }
        public int cellphoneNumber { get; set; }
    }   
}
