using System.Globalization;
using System.Text.Json;

using TestePraticoTargetSistemas.CLI.Exercicios.DadosFaturamento;

namespace TestePraticoTargetSistemas.CLI.Exercicios;

public class FaturamentoJson : IExercicio<ResultadoFaturamento?>
{
    private static readonly JsonSerializerOptions _opcoesSerializacao = new()
    {
        AllowTrailingCommas = true,
        PropertyNameCaseInsensitive = true,
        ReadCommentHandling = JsonCommentHandling.Skip
    };

    private readonly Faturamentos _faturamentos;

    public string Nome => nameof(FaturamentoJson);

    private FaturamentoJson(Faturamentos faturamentos)
    {
        _faturamentos = faturamentos;
    }

    public static FaturamentoJson? ObterResultadoFaturamentoLista(Faturamentos faturamentos)
    {
        return new(faturamentos);
    }

    public static FaturamentoJson? ObterResultadoFaturamentoJson(string infoFaturamento)
    {
        try
        {
            Faturamentos? informacoes = JsonSerializer.Deserialize<Faturamentos>(infoFaturamento, _opcoesSerializacao);

            return new(informacoes ?? new([]));
        }
        catch (JsonException jsonException)
        {
            string linha = jsonException.LineNumber is long l ? $"linha: {l}" : "";
            string posicao = jsonException.BytePositionInLine is long c ? $"coluna: {c}" : "";
            string caminho = jsonException.Path is string p ? $"propriedade: {p}" : "";

            var propriedadesErro = new List<string>() { linha, posicao, caminho }
                .Where(txt => !string.IsNullOrWhiteSpace(txt));

            string informacoesErro = $" ({string.Join(", ", propriedadesErro)})";

            Console.WriteLine($"Ocorreu um erro ao processar o arquivo JSON{propriedadesErro}.\n{jsonException.Message}");

            Console.WriteLine("[INFO]: O arquivo JSON precisa seguir esse formato:" + """
                {
                    "dados": [
                        {
                            "data": "<DATA EM DD/MM/YYYY>",
                            "faturamento": <NUMERO>
                        }, ...
                    ]
                }
                """);

            if (caminho.Contains("data"))
            {
                Console.WriteLine("[INFO]: Utilize o formato de data DD/MM/YYYY");
            }
        }
        catch (NotSupportedException)
        {
            Console.WriteLine($"O seu sitema não dá suporte à funcionalidade {nameof(JsonSerializer.Deserialize)}, use a versão de lista");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocorreu um erro ao executar o exercício. Mensagem: {ex.Message}");
        }

        return null;
    }

    public ResultadoFaturamento? Executar()
    {
        if (_faturamentos.Dados.Count == 0)
        {
            Console.WriteLine("Nenhum faturamento para processar");
            return null;
        }

        CultureInfo culture = CultureInfo.GetCultureInfo("pt-BR");

        List<(DateOnly Data, decimal Faturamento)> faturamentos = _faturamentos.Dados
            .Select(f => (DateOnly.Parse(f.Data, culture), f.Faturamento))
            .ToList();

        // Obter o mês que será processado
        int mesDeCalculo = faturamentos
            .First()
            .Data.Month;

        // Somente incluir os faturamentos do mês de cálculo
        var baseCalculo = faturamentos
            .Where(info => info.Data.Month == mesDeCalculo)
            .ToList();

        // Obter o menor faturamento do mês
        decimal menorFaturamento = baseCalculo
            .Min(info => info.Faturamento);

        // Obter o maior faturamento do mês
        decimal maiorFaturamento = baseCalculo
            .Max(info => info.Faturamento);

        // Obter a média de faturamento do mês
        decimal mediaFaturamento = baseCalculo
            .Where(info => info.Faturamento != 0.0m)
            .Average(info => info.Faturamento);

        // Obter o número de dias onde o faturamento foi maior do que a média mensal
        int diasFaturamentoAcimaDaMedia = baseCalculo
            .Count(info => info.Faturamento > mediaFaturamento);

        return new(menorFaturamento, maiorFaturamento, diasFaturamentoAcimaDaMedia);
    }
}
