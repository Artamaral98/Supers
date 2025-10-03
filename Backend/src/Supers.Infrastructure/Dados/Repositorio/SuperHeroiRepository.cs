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
            return await _dbContext.SuperHerois.Include(h => h.HeroisSuperPoderes).ThenInclude(hsp => hsp.SuperPoderes).ToListAsync();
        }

        public async Task<SuperHeroi?> ObterHeroiPorId(int id)
        {
            return await _dbContext.SuperHerois
                .Include(heroi => heroi.HeroisSuperPoderes)
                    .ThenInclude(relacionamento => relacionamento.SuperPoderes) 
                .FirstOrDefaultAsync(heroi => heroi.Id == id);
        }

        public Task AtualizarHeroiPorId(int id, SuperHeroi heroi)
        {
            _dbContext.SuperHerois.Update(heroi);
            return Task.CompletedTask;
        }

        public async Task ExcluirHeroiPorId(int id)
        {
            var heroiASerExcluido = await ObterHeroiPorId(id);

            if (heroiASerExcluido != null)
            {
                _dbContext.SuperHerois.Remove(heroiASerExcluido);
            };
            
        }

        public async Task<bool> ExisteHeroiCadastradoPorNomeHeroi(string nomeHeroi) => await _dbContext.SuperHerois.AnyAsync(x => x.NomeHeroi.Equals(nomeHeroi));
    }
}
