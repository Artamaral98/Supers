using Microsoft.EntityFrameworkCore;
using Supers.Domain.Entidades;
using Supers.Domain.Repositorios;

namespace Supers.Infrastructure.Dados.Repositorio
{
    public class SuperPoderRepository : ISuperPoderRepository
    {
        private readonly SupersDbContext _dbContext;

        public SuperPoderRepository(SupersDbContext dbContext) => _dbContext = dbContext;

        public async Task<IList<SuperPoderes>> ObterTodosSuperPoderes() => await _dbContext.SuperPoderes.AsNoTracking().ToListAsync();

        public async Task<int> PoderExisteNoBanco(List<int> poderesIds)
        {
            return await _dbContext.SuperPoderes.CountAsync(poder => poderesIds.Contains(poder.Id));
        }

    }
}
