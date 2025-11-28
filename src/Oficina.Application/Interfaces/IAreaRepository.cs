using Oficina.Domain.Entities;

namespace Oficina.Application.Interfaces;

public interface IAreaRepository
{
    Task<List<Area>> GetAllAsync(CancellationToken ct = default);

    Task AddAsync(Area area, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}
