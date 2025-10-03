using Supers.Domain.Entidades;

namespace Supers.Domain.Repositorios
{
    public interface ISuperHeroiRepository
    {
        Task CadastrarHeroi(SuperHeroi heroi);
        Task<List<SuperHeroi>> ObterTodosOsHerois();
        Task<SuperHeroi> ObterHeroiPorId(int id);
        void Excluir(SuperHeroi heroi);
        Task<bool> ExisteHeroiCadastradoPorNomeHeroi(string nomeHeroi);

        Task<bool> ExisteOutroHeroiComMesmoNomeEIdDiferente(int id, string nomeHeroi);
    }
}
