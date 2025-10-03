using Supers.Domain.Entidades;

namespace Supers.Domain.Repositorios
{
    public interface ISuperHeroiRepository
    {
        Task CadastrarHeroi(SuperHeroi heroi);
        Task<List<SuperHeroi>> ObterTodosOsHerois();
        Task<SuperHeroi> ObterHeroiPorId(long id);
        Task AtualizarHeroiPorId(long id, SuperHeroi heroi);
        Task ExcluirHeroiPorId(long id);
        Task<bool> ExisteHeroiCadastradoPorNomeHeroi(string nomeHeroi);
    }
}
