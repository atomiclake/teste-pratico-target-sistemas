using TestePraticoTargetSistemas.CLI.Exercicios;

namespace TestePraticoTargetSistemas.Tests;

public class TesteInverterString
{
    [Theory]
    [InlineData("")]
    [InlineData("a")]
    [InlineData("abcd")]
    [InlineData("invertido")]
    [InlineData("carro")]
    public void TesteInverterString_Exercicio_RetornaValoresCorretos(string texto)
    {
        string valorEsperado = string.Join("", texto.Reverse());
        InverterString exercicio = new(texto);

        string atual = exercicio.Executar();

        Assert.Equal(valorEsperado, atual);
    }
}
