using Supers.Communication.Requests;

namespace Supers.Application.UseCases.SuperHerois.ObterTodos
{
    public interface IObterTodosOsSupersUseCase
    {
        Task<RequestObterTodosOsSupers> Executar();
    }
}
