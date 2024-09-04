using TestePraticoTargetSistemas.CLI.Exercicios;

namespace TestePraticoTargetSistemas.CLI;

internal class Program
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

    static readonly string CAMINHO_PADRAO_JSON = Path.Combine(Environment.CurrentDirectory, "Resources", "dados.json");

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
        string conteudo = string.Empty;

        if (!File.Exists(arquivoJson))
        {
            Console.WriteLine($"Não foi possível achar o arquivo '{arquivoJson}', pulando para o próximo exercício.");
            conteudo = DADOS_TESTE_JSON;
        }
        else
        {
            conteudo = File.ReadAllText(arquivoJson);
        }

        Console.WriteLine(nameof(Exercicio3_FaturamentoJson));

        var resultado = FaturamentoJson.ObterResultadoFaturamentoJson(conteudo)?.Executar();

        if (resultado is null)
        {
            Console.WriteLine("Não foi possível obter o resultado, verifique sua fonte de dados e as mensagens de erro");
            return;
        }

        Console.WriteLine($"Menor faturamento: {resultado.Value.MenorFaturamento}");
        Console.WriteLine($"Maior faturamento: {resultado.Value.MaiorFaturamento}");
        Console.WriteLine($"Dias com faturamento acima da média: {resultado.Value.DiasFaturamentoAcimaDaMedia}");
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
            Console.WriteLine("\t2° argumento: Um número qualquer");
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
