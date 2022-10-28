namespace Bolao.TesteInjecaoDependencia
{
    public class OperacaoPadrao
        :IOperacaoScoped,
        IOperacaoSingleton,
        IOperacaoTransient
    {
        public OperacaoPadrao()
        {
            IdOperacao = Guid.NewGuid().ToString()[^4..];
        }
        public string IdOperacao { get; private set; }
    }
}
