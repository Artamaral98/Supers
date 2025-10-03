using Supers.Domain.Entidades;

namespace Supers.Domain.Repositorios
{
    public interface ISuperHeroiRepository
    {
        Task CadastrarHeroi(SuperHeroi heroi);
        Task<List<SuperHeroi>> ObterTodosOsHerois();
        Task<SuperHeroi> ObterHeroiPorId(int id);
        Task AtualizarHeroiPorId(int id, SuperHeroi heroi);
        Task ExcluirHeroiPorId(int id);
        Task<bool> ExisteHeroiCadastradoPorNomeHeroi(string nomeHeroi);
    }
}
