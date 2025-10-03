using AutoMapper;
using Supers.Communication.Requests;
using Supers.Communication.Responses;
using Supers.Domain.Entidades;
using Supers.Domain.Repositorios;
using Supers.Exceptions;

namespace Supers.Application.UseCases.SuperHerois.Cadastro
{
    public class CadastroDeSupersUseCase : ICadastroDeSupersUseCase
    {
        private readonly ISuperHeroiRepository _superHeroiRepository;
        private readonly ISuperPoderRepository _superPoderRepository;
        private readonly IMapper _mapper;
        private readonly IUnityOfWork _unityOfWork;

        public CadastroDeSupersUseCase(ISuperHeroiRepository repository, IMapper mapper, IUnityOfWork unityOfWork, ISuperPoderRepository superPoderRepsitory)
        {
            _superHeroiRepository = repository;
            _mapper = mapper;
            _unityOfWork = unityOfWork;
            _superPoderRepository = superPoderRepsitory;
        }
        public async Task<CadastroSuperResponse> Executar (CadastroSuperRequest request)
        {
            await Validar(request);

            var heroi = _mapper.Map<SuperHeroi>(request);

            if (request.SuperPoderes.Any())
            {
                foreach (var poderId in request.SuperPoderes)
                {
                    heroi.HeroisSuperPoderes.Add(new HeroiSuperPoder
                    {
                        SuperPoderId = poderId
                    });
                }
            }

            await _superHeroiRepository.CadastrarHeroi(heroi);

            await _unityOfWork.Commit();

            var novoHeroi = await _superHeroiRepository.ObterHeroiPorId(heroi.Id);

            return _mapper.Map<CadastroSuperResponse>(novoHeroi);
        }

        private async Task Validar(CadastroSuperRequest request)
        {
            var validador = new CadastroDeSupersValidacao();
            var resultado = validador.Validate(request);

            var heroiCadastrado = await _superHeroiRepository.ExisteHeroiCadastradoPorNomeHeroi(request.NomeHeroi);
            if (heroiCadastrado)
            {
                resultado.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, Mensagens.NOME_HEROI_CADASTRADO));
            }

            if (request.SuperPoderes.Any())
            {
                var countPoderesExistentes = await _superPoderRepository.PoderExisteNoBanco(request.SuperPoderes);

                if (countPoderesExistentes != request.SuperPoderes.Count)
                {
                    resultado.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, Mensagens.PODER_NÂO_EXISTENTE));
                }
            }

            if (resultado.IsValid == false)
            {
                var mensagemDeErros = resultado.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrosEmValidacaoException(mensagemDeErros);
            }
        }
    }
}
