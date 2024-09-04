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

    private readonly List<FaturamentoDiario> _faturamentos;

    public string Nome => nameof(FaturamentoJson);

    private FaturamentoJson(List<FaturamentoDiario> faturamentos)
    {
        _faturamentos = faturamentos;
    }

    public static FaturamentoJson? ObterResultadoFaturamentoJson(string infoFaturamento)
    {
        try
        {
            List<FaturamentoDiario> informacoes = JsonSerializer.Deserialize<List<FaturamentoDiario>>(infoFaturamento, _opcoesSerializacao) ?? [];

            return new(informacoes);
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
                [
                    {
                        "dia": "<NUMERO>",
                        "valor": <NUMERO>
                    }, ...
                ]
                """);
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
        if (_faturamentos.Count == 0)
        {
            Console.WriteLine("Nenhum faturamento para processar");
            return null;
        }

        // Obter o menor faturamento do mês
        decimal menorFaturamento = _faturamentos
            .Min(info => info.Valor);

        // Obter o maior faturamento do mês
        decimal maiorFaturamento = _faturamentos
            .Max(info => info.Valor);

        // Obter a média de faturamento do mês
        decimal mediaFaturamento = _faturamentos
            .Where(info => info.Valor != 0.0m)
            .Average(info => info.Valor);

        // Obter o número de dias onde o faturamento foi maior do que a média mensal
        int diasFaturamentoAcimaDaMedia = _faturamentos
            .Count(info => info.Valor > mediaFaturamento);

        return new(menorFaturamento, maiorFaturamento, diasFaturamentoAcimaDaMedia);
    }
}
