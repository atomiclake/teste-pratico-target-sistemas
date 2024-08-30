using TestePraticoTargetSistemas.CLI.Exercicios;

namespace TestePraticoTargetSistemas.CLI;

internal class Program
{
    static readonly string CAMINHO_PADRAO_JSON = Path.Combine(Environment.CurrentDirectory, "Resources", "arquivoPadraoFaturamento.json");

    static void Exercicio1_Soma()
    {
        Console.WriteLine(nameof(Exercicio1_Soma));

        int resultado = new SomaComLacosDeRepeticao().Executar();

        Console.WriteLine($"O resultado da soma é: {resultado}");
    }

    static void Exercicio2_Fibonacci(int numero)
    {
        Console.WriteLine(nameof(Exercicio2_Fibonacci));

        bool resultado = new EstaNaSequenciaFibonacci(numero).Executar();

        Console.WriteLine($"O número {numero}{(!resultado ? "não " : " ")}está presente na sequência Fibonacci");
    }

    static void Exercicio3_FaturamentoJson(string arquivoJson)
    {
        if (!File.Exists(arquivoJson))
        {
            Console.WriteLine($"Não foi possível achar o arquivo '{arquivoJson}', pulando para o próximo exercício.");
            return;
        }

        string conteudo = File.ReadAllText(arquivoJson);

        Console.WriteLine(nameof(Exercicio3_FaturamentoJson));

        var resultado = FaturamentoJson.ObterResultadoFaturamentoJson(conteudo)?.Executar();

        if (resultado is not null)
        {
            Console.WriteLine($"Menor faturamento: {resultado.Value.MenorFaturamento}");
            Console.WriteLine($"Maior faturamento: {resultado.Value.MaiorFaturamento}");
            Console.WriteLine($"Dias com faturamento acima da média: {resultado.Value.DiasFaturamentoAcimaDaMedia}");
            return;
        }

        Console.WriteLine("Não foi possível obter o resultado, verifique sua fonte de dados e as mensagens de erro");
    }

    static void Exercicio4_RepresentacaoEstado()
    {
        Console.WriteLine(nameof(Exercicio4_RepresentacaoEstado));

        var resultado = new RepresentacaoEstado().Executar();

        foreach (var item in resultado)
        {
            Console.WriteLine($"Estado: {item.SiglaEstado}");
            Console.WriteLine($"Representatividade: {item.Percentual:P2}");
        }
    }

    static void Exercicio5_InverterString(string texto)
    {
        Console.WriteLine(nameof(Exercicio5_InverterString));

        var resultado = new InverterString(texto).Executar();

        Console.WriteLine($"A string '{texto}' invertida é: '{resultado}'");
    }

    static void Main(string[] args)
    {
        string caminhoArquivo = CAMINHO_PADRAO_JSON;
        int numero = 4181;
        string texto = "Valor padrão";

        if (args.Length is < 3)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Nenhuma entrada de usuário detectada, usando valores padrões");
            }
            else if (args.Length < 3)
            {
                Console.WriteLine("Poucos argumentos detectados, usando valores padrão");
            }

            Console.WriteLine();
            Console.WriteLine("Para usar os seus dados, passe os seguintes argumentos:");
            Console.WriteLine("\t1° argumento: Caminho do arquivo .json");
            Console.WriteLine("\t2° argumento: Uma número qualquer");
            Console.WriteLine("\t3° argumento: Uma string qualquer");
            Console.WriteLine();
        }
        else
        {
            caminhoArquivo = args[0];
            
            if (!int.TryParse(args[1], out int numeroUsuario))
            {
                numero = numeroUsuario;
            }
            else
            {
                Console.WriteLine($"O número informado '{args[1]}' é inválido, usando valor padrão {numero}");
            }

            texto = args[2];
        }

        Console.WriteLine();
        Exercicio1_Soma();

        Console.WriteLine();
        Exercicio2_Fibonacci(numero);

        Console.WriteLine();
        Exercicio3_FaturamentoJson(caminhoArquivo);

        Console.WriteLine();
        Exercicio4_RepresentacaoEstado();

        Console.WriteLine();
        Exercicio5_InverterString(texto);
    }
}
