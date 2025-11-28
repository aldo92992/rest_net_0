using Oficina.Domain.Entities;

namespace Oficina.Application.Interfaces;

public interface IAreaRepository
{
    Task<List<Area>> GetAllAsync(CancellationToken ct = default);


}
