using Supers.Communication.Requests;
using Supers.Communication.Responses;
using Supers.Exceptions;

namespace Supers.Application.UseCases.SuperHerois.Cadastro
{
    public class CadastroDeSupersUseCase
    {
        public async Task<CadastroSuperResponse> Executar (CadastroSuperRequest request)
        {
            Validar(request);
            //Validar a request.

            //Mapear a request em uma entidade.

            //Salvar no DB

            var resposta = new CadastroSuperResponse()
            {
                Nome = request.Nome
            };

            return resposta;
        }

        private void Validar(CadastroSuperRequest request)
        {
            var validador = new CadastroDeSupersValidacao();
            var resultado = validador.Validate(request);

            if (resultado.IsValid == false)
            {
                var mensagemDeErros = resultado.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrosEmValidacaoException(mensagemDeErros);
            }
        }
    }
}
