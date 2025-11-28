namespace Oficina.Domain.Entities;

public class Empleado
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Email { get; set; } = string.Empty;

    public int AreaId { get; set; }
    public Area Area { get; set; } = null!;
}
