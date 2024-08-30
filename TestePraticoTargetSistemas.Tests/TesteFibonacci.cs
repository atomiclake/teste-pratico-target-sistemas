using TestePraticoTargetSistemas.CLI.Exercicios;

namespace TestePraticoTargetSistemas.Tests;

public class TesteFibonacci
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(13)]
    [InlineData(55)]
    [InlineData(89)]
    [InlineData(4181)]
    public void TesteFibonacci_Numeros_FazemParteDaSequencia(int numero)
    {
        EstaNaSequenciaFibonacci exercicio = new(numero);

        bool resultado = exercicio.Executar();

        Assert.True(resultado);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(14)]
    [InlineData(56)]
    [InlineData(4180)]
    public void TesteFibonacci_Numeros_NaoFazemParteDaSequencia(int numero)
    {
        EstaNaSequenciaFibonacci exercicio = new(numero);

        bool resultado = exercicio.Executar();

        Assert.False(resultado);
    }
}
