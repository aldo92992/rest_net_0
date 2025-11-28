using Oficina.Application.DTOs;

namespace Oficina.Application.Interfaces;

public interface IAreaService
{
    Task<List<AreaDto>> GetAllAsync(CancellationToken ct = default);
    Task<AreaDto> CreateAsync(CreateAreaRequest request, CancellationToken ct = default);
    Task<bool> UpdateAsync(int id, UpdateAreaRequest request, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}
