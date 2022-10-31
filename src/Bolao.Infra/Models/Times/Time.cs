using Bolao.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Bolao.Infra.Models.Times
{
    public class Time : Entidade<int>
    {
        [Required(ErrorMessage ="Nome é obrigatório!")]
        public string Nome { get; set; }
    }
}
