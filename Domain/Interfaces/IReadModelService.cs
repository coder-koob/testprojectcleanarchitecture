using Domain.Common;

namespace Domain.Interfaces;

public interface IReadModelService<TReadModel> where TReadModel : ReadModel, new()
{
    Task<TReadModel?> GetByIdAsync(string id);
    Task<TReadModel> SaveAsync(TReadModel readModel, string? id = null);
}