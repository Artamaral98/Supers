using Microsoft.AspNetCore.Mvc;
using Supers.Application.UseCases.SuperPoderes.ObterTodos;
using Supers.Communication.Responses;

namespace Supers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperPoderController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<SuperPoderResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListarTodos([FromServices] IObterTodosOsPoderesUseCase useCase)
        {
            var resultado = await useCase.Executar();
            return Ok(resultado);
        }
    }
}
