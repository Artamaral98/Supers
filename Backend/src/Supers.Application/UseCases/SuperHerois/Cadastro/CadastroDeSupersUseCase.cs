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
        private readonly ISuperHeroiRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnityOfWork _unityOfWork;

        public CadastroDeSupersUseCase(ISuperHeroiRepository repository, IMapper mapper, IUnityOfWork unityOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unityOfWork = unityOfWork;
        }
        public async Task<CadastroSuperResponse> Executar (CadastroSuperRequest request)
        {
            await Validar(request);

            var heroi = _mapper.Map<SuperHeroi>(request);

            if (request.SuperPoderesIds.Any())
            {
                foreach (var poderId in request.SuperPoderesIds)
                {
                    heroi.HeroisSuperPoderes.Add(new HeroiSuperPoder
                    {
                        SuperPoderId = poderId
                    });
                }
            }

            await _repository.CadastrarHeroi(heroi);

            await _unityOfWork.Commit();

            var response = _mapper.Map<CadastroSuperResponse>(heroi);

            return response;
        }

        private async Task Validar(CadastroSuperRequest request)
        {
            var validador = new CadastroDeSupersValidacao();
            var resultado = validador.Validate(request);

            var heroiCadastrado = await _repository.ExisteHeroiCadastradoPorNomeHeroi(request.NomeHeroi);

            if (heroiCadastrado)
            {
                resultado.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, Mensagens.NOME_HEROI_CADASTRADO));
            }

            if (resultado.IsValid == false)
            {
                var mensagemDeErros = resultado.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrosEmValidacaoException(mensagemDeErros);
            }
        }
    }
}
