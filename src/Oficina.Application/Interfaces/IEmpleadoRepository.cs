using Oficina.Domain.Entities;

namespace Oficina.Application.Interfaces;

public interface IEmpleadoRepository
{
    //Lecturas
    Task<List<Empleado>> GetAllWithAreaAsync(CancellationToken ct = default);
    Task<Empleado?> GetByIdWithAreaAsync(int id, CancellationToken ct = default);
    Task<List<Empleado>> GetByAreaAsync(int areaId, CancellationToken ct = default);
    Task<List<Empleado>> GetByNameAsync(String name, CancellationToken ct = default);

    //Escrituras
    Task AddAsync(Empleado empleado, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    Task<bool> ExistsEmailAsync(string email, int?ignoreId=null, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}
