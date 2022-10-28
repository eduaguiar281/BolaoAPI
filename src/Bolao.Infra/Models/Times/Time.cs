using System.ComponentModel.DataAnnotations;

namespace Bolao.Infra.Models.Times
{
    public class Time
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage ="Nome é obrigatório!")]
        public string Nome { get; set; }

    }
}
