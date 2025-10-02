using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Supers.Exceptions;
using Supers.Communication.Responses;

namespace Supers.API.Filtros
{
    public class FiltroDeExcecoes : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is SupersExceptionBase)
                IdentificarExcecaoEEnviarStatusCode(context);

            else
            {
                EnviarExcecaoDesconhecida(context);
            }

        }

        // Método que tem como objetivo identificar qual o tipo de erro encontrado e enviar o status code e a lista dos erros correspondentes.
        private void IdentificarExcecaoEEnviarStatusCode(ExceptionContext context)
        {
            if (context.Exception is ErrosEmValidacaoException)
            {
                var exception = context.Exception as ErrosEmValidacaoException; //Cast para transformar o context.Exception em ErrorOnValidationException

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                //ResponseErrorJson é um objeto criado para conter a lista de erros, evitando que seja enviado ao usuário mensagens de erros com dados sigilosos da aplicação.
                context.Result = new BadRequestObjectResult(new ErrosResponse(exception!.MensagensDeErros));
            }
        }

        //Método que será retornado em casos de erros desconhecidos.
        private void EnviarExcecaoDesconhecida(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new ErrosResponse(Mensagens.ERRO_DESCONHECIDO));
        }
    }
}
