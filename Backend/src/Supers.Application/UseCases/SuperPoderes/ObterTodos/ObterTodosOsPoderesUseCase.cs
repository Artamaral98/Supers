using AutoMapper;
using Supers.Communication.Requests;
using Supers.Communication.Responses;
using Supers.Domain.Repositorios;

namespace Supers.Application.UseCases.SuperPoderes.ObterTodos
{
    public class ObterTodosOsPoderesUseCase : IObterTodosOsPoderesUseCase
    {
        private readonly ISuperPoderRepository _repository;
        private readonly IMapper _mapper;

        public ObterTodosOsPoderesUseCase(ISuperPoderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<SuperPoderResponse>> Executar()
        {
            var superPoderesEntidades = await _repository.ObterTodosSuperPoderes();

            return _mapper.Map<IList<SuperPoderResponse>>(superPoderesEntidades);
        }
    }
}
