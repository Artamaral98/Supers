using Microsoft.EntityFrameworkCore;
using Supers.Domain.Entidades;
using Supers.Domain.Repositorios;

namespace Supers.Infrastructure.Dados.Repositorio
{
    public class SuperHeroiRepository : ISuperHeroiRepository
    {
        private readonly SupersDbContext _dbContext;

        public SuperHeroiRepository(SupersDbContext dbContext) => _dbContext = dbContext;


        public async Task CadastrarHeroi(SuperHeroi heroi) => await _dbContext.SuperHerois.AddAsync(heroi);

        public async Task<List<SuperHeroi>> ObterTodosOsHerois()
        {
            return await _dbContext.SuperHerois.AsNoTracking().Include(h => h.HeroisSuperPoderes)
                .ThenInclude(hsp => hsp.SuperPoderes).ToListAsync();
        }

        public async Task<SuperHeroi?> ObterHeroiPorId(int id)
        {
            return await _dbContext.SuperHerois.Include(h => h.HeroisSuperPoderes).ThenInclude(hsp => hsp.SuperPoderes)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Excluir(SuperHeroi heroi)
        {
            _dbContext.SuperHerois.Remove(heroi);
        }

        public async Task<bool> ExisteHeroiCadastradoPorNomeHeroi(string nomeHeroi) => await _dbContext.SuperHerois
            .AnyAsync(x => x.NomeHeroi.Equals(nomeHeroi));

        public async Task<bool> ExisteOutroHeroiComMesmoNomeEIdDiferente(int id, string nomeHeroi)
        {
            return await _dbContext.SuperHerois.AnyAsync(h => h.NomeHeroi.Equals(nomeHeroi) && h.Id != id);
        }
    }
}
