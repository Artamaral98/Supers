using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Supers.Communication.Responses;
using Supers.Communication.Responses.Wrapper;

namespace Supers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class SuperHeroiControllerBase : ControllerBase
    {
        protected IActionResult CriarRespostaDeSucesso<T>(T data)
        {
            var isList = data is ICollection;
            var isEmptyList = isList && ((ICollection)data).Count == 0;

            var message = isEmptyList ?
                "Nenhum registro encontrado." :
                "Operação realizada com sucesso.";

            var response = new RespostaSucesso<T>
            {
                Dados = data,
                Mensagem = message
            };

            return Ok(response);
        }

        protected IActionResult CriarRespostaDeSucesso(string mensagem)
        {
            var response = new SucessoResponse
            {
                Mensagem = mensagem
            };

            return Ok(response);
        }
    }
}
