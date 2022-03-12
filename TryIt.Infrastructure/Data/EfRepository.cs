using Ardalis.Specification.EntityFrameworkCore;
using TryIt.SharedKernel.Interfaces;

namespace TryIt.Infrastructure.Data
{
    public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
    {
        public EfRepository(SqliteDataContext dbContext) : base(dbContext)
        {
        }
    }
}
