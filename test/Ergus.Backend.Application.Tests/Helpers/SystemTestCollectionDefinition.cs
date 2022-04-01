using Xunit;

namespace Ergus.Backend.Application.Tests.Helpers
{
    // Classe criada para deixar todos os testes rodando em sequencia
    // Deixa-los rodando em paralelo, como é o default, causa erros por conta dos mocks
    [CollectionDefinition(nameof(SystemTestCollectionDefinition), DisableParallelization = true)]
    public class SystemTestCollectionDefinition { }
}
