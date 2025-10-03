using Supers.Domain.Entidades;
using Supers.Domain.Repositorios;

namespace Supers.Infrastructure.Dados.Repositorio
{
    public class HeroiSuperPoderRepository : IHeroiSuperPoderRepository
    {
        private readonly SupersDbContext _dbContext;

        public HeroiSuperPoderRepository(SupersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Adicionar(HeroiSuperPoder relacionamento)
        {
            await _dbContext.HeroisSuperPoderes.AddAsync(relacionamento);
        }
    }
}
