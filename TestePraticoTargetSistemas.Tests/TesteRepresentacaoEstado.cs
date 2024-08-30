using TestePraticoTargetSistemas.CLI.Exercicios;

namespace TestePraticoTargetSistemas.Tests;

public class TesteRepresentacaoEstado
{
    private static readonly Dictionary<string, double> RESULTADO_ESPERADO = new Dictionary<string, double>
    {
        { "SP", 0.38d },
        { "RJ", 0.20d },
        { "MG", 0.16d },
        { "ES", 0.15d },
        { "Outros", 0.110d }
    };

    [Fact]
    public void TesteRepresentacaoEstado_Exercicio_RetornaValoresCorretos()
    {
        RepresentacaoEstado exercicio = new();

        var resultado = exercicio.Executar().ToDictionary(key => key.SiglaEstado, element => element.Percentual);

        List<(double Esperado, double Atual)> dadosTeste =
        [
            (RESULTADO_ESPERADO["SP"], resultado["SP"]),
            (RESULTADO_ESPERADO["RJ"], resultado["RJ"]),
            (RESULTADO_ESPERADO["MG"], resultado["MG"]),
            (RESULTADO_ESPERADO["ES"], resultado["ES"]),
            (RESULTADO_ESPERADO["Outros"], resultado["Outros"]),
        ];

        Assert.All(dadosTeste, (dados) => Assert.Equal(dados.Esperado, dados.Atual));
    }
}
