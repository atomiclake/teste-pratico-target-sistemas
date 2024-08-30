namespace TestePraticoTargetSistemas.CLI;

public interface IExercicio<T>
{
    string Nome { get; }

    T Executar();
}
