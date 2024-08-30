using TestePraticoTargetSistemas.CLI.Exercicios;

namespace TestePraticoTargetSistemas.Tests;

public class TesteSomaComRepeticao
{
    private const int RESULTADO_ESPERADO = 91;

    [Fact]
    public void TesteSomaComRepeticao_Algoritmo_ProduzResultadoEsperado()
    {
        SomaComLacosDeRepeticao exercicio = new();

        int resultado = exercicio.Executar();

        Assert.Equal(RESULTADO_ESPERADO, 91);
    }
}
