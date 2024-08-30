using TestePraticoTargetSistemas.CLI.Exercicios;
using TestePraticoTargetSistemas.CLI.Exercicios.DadosFaturamento;

namespace TestePraticoTargetSistemas.Tests;

public class TesteFaturamentoJson
{
    private const string DADOS_TESTE_JSON = """
        {
            "dados": [
                {
                    "data": "01/08/2024",
                    "faturamento": 150.00
                },
                {
                    "data": "02/08/2024",
                    "faturamento": 320.00
                },
                {
                    "data": "03/08/2024",
                    "faturamento": 0
                },
                {
                    "data": "04/08/2024",
                    "faturamento": 0
                },
                {
                    "data": "05/08/2024",
                    "faturamento": 175.00
                },
                {
                    "data": "06/08/2024",
                    "faturamento": 221.00
                },
            ]
        }
        """;

    private const decimal MAIOR_FATURAMENTO_ESPERADO_DADOS_TESTE = 320.0m;

    private const decimal MENOR_FATURAMENTO_ESPERADO_DADOS_TESTE = 0.0m;

    private const int DIAS_ACIMA_MEDIA_ESPERADO_DADOS_TESTE = 2;

    [Fact]
    public void TesteFaturamentoJson_StringVazia_GeraResultadoNulo()
    {
        FaturamentoJson? exercicio = FaturamentoJson.ObterResultadoFaturamentoJson(string.Empty);

        var dados = exercicio?.Executar();

        Assert.Null(dados);
    }

    [Theory]
    [InlineData(DADOS_TESTE_JSON)]
    public void TesteFaturamentoJson_Builder_CriaInstanciaComSucesso(string dadosJson)
    {
        FaturamentoJson? exercicio = FaturamentoJson.ObterResultadoFaturamentoJson(dadosJson);

        Assert.NotNull(exercicio);
    }

    [Theory]
    [InlineData(DADOS_TESTE_JSON)]
    public void TesteFaturamentoJson_Executar_RetornaValoresEsperados(string dadosJson)
    {
        FaturamentoJson exercicio = FaturamentoJson.ObterResultadoFaturamentoJson(dadosJson)!;

        ResultadoFaturamento? resultado = exercicio.Executar();

        if (resultado is null)
        {
            Assert.Fail("Resultado não pode ser nulo");
            return;
        }

        List<(decimal Esperado, decimal Atual)> dadosTeste =
        [
            (MENOR_FATURAMENTO_ESPERADO_DADOS_TESTE, resultado.Value.MenorFaturamento),
            (MAIOR_FATURAMENTO_ESPERADO_DADOS_TESTE, resultado.Value.MaiorFaturamento),
            (DIAS_ACIMA_MEDIA_ESPERADO_DADOS_TESTE, resultado.Value.DiasFaturamentoAcimaDaMedia)
        ];

        Assert.All(dadosTeste, (dados) => Assert.Equal(dados.Esperado, dados.Atual));
    }
}
