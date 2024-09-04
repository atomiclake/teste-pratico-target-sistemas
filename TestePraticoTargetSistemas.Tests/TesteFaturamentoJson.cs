using TestePraticoTargetSistemas.CLI.Exercicios;
using TestePraticoTargetSistemas.CLI.Exercicios.DadosFaturamento;

namespace TestePraticoTargetSistemas.Tests;

public class TesteFaturamentoJson
{
    #region Dados de teste
    private const string DADOS_TESTE_JSON = """
        [
        	{
        		"dia": 1,
        		"valor": 22174.1664
        	},
        	{
        		"dia": 2,
        		"valor": 24537.6698
        	},
        	{
        		"dia": 3,
        		"valor": 26139.6134
        	},
        	{
        		"dia": 4,
        		"valor": 0.0
        	},
        	{
        		"dia": 5,
        		"valor": 0.0
        	},
        	{
        		"dia": 6,
        		"valor": 26742.6612
        	},
        	{
        		"dia": 7,
        		"valor": 0.0
        	},
        	{
        		"dia": 8,
        		"valor": 42889.2258
        	},
        	{
        		"dia": 9,
        		"valor": 46251.174
        	},
        	{
        		"dia": 10,
        		"valor": 11191.4722
        	},
        	{
        		"dia": 11,
        		"valor": 0.0
        	},
        	{
        		"dia": 12,
        		"valor": 0.0
        	},
        	{
        		"dia": 13,
        		"valor": 3847.4823
        	},
        	{
        		"dia": 14,
        		"valor": 373.7838
        	},
        	{
        		"dia": 15,
        		"valor": 2659.7563
        	},
        	{
        		"dia": 16,
        		"valor": 48924.2448
        	},
        	{
        		"dia": 17,
        		"valor": 18419.2614
        	},
        	{
        		"dia": 18,
        		"valor": 0.0
        	},
        	{
        		"dia": 19,
        		"valor": 0.0
        	},
        	{
        		"dia": 20,
        		"valor": 35240.1826
        	},
        	{
        		"dia": 21,
        		"valor": 43829.1667
        	},
        	{
        		"dia": 22,
        		"valor": 18235.6852
        	},
        	{
        		"dia": 23,
        		"valor": 4355.0662
        	},
        	{
        		"dia": 24,
        		"valor": 13327.1025
        	},
        	{
        		"dia": 25,
        		"valor": 0.0
        	},
        	{
        		"dia": 26,
        		"valor": 0.0
        	},
        	{
        		"dia": 27,
        		"valor": 25681.8318
        	},
        	{
        		"dia": 28,
        		"valor": 1718.1221
        	},
        	{
        		"dia": 29,
        		"valor": 13220.495
        	},
        	{
        		"dia": 30,
        		"valor": 8414.61
        	}
        ]
        """;
    #endregion

    private const decimal MAIOR_FATURAMENTO_ESPERADO_DADOS_TESTE = 48924.2448m;

    private const decimal MENOR_FATURAMENTO_ESPERADO_DADOS_TESTE = 0.0m;

    private const int DIAS_ACIMA_MEDIA_ESPERADO_DADOS_TESTE = 10;

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
