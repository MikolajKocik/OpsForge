using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpsForge.Domain.SeedWork.Interfaces;

public interface IRepository<T> where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }

    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);

    Task<List<T>> ListAsync(ISpecification<T> spec);
}
