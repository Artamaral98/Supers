using Supers.Communication.Responses;

namespace Supers.Application.UseCases.SuperPoderes.ObterTodos
{
    public interface IObterTodosOsPoderesUseCase
    {
        Task<IList<SuperPoderResponse>> Executar();
    }
}
