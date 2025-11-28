using Oficina.Application.DTOs;

namespace Oficina.Application.Interfaces;

public interface IEmpleadoService
{
    Task<List<EmpleadoDto>> GetAllAsync(CancellationToken ct = default);
    Task<EmpleadoDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<List<EmpleadoDto>> GetByAreaAsync(int areaId, CancellationToken ct = default);
    Task<List<EmpleadoDto>> GetByNameAsync(String name, CancellationToken ct = default);

    Task<EmpleadoDto> CreateAsync(CreateEmpleadoRequest request, CancellationToken ct = default);
    Task<bool> UpdateAsync(int id, UpdateEmpleadoRequest request, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}
