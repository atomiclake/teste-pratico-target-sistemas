using System.Text;

namespace TestePraticoTargetSistemas.CLI.Exercicios;

public class InverterString : IExercicio<string>
{
    private readonly string _texto;

    public string Nome => nameof(InverterString);

    public InverterString(string texto)
    {
        _texto = texto;
    }

    public string Executar()
    {
        StringBuilder sb = new();

        if (_texto.Length <= 1)
        {
            return _texto;
        }

        for (int i = _texto.Length - 1; i >= 0; i--)
        {
            sb.Append(_texto[i]);
        }

        return sb.ToString();
    }
}
