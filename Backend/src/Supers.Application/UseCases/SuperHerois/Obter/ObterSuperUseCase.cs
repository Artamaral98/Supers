using AutoMapper;
using Supers.Communication.Responses;
using Supers.Domain.Repositorios;
using Supers.Exceptions;

namespace Supers.Application.UseCases.SuperHerois.Obter
{
    public class ObterSuperUseCase : IObterSuperUseCase
    {
        private readonly ISuperHeroiRepository _repository;
        private readonly IMapper _mapper;

        public ObterSuperUseCase(ISuperHeroiRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CadastroSuperResponse> Executar(int id)
        {
            var heroi = await _repository.ObterHeroiPorId(id);

            if (heroi is null)
            {
                throw new NaoEncontradoException(Mensagens.HEROI_NAO_ENCONTRADO);
            }

            return _mapper.Map<CadastroSuperResponse>(heroi);
        }
    }
}
