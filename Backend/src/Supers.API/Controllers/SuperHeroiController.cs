using Microsoft.AspNetCore.Mvc;
using Supers.Application.UseCases.SuperHerois.Cadastro;
using Supers.Communication.Requests;
using Supers.Communication.Responses;

namespace Supers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroiController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(CadastroSuperResponse), StatusCodes.Status201Created)]
        public IActionResult Cadastrar (CadastroSuperRequest request)
        {
            var useCase = new CadastroDeSupersUseCase();

            var resultado = useCase.Executar(request);

            return Created(string.Empty, resultado);
        }
    }
}
