using Supers.Domain.Repositorios;
using Supers.Exceptions;

namespace Supers.Application.UseCases.SuperHerois.Excluir
{
    public class ExcluirSuperUseCase : IExcluirSuperUseCase
    {
        private readonly ISuperHeroiRepository _repository;
        private readonly IUnityOfWork _unityOfWork;

        public ExcluirSuperUseCase(ISuperHeroiRepository repository, IUnityOfWork unityOfWork)
        {
            _repository = repository;
            _unityOfWork = unityOfWork;
        }

        public async Task<string> Executar(int id)
        {
            var heroi = await _repository.ObterHeroiPorId(id);

                if (heroi is null)
                {
                throw new NaoEncontradoException(Mensagens.HEROI_NAO_ENCONTRADO);
            }

            _repository.Excluir(heroi);

            await _unityOfWork.Commit();

            return "Herói excluído com sucesso.";
        }
    }
}