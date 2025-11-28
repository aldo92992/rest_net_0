using Microsoft.EntityFrameworkCore;
using Oficina.Application.Interfaces;
using Oficina.Domain.Entities;
using Oficina.Infrastructure.Data;

namespace Oficina.Infrastructure.Repositories;

public class EmpleadoRepository : IEmpleadoRepository
{
    private readonly OficinaDbContext _db;

    public EmpleadoRepository(OficinaDbContext db)
    {
        _db = db;
    }

    public Task<List<Empleado>> GetAllWithAreaAsync(CancellationToken ct = default)
    {
        return _db.Empleados
            .Include(e => e.Area)
            .OrderBy(e => e.Name)
            .ToListAsync(ct);
    }

    public Task<Empleado?> GetByIdWithAreaAsync(int id, CancellationToken ct = default)
    {
        return _db.Empleados
            .Include(e => e.Area)
            .FirstOrDefaultAsync(e => e.Id == id, ct);
    }

    public Task<List<Empleado>> GetByAreaAsync(int areaId, CancellationToken ct = default)
    {
        return _db.Empleados
            .Include(e => e.Area)
            .Where(e => e.AreaId == areaId)
            .OrderBy(e => e.Name)
            .ToListAsync(ct);
    }

    public Task<List<Empleado>> GetByNameAsync(String name, CancellationToken ct = default)
    {
        return _db.Empleados
            .Include(e => e.Area)
            .Where(e => EF.Functions.Like(e.Name, $"%{name}%"))
            .OrderBy(e => e.Name)
            .ToListAsync(ct);
    }

    //Escrituras
    public async Task AddAsync(Empleado empleado, CancellationToken ct = default) 
    { 
        await _db.Empleados.AddAsync(empleado, ct);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var empleado = await _db.Empleados.FindAsync(new object?[] { id }, ct);
        if (empleado is null) return false;
        _db.Empleados.Remove(empleado);
        return true;
    }

    public Task<bool> ExistsEmailAsync(string email, int? ignoreId = null, CancellationToken ct = default)
    {
        var query = _db.Empleados.AsQueryable();
        if (ignoreId.HasValue)
        {
            query = query.Where(e => e.Id != ignoreId.Value);
        }
        return query.AnyAsync(e => e.Email == email, ct);
    }

    public Task SaveChangesAsync(CancellationToken ct = default)
    {
        return _db.SaveChangesAsync(ct);
    }

}
