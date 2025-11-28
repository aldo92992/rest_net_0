using Oficina.Application.DTOs;
using Oficina.Application.Interfaces;

namespace Oficina.Application.Services;

public class AreaService : IAreaService
{
    private readonly IAreaRepository _areaRepo;

    public AreaService(IAreaRepository areaRepo)
    {
        _areaRepo = areaRepo;
    }

    public async Task<List<AreaDto>> GetAllAsync(CancellationToken ct = default)
    {
        var areas = await _areaRepo.GetAllAsync(ct);

        return areas
            .Select(a => new AreaDto(a.Id, a.Description))
            .ToList();
    }
}
