namespace TestePraticoTargetSistemas.CLI.Exercicios;

public class SomaComLacosDeRepeticao : IExercicio<int>
{
    public string Nome => nameof(SomaComLacosDeRepeticao);

    public int Executar()
    {
        // Inicializar as variáveis
        int indice = 13;
        int soma = 0;
        int k = 0;

        // Executar laço de repetição
        while (k < indice)
        {
            // Incrementa o valor da variável 'k', e atribui o valor 'soma + k' à variabel soma
            soma += ++k;
        }

        return soma;
    }
}
