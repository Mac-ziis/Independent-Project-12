using Microsoft.EntityFrameworkCore;
using LocalParks.Models;
using System.Linq.Expressions;
using LocalParks.Contracts;

namespace LocalParks.Repository
{
  public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
  {
    protected LocalParksContext RepositoryContext { get; set;}
    public RepositoryBase(LocalParksContext repositoryContext)
    {
      this.RepositoryContext = repositoryContext;
    }

    public IQueryable<T> FindAll()
    {
      return this.RepositoryContext.Set<T>()
      .AsNoTracking();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
    {
      return this.RepositoryContext.Set<T>()
      .Where(expression)
      .AsNoTracking();
    }
  }
}