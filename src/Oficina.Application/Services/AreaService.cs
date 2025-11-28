using Oficina.Application.DTOs;
using Oficina.Application.Interfaces;
using Oficina.Domain.Entities;

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

    // CREATE
    public async Task<AreaDto> CreateAsync(CreateAreaRequest request, CancellationToken ct = default)
    {
        var area = new Area
        {
            Description = request.Description
        };
        await _areaRepo.AddAsync(area, ct);
        await _areaRepo.SaveChangesAsync(ct);
        return new AreaDto(area.Id, area.Description);
    }

    // UPDATE
    public async Task<bool> UpdateAsync(int id, UpdateAreaRequest request, CancellationToken ct = default)
    {
        var areas = await _areaRepo.GetAllAsync(ct);
        var area = areas.FirstOrDefault(a => a.Id == id);
        if (area is null)
            throw new KeyNotFoundException("El área especificada no existe.");
        area.Description = request.Description;
        await _areaRepo.SaveChangesAsync(ct);
        return true;
    }

    // DELETE
    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default) 
    { 
        var deleted = await _areaRepo.DeleteAsync(id, ct);
        if (!deleted) return false;
        await _areaRepo.SaveChangesAsync(ct);
        return true;
    }
}
