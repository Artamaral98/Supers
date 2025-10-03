using Microsoft.AspNetCore.Mvc;
using Supers.Application.UseCases.SuperHerois.Atualizar;
using Supers.Application.UseCases.SuperHerois.Cadastro;
using Supers.Application.UseCases.SuperHerois.Obter;
using Supers.Application.UseCases.SuperHerois.ObterTodos;
using Supers.Communication.Requests;
using Supers.Communication.Responses;
using Supers.Exceptions;

namespace Supers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroiController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(CadastroSuperResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Cadastrar(
            [FromServices] ICadastroDeSupersUseCase useCase,
            [FromBody] CadastroSuperRequest request)
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

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CadastroSuperResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorId(
        [FromServices] IObterSuperUseCase useCase,
        [FromRoute] int id)
        {
            var resultado = await useCase.Executar(id);
            return Ok(resultado);

        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CadastroSuperResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrosResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrosResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Atualizar(
        [FromServices] IAtualizarSuperUseCase useCase,
        [FromRoute] int id,
        [FromBody] CadastroSuperRequest request)
        {
            var resultado = await useCase.Executar(id, request);
            return Ok(resultado);
        }
    }
}
