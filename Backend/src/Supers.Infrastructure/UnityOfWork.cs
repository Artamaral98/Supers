using Supers.Domain.Repositorios;
using Supers.Infrastructure.Dados;

namespace Supers.Infrastructure
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly SupersDbContext _dbContext;

        public UnityOfWork(SupersDbContext dbContext) => _dbContext = dbContext;
        public async Task Commit() => await _dbContext.SaveChangesAsync();
    }
}
