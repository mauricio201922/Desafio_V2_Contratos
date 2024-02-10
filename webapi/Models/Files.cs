using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{
    public class Files
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Path { get; set; } = string.Empty;
        public int UsuarioId { get; set; }
        public virtual Usuarios Usuario { get; set; }
    }
}
