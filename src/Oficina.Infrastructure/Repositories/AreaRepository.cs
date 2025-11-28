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
}
