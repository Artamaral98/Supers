using AutoMapper;
using Supers.Communication.Requests;
using Supers.Domain.Repositorios;

namespace Supers.Application.UseCases.SuperHerois.ObterTodos
{
    public class ObterTodosOsSupersUseCase : IObterTodosOsSupersUseCase
    {
        private readonly ISuperHeroiRepository _repository;
        private readonly IMapper _mapper;
        public ObterTodosOsSupersUseCase(ISuperHeroiRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<RequestObterTodosOsSupers> Executar()
        {
            var listaDeHerois = await _repository.ObterTodosOsHerois();

            var response = new RequestObterTodosOsSupers
            {
                Herois = _mapper.Map<List<SumarioHerois>>(listaDeHerois)
            };

            return response;
        }
    }
}
