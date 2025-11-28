using Oficina.Application.DTOs;

namespace Oficina.Application.Interfaces;

public interface IAreaService
{
    Task<List<AreaDto>> GetAllAsync(CancellationToken ct = default);
}
