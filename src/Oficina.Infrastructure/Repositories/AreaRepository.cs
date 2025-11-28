using Microsoft.EntityFrameworkCore;
using Oficina.Application.Interfaces;
using Oficina.Domain.Entities;
using Oficina.Infrastructure.Data;

namespace Oficina.Infrastructure.Repositories;

public class AreaRepository : IAreaRepository
{
    private readonly OficinaDbContext _db;

    public AreaRepository(OficinaDbContext db)
    {
        _db = db;
    }

    public Task<List<Area>> GetAllAsync(CancellationToken ct = default)
    {
        return _db.Areas
            .OrderBy(a => a.Description)
            .ToListAsync(ct);
    }

    //Escrituras

    public async Task AddAsync(Area area, CancellationToken ct = default) 
    { 
        await _db.Areas.AddAsync(area, ct);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var area =  await _db.Areas.FindAsync(new object?[] { id }, ct);
        if (area is not null)
        {
            _db.Areas.Remove(area);
            return true;
        } else {             
            return false; 
        }
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await _db.SaveChangesAsync(ct);
    }
}
