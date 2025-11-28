namespace Oficina.Application.DTOs;

public record EmpleadoDto(
    int Id,
    string Name,
    int Age,
    string Email,
    int AreaId,
    string AreaDescription
);
