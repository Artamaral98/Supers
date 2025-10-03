using Supers.Communication.Responses;

namespace Supers.Application.UseCases.SuperHerois.Obter
{
    public interface IObterSuperUseCase
    {
        Task<CadastroSuperResponse> Executar(int id);
    }
}
