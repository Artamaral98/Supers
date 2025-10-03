using AutoMapper;
using FluentAssertions;
using Moq;
using Supers.Application.UseCases.SuperHerois.Cadastro;
using Supers.Communication.Requests;
using Supers.Communication.Responses;
using Supers.Domain.Repositorios;
using Supers.Exceptions;
using TestUtils.Fakers;
using S = Supers.Domain.Entidades;

namespace UseCaseTests.SuperHeroi.Cadastrar
{
    public class CadastroDeSupersUseCaseTest
    {
        private readonly Mock<ISuperHeroiRepository> _mockSuperHeroiRepository;
        private readonly Mock<ISuperPoderRepository> _mockSuperPoderRepository;
        private readonly Mock<IUnityOfWork> _mockUnityOfWork;
        private readonly Mock<IMapper> _mockMapper;

        public CadastroDeSupersUseCaseTest()
        {
            _mockSuperHeroiRepository = new Mock<ISuperHeroiRepository>();
            _mockSuperPoderRepository = new Mock<ISuperPoderRepository>();
            _mockUnityOfWork = new Mock<IUnityOfWork>();
            _mockMapper = new Mock<IMapper>();
        }

        private CadastroDeSupersUseCase CreateUseCase()
        {
            return new CadastroDeSupersUseCase(
                _mockSuperHeroiRepository.Object,
                _mockMapper.Object,
                _mockUnityOfWork.Object,
                _mockSuperPoderRepository.Object);
        }

        [Fact]
        public async Task Executar_DeveCadastrarHeroi_QuandoRequestForValido()
        {
            // Arra
            var request = CadastroSuperRequestFaker.Gerar().Generate();

            _mockSuperHeroiRepository.Setup(repo => repo.ExisteHeroiCadastradoPorNomeHeroi(request.NomeHeroi)).ReturnsAsync(false);
            _mockSuperPoderRepository.Setup(repo => repo.PoderExisteNoBanco(request.SuperPoderes)).ReturnsAsync(request.SuperPoderes.Count);

            var heroiMapeado = SuperHeroiFaker.Gerar().Generate();
            _mockMapper.Setup(mapper => mapper.Map<S.SuperHeroi>(request)).Returns(heroiMapeado);
            _mockSuperHeroiRepository.Setup(repo => repo.ObterHeroiPorId(heroiMapeado.Id)).ReturnsAsync(heroiMapeado);
            _mockMapper.Setup(mapper => mapper.Map<CadastroSuperResponse>(heroiMapeado))
                .Returns(new CadastroSuperResponse { Id = heroiMapeado.Id, Nome = heroiMapeado.Nome });

            var useCase = CreateUseCase();

            // Act
            var response = await useCase.Executar(request);

            // Assert
            response.Should().NotBeNull();
            response.Id.Should().Be(heroiMapeado.Id);

            _mockSuperHeroiRepository.Verify(repo => repo.CadastrarHeroi(It.IsAny<S.SuperHeroi>()), Times.Once);
            _mockUnityOfWork.Verify(uow => uow.Commit(), Times.Once);
        }


        [Fact]
        public async Task Executar_DeveLancarExcecao_QuandoNomeDeHeroiJaExiste()
        {
            var request = CadastroSuperRequestFaker.Gerar().Generate();

            _mockSuperHeroiRepository.Setup(repo => repo.ExisteHeroiCadastradoPorNomeHeroi(request.NomeHeroi)).ReturnsAsync(true);

            var useCase = CreateUseCase();

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

            var useCase = CreateUseCase();

            Func<Task> act = async () => await useCase.Executar(request);

            await act.Should().ThrowAsync<ErrosEmValidacaoException>()
                .Where(ex => ex.MensagensDeErros.Contains(Mensagens.NOME_HEROI_VAZIO));

            _mockUnityOfWork.Verify(uow => uow.Commit(), Times.Never);
        }
    }
}