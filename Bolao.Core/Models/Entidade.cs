namespace Bolao.Core.Models
{
    public abstract class Entidade<TKey>
    {
        public TKey Id { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public bool? Ativo { get; set; } = true;
    }
}
