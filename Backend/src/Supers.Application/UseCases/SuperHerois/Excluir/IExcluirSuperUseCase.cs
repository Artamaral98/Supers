namespace Supers.Application.UseCases.SuperHerois.Excluir
{
    public interface IExcluirSuperUseCase
    {
        Task<string> Executar(int id);
    }
}
