using AutoMapper;
using Supers.Communication.Requests;
using Supers.Communication.Responses;
using Supers.Domain.Repositorios;
using Supers.Exceptions;

namespace Supers.Application.UseCases.SuperHerois.Cadastro
{
    public class CadastroDeSupersUseCase
    {
        private readonly ISuperHeroiRepository _repository;
        public async Task<CadastroSuperResponse> Executar (CadastroSuperRequest request)
        {
            Validar(request);

            //Salvar no DB

            var resposta = new CadastroSuperResponse()
            {
                Nome = request.Nome
            };

            return resposta;
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
