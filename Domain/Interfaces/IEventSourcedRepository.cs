using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Interfaces;

public interface IEventSourcedRepository<T> where T : Entity
{
    Task<T> GetByIdAsync(Guid id);
    Task SaveAsync(Guid aggregateId, T entity);
}