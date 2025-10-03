using AutoMapper;
using Supers.Application.UseCases.SuperHerois.Cadastro;
using Supers.Communication.Requests;
using Supers.Communication.Responses;
using Supers.Domain.Entidades;
using Supers.Domain.Repositorios;
using Supers.Exceptions;

namespace Supers.Application.UseCases.SuperHerois.Atualizar
{
    public class AtualizarSuperUseCase : IAtualizarSuperUseCase
    {
        private readonly ISuperHeroiRepository _superHeroiRepository;
        private readonly ISuperPoderRepository _superPoderRepository;
        private readonly IMapper _mapper;
        private readonly IUnityOfWork _unityOfWork;

        public AtualizarSuperUseCase(ISuperHeroiRepository superHeroiRepository, IMapper mapper, IUnityOfWork unityOfWork,
            ISuperPoderRepository superPoderRepository)
        {
            _superHeroiRepository = superHeroiRepository;
            _superPoderRepository = superPoderRepository;
            _mapper = mapper;
            _unityOfWork = unityOfWork;
        }

        public async Task<CadastroSuperResponse> Executar(int id, CadastroSuperRequest request)
        {
            await Validar(id, request);

            var heroiAntigo = await _superHeroiRepository.ObterHeroiPorId(id);

            if (heroiAntigo is null)
            {
                throw new NaoEncontradoException(Mensagens.HEROI_NAO_ENCONTRADO);
            }

            _mapper.Map(request, heroiAntigo);

            heroiAntigo.HeroisSuperPoderes.Clear();
            if (request.SuperPoderes.Any())
            {
                foreach (var poderId in request.SuperPoderes)
                {
                    heroiAntigo.HeroisSuperPoderes.Add(new HeroiSuperPoder
                    {
                        SuperPoderId = poderId
                    });
                }
            }

            await _unityOfWork.Commit();
            var heroiAtualizado = await _superHeroiRepository.ObterHeroiPorId(id);

            return _mapper.Map<CadastroSuperResponse>(heroiAtualizado);
        }

        private async Task Validar(int id, CadastroSuperRequest request)
        {
            var validador = new CadastroDeSupersValidacao();
            var resultado = validador.Validate(request);

            var heroiCadastrado = await _superHeroiRepository.ExisteOutroHeroiComMesmoNomeEIdDiferente(id, request.NomeHeroi);
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