namespace Oficina.Application.DTOs;
public record CreateEmpleadoRequest
(
    string Name,
    int Age,
    string Email,
    int AreaId
);