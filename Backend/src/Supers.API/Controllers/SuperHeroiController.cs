using Microsoft.AspNetCore.Mvc;
using Supers.Application.UseCases.SuperHerois.Atualizar;
using Supers.Application.UseCases.SuperHerois.Cadastro;
using Supers.Application.UseCases.SuperHerois.Excluir;
using Supers.Application.UseCases.SuperHerois.Obter;
using Supers.Application.UseCases.SuperHerois.ObterTodos;
using Supers.Communication.Requests;
using Supers.Communication.Responses;
using Supers.Communication.Responses.Wrapper;

namespace Supers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroiController : SuperHeroiControllerBase
    {
        [HttpPost("Cadastro")]
        [ProducesResponseType(typeof(CadastroSuperResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Cadastrar(
            [FromServices] ICadastroDeSupersUseCase useCase,
            [FromBody] CadastroSuperRequest request)
        {
            var resultado = await useCase.Executar(request);

            var resposta = new RespostaSucesso<CadastroSuperResponse>
            {
                Mensagem = "Herói criado com sucesso.",
                Dados = resultado
            };

            return Created(string.Empty, resposta);
        }

        [HttpGet("ListarTodos")]
        [ProducesResponseType(typeof(RespostaSucesso<List<SumarioHerois>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListarTodos(
        [FromServices] IObterTodosOsSupersUseCase useCase)
        {
            var resultado = await useCase.Executar();

            return CriarRespostaDeSucesso(resultado.Herois);
        }

        [HttpGet("Obter/{id}")]
        [ProducesResponseType(typeof(CadastroSuperResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorId(
        [FromServices] IObterSuperUseCase useCase,
        [FromRoute] int id)
        {
            var resultado = await useCase.Executar(id);
            return Ok(resultado);

        }

        [HttpPut("Atualizar/{id}")]
        [ProducesResponseType(typeof(RespostaSucesso<CadastroSuperResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrosResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrosResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Atualizar(
            [FromServices] IAtualizarSuperUseCase useCase,
            [FromRoute] int id,
            [FromBody] CadastroSuperRequest request)
        {
            var resultado = await useCase.Executar(id, request);

            return CriarRespostaDeSucesso(resultado);
        }

        [HttpDelete("Excluir/{id}")]
        [ProducesResponseType(typeof(SucessoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrosResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Excluir(
            [FromServices] IExcluirSuperUseCase useCase,
            [FromRoute] int id)
        {
            var mensagem = await useCase.Executar(id);

            return CriarRespostaDeSucesso(mensagem);
        }
    }
}
