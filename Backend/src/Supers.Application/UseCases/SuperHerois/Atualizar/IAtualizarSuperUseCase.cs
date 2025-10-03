using Supers.Communication.Requests;
using Supers.Communication.Responses;

namespace Supers.Application.UseCases.SuperHerois.Atualizar
{
    public interface IAtualizarSuperUseCase
    {
        Task<CadastroSuperResponse> Executar(int id, CadastroSuperRequest request);
    }
}