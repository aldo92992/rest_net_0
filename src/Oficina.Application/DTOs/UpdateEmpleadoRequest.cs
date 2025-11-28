namespace Oficina.Application.DTOs;
public record UpdateEmpleadoRequest
(
    string Name,
    int Age,
    string Email,
    int AreaId
);