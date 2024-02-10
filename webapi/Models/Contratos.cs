using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{
    public class Contratos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public int Contrato { get; set; }
        public string Produto {  get; set; } = string.Empty;
        public string Vencimento { get; set; } = DateTime.Now.Date.ToString();
        public double Valor { get; set; }
        public int UsuarioId { get; set; }
    }
}
