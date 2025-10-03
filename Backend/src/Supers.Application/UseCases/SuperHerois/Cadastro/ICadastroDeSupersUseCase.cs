using Supers.Communication.Requests;
using Supers.Communication.Responses;

namespace Supers.Application.UseCases.SuperHerois.Cadastro
{
    public interface ICadastroDeSupersUseCase
    {
        public Task<CadastroSuperResponse> Executar(CadastroSuperRequest request);
    }
}
