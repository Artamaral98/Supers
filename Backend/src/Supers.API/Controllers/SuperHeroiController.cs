using Microsoft.AspNetCore.Mvc;
using Supers.Application.UseCases.SuperHerois.Cadastro;
using Supers.Application.UseCases.SuperHerois.ObterTodos;
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
        public async Task<IActionResult> Cadastrar(
            [FromServices]ICadastroDeSupersUseCase useCase,
            [FromBody]CadastroSuperRequest request)
        {
            var resultado = await useCase.Executar(request);

            return Created(string.Empty, resultado);
        }

        [HttpGet("ListarTodos")]
        [ProducesResponseType(typeof(CadastroSuperResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListarTodos(
        [FromServices] IObterTodosOsSupersUseCase useCase)
        {
            var resultado = await useCase.Executar();

            return Ok(resultado);
        }
    }
}
