using Oficina.Application.DTOs;
using Oficina.Application.Interfaces;
using Oficina.Domain.Entities;

namespace Oficina.Application.Services;

public class EmpleadoService : IEmpleadoService
{
    private readonly IEmpleadoRepository _empleadoRepo;
    private readonly IAreaRepository _areaRepo;

    public EmpleadoService(IEmpleadoRepository empleadoRepo, IAreaRepository areaRepo)
    {
        _empleadoRepo = empleadoRepo;
        _areaRepo = areaRepo;
    }

    public async Task<List<EmpleadoDto>> GetAllAsync(CancellationToken ct = default)
    {
        var empleados = await _empleadoRepo.GetAllWithAreaAsync(ct);

        return empleados
            .Select(e => new EmpleadoDto(
                e.Id,
                e.Name,
                e.Age,
                e.Email,
                e.AreaId,
                e.Area.Description
            ))
            .ToList();
    }

    public async Task<EmpleadoDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var e = await _empleadoRepo.GetByIdWithAreaAsync(id, ct);
        if (e is null) return null;

        return new EmpleadoDto(
            e.Id,
            e.Name,
            e.Age,
            e.Email,
            e.AreaId,
            e.Area.Description
        );
    }

    public async Task<List<EmpleadoDto>> GetByAreaAsync(int areaId, CancellationToken ct = default)
    {
        var empleados = await _empleadoRepo.GetByAreaAsync(areaId, ct);

        return empleados
            .Select(e => new EmpleadoDto(
                e.Id,
                e.Name,
                e.Age,
                e.Email,
                e.AreaId,
                e.Area.Description
            ))
            .ToList();
    }

    public async Task<List<EmpleadoDto>> GetByNameAsync(String name, CancellationToken ct = default)
    {
        var empleados = await _empleadoRepo.GetByNameAsync(name, ct);

        return empleados
            .Select(e => new EmpleadoDto(
                e.Id,
                e.Name,
                e.Age,
                e.Email,
                e.AreaId,
                e.Area.Description
            ))
            .ToList();
    }

    // CREATE
    public async Task<EmpleadoDto> CreateAsync(CreateEmpleadoRequest request, CancellationToken ct = default)
    {
        if(request.Age < 0)
            throw new ArgumentException("La edad debe ser un número positivo.", nameof(request.Age));
        
        var area = await _areaRepo.GetAllAsync(ct);

        if (!area.Any(a => a.Id == request.AreaId))
            throw new ArgumentException("El área especificada no existe.", nameof(request.AreaId));

        var emailExists = await _empleadoRepo.ExistsEmailAsync(request.Email, null, ct);
        if (emailExists)
            throw new InvalidOperationException("El correo electrónico ya está en uso.");


        var empleado = new Empleado
        {
            Name = request.Name,
            Age = request.Age,
            Email = request.Email,
            AreaId = request.AreaId
        };
            
        await _empleadoRepo.AddAsync(empleado, ct);
        await _empleadoRepo.SaveChangesAsync(ct);

        var loaded = await _empleadoRepo.GetByIdWithAreaAsync(empleado.Id, ct) ?? empleado;

        return new EmpleadoDto(
            loaded.Id,
            loaded.Name,
            loaded.Age,
            loaded.Email,
            loaded.AreaId,
            loaded?.Area?.Description ?? string.Empty
        );
    }

    // UPDATE
    public async Task<bool> UpdateAsync(int id, UpdateEmpleadoRequest request, CancellationToken ct)
    {
        var empleado = await _empleadoRepo.GetByIdWithAreaAsync(id, ct);
        if (empleado is null) return false;
        
        if(request.Age < 0)
            throw new ArgumentException("La edad debe ser un número positivo.", nameof(request.Age));
        var area = await _areaRepo.GetAllAsync(ct);
        
        if (!area.Any(a => a.Id == request.AreaId))
            throw new ArgumentException("El área especificada no existe.", nameof(request.AreaId));

        var emailExists = await _empleadoRepo.ExistsEmailAsync(request.Email, id,  ct);
        if (emailExists)
            throw new InvalidOperationException("El correo electrónico ya está en uso.");

        empleado.Name = request.Name;
        empleado.Age = request.Age;
        empleado.Email = request.Email;
        empleado.AreaId = request.AreaId;

        await _empleadoRepo.SaveChangesAsync(ct);
        return true;
    }

    // DELETE
    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        var deleted = await _empleadoRepo.DeleteAsync(id, ct);
        if (!deleted) return false;
        await _empleadoRepo.SaveChangesAsync(ct);
        return true;
    }
}