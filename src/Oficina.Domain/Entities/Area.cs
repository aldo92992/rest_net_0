namespace Oficina.Domain.Entities;

public class Area
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;

    public ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
