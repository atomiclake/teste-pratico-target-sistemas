using TestePraticoTargetSistemas.CLI.Exercicios.DadosRepresentacaoEstado;

namespace TestePraticoTargetSistemas.CLI.Exercicios;

public class RepresentacaoEstado : IExercicio<IEnumerable<ResultadoRepresentacaoEstado>>
{
    private readonly Dictionary<string, decimal> _faturamentoPorEstado;

    public string Nome => nameof(RepresentacaoEstado);

    public RepresentacaoEstado()
    {
        _faturamentoPorEstado = new Dictionary<string, decimal>()
        {
            { "SP", 67836.43m },
            { "RJ", 36678.66m },
            { "MG", 29229.88m },
            { "ES", 27165.48m },
            { "Outros", 19849.53m },
        };
    }

    private double CalcularPercentualRepresentacaoEstado(string estado)
    {
        decimal totalFaturamento = _faturamentoPorEstado.Sum(d => d.Value);

        decimal valor = _faturamentoPorEstado[estado];

        return totalFaturamento != 0.0m ?
                    Math.Round((double)(valor / totalFaturamento), 2) :
                    0.0d; ;
    }

    public IEnumerable<ResultadoRepresentacaoEstado> Executar()
    {
        return _faturamentoPorEstado
            .Select(d => new ResultadoRepresentacaoEstado(d.Key, CalcularPercentualRepresentacaoEstado(d.Key)));
    }
}
