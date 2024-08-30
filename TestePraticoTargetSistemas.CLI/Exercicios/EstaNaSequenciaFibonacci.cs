namespace TestePraticoTargetSistemas.CLI.Exercicios;

public class EstaNaSequenciaFibonacci : IExercicio<bool>
{
    private readonly int _numero;

    private readonly Dictionary<int, int> _mapa;

    private readonly List<int> _sequencia;

    public string Nome
    {
        get
        {
            return nameof(EstaNaSequenciaFibonacci);
        }
    }

    public EstaNaSequenciaFibonacci(int numero)
    {
        _numero = numero;

        // Adicionamos os casos "base" do algorítmo
        _sequencia = [0, 1];
        _mapa = new Dictionary<int, int>
        {
            { 0, 0 },
            { 1, 1 }
        };
    }

    // Utiliza conceitos de programação dinâmica (algorítmos) para reduzir a complexidade computacional do cálculo da sequência fibonacci
    private int GerarSequenciaFibonacci(int numero)
    {
        // Ainda não calculamos o valor da sequência para este número
        // Guardamos o resultado da computação atual em um dicionário, assim evitamos calular o mesmo valor diversas vezes
        if (!_mapa.TryGetValue(numero, out int value))
        {
            value = GerarSequenciaFibonacci(numero - 1) + GerarSequenciaFibonacci(numero - 2);

            _mapa.Add(numero, value);
            _sequencia.Add(value);
        }

        return value;
    }

    public bool Executar()
    {
        if (_numero < 0)
        {
            return false;
        }

        _ = GerarSequenciaFibonacci(_numero);

        return _sequencia.Contains(_numero);
    }
}
