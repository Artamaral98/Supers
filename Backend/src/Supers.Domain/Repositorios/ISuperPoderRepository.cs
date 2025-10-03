using Supers.Domain.Entidades;

namespace Supers.Domain.Repositorios
{
    public interface ISuperPoderRepository
    {
        Task<IList<SuperPoderes>> ObterTodosSuperPoderes();
        Task<int> PoderExisteNoBanco(List<int> poderesIds);
    }
}
