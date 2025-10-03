using AutoMapper;
using FluentAssertions;
using Moq;
using Supers.Application.UseCases.SuperHerois.Atualizar;
using Supers.Application.UseCases.SuperHerois.Cadastro;
using Supers.Application.UseCases.SuperHerois.Excluir;
using Supers.Application.UseCases.SuperHerois.Obter;
using Supers.Application.UseCases.SuperHerois.ObterTodos;
using Supers.Communication.Responses;
using Supers.Domain.Entidades;
using Supers.Domain.Repositorios;
using Supers.Exceptions;
using TestUtils.Fakers;
using S = Supers.Domain.Entidades;

namespace UseCaseTests.SuperHeroi
{
    public class ExecutarUseCasesTests
    {
        private readonly Mock<ISuperHeroiRepository> _mockSuperHeroiRepository;
        private readonly Mock<ISuperPoderRepository> _mockSuperPoderRepository;
        private readonly Mock<IUnityOfWork> _mockUnityOfWork;
        private readonly Mock<IMapper> _mockMapper;

        public ExecutarUseCasesTests()
        {
            _mockSuperHeroiRepository = new Mock<ISuperHeroiRepository>();
            _mockSuperPoderRepository = new Mock<ISuperPoderRepository>();
            _mockUnityOfWork = new Mock<IUnityOfWork>();
            _mockMapper = new Mock<IMapper>();
        }

        private CadastroDeSupersUseCase CadastroCreateUseCase()
        {
            return new CadastroDeSupersUseCase(
                _mockSuperHeroiRepository.Object,
                _mockMapper.Object,
                _mockUnityOfWork.Object,
                _mockSuperPoderRepository.Object);
        }

        private ExcluirSuperUseCase ExcluirSuperCreateUseCase()
        {
            return new ExcluirSuperUseCase(
                _mockSuperHeroiRepository.Object,
                _mockUnityOfWork.Object);
        }

        private ObterSuperUseCase ObterSuperCreateUseCase()
        {
            return new ObterSuperUseCase(
                _mockSuperHeroiRepository.Object,
                _mockMapper.Object);
        }

        [Fact]
        public async Task Executar_DeveCadastrarHeroi_QuandoRequestForValido()
        {
            var request = CadastroSuperRequestFaker.Gerar().Generate();

            _mockSuperHeroiRepository
                .Setup(repo => repo.ExisteHeroiCadastradoPorNomeHeroi(request.NomeHeroi))
                .ReturnsAsync(false);

            _mockSuperPoderRepository
                .Setup(repo => repo.PoderExisteNoBanco(request.SuperPoderes))
                .ReturnsAsync(request.SuperPoderes.Count);

            var heroiMapeado = SuperHeroiFaker.Gerar().Generate();
            heroiMapeado.HeroisSuperPoderes = new List<HeroiSuperPoder>();

            _mockMapper
                .Setup(mapper => mapper.Map<S.SuperHeroi>(request))
                .Returns(heroiMapeado);

            _mockSuperHeroiRepository
                .Setup(repo => repo.ObterHeroiPorId(heroiMapeado.Id))
                .ReturnsAsync(heroiMapeado);

            _mockMapper
                .Setup(mapper => mapper.Map<CadastroSuperResponse>(heroiMapeado))
                .Returns(new CadastroSuperResponse { Id = heroiMapeado.Id, Nome = heroiMapeado.Nome });

            var useCase = CadastroCreateUseCase();

            var response = await useCase.Executar(request);

            response.Should().NotBeNull();
            response.Id.Should().Be(heroiMapeado.Id);

            _mockSuperHeroiRepository.Verify(repo => repo.CadastrarHeroi(It.IsAny<S.SuperHeroi>()), Times.Once);
            _mockUnityOfWork.Verify(uow => uow.Commit(), Times.Once);

            heroiMapeado.HeroisSuperPoderes.Should().NotBeEmpty();
            heroiMapeado.Nome.Should().NotBeEmpty();
            heroiMapeado.NomeHeroi.Should().NotBeEmpty();
            heroiMapeado.Peso.Should().BePositive();
        }

        [Fact]
        public async Task Executar_DeveLancarExcecao_QuandoNomeDeHeroiJaExiste()
        {
            var request = CadastroSuperRequestFaker.Gerar().Generate();

            _mockSuperHeroiRepository.Setup(repo => repo.ExisteHeroiCadastradoPorNomeHeroi(request.NomeHeroi)).ReturnsAsync(true);

            var useCase = CadastroCreateUseCase();

            Func<Task> act = async () => await useCase.Executar(request);

            await act.Should().ThrowAsync<ErrosEmValidacaoException>()
                .Where(ex => ex.MensagensDeErros.Contains(Mensagens.NOME_HEROI_CADASTRADO));

            _mockUnityOfWork.Verify(uow => uow.Commit(), Times.Never);
        }

        [Fact]
        public async Task Executar_DeveLancarExcecao_QuandoNomeDeHeroiNaoForPassado()
        {
            var request = CadastroSuperRequestFaker.Gerar().Generate();

            request.NomeHeroi = "";

            var useCase = CadastroCreateUseCase();

            Func<Task> act = async () => await useCase.Executar(request);

            await act.Should().ThrowAsync<ErrosEmValidacaoException>()
                .Where(ex => ex.MensagensDeErros.Contains(Mensagens.NOME_HEROI_VAZIO));

            _mockUnityOfWork.Verify(uow => uow.Commit(), Times.Never);
        }

        [Fact]
        public async Task Executar_Excluir_DeveExcluir()
        {
            var super = SuperHeroiFaker.Gerar().Generate();

            _mockSuperHeroiRepository
                .Setup(repo => repo.ObterHeroiPorId(super.Id))
                .ReturnsAsync(super);

            var useCase = ExcluirSuperCreateUseCase();

            var resultado = await useCase.Executar(super.Id);

            _mockSuperHeroiRepository.Verify(r => r.ObterHeroiPorId(super.Id), Times.Once);
            _mockSuperHeroiRepository.Verify(r => r.Excluir(super), Times.Once);
            _mockUnityOfWork.Verify(u => u.Commit(), Times.Once);

            resultado.Should().Be("Herói excluído com sucesso.");
        }

        [Fact]
        public async Task Executar_Excluir_NaoDeveExcluir_IdNaoEncontrado()
        {
            var idInvalido = 999;

            _mockSuperHeroiRepository
                .Setup(repo => repo.ObterHeroiPorId(idInvalido))
                .ReturnsAsync((S.SuperHeroi?)null);

            var useCase = ExcluirSuperCreateUseCase();

            Func<Task> act = async () => await useCase.Executar(idInvalido);

            await act.Should().ThrowAsync<NaoEncontradoException>()
                .WithMessage(Mensagens.HEROI_NAO_ENCONTRADO);

            _mockSuperHeroiRepository.Verify(r => r.Excluir(It.IsAny<S.SuperHeroi>()), Times.Never);
            _mockUnityOfWork.Verify(u => u.Commit(), Times.Never);
        }

        [Fact]
        public async Task Executar_ObterSuper_DeveRetornar()
        {
            var id = 10;
            var heroi = SuperHeroiFaker.Gerar().Generate();
            var responseEsperado = new CadastroSuperResponse { Id = heroi.Id, Nome = heroi.Nome };

            _mockSuperHeroiRepository
                .Setup(repo => repo.ObterHeroiPorId(id))
                .ReturnsAsync(heroi);

            _mockMapper
                .Setup(mapper => mapper.Map<CadastroSuperResponse>(heroi))
                .Returns(responseEsperado);

            var useCase = ObterSuperCreateUseCase();

            var response = await useCase.Executar(id);

            response.Should().NotBeNull();
            response.Id.Should().Be(heroi.Id);
            response.Nome.Should().Be(heroi.Nome);

            _mockSuperHeroiRepository.Verify(repo => repo.ObterHeroiPorId(id), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<CadastroSuperResponse>(heroi), Times.Once);
        }

        [Fact]
        public async Task Executar_ObterSuper_IdNaoEncontrado()
        {
            var idInvalido = 999;

            _mockSuperHeroiRepository
                .Setup(repo => repo.ObterHeroiPorId(idInvalido))
                .ReturnsAsync((S.SuperHeroi)null); 

            var useCase = new ObterSuperUseCase(_mockSuperHeroiRepository.Object, _mockMapper.Object);

            var act = async () => await useCase.Executar(idInvalido);

            await act.Should()
                     .ThrowAsync<NaoEncontradoException>()
                     .WithMessage(Mensagens.HEROI_NAO_ENCONTRADO);

            _mockSuperHeroiRepository.Verify(repo => repo.ObterHeroiPorId(idInvalido), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<CadastroSuperResponse>(It.IsAny<S.SuperHeroi>()), Times.Never);
        }

    }
}