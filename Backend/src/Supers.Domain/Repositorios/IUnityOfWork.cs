namespace Supers.Domain.Repositorios
{
    public interface IUnityOfWork
    {
        public Task Commit();
    }
}
